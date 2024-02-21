using Background.DTOS;
using Background.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Background.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController(TodoDb todoDb) : ControllerBase
{
    private readonly TodoDb _todoDb = todoDb;
    [HttpGet("GetAll")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var todos = await _todoDb.Todos.Find(c => true).ToListAsync();
            var dtos = todos.Select(c => (TodoDto)(c)).FirstOrDefault();
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(TodoDto updateDto)
    {
        try
        {
            var todo = (Todo)updateDto;
            var tod = await _todoDb.Todos.ReplaceOneAsync(c => c.Id == todo.Id, todo);
            return Ok("Updated");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Add(AddTodoDto addTodo)
    {
        try
        {
            await _todoDb.Todos.InsertOneAsync(addTodo);
            return Ok("Added");
        }
        catch (Exception ex)
        {
            return BadRequest(nameof(ex.Message));
        }
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchComplete(string id, [FromBody] PatchDto patchDto)
    {
        try
        {
            var todo = await _todoDb.Todos.FindOneAndUpdateAsync(
            Builders<Todo>.Filter.Eq(t => t.Id, ObjectId.Parse(id)),
            Builders<Todo>.Update.Set(t => t.Completed, patchDto.Completd),
            new FindOneAndUpdateOptions<Todo> { ReturnDocument = ReturnDocument.After });
            if (todo == null)
                return NotFound();
            var todoDto = (TodoDto)todo;
            return Ok(todoDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await _todoDb.Todos.DeleteOneAsync(c => c.Id == ObjectId.Parse(id));
            return Ok("Deleted");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}