using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Lab2.Repositories;
using Laba2.DTO;

namespace Lab2.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly Repository _repository;

        public ItemsController(Repository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _repository.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _repository.GetById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Add(ItemAddDTO itemAdd)
        {
            var newItem = new Item(itemAdd.Value);
            _repository.Add(newItem);
            var itemRead = new ItemReadDTO { Value = newItem.Value, Id = newItem.Id };
            return CreatedAtAction(nameof(Add), itemRead);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, ItemAddDTO itemUpdate)
        {
            var item = _repository.GetById(id);
            if (item == null) return NotFound();

            item.Value = itemUpdate.Value;
            _repository.Update(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _repository.GetById(id);
            if (item == null) return NotFound();

            _repository.Delete(id);
            return NoContent();
        }
    }
}
