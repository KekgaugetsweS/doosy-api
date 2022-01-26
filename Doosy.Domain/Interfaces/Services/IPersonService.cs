using Doosy.Domain.Entities;
using Doosy.Domain.Requests;
using Doosy.Domain.Requests.Filters;
using Doosy.Domain.Responses;
using Doosy.Framework.Domain;

namespace Doosy.Domain.Interfaces
{
    public interface IPersonService
    {
        CreationResponse Create(PersonRequest request);
        ResponseBase Update(PersonRequest request);
        ObjectResponse<Person> GetById(object id);
        PagedDataResponce<PersonListResponse> Filter(PersonFilter filter);
        ExportResponse Export(PersonFilter filter);
    }
}
