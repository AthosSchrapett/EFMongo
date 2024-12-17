# EFMongo - Integração do MongoDB com Entity Framework Core

Este projeto demonstra como integrar o **MongoDB** com o **Entity Framework Core**, permitindo que desenvolvedores utilizem o Entity Framework (EF) como ORM em um banco NoSQL. A solução é ideal para cenários que exigem alta flexibilidade de dados e a produtividade do EF.

---

## 🔎 Visão Geral

O **EFMongo** é uma implementação que permite trabalhar com coleções do MongoDB de forma semelhante ao modelo relacional suportado pelo Entity Framework. A abordagem proporciona:
- Consultas LINQ com suporte ao MongoDB.
- Mapeamento de classes (Entities) para coleções.
- Facilidade de migração entre bancos relacionais e não-relacionais.

---

## 💼 Tecnologias Utilizadas
- **C#**
- **.NET 9**
- **Entity Framework Core**
- **LINQ**

---

## 🔧 Configuração do Projeto

### 1. **Clonando o Repositório**

```bash
git clone https://github.com/AthosSchrapett/EFMongo.git
cd EFMongo
```

### 2. **Configurar a String de Conexão**

Adicione a string de conexão do MongoDB no arquivo `appsettings.json`:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "Database": "produtosDB"
  }
}
```

> **Nota**: Substitua `mongodb://localhost:27017` conforme sua configuração do MongoDB.

### 3. **Instalação das Dependências**

Certifique-se de instalar os pacotes necessários:

```bash
dotnet add package MongoDB.EntityFrameworkCore
```

---

## ✅ Como Executar

1. Certifique-se de que o **MongoDB** esteja em execução localmente ou em um servidor acessível.
2. Execute o projeto utilizando o **.NET CLI**:
   ```bash
   dotnet run
   ```
3. Verifique os logs e testes para garantir que as consultas estão funcionando corretamente.

---

## 📊 Estrutura do Projeto

```
EFMongo/
|-- Controllers/          # Controllers da API (exemplo)
|-- Context/              # Configuração do DbContext
|-- Models/               # Classes de entidade (models)
|-- DTOs/                 # Classes Anêmicas (DTO)
|-- appsettings.json      # Configuração do MongoDB
|-- Program.cs            # Ponto de entrada
|-- README.md             # Documentação
```

---

## 🔬 Exemplo de Uso

Aqui está um exemplo básico de como definir um `DbContext` e uma entidade:

### **1. Configurar o DbContext**
```csharp
public class MongoDbContext : DbContext
{
    public DbSet<Produto>? Produtos { get; set; }

    public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Produto>().ToCollection("Produtos");
    }
}
```

### **2. Definir uma Entidade**
```csharp
using MongoDB.Bson; //Para utilização do ObjectId

namespace EFMongo.Models;

public class Produto
{
    public ObjectId Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Preco { get; private set; }

    public Produto(string nome, string descricao, decimal preco)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
    }
}
```

### **3. Definir um DTO**
```csharp
namespace EFMongo.DTOs;

public record ProdutoDTO
(
    string Id,
    string Nome,
    string Descricao,
    decimal Preco
);
```

### **4. Inserir e Consultar Dados**
```csharp

// Inserir dados
await _context.AddAsync(produto);
await _context.SaveChangesAsync();

// Consultar dados
ObjectId.TryParse(id, out ObjectId objId); //Converter o id de string para ObjectId
var produto = _context.Produtos?.FirstOrDefault(x => x.Id == objId);
```

---

## 💡 Contribuições
Contribuições são bem-vindas! Sinta-se à vontade para abrir uma **issue** ou enviar um **pull request** com melhorias e correções.

1. **Fork** o repositório
2. Crie sua branch com a funcionalidade/correção: `git checkout -b minha-feature`
3. Commit suas alterações: `git commit -m "Minha nova feature"`
4. Push: `git push origin minha-feature`
5. Abra um **Pull Request**

---

## 📊 Licença
Este projeto está licenciado sob a **MIT License**. Consulte o arquivo [LICENSE](LICENSE) para mais informações.

---

## 🌐 Contato
Caso tenha dúvidas ou precise de ajuda:
- **Athos Schrapett**
- [LinkedIn](https://www.linkedin.com/in/athos-louzeiro-schrapett)
- [GitHub](https://github.com/AthosSchrapett)

---

Feito com ❤️ por [AthosSchrapett](https://github.com/AthosSchrapett)
