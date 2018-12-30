using System;
using System.Collections.Generic;
using Feedbag.Models;
using Feedbag.DataAccess.Repositories;
using Feedbag.Business.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace Feedbag.Business.Providers{
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
            this.ingredientRepository.Remove(id);
        }

        public async Task<IEnumerable<IngredientDto>> GetAllByRecipeId(int id)
        {
           var result = await this.ingredientRepository.GetAllByRecipeId(id);
           return result.Select(this.mapper.ToDto);
        }

        public void Save(IngredientDto ingredient)
        {
            var model = this.mapper.FromDto(ingredient);

            this.ingredientRepository.Update(model);
        }
    }
}