using System.Collections.Generic;
using Elzar.Models;
using Elzar.DataAccess.Repositories;
using Elzar.Business.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace Elzar.Business.Providers
{
    public class CookingProcedureProvider : ICookingProcedureProvider
    {
        private readonly ICookingProcedureRepository cookingProcedureRepository;
        private readonly ICookingProcedureMapper mapper;

        public CookingProcedureProvider(ICookingProcedureRepository cookingProcedureRepository, ICookingProcedureMapper mapper)
        {
            this.cookingProcedureRepository = cookingProcedureRepository;
            this.mapper = mapper;
        }

        public void Delete(int id)
        {
            this.cookingProcedureRepository.Delete(id);
        }

        public void DeleteByRecipeId(int id)
        {
            this.cookingProcedureRepository.DeleteByRecipeId(id);
        }

        public async Task<IEnumerable<CookingProcedureDto>> GetAllByRecipeId(int id)
        {
            var result = await this.cookingProcedureRepository.GetAllByRecipeId(id);

            return result.Select(this.mapper.ToDto);
        }

        public void Save(CookingProcedureDto step)
        {
            var model = this.mapper.FromDto(step);

            this.cookingProcedureRepository.Update(model);
        }
    }
}