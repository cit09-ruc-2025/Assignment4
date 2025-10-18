using DataServiceLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataServiceLayer.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public static CategoryDTO ConvertToDTO(Category category)
        {
            return new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public static List<CategoryDTO> ConvertToDTO(IEnumerable<Category> categories)
        {
            return categories.Select(ConvertToDTO).ToList();
        }
    }
}
