# 🔐 Login_User

Sistema de autenticação de utilizadores desenvolvido em **C#** com **.NET**, utilizando **SQL Server (LocalDB)** como base de dados.

---

## 📋 Descrição

Projeto de login e registo de utilizadores com ligação direta ao SQL Server, sem uso de ORM. Implementado com boas práticas de segurança como hash de senha e controlo de estado do utilizador.

---

## 🛠️ Tecnologias

- C# / .NET
- SQL Server (LocalDB)
- Microsoft.Data.SqlClient

---

## 🗄️ Estrutura da Base de Dados

```sql
CREATE TABLE Usuario
(
    Id            INT           IDENTITY(1,1) PRIMARY KEY,
    Nome          VARCHAR(100)  NOT NULL,
    Email         VARCHAR(254)  UNIQUE NOT NULL,
    SenhaHash     VARCHAR(255)  NOT NULL,
    Ativo         BIT           NOT NULL DEFAULT 1,
    DataCriacao   DATETIME2     NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2     NULL
);
```

---

## 📁 Estrutura do Projeto

```
Login_User/
├── Data/
│   └── DbConnectionFactory.cs   # Fábrica de conexão com o banco
├── Models/
│   └── User.cs                  # Modelo do utilizador
└── Program.cs                   # Ponto de entrada da aplicação
```

---

## ⚙️ Como executar

### Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download)
- SQL Server ou LocalDB instalado

### 1. Clona o repositório

```bash
git clone https://github.com/teu-usuario/Login_User.git
cd Login_User
```

### 2. Instala as dependências

```bash
dotnet add package Microsoft.Data.SqlClient
```

### 3. Configura a connection string

Em `Data/DbConnectionFactory.cs`, ajusta a connection string conforme o teu ambiente:

```csharp
"Server=(localdb)\\MSSQLLocalDB;Database=Login_User;Trusted_Connection=True;TrustServerCertificate=True;"
```

### 4. Cria a base de dados

Abre o **SQL Server Management Studio (SSMS)** e executa o script SQL da secção acima.

### 5. Executa o projeto

```bash
dotnet run
```

---

## 🔒 Segurança

- As senhas são armazenadas como **hash** — nunca em texto simples
- Utilizadores podem ser desativados sem serem eliminados (`Ativo = false`)
- Datas armazenadas em **UTC** para consistência entre fusos horários

---

## 📌 Estado do Projeto

🚧 Em desenvolvimento

---

## 📄 Licença

Este projeto está sob a licença MIT.
