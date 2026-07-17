# Controle de Gastos Residenciais

Sistema web para controle de receitas e despesas de uma residência.

O sistema permite cadastrar pessoas, registrar transações financeiras, visualizar saldos individuais e acompanhar o total geral de receitas, despesas e saldo da residência.

## Funcionalidades

- Cadastro de pessoas
- Edição de pessoas
- Exclusão de pessoas
- Pesquisa de pessoas
- Visualização do saldo de cada pessoa
- Visualização dos detalhes de uma pessoa
- Cadastro de receitas e despesas
- Listagem de transações
- Pesquisa de transações por descrição
- Filtro de transações por tipo
- Cálculo do total de receitas
- Cálculo do total de despesas
- Cálculo do saldo geral
- Navegação entre Início, Pessoas e Transações

## Regra de negócio

Pessoas menores de 18 anos não podem receber transações do tipo receita.

Elas podem possuir apenas despesas.

## Tecnologias utilizadas

### Front-end

- React
- TypeScript
- Vite
- React Router
- CSS

### Back-end

- ASP.NET Core Web API
- C#
- Entity Framework Core

### Banco de dados

- SQLite


## Estrutura do projeto

```text
ControleGastosResidenciais/
├── backend/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Data/
│   ├── Enums/
│   ├── Migrations/
│   ├── Models/
│   ├── Properties/
│   ├── Services/
│   ├── Program.cs
│   └── appsettings.json
│
└── frontend/
    ├── src/
    │   ├── components/
    │   ├── config/
    │   │   └── api.ts
    │   ├── pages/
    │   │   ├── Home/
    │   │   ├── Pessoas/
    │   │   └── Transacoes/
    │   ├── App.tsx
    │   ├── main.tsx
    │   └── index.css
    ├── .env.example
    └── package.json
```

## Como executar o projeto

Execute os comandos na ordem abaixo.

### 1. Clonar o repositório

```bash
git clone https://github.com/eliezernovais/ControleGastosResidenciais.git
```

Entre na pasta do projeto:

```bash
cd ControleGastosResidenciais
```

---

### 2. Configurar o back-end

Entre na pasta do back-end:

```bash
cd Backend
```

Restaure as dependências:

```bash
dotnet restore
```

Caso o Entity Framework CLI ainda não esteja instalado:

```bash
dotnet tool install --global dotnet-ef
```

Crie ou atualize o banco de dados usando as migrations:

```bash
dotnet ef database update
```

Inicie a API:

```bash
dotnet run
```

A API será iniciada em:

```text
http://localhost:5213
```

Mantenha esse terminal aberto.

---

### 3. Configurar o front-end

Abra outro terminal e entre na pasta do front-end:

```bash
cd Frontend
```

Instale as dependências:

```bash
npm install
```

Crie o arquivo `.env` usando o `.env.example`.

No PowerShell:

```powershell
Copy-Item .env.example .env
```

No Prompt de Comando:

```bat
copy .env.example .env
```

Confira se o arquivo `.env` contém:

```env
VITE_API_URL=http://localhost:5213
```

Inicie o front-end:

```bash
npm run dev
```

O front-end será iniciado normalmente em:

```text
http://localhost:5173
```

Abra esse endereço no navegador.

---

## Resumo dos comandos

### Terminal 1 — Back-end

```powershell
cd Backend
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet run
```

> O comando `dotnet tool install --global dotnet-ef` só precisa ser executado uma vez no computador.

### Terminal 2 — Front-end

```powershell
cd Frontend
npm install
Copy-Item .env.example .env
npm run dev
```

> O comando `Copy-Item .env.example .env` só precisa ser executado quando o arquivo `.env` ainda não existir.

## Início rápido

Após realizar a configuração inicial, abra dois terminais.

### Terminal 1 — Back-end

```powershell
cd Backend
dotnet run
```

### Terminal 2 — Front-end

```powershell
cd Frontend
npm run dev
```
