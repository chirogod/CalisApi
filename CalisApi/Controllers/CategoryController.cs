using CalisApi.Database.Interfaces;
using CalisApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            try
            {
                await _categoryRepository.AddCategoryAsync(category);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category category)
        {
            var exist = await _categoryRepository.GetCategoryById(id);
            if(exist == null)
            {
                return NotFound();
            }
            try
            {
                exist.Name = category.Name;
                exist.Description = category.Description;
                await _categoryRepository.UpdateCategoryAsync(exist);
                return Ok(exist);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _categoryRepository.GetCategoryById(id); 
            if(exist == null)
            {
                return NotFound($"Categoria no encontrada");
            }
            try
            {
                await _categoryRepository.DeleteCategory(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
