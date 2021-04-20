using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICategoryImageService
    {
        IDataResult<List<CategoryImage>> GetAll();
        IDataResult<CategoryImage> GetById(int id);
        IResult Add(CategoryImage categoryImage);
        IResult Delete(CategoryImage categoryImage);
        IResult Update(CategoryImage categoryImage);
    }
}