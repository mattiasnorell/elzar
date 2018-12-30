using Feedbag.DataAccess.Entites;
using Feedbag.Models;

namespace Feedbag.Business.Mappers{
    public interface IHowToMapper{
        HowToStepDto ToDto(HowToStep recipe);
        HowToStep FromDto(HowToStepDto recipe);
    }
}