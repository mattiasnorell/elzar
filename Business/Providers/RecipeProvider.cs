using System;
using System.Collections.Generic;
using Feedbag.Models;
using Feedbag.DataAccess.Repositories;
using Feedbag.Business.Mappers;
using System.Linq;

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

        public RecipeDto Get(Guid id)
        {
           var dao = this.recipeRepository.Get(id);
           return this.mapper.ToDto(dao);
        }

        public IEnumerable<RecipeDto> GetAll()
        {
            var recipes = this.recipeRepository.GetAll();

            return recipes.Select(this.mapper.ToDto);
        }

        public void Save(UpdateRecipeDto recipe)
        {
            throw new NotImplementedException();
        }
    }
}