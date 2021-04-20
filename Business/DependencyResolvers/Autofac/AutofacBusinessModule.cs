using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SliderManager>().As<ISliderService>().SingleInstance();
            builder.RegisterType<EfSliderDal>().As<ISliderDal>().SingleInstance();

            builder.RegisterType<PageManager>().As<IPageService>().SingleInstance();
            builder.RegisterType<EfPageDal>().As<IPageDal>().SingleInstance();

            builder.RegisterType<SocialManager>().As<ISocialService>().SingleInstance();
            builder.RegisterType<EfSocialDal>().As<ISocialDal>().SingleInstance();

            builder.RegisterType<SettingManager>().As<ISettingService>().SingleInstance();
            builder.RegisterType<EfSettingDal>().As<ISettingDal>().SingleInstance();

            builder.RegisterType<GalleryManager>().As<IGalleryService>().SingleInstance();
            builder.RegisterType<EfGalleryDal>().As<IGalleryDal>().SingleInstance();

            builder.RegisterType<CategoryImageManager>().As<ICategoryImageService>().SingleInstance();
            builder.RegisterType<EfCategoryImageDal>().As<ICategoryImageDal>().SingleInstance();

            builder.RegisterType<GalleryCategoryManager>().As<IGalleryCategoryService>().SingleInstance();
            builder.RegisterType<EfGalleryCategoryDal>().As<IGalleryCategoryDal>().SingleInstance();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}