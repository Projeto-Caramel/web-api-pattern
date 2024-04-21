# Web API Pattern 🚀
> Projeto Padrão para a Criação de Web APIs com .NET 8 para o Projeto Caramel (Projeto de TCC para a Faculdade de Engenharia da Computação do ano de 2024)

## 🏛️ Arquitetura e Padrões
- [Clean Architecture](Documentation/CleanArchitecture.md)
- [Domain Driven Design (DDD)](Documentation/DDD.md)
- [Repository Pattern](Documentation/RepositoryPattern.md)
- [Unit of Work](Documentation/UnitOfWork.md)

## 🛢️ Banco de Dados
- [Entity Framework](Documentation/EntityFramework.md)
- [SQL Server](Documentation/SqlServer.md)

## 🚀 Funcionalidades e Middleware
- [Exception Middleware](Documentation/ExceptionMiddleware.md)
- [Custom Exception](Documentation/CustomExceptions.md)
- [Fluent Validators](Documentation/FluentValidator.md)
- [API Documentation - Swashbuckle](Documentation/Swashbuckle.md)
- [REST API](Documentation/RepositoryPattern.md)
- [AutoMapper](Documentation/AutoMapper.md)

## ⚙️ Testes
- [Unit Tests with XUnit](Documentation/UnitTests.md)

  
## Descrição 📝
O Projeto em questão refere-se à uma REST API para gerenciamento de ONGs e seus Pets. Apesar da limitação das informações e métodos,
a arquitetura pode ser utilizada como referência para as demais APIs do Projeto. Dentre as Rotas da Aplicação, encontramos: 

<details>
<summary><strong>Ongs Parceiras 🤝</strong></summary>

A controller `PartnersController` oferece as seguintes funcionalidades para lidar com Ongs Parceiras:

1. **Recuperação de Todas as Ongs Parceiras**: 
   - Endpoint: `GET /api/v1/partners`
   - Descrição: Recupera uma lista paginada de todas as Ongs Parceiras.
   - Parâmetros:
     - `page`: Página atual da lista (padrão: 1).
     - `pageSize`: Quantidade de Ongs por página (padrão: 10).
   - Respostas:
     - `200 OK`: Retorna a lista de Ongs Parceiras paginada.
     - `500 Internal Server Error`: Em caso de erro no servidor.

2. **Recuperação de uma Ong Parceira por ID**:
   - Endpoint: `GET /api/v1/partners/{partnerId}`
   - Descrição: Recupera uma Ong Parceira específica com base no ID fornecido.
   - Parâmetros:
     - `partnerId`: ID da Ong Parceira a ser recuperada.
   - Respostas:
     - `200 OK`: Retorna a Ong Parceira correspondente ao ID fornecido.
     - `500 Internal Server Error`: Em caso de erro no servidor.

3. **Criação de uma Nova Ong Parceira**:
   - Endpoint: `POST /api/v1/partners`
   - Descrição: Cria uma nova Ong Parceira com base nos dados fornecidos.
   - Parâmetros:
     - `partner`: Dados da nova Ong Parceira a serem criados.
   - Respostas:
     - `201 Created`: Retorna a Ong Parceira criada com sucesso.
     - `422 Unprocessable Entity`: Em caso de dados inválidos ou faltando.
     - `400 Bad Request`: Em caso de erro ao Adicionar o Parceiro.
     - `500 Internal Server Error`: Em caso de erro no servidor.

4. **Atualização de uma Ong Parceira Existente**:
   - Endpoint: `PUT /api/v1/partners`
   - Descrição: Atualiza os dados de uma Ong Parceira existente com base nos dados fornecidos.
   - Parâmetros:
     - `partner`: Dados atualizados da Ong Parceira.
   - Respostas:
     - `200 OK`: Retorna a Ong Parceira atualizada com sucesso.
     - `422 Unprocessable Entity`: Em caso de dados inválidos ou faltando.
     - `400 Bad Request`: Em caso de erro ao Atualizar o Parceiro.
     - `500 Internal Server Error`: Em caso de erro no servidor.

5. **Exclusão de uma Ong Parceira por ID**:
   - Endpoint: `DELETE /api/v1/partners/{partnerId}`
   - Descrição: Exclui uma Ong Parceira específica com base no ID fornecido.
   - Parâmetros:
     - `partnerId`: ID da Ong Parceira a ser excluída.
   - Respostas:
     - `204 No Content`:Em caso de a Ong Parceira foi excluída com sucesso.
     - `422 Unprocessable Entity`: Em caso de dados inválidos ou faltando.
     - `400 Bad Request`: Em caso de erro ao Excluir o Parceiro.
     - `500 Internal Server Error`: Em caso de erro no servidor.

</details>

<details>
<summary><strong>Pets 🐾</strong></summary>

A controller `PetsController` oferece as seguintes funcionalidades para lidar com Pets:

1. **Recuperação de Pets de um Parceiro Específico**: 
   - Endpoint: `GET /api/v1/pets`
   - Descrição: Recupera uma lista paginada de Pets associados a um parceiro específico.
   - Parâmetros:
     - `partnerId`: ID do parceiro.
     - `page`: Página atual da lista (padrão: 1).
     - `pageSize`: Quantidade de Pets por página (padrão: 10).
   - Respostas:
     - `200 OK`: Retorna a lista de Pets paginada.
     - `422 Unprocessable Entity`: Em caso de dados inválidos ou faltando.
     - `500 Internal Server Error`: Em caso de erro no servidor.

