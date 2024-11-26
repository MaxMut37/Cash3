using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Lab2.Repositories;

namespace Lab2.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class HomeController : ControllerBase
    {
        private readonly Repository _repository;

        public HomeController(Repository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAllData()
        {
            return Ok(_repository.Items);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateData(int id, [FromBody] string newValue)
        {
            var item = _repository.Items.FirstOrDefault(d => d.Id == id);
            if (item == null)
                return NotFound();

            item.Value = newValue;
            _repository.SaveDataToFile();

            Console.WriteLine($"Data updated: {id} = {newValue}");
            return NoContent();
        }
    }
}
