using Microsoft.AspNetCore.Mvc;

namespace VulnerableApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    [HttpGet("download")]
    public IActionResult DownloadFile([FromQuery] string filename)
    {
        // VULNERABILITY: Path traversal vulnerability
        var filePath = Path.Combine("uploads", filename);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        // VULNERABILITY: No validation of file type or content
        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "application/octet-stream", filename);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        // VULNERABILITY: No file size limit
        // VULNERABILITY: No file type validation
        // VULNERABILITY: Using original filename without sanitization
        
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }

        var uploadsFolder = "uploads";
        Directory.CreateDirectory(uploadsFolder);

        // VULNERABILITY: Using user-supplied filename directly
        var filePath = Path.Combine(uploadsFolder, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { filename = file.FileName, path = filePath });
    }

    [HttpGet("read")]
    public IActionResult ReadFile([FromQuery] string path)
    {
        // VULNERABILITY: Arbitrary file read
        try
        {
            var content = System.IO.File.ReadAllText(path);
            return Ok(new { content });
        }
        catch (Exception ex)
        {
            // VULNERABILITY: Information disclosure
            return BadRequest(new { error = ex.Message, stackTrace = ex.StackTrace });
        }
    }
}
