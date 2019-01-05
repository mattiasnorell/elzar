using System;
using System.Collections.Generic;
using Feedbag.Models;
using Feedbag.DataAccess.Repositories;
using Feedbag.Business.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace Feedbag.Business.Providers{
    public class HowToProvider : IHowToProvider
    {
        private readonly IHowToRepository howToRepository;
        private readonly IHowToMapper mapper;

        public HowToProvider(IHowToRepository howToRepository, IHowToMapper mapper)
        {
            this.howToRepository = howToRepository;
            this.mapper = mapper;
        }

        public void Delete(int id)
        {
            this.howToRepository.Delete(id);
        }

        public void DeleteByRecipeId(int id)
        {
            this.howToRepository.DeleteByRecipeId(id);
        }

        public async Task<IEnumerable<HowToStepDto>> GetAllByRecipeId(int id)
        {
            var result = await this.howToRepository.GetAllByRecipeId(id);

            return result.Select(this.mapper.ToDto);
        }

        public void Save(HowToStepDto step)
        {
            var model = this.mapper.FromDto(step);

            this.howToRepository.Update(model);
        }
    }
}