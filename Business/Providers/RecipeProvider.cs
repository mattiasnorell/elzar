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

        public async Task<RecipeDto> Get(Guid id)
        {
           var dao = await this.recipeRepository.Get(id);
           return this.mapper.ToDto(dao);
        }

        public async Task<IEnumerable<RecipeDto>> GetAll()
        {
            var recipes = await this.recipeRepository.GetAll();

            return recipes.Select(this.mapper.ToDto);
        }

        public int Save(RecipeDto recipe)
        {
            var model = this.mapper.FromDto(recipe);
            model.CreatedAtUtc = DateTime.UtcNow.ToString();
            model.UpdatedAtUtc = DateTime.UtcNow.ToString();

            return this.recipeRepository.Update(model);
        }
    }
}