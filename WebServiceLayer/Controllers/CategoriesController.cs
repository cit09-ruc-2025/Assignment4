using DataServiceLayer;
using DataServiceLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IDataService _dataService;

        public CategoriesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CategoryDTO>> GetCategories()
        {
            var categories = _dataService.GetCategories();
            if (!categories.Any()) return NoContent();

            return Ok(CategoryDTO.ConvertToDTO(categories));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryDTO> Get(int id)
        {
            var category = _dataService.GetCategory(id);
            if (category is null) return NotFound();

            return Ok(CategoryDTO.ConvertToDTO(category));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<CategoryDTO> Post([FromBody] CategoryDTO category)
        {
            var createdCategory = _dataService.CreateCategory(category.Name, category.Description);

            return Created("/" + createdCategory.Id, CategoryDTO.ConvertToDTO(createdCategory));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Put(int id, [FromBody] CategoryDTO category)
        {
            var isCategoryUpdated = _dataService.UpdateCategory(id, category.Name, category.Description);
            return isCategoryUpdated ? Ok(isCategoryUpdated) : NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Delete(int id)
        {
            var isDeleted = _dataService.DeleteCategory(id);
            return isDeleted ? Ok(isDeleted) : NotFound();
        }
    }
}
