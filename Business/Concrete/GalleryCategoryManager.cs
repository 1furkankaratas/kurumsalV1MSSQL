using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

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
        public IResult Add(GalleryCategory galleryCategory)
        {
            _galleryCategoryDal.Add(galleryCategory);
            return new SuccessResult();

        }

        [CacheRemoveAspect("IGalleryCategoryService.Get")]
        public IResult Delete(GalleryCategory galleryCategory)
        {
            _galleryCategoryDal.Delete(galleryCategory);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IGalleryCategoryService.Get")]
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