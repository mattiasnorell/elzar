using System;
using System.Collections.Generic;
using Elzar.Models;
using Elzar.DataAccess.Repositories;
using Elzar.Business.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace Elzar.Business.Providers{
    public class RecipeProvider : IRecipeProvider
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IRecipeMapper mapper;

        public RecipeProvider(IRecipeRepository recipeRepository, IRecipeMapper mapper)
        {
            this.recipeRepository = recipeRepository;
            this.mapper = mapper;
        }

        public void Delete(int id)
        {
            this.recipeRepository.Delete(id);
        }

        public async Task<RecipeDto> Get(int id)
        {
           var dao = await this.recipeRepository.Get(id);
           return this.mapper.ToDto(dao);
        }

        public async Task<IEnumerable<RecipeDto>> GetByTags(string[] tags)
        {
           var dao = await this.recipeRepository.GetByTags(tags);
           return dao.Select(this.mapper.ToDto);
        }

        public async Task<Boolean> Exist(string url)
        {
           var result = await this.recipeRepository.GetAll();
           return result.Any(x => x.SourceUrl == url);
        } 

        public async Task<IEnumerable<RecipeDto>> GetAll()
        {
            var recipes = await this.recipeRepository.GetAll();

            return recipes.Select(this.mapper.ToDto);
        }

        public int Save(UpdateRecipeDto recipe)
        {
            var model = this.mapper.ToDao(recipe);
            model.CreatedAtUtc = DateTime.UtcNow.ToString();
            model.UpdatedAtUtc = DateTime.UtcNow.ToString();

            return this.recipeRepository.Update(model);
        }
    }
}