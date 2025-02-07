# NFL Fantasy Bolas Back-end APP 🏈

Um **Web App** desenvolvido em React (front-end) e C# .NET 8 (back-end) para gerenciar ligas de Fantasy Football da NFL usando a API do [Sleeper](https://docs.sleeper.com/). 
Projeto criado para acompanhar estatísticas, standings e playoffs entre amigos.

---

## Funcionalidades ⚡

- **Dados da Liga**: Visualização de informações básicas da liga (nome, temporada, status).  
- **Classificação (Standings)**: Tabela com vitórias, derrotas, pontos a favor e posição dos times.  
- **Playoffs**: Detalhes das partidas da fase final, campeão e vice-campeão.  
- **Integração com Sleeper API**: Consumo de dados em tempo real de usuários, times e drafts.  

---

## Tecnologias Utilizadas 🛠️

| **Back-end**          | **Ferramentas**          |
|-----------------------|--------------------------|
| .NET 8                | Docker                   |
| ASP.NET Core Web API  | Swagger (OpenAPI)        |
| Entity Framework      | GitHub Actions (CI/CD)   |
| Redis (Cache)         | Visual Studio / VS Code  |

---

## Como Executar Localmente 🚀

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)

### Passo a Passo

1. **Clone o Repositório**:
   ```bash
   git clone https://github.com/seu-usuario/NFBAPPService.git
   cd nfl-fantasy-dashboard
   ```

2. **Back-end (.NET)**:
   ```bash
   cd NFBAPPService
   dotnet restore
   dotnet run
   ```
   A API estará em http://localhost:5000 (ou https://localhost:5001).

4. **Acesse a Documentação da API**:
   - **Swagger UI**: http://localhost:5000/swagger

---

## Endpoints Principais 🔗

| **Endpoint** | **Descrição** |
|-------------|--------------|
| GET /api/sleeper/league/{leagueId} | Retorna dados da liga. |
| GET /api/sleeper/rosters/{leagueId} | Informações dos participantes. |
| GET /api/sleeper/standings/{leagueId} | Classificação dos times. |
| GET /api/sleeper/playoffs/{leagueId} | Detalhes dos playoffs e campeão. |

---

### Exemplo de Dockerfile para o Back-end:
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SleeperBackend.dll"]
```

---

## Como Contribuir 🤝

1. **Faça um fork do projeto**.
2. **Crie uma branch**:
   ```bash
   git checkout -b feature/nova-funcionalidade
   ```
3. **Commit suas mudanças**:
   ```bash
   git commit -m 'Adicionei algo incrível!'
   ```
4. **Push para a branch**:
   ```bash
   git push origin feature/nova-funcionalidade
   ```
5. **Abra um Pull Request** e descreva suas alterações.

---

## Licença 📝

Distribuído sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

---

## Contato ✉️

- **Seu Nome**
- **GitHub**: [@Cissa09](https://github.com/Cissa09)
- **Email**: cicero.viganon@hotmail.com

