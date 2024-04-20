using Microsoft.AspNetCore.Mvc;

namespace Caramel.Pattern.Services.Api.Example.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController() { }

        public IEnumerable<TEntity> ReturnPaginated<TEntity>(IEnumerable<TEntity> collection, int page, int pageSize)
        {
            if (page == 0) page = 1;
            if (pageSize == 0) page = 10;

            return collection
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
