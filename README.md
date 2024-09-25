# Documentação Técnica do Projeto: Gestão de Vendas de Brigadeiros
## 1. Banco de Dados
### SQL Server
- **Versão**: SQL Server 2022.
### Estrutura do Banco de Dados
O banco de dados é composto pelas seguintes tabelas:

1. **Clientes**
   - **Descrição**: Armazena informações dos clientes, como nome, telefone, endereço, email e data de cadastro.

2. **Brigadeiros**
   - **Descrição**: Contém os sabores dos brigadeiros disponíveis para venda.

3. **Gastos**
   - **Descrição**: Registra os gastos com insumos, incluindo o valor unitário, quantidade comprada e data da compra. O valor total é calculado automaticamente.

4. **Vendas**
   - **Descrição**: Guarda informações sobre as vendas realizadas, incluindo o cliente associado, data da venda, valor total e forma de pagamento.

5. **VendaBrigadeiros**
   - **Descrição**: Relaciona os brigadeiros vendidos em uma venda específica, armazenando a quantidade de cada brigadeiro vendido.

6. **Encomendas**
   - **Descrição**: Registra as encomendas feitas pelos clientes, incluindo data de solicitação, data de entrega, valor total e forma de pagamento.

7. **EncomendaBrigadeiros**
   - **Descrição** Relaciona os brigadeiros associados a uma encomenda específica, armazenando a quantidade de cada brigadeiro encomendado.

### Exemplo de Estrutura SQL
```sql
CREATE TABLE Clientes (
    IdCliente INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(100) NOT NULL,
    Telefone NVARCHAR(20) NOT NULL,
    Endereco NVARCHAR(255),
    Email NVARCHAR(100),
    DataCadastro DATETIME DEFAULT GETDATE()
);
```
## 2. Tecnologias Utilizadas
- **C#**: Utilizado para a implementação do backend da aplicação.
- **ADO.NET**: Utilizado para a interação com o banco de dados SQL Server, permitindo a execução de comandos SQL e manipulação de dados. Utilizou-se o ADO.NET como uma opção paliativa, pois ainda não foi possível conectar a API no Web Forms.
- **API ASP.NET**: A API utiliza o ADO.NET para gerenciar operações de CRUD (Create, Read, Update, Delete) nas tabelas do banco de dados. 
- **Web Forms**: Utilizado para a criação de páginas web dinâmicas, permitindo a construção das páginas "Sobre", "Vendas" e "Gastos".
- **CSS**: Usado para o estilização das páginas web.

### Pacotes NuGet

- **System.Data.SqlClient**: Fornece classes para trabalhar com SQL Server, permitindo a conexão e a execução de comandos no banco de dados.
- **System.Data**: Fornece classes fundamentais para trabalhar com dados e bases de dados, incluindo suporte para a criação de conexões, comandos e manipulação de dados.

## 3. Uso de APIs
A aplicação faz uso de APIs RESTful para gerenciar operações de CRUD (Criar, Ler, Atualizar e Deletar) nas entidades do sistema.

### Chamadas das APIs
- **Clientes**

    - GET /api/Clientes/BuscasTodosClientes: Retorna uma lista de todos os clientes.
    - GET /api/Clientes/BuscasTodosClientesPorNome: Retorna o cadastro do cliente a partir do nome informado.
    - POST /api/Clientes/InserirDadosCliente: Insere um novo cliente no banco de dados.
    - PUT /api/Clientes/AtualizarClientes: Atualiza as informações de cadastro do cliente de acordo com o id informado.
    - DELETE /api/Clientes/ExclurrClientes: Deleta o cadastro do cliente de acordo com o id informado.
- **Vendas**

    - GET /api/Venda/BuscarTodasVendas: Retorna uma lista de todas as vendas.
    - POST /api/Venda/InserirDadosVendas: Insere uma nova venda no banco de dados.
- **Gastos**

    - GET /api/GastosBrigadeiro/BuscarTodosOsGastos: Retorna uma lista de todos os gastos registrados.
    - POST /api/GastosBrigadeiro/InserirGasto: Insere um novo gasto no banco de dados.
- **Encomenda**

    - GET /api/GastosBrigadeiro/BuscarTodosOsGastos: Retorna uma lista de todos os gastos registrados.
    - POST /api/GastosBrigadeiro/InserirGasto: Insere um novo gasto no banco de dados.
## 4. GitHub
Repositório: https://github.com/alinesanoli

Detalhes Relevantes: O repositório contém toda a estrutura do projeto, incluindo as classes do backend, a estrutura do banco de dados e as páginas web. Utilize o comando git clone para clonar o repositório.

## 5. Considerações Finais
Esta documentação fornece uma visão geral do projeto e suas principais funcionalidades. Para detalhes mais específicos, consulte o código fonte disponível no repositório do GitHub.