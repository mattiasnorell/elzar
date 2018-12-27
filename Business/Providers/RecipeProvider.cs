using System;
using System.Collections.Generic;
using Feedbag.Models;
using Feedbag.DataAccess.Repositories;
using Feedbag.Business.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace Feedbag.Business.Providers{
    public class RecipeProvider : IRecipeProvider
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IRecipeMapper mapper;

        public RecipeProvider(IRecipeRepository recipeRepository, IRecipeMapper mapper)
        {
            this.recipeRepository = recipeRepository;
            this.mapper = mapper;
        }

        public void Delete(Guid id)
        {
            this.recipeRepository.Remove(id);
        }

        public async Task<RecipeDto> GetAsync(Guid id)
        {
           var dao = await this.recipeRepository.Get(id);
           return this.mapper.ToDto(dao);
        }

        public async Task<IEnumerable<RecipeDto>> GetAllAsync()
        {
            var recipes = await this.recipeRepository.GetAll();

            return recipes.Select(this.mapper.ToDto);
        }

        public void Save(RecipeDto recipe)
        {
            var model = this.mapper.FromDto(recipe);
            model.CreatedAtUtc = DateTime.UtcNow;
            model.UpdatedAtUtc = DateTime.UtcNow;
            
            this.recipeRepository.Update(model);
        }
    }
}