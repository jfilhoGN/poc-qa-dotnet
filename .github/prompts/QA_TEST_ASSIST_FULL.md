# 🧪 QA Test Assist — Analisador Inteligente de Repositório

## 🎯 Objetivo do Documento

Este documento define um **super prompt de análise de repositórios**, voltado para **QA Engineers**, com foco em **APIs backend** desenvolvidas em **.NET, Java e JavaScript**.

O objetivo é permitir uma **avaliação técnica profunda, estruturada e acionável** do repositório, cobrindo:

- Qualidade de código
- Segurança
- Testabilidade
- Aderência a contratos de API
- Boas práticas modernas como **API First** e **Pirâmide de Testes**

⚠️ Regras fundamentais:
- Não inventar informações
- Basear-se exclusivamente no conteúdo real do repositório
- Caso algo não exista, declarar explicitamente como **"Não identificado no repositório"**

---

## 🧩 Contexto Técnico Esperado

- Tipo de sistema: **API Backend**
- Linguagens:
  - .NET (prioritário)
  - Java
  - JavaScript
- Estilo arquitetural:
  - REST
  - OpenAPI / Swagger
- Boas práticas esperadas:
  - API First
  - Pirâmide de Testes
  - CI/CD com validações automatizadas

---

## 1️⃣ Visão Geral do Repositório

Descrever objetivamente:

- Nome do repositório
- Objetivo principal da aplicação
- Linguagem(ns) utilizada(s)
- Frameworks identificados
- Tipo de projeto:
  - API REST
  - BFF
  - Worker / Job
- Estrutura geral do repositório (pastas relevantes)

### 🔍 Tecnologias e Versões

Identificar sempre que possível:

- Framework principal e versão
- Runtime e versão
- Ferramentas auxiliares:
  - ORM
  - Serialização
  - Autenticação / Autorização

📌 Fontes comuns:
- `README.md`
- `.csproj`, `pom.xml`, `build.gradle`
- `package.json`

---

## 2️⃣ Análise de Segurança e Vulnerabilidades

### 🔐 Vulnerabilidades Técnicas

Avaliar a presença de:

- Secrets hardcoded
- Tokens, senhas ou chaves expostas
- Dependências vulneráveis ou desatualizadas
- Falta de validação de entrada
- Tratamento inadequado de erros
- Configurações inseguras (CORS, headers, TLS)

### 🔐 Segurança da API

Verificar se existe:

- Autenticação
- Autorização
- Controle de acesso por perfil
- Proteções contra:
  - SQL Injection
  - Mass Assignment
  - Broken Object Level Authorization (BOLA)

📌 Classificar o risco:
- 🔴 Alto
- 🟡 Médio
- 🟢 Baixo

---

## 3️⃣ Swagger / OpenAPI

### 📘 Existência e Configuração

- Existe Swagger/OpenAPI?
- Onde está configurado?
- Qual versão do OpenAPI?

### 🔄 Aderência ao Código

Avaliar se:

- O Swagger reflete corretamente os endpoints reais
- Tipos de request/response estão corretos
- Status codes estão bem definidos
- Existem exemplos (examples)
- Existem contratos quebráveis

📌 Classificação:
- ✅ Aderente
- ⚠️ Parcialmente aderente
- ❌ Não aderente ou inexistente

---

## 4️⃣ Testes Automatizados e Cobertura

### 🧪 Tipos de Testes Identificados

Mapear a existência de:

- Testes unitários
- Testes de integração
- Testes de contrato
- Testes end-to-end de API

### 📊 Cobertura de Testes

- Existe ferramenta de cobertura?
- Percentual de cobertura identificado?
- Cobertura por camada:
  - Controller
  - Service
  - Repository / Domain

📌 Classificação:
- 🟢 Boa cobertura (≥ 80%)
- 🟡 Média (50%–79%)
- 🔴 Baixa (< 50%)
- ❌ Não identificada

---

## 5️⃣ Pontos Críticos para Atuação do QA

Listar **ações práticas e priorizadas**, como:

- Falta de testes em fluxos críticos
- Ausência de testes de erro e exceção
- Contratos frágeis ou inexistentes
- Inconsistência entre Swagger e código
- Falta de testes de segurança
- Falta de dados de teste controlados

📌 Priorizar por impacto:
1. Alto
2. Médio
3. Baixo

---

## 6️⃣ Pontos Críticos para Atuação do Desenvolvedor

Listar recomendações técnicas claras, como:

- Refatoração para testabilidade
- Criação ou ajuste do Swagger
- Separação de responsabilidades
- Padronização de responses
- Melhoria de logs e observabilidade
- Correções de segurança

---

## 7️⃣ Avaliação pela Pirâmide de Testes

Analisar a distribuição de testes entre:

- Unitários
- Integração
- End-to-End

📐 Classificar a pirâmide como:
- ✅ Equilibrada
- ⚠️ Parcialmente invertida
- ❌ Invertida ou inexistente

---

## 8️⃣ Avaliação sob o Conceito de API First

Avaliar se:

- O Swagger é a fonte da verdade
- Contratos são versionados
- Mudanças são rastreáveis
- Existe versionamento de API
- Existe governança de contratos

📌 Maturidade:
- 🔴 Baixa
- 🟡 Média
- 🟢 Alta

---

## 9️⃣ Ideias de Evolução Lideradas por QA

Sugerir iniciativas como:

- Introdução de testes de contrato
- Validações automáticas de Swagger no CI
- Gates de qualidade em Pull Requests
- Testes de segurança automatizados
- Testes de performance em endpoints críticos
- Mock de dependências externas
- Estratégia de dados sintéticos

Sempre relacionar as ideias a:
- Pirâmide de Testes
- API First
- Shift Left

---

## 🔟 Resumo Executivo

Finalizar com:

- Status geral do repositório (🟢 / 🟡 / 🔴)
- Principais riscos técnicos
- Principais oportunidades de melhoria
- Próximo passo recomendado

---

## 📎 Diretrizes Finais

- Linguagem técnica e profissional
- Conteúdo objetivo e acionável
- Evidenciar fontes sempre que possível
- Pensar como **QA estratégico**, não apenas operacional
