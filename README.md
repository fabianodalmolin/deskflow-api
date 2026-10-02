# 🎧 DeskFlow API — Gestão de Chamados e Helpdesk de TI

## 🎯 Sobre o Projeto
A **DeskFlow API** é uma Web API RESTful construída em .NET Core 10 utilizando Entity Framework Core e SQL Server. O sistema automatiza o gerenciamento de chamados de suporte técnico, histórico de interações e acompanhamento de status do atendimento.

## 🛠️ Tecnologias Utilizadas
- .NET Core 10 / Web API
- Entity Framework Core 10
- SQL Server
- OpenAPI / Swagger

## 🚀 Como Executar a Aplicação

### Pré-requisitos
- .NET SDK 10 (ou superior)
- SQL Server em execução (SQL Server Express instalado localmente)

### Passo a Passo
1. Clone este repositório:
   ```bash
   git clone https://github.com
   ```

2. Acesse a pasta do projeto:
   ```bash
   cd deskflow-api
   ```

3. Configure a Connection String no arquivo `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=DeskFlowDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

4. Execute as Migrations para criar a estrutura no banco de dados:
   ```bash
   dotnet ef database update
   ```

5. Execute a API:
   ```bash
   dotnet run
   ```

6. Acesse a documentação OpenAPI para testar os endpoints:
   ```text
   http://localhost:5026/openapi/v1.json
   ```

## 🧠 Ciclo de Vida do Chamado
- **Aberto**: Chamado registrado pelo solicitante. Todo chamado nasce automaticamente neste status.
- **EmAndamento**: Suporte técnico em atendimento ao chamado. Bloqueia alterações se o ticket já estiver finalizado.
- **Fechado**: Chamado encerrado. Torna o texto de solução obrigatório e registra a data e hora exatas de conclusão, impedindo novas interações históricas.

## 🧱 Arquitetura em Camadas
- **Controllers**: Recebem as requisições HTTP, efetuam o mapeamento dos endpoints e definem os Status Codes adequados.
- **Services**: Contêm as regras de negócio do Helpdesk e a validação do ciclo de vida dos status.
- **Repositories**: Executam os comandos de persistência e consultas de banco isolando o EF Core.
- **Middlewares**: Interceptam requisições para fornecer tratamento e padronização de erros globais na API.

