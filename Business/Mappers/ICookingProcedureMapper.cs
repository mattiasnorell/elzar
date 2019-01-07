using Feedbag.DataAccess.Entites;
using Feedbag.Models;

namespace Feedbag.Business.Mappers{
    public interface ICookingProcedureMapper{
        CookingProcedureDto ToDto(CookingProcedureStep recipe);
        CookingProcedureStep FromDto(CookingProcedureDto recipe);
    }
}