using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ISocialMediaService
    {
        IDataResult<List<SocialMedia>> GetAll();
        IDataResult<SocialMedia> GetById(int socialId);
        IDataResult<List<SocialMedia>> GetByType(int type);
        IResult Add(SocialMedia socialMedia);
        IResult Delete(SocialMedia socialMedia);
        IResult Update(SocialMedia socialMedia);
    }
}