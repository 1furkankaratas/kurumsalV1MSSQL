
using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace Business.Concrete
{
    public class PageManager : IPageService
    {
        private readonly IPageDal _pageDal;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PageManager(IPageDal pageDal, IHostingEnvironment hostingEnvironment)
        {
            _pageDal = pageDal;
            _hostingEnvironment = hostingEnvironment;
        }

        [CacheAspect]
        public IDataResult<List<Page>> GetAll()
        {
            return new SuccessDataResult<List<Page>>(_pageDal.GetAll().OrderByDescending(x => x.Id).ToList());
        }

        [CacheAspect]
        public IDataResult<Page> GetById(int pageId)
        {
            return new SuccessDataResult<Page>(_pageDal.Get(x => x.Id == pageId));
        }

        [CacheAspect]
        public IDataResult<List<Page>> GetByType(int type)
        {
            return new SuccessDataResult<List<Page>>(_pageDal.GetAll(x => x.Type == type && x.IsActive));
        }

        [CacheRemoveAspect("IPageService.Get")]
        public IResult Add(Page page)
        {
            _pageDal.Add(page);
            return new SuccessResult(Messages.PageAdded);
        }

        [CacheRemoveAspect("IPageService.Get")]
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
        public IResult Update(Page page)
        {

            _pageDal.Update(page);
            return new SuccessResult(Messages.PageUpdated);
        }

        [CacheRemoveAspect("IPageService.Get")]
        private IResult DeletePageImage(string source)
        {
            string dir = _hostingEnvironment.WebRootPath;
            string path = dir + ImageSavePaths.PageSavePath + source;
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