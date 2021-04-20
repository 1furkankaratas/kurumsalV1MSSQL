using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPageService
    {
        IDataResult<List<Page>> GetAll();
        IDataResult<Page> GetById(int pageId);
        IDataResult<List<Page>> GetByType(int type);
        IResult Add(Page page);
        IResult Delete(Page page);
        IResult UpdateWithImage(Page page,string source);
        IResult Update(Page page);
    }
}