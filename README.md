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
