using Elzar.DataAccess.Entites;
using Elzar.Models;

namespace Elzar.Business.Mappers{
    public interface ICookingProcedureMapper{
        CookingProcedureDto ToDto(CookingProcedureStep recipe);
        CookingProcedureStep FromDto(CookingProcedureDto recipe);
    }
}