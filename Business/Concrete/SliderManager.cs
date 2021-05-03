using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Business.Concrete
{
    public class SliderManager : ISliderService
    {
        private readonly ISliderDal _sliderDal;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SliderManager(ISliderDal sliderDal, IHostingEnvironment hostingEnvironment)
        {
            _sliderDal = sliderDal;
            _hostingEnvironment = hostingEnvironment;
        }

        [CacheAspect]
        public IDataResult<List<Slider>> GetAll()
        {
            return new SuccessDataResult<List<Slider>>(_sliderDal.GetAll().OrderByDescending(x => x.Id).ToList());
        }

        [CacheAspect]
        public IDataResult<Slider> GetById(int sliderId)
        {
            return new SuccessDataResult<Slider>(_sliderDal.Get(x => x.Id == sliderId));

        }

        [CacheRemoveAspect("ISliderService.Get")]
        public IResult Add(Slider slider)
        {
            _sliderDal.Add(slider);

            return new SuccessResult(Messages.SliderAdded);
        }

        [CacheRemoveAspect("ISliderService.Get")]
        public IResult Delete(Slider slider)
        {
            var result = DeleteSliderImage(slider);
            if (result.Success)
            {
                _sliderDal.Delete(slider);
                return new SuccessResult(Messages.SliderDeleted);
            }

            return new ErrorResult(Messages.GeneralError);

        }

        [CacheRemoveAspect("ISliderService.Get")]
        public IResult Update(Slider slider)
        {
            _sliderDal.Update(slider);

            return new SuccessResult(Messages.SliderUpdated);
        }

        [CacheRemoveAspect("ISliderService.Get")]
        public IResult UpdateWithImage(Slider slider, string source)
        {
            Slider sourceSlider = new Slider {Source = source};
            var result = DeleteSliderImage(sourceSlider);
            if (result.Success)
            {
                _sliderDal.Update(slider);
                return new SuccessResult(Messages.SliderUpdated);
            }

            DeleteSliderImage(slider);
            return new ErrorResult();
        }

        [CacheRemoveAspect("ISliderService.Get")]
        private IResult DeleteSliderImage(Slider slider)
        {
            string dir = _hostingEnvironment.WebRootPath;
            string path = dir + ImageSavePaths.SliderSavePath + slider.Source;
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