# SupportHub API

API REST para gerenciamento de chamados de suporte técnico, desenvolvida com **C#**, **ASP.NET Core (.NET 10)**, **Entity Framework Core** e **PostgreSQL**.

O SupportHub é um projeto de estudos para praticar o desenvolvimento back-end no ecossistema .NET, com foco em organização por camadas, programação assíncrona, relacionamentos entre entidades e persistência de dados.

## Objetivo

Permitir que usuários abram chamados de suporte, acompanhem seu status e interajam por meio de comentários. A aplicação também prevê atribuição de chamados a responsáveis e atualização de prioridade.

## Tecnologias

- C# / .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- Npgsql (provider do PostgreSQL para EF Core)
- PostgreSQL
- Git e GitHub

## Estrutura do projeto

```text
SupportHub/
├── SupportHub.slnx
└── SupportHub.Api/
    ├── Controllers/      # Endpoints HTTP
    ├── Data/             # AppDbContext
    ├── DTOs/             # Objetos de entrada e saída da API
    ├── Models/           # Entidades de domínio
    ├── Services/         # Contratos e regras de negócio
    ├── Migrations/       # Versionamento do esquema do banco
    ├── Program.cs        # Configuração da aplicação
    └── appsettings.json  # Configurações gerais
```

> A estrutura acima representa a organização em desenvolvimento e pode evoluir conforme novas funcionalidades forem implementadas.

## Domínio

As entidades principais são:

- **User:** usuário do sistema, com nome, e-mail e papel de acesso.
- **Ticket:** chamado de suporte, com título, descrição, status, prioridade, datas, solicitante e responsável opcional.
- **Comment:** comentário associado a um chamado e ao usuário que o escreveu.

Os papéis, status e prioridades são representados por enums.

## Como executar localmente

### Pré-requisitos

- .NET SDK 10
- PostgreSQL em execução
- Ferramenta `dotnet-ef` (para migrations)

### 1. Clonar o projeto

```bash
git clone https://github.com/SEU-USUARIO/SupportHub.git
cd SupportHub/SupportHub.Api
```

> Substitua `SEU-USUARIO` pelo nome do seu usuário do GitHub depois de publicar o repositório.

### 2. Criar banco e usuário no PostgreSQL

No `psql`, com uma conta que tenha permissão para criar usuários e bancos:

```sql
CREATE USER supporthub_user WITH PASSWORD 'SUA_SENHA_FORTE';
CREATE DATABASE supporthub OWNER supporthub_user;
```

### 3. Configurar a conexão com o banco

Para desenvolvimento local, prefira **User Secrets** a colocar senhas no Git:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=supporthub;Username=supporthub_user;Password=SUA_SENHA_FORTE"
```

A aplicação lê essa configuração através do `Program.cs`. Não publique senhas reais no `appsettings.json` ou no repositório.

### 4. Restaurar dependências e aplicar migrations

```bash
dotnet restore
dotnet tool install --global dotnet-ef  # se ainda não estiver instalada
dotnet ef database update
```

### 5. Executar a API

```bash
dotnet run
```

A URL local aparecerá no terminal, por exemplo `http://localhost:5000` (a porta pode variar).

## Endpoints planejados

| Método | Rota | Descrição |
|---|---|---|
| `POST` | `/api/tickets` | Criar chamado |
| `GET` | `/api/tickets` | Listar chamados |
| `GET` | `/api/tickets/{id}` | Consultar chamado pelo ID |
| `GET` | `/api/tickets/user/{userId}` | Listar chamados de um usuário |
| `PATCH` | `/api/tickets/{id}/status` | Atualizar status |
| `PATCH` | `/api/tickets/{id}/priority` | Atualizar prioridade |
| `POST` | `/api/tickets/{id}/comments` | Adicionar comentário |

> **Status:** a estrutura inicial e a primeira migration do banco já foram criadas. A camada de serviços, os DTOs e os endpoints estão em implementação; nem todas as rotas desta tabela estão disponíveis ainda.

## Próximos passos

- [ ] Finalizar e testar o `TicketService`
- [ ] Concluir DTOs e `TicketsController`
- [ ] Testar operações no PostgreSQL e via Postman
- [ ] Criar endpoints de usuários e comentários
- [ ] Adicionar validação e tratamento global de exceções
- [ ] Implementar autenticação e autorização
- [ ] Adicionar testes automatizados
- [ ] Preparar containerização com Docker

## Aprendizados do projeto

Este projeto é utilizado para estudar conceitos do .NET, incluindo `async`/`await`, `Task<T>`, injeção de dependência, LINQ, EF Core, migrations, DTOs e APIs REST.

---

**Projeto em desenvolvimento, com finalidade de aprendizado e portfólio.**
