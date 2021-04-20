using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using Encoder = System.Drawing.Imaging.Encoder;
using Image = SixLabors.ImageSharp.Image;

namespace Business.Concrete
{
    public class SettingManager : ISettingService
    {
        private readonly ISettingDal _settingDal;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SettingManager(ISettingDal settingDal, IHostingEnvironment hostingEnvironment)
        {
            _settingDal = settingDal;
            _hostingEnvironment = hostingEnvironment;
        }

        [CacheAspect]
        public IDataResult<Setting> GetAll()
        {
            var result = _settingDal.GetAll() ?? null;

            if (result != null && result.Count > 0)
            {
                var result2 = result.ToArray()[0];
                return new SuccessDataResult<Setting>(result2);
            }

            return new ErrorDataResult<Setting>();

        }

        [CacheRemoveAspect("ISettingService.Get")]
        //[ValidationAspect(typeof(SettingValidator))]
        public IResult Update(Setting setting)
        {
            _settingDal.Update(setting);
            return new SuccessResult(Messages.SettingUpdate);
        }

        [CacheRemoveAspect("ISettingService.Get")]
        public IDataResult<string> UploadLogo(IFormFile file)
        {
            

            if (file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/png")
            {
                string dir = _hostingEnvironment.WebRootPath;
                string fileName = "Logo";
                string extension = Path.GetExtension(file.FileName);
                fileName = fileName + extension;
                string path = dir + ImageSavePaths.LogoPath;
                string savePath = Path.Combine(path, fileName);


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using var image = Image.Load(file.OpenReadStream());
                image.SaveAsync(savePath);
                if (File.Exists(savePath))
                {
                    path = dir + ImageSavePaths.LogoPath2x;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    savePath = Path.Combine(path, fileName);
                    using var image2x = Image.Load(file.OpenReadStream());
                    image2x.Mutate(x => x.Resize((image2x.Width * 2), (image2x.Height * 2)));
                    image2x.SaveAsync(savePath);
                    if (File.Exists(savePath))
                    {
                        return new SuccessDataResult<string>(fileName, "");
                    }

                }
            }
            return new ErrorDataResult<string>(Messages.GeneralError);

        }



        [CacheRemoveAspect("ISettingService.Get")]
        public IDataResult<string> UploadFavicon(IFormFile file)
        {
            if (file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/png")
            {
                string dir = _hostingEnvironment.WebRootPath;
                string fileName = "Favicon";
                string extension = Path.GetExtension(file.FileName);
                fileName = fileName + extension;
                string path = dir + ImageSavePaths.LogoPath;
                string savePath = Path.Combine(path, fileName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                
                using var image = Image.Load(file.OpenReadStream());

                image.SaveAsync(savePath);
                if (File.Exists(savePath))
                {
                    return new SuccessDataResult<string>(fileName, "");
                }
            }
            return new ErrorDataResult<string>(Messages.GeneralError);

        }

        [CacheRemoveAspect("ISettingService.Get")]
        public IResult DeleteSettingImage(string source, string deletePath)
        {
            string path = deletePath + source;
            if (File.Exists(path))
            {
                File.Delete(path);
                if (!File.Exists(path))
                {
                    return new SuccessDataResult<bool>("");
                }

            }

            return new ErrorDataResult<bool>(Messages.GeneralError);

        }
    }
}