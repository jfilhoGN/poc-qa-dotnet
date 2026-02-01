# VulnerableApi - POC para Análise de QA

Este é um projeto de exemplo com **vulnerabilidades intencionais** e **baixa cobertura de testes** para demonstrar análise de QA automatizada.

## ⚠️ AVISO
Este projeto contém vulnerabilidades de segurança INTENCIONAIS para fins educacionais. **NÃO USE EM PRODUÇÃO!**

## Vulnerabilidades Presentes

### 1. **SQL Injection**
- `DatabaseService.GetUserById()` - Concatenação direta de input do usuário
- `DatabaseService.ExecuteRawQuery()` - Permite execução de SQL arbitrário

### 2. **Armazenamento Inseguro de Senhas**
- Senhas armazenadas em texto plano
- Senhas expostas em logs
- Senhas retornadas em respostas da API

### 3. **Path Traversal**
- `FileController.DownloadFile()` - Permite acesso a arquivos arbitrários
- `FileController.ReadFile()` - Leitura de arquivos sem validação

### 4. **Divulgação de Informações**
- Credenciais hardcoded no código
- Segredos no appsettings.json
- Stack traces expostos
- Logs com informações sensíveis

### 5. **Falta de Autenticação/Autorização**
- Endpoints sem proteção
- Qualquer usuário pode deletar outros usuários
- Mass assignment permite escalação de privilégios

### 6. **Falta de Validação de Input**
- Sem validação de tamanho de arquivo
- Sem validação de tipo de arquivo
- Sem sanitização de nomes de arquivo
- Sem validação de dados do usuário

### 7. **Configurações Inseguras**
- HTTPS redirection comentado
- Credenciais em texto plano
- CORS não configurado adequadamente

## Cobertura de Testes

### Status Atual: ~10-15% de cobertura

**Testes Existentes:**
- ✅ 2 testes unitários básicos no `UserControllerTests`
- ❌ Sem testes para `FileController`
- ❌ Sem testes para `DatabaseService`
- ❌ Sem testes de integração
- ❌ Sem testes de segurança
- ❌ Sem testes de validação
- ❌ Sem testes de casos de erro

**Faltam:**
- Testes para CreateUser
- Testes para UpdateUser
- Testes para DeleteUser
- Testes para GetUser
- Testes de upload/download de arquivos
- Testes de validação de input
- Testes de autorização
- Testes end-to-end

## Como Executar

```bash
# Restaurar dependências
dotnet restore

# Compilar
dotnet build

# Executar a API
dotnet run --project src/VulnerableApi/VulnerableApi.csproj

# Executar testes
dotnet test

# Gerar relatório de cobertura
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## Endpoints da API

- `GET /api/users` - Listar todos usuários
- `GET /api/users/{id}` - Obter usuário por ID (SQL Injection)
- `POST /api/users` - Criar novo usuário
- `PUT /api/users/{id}` - Atualizar usuário (Mass Assignment)
- `DELETE /api/users/{id}` - Deletar usuário (Sem autorização)
- `POST /api/users/login` - Login (Senha em texto plano)
- `GET /api/file/download?filename=` - Download arquivo (Path Traversal)
- `POST /api/file/upload` - Upload arquivo (Sem validação)
- `GET /api/file/read?path=` - Ler arquivo (Arbitrary File Read)

## Objetivos da POC

Use este projeto para testar ferramentas de:
- ✅ Análise estática de segurança (SAST)
- ✅ Detecção de vulnerabilidades
- ✅ Análise de cobertura de código
- ✅ Sugestões de melhorias de QA
- ✅ Automação de code review

## Melhorias Necessárias

1. Implementar hashing de senhas (bcrypt/Argon2)
2. Usar prepared statements/parametrized queries
3. Adicionar autenticação JWT
4. Implementar autorização baseada em roles
5. Validar e sanitizar todos os inputs
6. Remover credenciais hardcoded
7. Implementar rate limiting
8. Adicionar logging seguro
9. Configurar HTTPS
10. Aumentar cobertura de testes para >80%
