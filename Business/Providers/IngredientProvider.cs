using System;
using System.Collections.Generic;
using Elzar.Models;
using Elzar.DataAccess.Repositories;
using Elzar.Business.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace Elzar.Business.Providers{
    public class IngredientProvider : IIngredientProvider
    {
        private readonly IIngredientRepository ingredientRepository;
        private readonly IIngredientMapper mapper;

        public IngredientProvider(IIngredientRepository ingredientRepository, IIngredientMapper mapper)
        {
            this.ingredientRepository = ingredientRepository;
            this.mapper = mapper;
        }

        public void Delete(int id)
        {
            this.ingredientRepository.Delete(id);
        }

        public void DeleteByRecipeId(int id)
        {
            this.ingredientRepository.DeleteByRecipeId(id);
        }

        public async Task<IEnumerable<IngredientDto>> GetAllByRecipeId(int id)
        {
           var result = await this.ingredientRepository.GetAllByRecipeId(id);
           return result.Select(this.mapper.ToDto);
        }

        public void Save(UpdateIngredientDto ingredient)
        {
            var model = this.mapper.FromDto(ingredient);

            this.ingredientRepository.Update(model);
        }
    }
}