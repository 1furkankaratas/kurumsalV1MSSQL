using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ISocialService
    {
        IDataResult<List<SocialMedia>> GetAll();
        IDataResult<SocialMedia> GetById(int Id);
        IDataResult<List<SocialMedia>> GetByType(string type);
        IResult Add(SocialMedia socialMedia);
        IResult Delete(SocialMedia socialMedia);
        IResult Update(SocialMedia socialMedia);
    }
}