2. **Recuperação de Pets Filtrados por Critérios Específicos**:
   - Endpoint: `GET /api/v1/pets/filtered`
   - Descrição: Recupera uma lista paginada de Pets filtrada por critérios específicos para um parceiro.
   - Parâmetros:
     - `partnerId`: ID do parceiro.
     - `page`: Página atual da lista (padrão: 1).
     - `pageSize`: Quantidade de Pets por página (padrão: 10).
     - `filter`: Filtro a ser aplicado (objeto PetFilter).
   - Respostas:
     - `200 OK`: Retorna a lista de Pets filtrada e paginada.
     - `422 Unprocessable Entity`: Em caso de dados inválidos ou faltando.
     - `500 Internal Server Error`: Em caso de erro no servidor.

3. **Recuperação de um Pet Específico por ID**:
   - Endpoint: `GET "/api/v1/pets/{petId}`
   - Descrição: Recupera um Pet específico com base no ID fornecido.
   - Parâmetros:
     - `petId`: ID do Pet a ser recuperado.
   - Respostas:
     - `200 OK`: Retorna o Pet correspondente ao ID fornecido.
     - `422 Unprocessable Entity`: Em caso de dados inválidos ou faltando.
     - `500 Internal Server Error`: Em caso de erro no servidor.

4. **Recuperação do Status de um Pet por ID**:
   - Endpoint: `GET /api/v1/pets/{petId}/status`
   - Descrição: Recupera o status de um Pet específico com base no ID fornecido.
   - Parâmetros:
     - `petId`: ID do Pet a ser consultado.
   - Respostas:
     - `200 OK`: Retorna o status do Pet correspondente ao ID fornecido.
     - `422 Unprocessable Entity`: Em caso de dados inválidos ou faltando.
     - `500 Internal Server Error`: Em caso de erro no servidor.

5. **Criação de um Novo Pet**:
   - Endpoint: `POST /api/v1/pets`
   - Descrição: Cria um novo Pet com base nos dados fornecidos.
   - Parâmetros:
     - `pet`: Dados do novo Pet a serem criados.
   - Respostas:
     - `201 Created`: Retorna o Pet criado com sucesso.
     - `422 Unprocessable Entity`: Em caso de dados inválidos ou faltando.
     - `400 Bad Request`: Em caso de erro ao Adicionar o Pet.
     - `500 Internal Server Error`: Em caso de erro no servidor.

6. **Atualização de um Pet Existente**:
   - Endpoint: `PUT /api/v1/pets`
   - Descrição: Atualiza os dados de um Pet existente com base nos dados fornecidos.
   - Parâmetros:
     - `pet`: Dados atualizados do Pet.
   - Respostas:
     - `200 OK`: Retorna o Pet atualizado com sucesso.
     - `422 Unprocessable Entity`: Em caso de dados inválidos ou faltando.
     - `400 Bad Request`: Em caso de erro ao Atualizar o Pet.
     - `500 Internal Server Error`: Em caso de erro no servidor.

7. **Atualização do Status de um Pet Existente**:
   - Endpoint: `PATCH /api/v1/pets/{petId}/status`
   - Descrição: Atualiza o status de um Pet existente com base no ID fornecido.
   - Parâmetros:
     - `petId`: ID do Pet a ser atualizado.
     - `status`: Novo status do Pet.
   - Respostas:
     - `200 OK`: Retorna o status do Pet atualizado com sucesso.
     - `422 Unprocessable Entity`: Em caso de dados inválidos ou faltando.
     - `400 Bad Request`: Em caso de erro ao Atualizar o Status de um Pet.
     - `500 Internal Server Error`: Em caso de erro no servidor.

8. **Exclusão de um Pet Existente**:
   - Endpoint: `DELETE /api/v1/pets/{petId}`
   - Descrição: Exclui um Pet específico com base no ID fornecido.
   - Parâmetros:
     - `petId`: ID do Pet a ser excluído.
   - Respostas:
     - `200 OK`: Retorna `true` se o Pet foi excluído com sucesso.
     - `422 Unprocessable Entity`: Em caso de dados inválidos ou faltando.
     - `400 Bad Request`: Em caso de erro ao Deletar o Pet.
     - `500 Internal Server Error`: Em caso de erro no servidor.

</details>

## Instalação 🔧

1. **Clone o repositório**:
   - Execute o comando no terminal:
     ```
     git clone https://github.com/andrecini/web-api-pattern.git
     ```

2. **Abra a Solution**:
   - Navegue até o diretório clonado e abra a Solution (`web-api-pattern.sln`) em sua IDE preferida.

3. **Instale as dependências do projeto**:
   - Certifique-se de que todas as dependências do projeto estão instaladas. Você pode fazer isso restaurando os pacotes NuGet. Você pode usar o Visual Studio para isso ou executar o seguinte comando no terminal:
     ```
     dotnet restore
     ```

4. **Altere a ConnectionString**:
   - No arquivo `appSettings.json`, localize a seção onde está definida a ConnectionString e substitua-a pelo caminho correto do seu banco de dados:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "SuaConnectionStringAqui"
     }
     ```

5. **Execute a migração para criar as tabelas no banco de dados**:
   - Abra o NuGet Package Manager Console no Diretório da projeto 'Caramel.Pattern.Services.Infra' e execute o seguinte comando para aplicar as migrações e criar as tabelas no banco de dados:
     ```
     update-database
     ```

6. **Rode a aplicação**:
   - Inicie a aplicação a partir da IDE ou execute o seguinte comando no terminal:
     ```
     dotnet run
     ```


