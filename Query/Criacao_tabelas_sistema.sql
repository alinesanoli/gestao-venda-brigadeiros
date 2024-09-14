CREATE TABLE Clientes(
 IdCliente INT PRIMARY KEY IDENTITY,
 Nome NVARCHAR(100) NOT NULL,
 Telefone NVARCHAR(20) NOT NULL,
 Endereco NVARCHAR(255),
 Email NVARCHAR(100),
 DataCadastro DATETIME DEFAULT GETDATE()
);

CREATE TABLE Brigadeiros(
 IdBrigadeiro INT PRIMARY KEY IDENTITY,
 Sabor NVARCHAR(100) NOT NULL
);

CREATE TABLE Gastos (
 IdGastos INT PRIMARY KEY IDENTITY,
 Insumo NVARCHAR(100) NOT NULL,
 ValorUnitario DECIMAL(10, 2) NOT NULL,
 QuantidadeComprada INT NOT NULL,
 ValorTotal AS (ValorUnitario * QuantidadeComprada),
 DataCompra DATETIME NOT NULL
);

CREATE TABLE Vendas(
 IdVenda INT PRIMARY KEY IDENTITY,
 IdCliente INT FOREIGN KEY REFERENCES Clientes(IdCliente),
 DataVenda DATETIME NOT NULL	,
 ValorTotal DECIMAL(10, 2) NOT NULL,
 FormaPagamento NVARCHAR(50),
 CONSTRAINT FK_Vendas_Clientes FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente)
);


CREATE TABLE VendaBrigadeiros (
 IdVenda INT FOREIGN KEY REFERENCES Vendas(IdVenda),
 IdBrigadeiro INT FOREIGN KEY REFERENCES Brigadeiros(IdBrigadeiro),
 Quantidade INT NOT NULL,
 PRIMARY KEY (IdVenda, IdBrigadeiro)
);

CREATE TABLE Encomendas (
 IdEncomenda INT PRIMARY KEY IDENTITY,
 IdCliente INT FOREIGN KEY REFERENCES Clientes(IdCliente),
 DataSolicitacao DATETIME DEFAULT GETDATE() NOT NULL,
 DataEntrega DATETIME NOT NULL,
 ValorTotal DECIMAL(10, 2),
 FormaPagamento NVARCHAR(50),
 CONSTRAINT FK_Encomendas_Clientes FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente)
);

CREATE TABLE EncomendaBrigadeiros (
 IdEncomenda INT FOREIGN KEY REFERENCES Encomendas(IdEncomenda),
 IdBrigadeiro INT FOREIGN KEY REFERENCES Brigadeiros(IdBrigadeiro),
 Quantidade INT NOT NULL,
 PRIMARY KEY (IdEncomenda, IdBrigadeiro)
)

INSERT INTO Brigadeiros (Sabor) VALUES 
('Chocolate 50%'),
('Chocolate branco'),
('Romeu e Julieta'),
('Casadinho'),
('Café'),
('Tapioca com doce de leite');

INSERT INTO Clientes (Nome, Telefone, Endereco, Email, DataCadastro) 
VALUES
('Aline', '11999999999', 'Rua Alguma Coisa, Bairro Tal', 'aline@teste.com.br');
('Mariana Oliveira', '51955555555', 'Travessa das Árvores, 10, Parque Verde', 'mariana.oliveira@teste.com.br'),
('Rafael Mendes', '61944444444', 'Rua do Comércio, 65, Bairro Industrial', 'rafael.mendes@teste.com.br');

ALTER TABLE Vendas
DROP CONSTRAINT DF__Vendas__DataVend__3F466844;

ALTER TABLE Vendas
ALTER COLUMN DataVenda DATE;

ALTER TABLE Clientes
ALTER COLUMN DataCadastro DATE;

ALTER TABLE Clientes
ADD CONSTRAINT DF_Clientes_DataCadastro DEFAULT GETDATE() FOR DataCadastro;