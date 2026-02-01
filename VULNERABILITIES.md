# Lista Detalhada de Vulnerabilidades

## 🔴 Críticas

### 1. SQL Injection (CWE-89)
**Localização:** `DatabaseService.cs:17`
```csharp
string query = $"SELECT * FROM Users WHERE Id = {userId}";
```
**Impacto:** Execução arbitrária de código SQL, perda de dados, acesso não autorizado  
**Solução:** Usar parametrized queries

### 2. Path Traversal (CWE-22)
**Localização:** `FileController.cs:12`
```csharp
var filePath = Path.Combine("uploads", filename);
```
**Impacto:** Leitura de arquivos arbitrários do sistema  
**Exemplo de exploit:** `?filename=../../../windows/system32/config/sam`  
**Solução:** Validar e sanitizar nomes de arquivo

### 3. Arbitrary File Read (CWE-22)
**Localização:** `FileController.cs:49`
```csharp
var content = System.IO.File.ReadAllText(path);
```
**Impacto:** Leitura de arquivos sensíveis do sistema  
**Solução:** Implementar whitelist de diretórios permitidos

## 🟠 Altas

### 4. Senhas em Texto Plano (CWE-256, CWE-522)
**Localização:** `User.cs:7`, `UsersController.cs:13-14`
```csharp
public string Password { get; set; } = string.Empty;
```
**Impacto:** Exposição de credenciais  
**Solução:** Hash com bcrypt, Argon2, ou PBKDF2

### 5. Credenciais Hardcoded (CWE-798)
**Localização:** `DatabaseService.cs:9`, `appsettings.json:11-13`
```csharp
private const string ConnectionString = "Server=localhost;Database=VulnerableDB;User Id=sa;Password=P@ssw0rd123;";
```
**Impacto:** Acesso não autorizado ao banco de dados  
**Solução:** Usar Azure Key Vault, variáveis de ambiente, ou User Secrets

### 6. Information Disclosure (CWE-209, CWE-532)
**Localização:** `FileController.cs:56`, `UsersController.cs:45`
```csharp
return BadRequest(new { error = ex.Message, stackTrace = ex.StackTrace });
```
**Impacto:** Exposição de detalhes internos do sistema  
**Solução:** Logging genérico para usuários, detalhado apenas em logs

## 🟡 Médias

### 7. Falta de Validação de Upload (CWE-434)
**Localização:** `FileController.cs:27-43`
- Sem limite de tamanho
- Sem validação de tipo MIME
- Sem antivírus scan

**Impacto:** Upload de malware, DoS  
**Solução:** Validar tipo, tamanho, e escanear arquivos

### 8. Mass Assignment (CWE-915)
**Localização:** `UsersController.cs:85`
```csharp
user.Role = updatedUser.Role; // User can escalate privileges
```
**Impacto:** Escalação de privilégios  
**Solução:** Usar DTOs com propriedades específicas

### 9. Falta de Autorização (CWE-862)
**Localização:** `UsersController.cs:66-77`
```csharp
public IActionResult DeleteUser(int id)
{
    // VULNERABILITY: No authorization check
```
**Impacto:** Qualquer usuário pode deletar outros  
**Solução:** Implementar [Authorize] attributes e policy-based authorization

### 10. HTTPS Não Forçado (CWE-319)
**Localização:** `Program.cs:16`
```csharp
// VULNERABILITY: Missing HTTPS redirection
// app.UseHttpsRedirection();
```
**Impacto:** Transmissão de dados sensíveis em texto plano  
**Solução:** Descomentar UseHttpsRedirection()

## 🔵 Baixas / Boas Práticas

### 11. Exposição de Dados Sensíveis em Logs (CWE-532)
**Localização:** `UsersController.cs:45`, `UsersController.cs:60`
```csharp
_logger.LogWarning($"Failed login attempt for username: {request.Username} with password: {request.Password}");
```
**Solução:** Nunca logar senhas

### 12. Violação de DI
**Localização:** `UsersController.cs:21`
```csharp
_dbService = new DatabaseService(); // Should be injected
```
**Solução:** Injetar via construtor

### 13. Falta de Rate Limiting
**Impacto:** Brute force attacks, DoS  
**Solução:** Implementar AspNetCoreRateLimit

## Resumo de Impacto

| Severidade | Quantidade | CWEs |
|------------|------------|------|
| 🔴 Crítica | 3 | CWE-89, CWE-22 |
| 🟠 Alta | 3 | CWE-256, CWE-522, CWE-798, CWE-209, CWE-532 |
| 🟡 Média | 4 | CWE-434, CWE-915, CWE-862, CWE-319 |
| 🔵 Baixa | 3 | Boas práticas |

**Total: 13 vulnerabilidades identificadas**
