# 🤖 Instruções para Análise com GitHub Copilot

## Como Executar o Super Prompt de QA

### No VS Code (Recomendado)

1. **Abra o repositório** no VS Code
2. **Abra o GitHub Copilot Chat** (Ctrl+Alt+I ou View > Copilot Chat)
3. **Execute o comando:**

```
@workspace Analise este repositório seguindo todas as diretrizes do arquivo .github/prompts/QA_TEST_ASSIST_FULL.md. 

Forneça um relatório completo cobrindo:
1. Visão Geral do Repositório
2. Análise de Segurança e Vulnerabilidades
3. Swagger / OpenAPI
4. Testes Automatizados e Cobertura
5. Pontos Críticos para QA
6. Pontos Críticos para Desenvolvedores
7. Avaliação da Pirâmide de Testes
8. Avaliação sob API First
9. Ideias de Evolução
10. Resumo Executivo

Seja específico, cite arquivos e linhas de código quando relevante.
```

### No GitHub Copilot Web

1. Acesse [github.com/copilot](https://github.com/copilot)
2. Referencie o repositório: `@jfilhoGN/poc-qa-dotnet`
3. Cole o mesmo prompt acima

### Via GitHub CLI com Copilot

```bash
gh copilot suggest "Analise o repositório jfilhoGN/poc-qa-dotnet seguindo as diretrizes em .github/prompts/QA_TEST_ASSIST_FULL.md"
```

---

## 📋 Checklist Pós-Análise

Após executar o prompt, verifique se o relatório inclui:

- [ ] Identificação de todas as 13 vulnerabilidades documentadas
- [ ] Análise da baixa cobertura de testes (~10%)
- [ ] Avaliação da configuração do Swagger
- [ ] Classificação de riscos (Alto/Médio/Baixo)
- [ ] Recomendações priorizadas
- [ ] Métricas quantitativas (%, números)
- [ ] Referências a arquivos específicos

---

## 🎯 Exemplos de Perguntas Específicas

Após a análise geral, você pode fazer perguntas focadas:

```
@workspace Quais são os 5 maiores riscos de segurança neste repositório?
```

```
@workspace Sugira 10 testes de integração que deveriam existir mas não existem
```

```
@workspace Como este projeto se compara com as melhores práticas de API First?
```

```
@workspace Crie um plano de ação de 30 dias para aumentar a cobertura de testes de 10% para 60%
```

---

## 🔄 Automação em CI/CD

O workflow `.github/workflows/qa-analysis.yml` executa automaticamente:

- ✅ Build e testes
- ✅ Relatório de cobertura
- ✅ Comentário em Pull Requests
- ✅ Upload de artefatos

**Executar manualmente:**
1. Vá em Actions > QA Analysis Report
2. Clique em "Run workflow"
3. Aguarde o relatório

---

## 📊 Ferramentas Complementares

Considere integrar:

- **SonarQube/SonarCloud** - Análise estática
- **OWASP Dependency-Check** - Vulnerabilidades em dependências
- **Coverlet** - Cobertura de código .NET
- **Swagger Validator** - Validação de contratos
- **Postman/Newman** - Testes de API automatizados

---

## 💡 Dicas

1. **Seja específico** nas perguntas ao Copilot
2. **Itere** - refine o prompt baseado nas respostas
3. **Salve os relatórios** - crie issues para cada item
4. **Compare** - execute mensalmente para acompanhar evolução
5. **Customize** - adapte o prompt para necessidades específicas
