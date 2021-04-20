using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ISliderService
    {
        IDataResult<List<Slider>> GetAll();
        IDataResult<Slider> GetById(int sliderId);
        IResult Add(Slider slider);
        IResult Delete(Slider slider);
        IResult Update(Slider slider);
    }
}