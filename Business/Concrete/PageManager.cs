
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

namespace Business.Concrete
{
    public class PageManager : IPageService
    {
        private readonly IPageDal _pageDal;

        public PageManager(IPageDal pageDal)
        {
            _pageDal = pageDal;
        }

        [CacheAspect]
        public IDataResult<List<Page>> GetAll()
        {
            return new SuccessDataResult<List<Page>>(_pageDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Page> GetById(int pageId)
        {
            return new SuccessDataResult<Page>(_pageDal.Get(x => x.Id == pageId));
        }

        [CacheAspect]
        public IDataResult<List<Page>> GetByType(int type)
        {
            return new SuccessDataResult<List<Page>>(_pageDal.GetAll(x => x.Type == type));
        }

        [CacheRemoveAspect("IPageService.Get")]
        //[ValidationAspect(typeof(PageValidator))]
        public IResult Add(Page page)
        {
            _pageDal.Add(page);
            return new SuccessResult(Messages.PageAdded);
        }

        [CacheRemoveAspect("IPageService.Get")]
        //[ValidationAspect(typeof(PageValidator))]
        public IResult Delete(Page page)
        {
            var result = DeletePageImage(page.Image);
            if (result.Success)
            {
                _pageDal.Delete(page);
                return new SuccessResult(Messages.PageDeleted);
            }

            return new ErrorResult();

        }

        [CacheRemoveAspect("IPageService.Get")]
        //[ValidationAspect(typeof(PageValidator))]
        public IResult UpdateWithImage(Page page, string source)
        {
            var result = DeletePageImage(source);
            if (result.Success)
            {
               _pageDal.Update(page);
                return new SuccessResult(Messages.PageUpdated);
            }

            DeletePageImage(page.Image);
            return new ErrorResult();
        }

        [CacheRemoveAspect("IPageService.Get")]
        //[ValidationAspect(typeof(PageValidator))]
        public IResult Update(Page page)
        {

            _pageDal.Update(page);
            return new SuccessResult(Messages.PageUpdated);
        }

        [CacheRemoveAspect("IPageService.Get")]
        private IResult DeletePageImage(string source)
        {
            string path = ImageSavePaths.PageSavePath + source;
            if (File.Exists(path))
            {
                File.Delete(path);
                if (!File.Exists(path))
                {
                    return new SuccessDataResult<bool>("");
                }

            }

            return new ErrorDataResult<bool>();

        }
    }
}