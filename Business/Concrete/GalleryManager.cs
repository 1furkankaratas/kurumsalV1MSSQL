using System;
using System.Collections.Generic;
using System.IO;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Business.Concrete
{
    public class GalleryManager : IGalleryService
    {
        private readonly IGalleryDal _galleryDal;
        private readonly IHostingEnvironment _hostingEnvironment;

        public GalleryManager(IGalleryDal galleryDal, IHostingEnvironment hostingEnvironment)
        {
            _galleryDal = galleryDal;
            _hostingEnvironment = hostingEnvironment;
        }

        [CacheAspect]
        public IDataResult<List<GalleryImage>> GetAll()
        {
            return new SuccessDataResult<List<GalleryImage>>(_galleryDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<GalleryImage> GetById(int id)
        {
            return new SuccessDataResult<GalleryImage>(_galleryDal.Get(x => x.Id == id));

        }

        [CacheRemoveAspect("IGalleryService.Get")]
        public IResult Add(GalleryImage galleryImage)
        {
            _galleryDal.Add(galleryImage);
            return new SuccessResult(Messages.GalleryImageAdded);
        }

        [CacheRemoveAspect("IGalleryService.Get")]
        public IResult Delete(GalleryImage galleryImage)
        {
            var result = DeleteGalleryImage(galleryImage.Source);
            if (result.Success)
            {
                _galleryDal.Delete(galleryImage);
                return new SuccessResult(Messages.GalleryImageDeleted);
            }

            return new ErrorResult(Messages.GeneralError);
        }

        [CacheRemoveAspect("IGalleryService.Get")]
        public IResult Update(GalleryImage galleryImage, string source)
        {
            if (source == "")
            {
                _galleryDal.Update(galleryImage);
                return new SuccessResult();
            }

            var result = DeleteGalleryImage(source);

            if (result.Success)
            {
                _galleryDal.Update(galleryImage);
                return new SuccessResult();
            }

            DeleteGalleryImage(galleryImage.Source);
            return new ErrorResult(Messages.GeneralError);
        }

        [CacheRemoveAspect("IGalleryService.Get")]
        public IResult UpdateIsActive(GalleryImage galleryImage)
        {

            _galleryDal.Update(galleryImage);
            return new SuccessResult();

        }

        [CacheAspect]
        public IDataResult<List<GalleryPageListDto>> GetGalleries()
        {
            return new SuccessDataResult<List<GalleryPageListDto>>(_galleryDal.GetGalleries());
        }

        [CacheRemoveAspect("IGalleryService.Get")]
        public IDataResult<string> UploadGalleryImage(IFormFile file, string savingPath)
        {
            if (file.ContentType == "image/jpeg" || file.ContentType == "image/jpg")
            {
                string dir = _hostingEnvironment.WebRootPath;
                string fileName = DateTime.Now.ToString("yyyymmddfff") + new Random().Next(1000, 9999);
                string extension = Path.GetExtension(file.FileName);
                fileName = fileName + extension;
                string path = dir + savingPath;
                string savePath = Path.Combine(path, fileName);


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using var image = Image.Load(file.OpenReadStream());
                JpegEncoder jpegEncoder = new JpegEncoder();
                jpegEncoder.Quality = 50;
                image.SaveAsync(savePath, jpegEncoder);
                if (File.Exists(savePath))
                {
                    return new SuccessDataResult<string>(fileName, Messages.GalleryImageUpdated);
                }
            }
            return new ErrorDataResult<string>(Messages.GeneralError);

        }




        [CacheRemoveAspect("IGalleryService.Get")]
        public IResult DeleteGalleryImage(string source)
        {
            string dir = _hostingEnvironment.WebRootPath;
            string path = dir + ImageSavePaths.GallerySavePath + source;
            if (File.Exists(path))
            {
                File.Delete(path);
                if (!File.Exists(path))
                {
                    return new SuccessDataResult<bool>(Messages.GalleryImageDeleted);
                }

            }

            return new ErrorDataResult<bool>(Messages.GeneralError);

        }
    }
}