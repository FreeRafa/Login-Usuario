
--------------------------------
-- CRIANDO BANCO DE DADOS
--------------------------------

CREATE DATABASE Login_User;
GO

--------------------------------
-- CRIANDO A TABeLA
--------------------------------

CREATE TABLE Usuario 
(
Id INT IDENTITY (1,1) PRIMARY KEY,
Nome VARCHAR (100) NOT NULL, 
Email VARCHAR (254) UNIQUE NOT NULL,
SenhaHash VARCHAR (255) NOT NULL,
Ativo BIT NOT NULL DEFAULT 1,
DataCriacao DATETIME2 NOT NULL DEFAULT GETDATE(),
DataAlteracao DATETIME2(0) NULL --Armazena data e hora, o (0) significa sem casas decimais ex. » 2025-05-17 14:30:00
);

