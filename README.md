# Web API Pattern 🚀
> Projeto Padrão para a Criação de Web APIs com .NET 8 para o Projeto Caramel (Projeto de TCC para a Faculdade de Engenharia da Computação do ano de 2024)

## 🏛️ Arquitetura e Padrões
- Clean Architecture
- Domain Driven Design (DDD)
- Repository
- Unit of Work

## 🛢️ Banco de Dados
- Entity Framework
- SQL Server

## 🚀 Funcionalidades e Middleware
- Exception Middleware
- Custom Exception
- Fluent Validators
- API Documentation - Swashbuckle
- REST API

## ⚙️ Testes
- Unit Tests with XUnit

  
## Descrição 📝
O Projeto em questão refere-se à uma REST API para gerenciamento de ONGs e seus Pets. Apesar da limitação das informações e métodos,
a arquitetura pode ser utilizada como referência para as demais APIs do Projeto. Dentre as Rotas da Aplicação, encontramos: 

### Ongs Parceiras 🤝

A controller `PartnersController` oferece as seguintes funcionalidades para lidar com Ongs Parceiras:

1. **Recuperação de Todas as Ongs Parceiras**: 
   - Endpoint: `GET /api/partners`
   - Descrição: Recupera uma lista paginada de todas as Ongs Parceiras.
   - Parâmetros:
     - `page`: Página atual da lista (padrão: 1).
     - `pageSize`: Quantidade de Ongs por página (padrão: 10).
   - Respostas:
     - `200 OK`: Retorna a lista de Ongs Parceiras paginada.
     - `500 Internal Server Error`: Em caso de erro no servidor.

2. **Recuperação de uma Ong Parceira por ID**:
   - Endpoint: `GET /api/partner/{partnerId}`
   - Descrição: Recupera uma Ong Parceira específica com base no ID fornecido.
   - Parâmetros:
     - `partnerId`: ID da Ong Parceira a ser recuperada.
   - Respostas:
     - `200 OK`: Retorna a Ong Parceira correspondente ao ID fornecido.
     - `500 Internal Server Error`: Em caso de erro no servidor.

3. **Criação de uma Nova Ong Parceira**:
   - Endpoint: `POST /api/partner`
   - Descrição: Cria uma nova Ong Parceira com base nos dados fornecidos.
   - Parâmetros:
     - `partner`: Dados da nova Ong Parceira a serem criados.
   - Respostas:
     - `201 Created`: Retorna a Ong Parceira criada com sucesso.
     - `400 Bad Request`: Em caso de dados inválidos ou faltando.
     - `500 Internal Server Error`: Em caso de erro no servidor.

4. **Atualização de uma Ong Parceira Existente**:
   - Endpoint: `PUT /api/partner`
   - Descrição: Atualiza os dados de uma Ong Parceira existente com base nos dados fornecidos.
   - Parâmetros:
     - `partner`: Dados atualizados da Ong Parceira.
   - Respostas:
     - `200 OK`: Retorna a Ong Parceira atualizada com sucesso.
     - `404 Not Found`: Se a Ong Parceira não for encontrada.
     - `400 Bad Request`: Em caso de dados inválidos ou faltando.
     - `500 Internal Server Error`: Em caso de erro no servidor.

5. **Exclusão de uma Ong Parceira por ID**:
   - Endpoint: `DELETE /api/partner`
   - Descrição: Exclui uma Ong Parceira específica com base no ID fornecido.
   - Parâmetros:
     - `partnerId`: ID da Ong Parceira a ser excluída.
   - Respostas:
     - `200 OK`: Retorna `true` se a Ong Parceira foi excluída com sucesso.
     - `404 Not Found`: Se a Ong Parceira não for encontrada.
     - `400 Bad Request`: Em caso de dados inválidos ou faltando.
     - `500 Internal Server Error`: Em caso de erro no servidor.

### Pets 🐾

A controller `PetsController` oferece as seguintes funcionalidades para lidar com Pets:

1. **Recuperação de Pets de um Parceiro Específico**: 
   - Endpoint: `GET /api/pets`
   - Descrição: Recupera uma lista paginada de Pets associados a um parceiro específico.
   - Parâmetros:
     - `partnerId`: ID do parceiro.
     - `page`: Página atual da lista (padrão: 1).
     - `pageSize`: Quantidade de Pets por página (padrão: 10).
   - Respostas:
     - `200 OK`: Retorna a lista de Pets paginada.
     - `404 Not Found`: Se o parceiro não for encontrado.
     - `500 Internal Server Error`: Em caso de erro no servidor.

