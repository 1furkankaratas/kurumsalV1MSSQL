using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class GalleryCategoryManager : IGalleryCategoryService
    {
        private readonly IGalleryCategoryDal _galleryCategoryDal;

        public GalleryCategoryManager(IGalleryCategoryDal galleryCategoryDal)
        {
            _galleryCategoryDal = galleryCategoryDal;
        }

        [CacheAspect]
        public IDataResult<List<GalleryCategory>> GetAll()
        {
            return new SuccessDataResult<List<GalleryCategory>>(_galleryCategoryDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<GalleryCategory> GetById(int id)
        {
            return new SuccessDataResult<GalleryCategory>(_galleryCategoryDal.Get(x => x.Id == id));
        }

        [CacheRemoveAspect("IGalleryCategoryService.Get")]
        //[ValidationAspect(typeof(GalleryCategoryValidator))]
        public IResult Add(GalleryCategory galleryCategory)
        {
            _galleryCategoryDal.Add(galleryCategory);
            return new SuccessResult();

        }
        
        [CacheRemoveAspect("IGalleryCategoryService.Get")]
        //[ValidationAspect(typeof(GalleryCategoryValidator))]
        public IResult Delete(GalleryCategory galleryCategory)
        {
            _galleryCategoryDal.Delete(galleryCategory);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IGalleryCategoryService.Get")]
        //[ValidationAspect(typeof(GalleryCategoryValidator))]
        public IResult Update(GalleryCategory galleryCategory)
        {
            _galleryCategoryDal.Update(galleryCategory);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<GalleryCategory>> GetGalleryId(int id)
        {
            return new SuccessDataResult<List<GalleryCategory>>(_galleryCategoryDal.GetAll(x => x.GalleryImageId == id));
        }

        [CacheAspect]
        public IDataResult<List<GalleryCategory>> GetCategoryId(int id)
        {
            return new SuccessDataResult<List<GalleryCategory>>(_galleryCategoryDal.GetAll(x => x.CategoryImageId == id));

        }

        [CacheAspect]
        public IDataResult<GalleryCategory> GetGalleyIdAndCategoryId(int galleryId, int categoryId)
        {
            return new SuccessDataResult<GalleryCategory>(_galleryCategoryDal.Get(x => x.GalleryImageId == galleryId && x.GalleryImageId == categoryId));

        }
    }
}