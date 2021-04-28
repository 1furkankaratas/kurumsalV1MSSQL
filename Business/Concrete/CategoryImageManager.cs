using System.Collections.Generic;
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
    public class CategoryImageManager:ICategoryImageService
    {
        private readonly ICategoryImageDal _categoryImageDal;

        public CategoryImageManager(ICategoryImageDal categoryImageDal)
        {
            _categoryImageDal = categoryImageDal;
        }

        [CacheAspect]
        public IDataResult<List<CategoryImage>> GetAll()
        {
            return new SuccessDataResult<List<CategoryImage>>(_categoryImageDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<CategoryImage> GetById(int id)
        {
            return new SuccessDataResult<CategoryImage>(_categoryImageDal.Get(x=>x.Id==id));
        }

        [CacheRemoveAspect("ICategoryImageService.Get")]
        public IResult Add(CategoryImage categoryImage)
        {
            _categoryImageDal.Add(categoryImage);
            return new SuccessResult(Messages.CategoryImageAdded);
        }

        [CacheRemoveAspect("ICategoryImageService.Get")]
        public IResult Delete(CategoryImage categoryImage)
        {
            _categoryImageDal.Delete(categoryImage);
            return new SuccessResult(Messages.CategoryImageDeleted);
        }

        [CacheRemoveAspect("ICategoryImageService.Get")]
        public IResult Update(CategoryImage categoryImage)
        {
            _categoryImageDal.Update(categoryImage);
            return new SuccessResult(Messages.CategoryImageUpdated);
        }
    }
}