2. **Recuperação de Pets Filtrados por Critérios Específicos**:
   - Endpoint: `GET /api/pets-filtered`
   - Descrição: Recupera uma lista paginada de Pets filtrada por critérios específicos para um parceiro.
   - Parâmetros:
     - `partnerId`: ID do parceiro.
     - `page`: Página atual da lista (padrão: 1).
     - `pageSize`: Quantidade de Pets por página (padrão: 10).
     - `filter`: Filtro a ser aplicado (objeto PetFilter).
   - Respostas:
     - `200 OK`: Retorna a lista de Pets filtrada e paginada.
     - `404 Not Found`: Se o parceiro não for encontrado.
     - `500 Internal Server Error`: Em caso de erro no servidor.

3. **Recuperação de um Pet Específico por ID**:
   - Endpoint: `GET /api/pet`
   - Descrição: Recupera um Pet específico com base no ID fornecido.
   - Parâmetros:
     - `petId`: ID do Pet a ser recuperado.
   - Respostas:
     - `200 OK`: Retorna o Pet correspondente ao ID fornecido.
     - `404 Not Found`: Se o Pet não for encontrado.
     - `500 Internal Server Error`: Em caso de erro no servidor.

4. **Recuperação do Status de um Pet por ID**:
   - Endpoint: `GET /api/pet/status`
   - Descrição: Recupera o status de um Pet específico com base no ID fornecido.
   - Parâmetros:
     - `petId`: ID do Pet a ser consultado.
   - Respostas:
     - `200 OK`: Retorna o status do Pet correspondente ao ID fornecido.
     - `404 Not Found`: Se o Pet não for encontrado.
     - `500 Internal Server Error`: Em caso de erro no servidor.

5. **Criação de um Novo Pet**:
   - Endpoint: `POST /api/pet`
   - Descrição: Cria um novo Pet com base nos dados fornecidos.
   - Parâmetros:
     - `pet`: Dados do novo Pet a serem criados.
   - Respostas:
     - `201 Created`: Retorna o Pet criado com sucesso.
     - `500 Internal Server Error`: Em caso de erro no servidor.

6. **Atualização de um Pet Existente**:
   - Endpoint: `PUT /api/pet`
   - Descrição: Atualiza os dados de um Pet existente com base nos dados fornecidos.
   - Parâmetros:
     - `pet`: Dados atualizados do Pet.
   - Respostas:
     - `200 OK`: Retorna o Pet atualizado com sucesso.
     - `404 Not Found`: Se o Pet não for encontrado.
     - `500 Internal Server Error`: Em caso de erro no servidor.

7. **Atualização do Status de um Pet Existente**:
   - Endpoint: `PATCH /api/pet/status`
   - Descrição: Atualiza o status de um Pet existente com base no ID fornecido.
   - Parâmetros:
     - `petId`: ID do Pet a ser atualizado.
     - `status`: Novo status do Pet.
   - Respostas:
     - `200 OK`: Retorna o status do Pet atualizado com sucesso.
     - `404 Not Found`: Se o Pet não for encontrado.
     - `500 Internal Server Error`: Em caso de erro no servidor.

8. **Exclusão de um Pet Existente**:
   - Endpoint: `DELETE /api/pet`
   - Descrição: Exclui um Pet específico com base no ID fornecido.
   - Parâmetros:
     - `petId`: ID do Pet a ser excluído.
   - Respostas:
     - `200 OK`: Retorna `true` se o Pet foi excluído com sucesso.
     - `404 Not Found`: Se o Pet não for encontrado.
     - `500 Internal Server Error`: Em caso de erro no servidor.

## Instalação 🔧
- Clone o repositório com o seguinte comando: `git clone https://github.com/andrecini/web-api-pattern.git`
- Abra a Solution na pasta do Projeto
- Intale as dependências do Projeto
- Altere a ConnectionString no Arquivo de appSettings para o seu Banco de Dados
- Crie as tabelas no Database a partir da Migration com o seguinte comando no Nuget Package Manager: `update-database`
- Rode a aplicação

