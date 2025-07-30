using Microsoft.Extensions.Logging;
using Rika_CategoryProvider.Infrastructure.Entities;
using Rika_CategoryProvider.Infrastructure.Repos;


public class CategoryService
{
	private readonly IBaseRepository<CategoryEntity> _categoryRepository;
	private readonly ILogger<CategoryService> _logger;

	public CategoryService(IBaseRepository<CategoryEntity> categoryRepository, ILogger<CategoryService> logger)
	{
		_categoryRepository = categoryRepository;
		_logger = logger;
	}

    public async Task<CategoryEntity> AddCategoryAsync(string categoryName)
    {
        var existingCategory = await _categoryRepository.GetByNameAsync(c => c.CategoryName == categoryName);
        if (existingCategory != null)
        {
            throw new InvalidOperationException("Category already exists.");
        }

        var newCategory = new CategoryEntity { CategoryName = categoryName };
        await _categoryRepository.AddAsync(newCategory);

        return newCategory;
    }


    public async Task<IEnumerable<CategoryEntity>> GetAllCategoriesAsync()
	{
		try
		{
			return await _categoryRepository.GetAllAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while getting all categories.");
			throw;
		}
	}

    public async Task<CategoryEntity> GetCategoryByIdAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                _logger.LogWarning($"Category with ID: {id} not found.");
            }

            return category;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while getting the category with ID: {id}");
            throw;
        }
    }


    public async Task<bool> DeleteCategoryAsync(int id)
	{
		try
		{
			var category = await _categoryRepository.GetByIdAsync(id);
			if (category == null)
			{
				return false;
			}

			await _categoryRepository.DeleteAsync(category);
			return true;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while deleting the category.");
			throw;
		}
	}

	public async Task<bool> UpdateCategoryAsync(int id, CategoryEntity updatedCategory)
	{
		try
		{
			var existingCategory = await _categoryRepository.GetByIdAsync(id);
			if (existingCategory == null)
			{
				return false;
			}

			existingCategory.CategoryName = updatedCategory.CategoryName;
			await _categoryRepository.UpdateAsync(existingCategory);
			return true;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while updating the category with ID: {Id}", id);
			throw;
		}
	}
}
