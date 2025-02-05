# JsonPlaceholder API

Este projeto foi desenvolvido para consumir a API JSONPlaceHolder usando ASP.NET Core, aplicando boas práticas de desenvolvimento e implementando testes unitários com Moq e xUnit. O objetivo é demonstrar como realizar chamadas HTTP de forma segura e eficiente, além de validar o funcionamento da aplicação através de testes automatizados.

## Índice

- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Como Instalar e Executar o Projeto](#como-instalar-e-executar-o-projeto)
- [Endpoints da API](#endpoints-principais)
- [Funcionalidade](#funcionalidade)
- [Contato](#contato)

## Tecnologias Utilizadas
- **ASP NET Core**
- **HttpClientFactory**
- **xUnit para testes unitários**
- **Moq para simulação de dependências**
- **ILogger para logging**

## Como instalar e Executar o projeto
1. Clone o repositório:
   ```bash
   git clone https://github.com/carloseduardo-alves/JsonPlaceholderAPI.git
   ```

2. Instale as dependências
   ```bash
   dotnet restore
   ```

3. Execute o projeto
   ```bash
   dotnet run
   ```

4. Executar os testes
   ```bash
   dotnet test
   ```

## Endpoints principais: 
- **GET** `/api/posts` - Lista todos os posts.
- **GET** `/api/posts/{id}` - Busca um post específico pelo ID.
- **POST** `/api/posts` - Cria um novo post.
- **PUT** `/api/posts/{id}` - Atualiza um post existente.
- **DELETE** `/api/posts/{id}` - Deleta um post pelo ID.

## Funcionalidade
- Consumo da API JsonPlaceholder para buscar postagens.
- Implementação de um serviço HTTP reutilizável.
- Tratamento de erros e logging de falhas.
- Testes unitários utilizando Moq e xUnit.

## Contato
Se tiver dúvidas ou sugestões, entre em contato:
- Email: c.eduardoalves9@gmail.com
- Linkedln: https://www.linkedin.com/in/carloseduardo-alves-/

## Licença
Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.








