using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ISettingService
    {
        IDataResult<Setting> GetAll();
        IResult Update(Setting setting);
        IDataResult<string> UploadLogo(IFormFile file);
        IDataResult<string> UploadFavicon(IFormFile file);
        IResult DeleteSettingImage(string source,string deletePath);
    }
}