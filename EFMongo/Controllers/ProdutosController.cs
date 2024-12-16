using EFMongo.Context;
using EFMongo.DTOs;
using EFMongo.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EFMongo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly MongoDbContext _context;

    public ProdutosController(MongoDbContext dbContext)
    {
        _context = dbContext;
    }

    [HttpGet]
    public ActionResult<List<ProdutoDTO>> GetAll()
    {
        var produtos = _context.Produtos?.ToList().Select(x => new ProdutoDTO(x.Id.ToString(), x.Nome, x.Descricao, x.Preco));
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public ActionResult<ProdutoDTO> GetById(string id)
    {
        ObjectId.TryParse(id, out ObjectId objId);
        var produto = _context.Produtos?.FirstOrDefault(x => x.Id == objId);

        var dto = new ProdutoDTO(produto.Id.ToString(), produto.Nome, produto.Descricao, produto.Preco);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDTO>> Post([FromBody] Produto produto)
    {
        await _context.AddAsync(produto);
        await _context.SaveChangesAsync();

        var dto = new ProdutoDTO(produto.Id.ToString(), produto.Nome, produto.Descricao, produto.Preco);
        return Ok(dto);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ProdutoDTO dto)
    {
        ObjectId.TryParse(dto.Id, out ObjectId objId);
        var produto = _context.Produtos?.FirstOrDefault(x => x.Id == objId);

        produto?.Update(dto.Nome, dto.Descricao, dto.Preco);
        await _context.SaveChangesAsync();

        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Produto>> Delete(string id)
    {
        ObjectId.TryParse(id, out ObjectId objId);
        var produto = _context.Produtos?.FirstOrDefault(x => x.Id == objId);

        _context.Remove(produto);
        await _context.SaveChangesAsync();

        var dto = new ProdutoDTO(produto.Id.ToString(), produto.Nome, produto.Descricao, produto.Preco);

        return Ok(dto);
    }
}
