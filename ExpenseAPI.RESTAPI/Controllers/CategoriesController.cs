using System;
using System.Collections.Generic;
using System.Web.Http;
using ExpenseAPI.Models.Category;

namespace ExpenseAPI.RESTAPI.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IEnumerable<CategoryGet> GetCategories()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{name}")]
        public CategoryGet GetCategory(string name)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void AddCategory([FromBody]CategoryPost category)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{name}")]
        public void UpdateCategory(string name, [FromBody]CategoryPut category)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{name}")]
        public void DeleteCategory(string name)
        {
            throw new NotImplementedException();
        }
    }
}
