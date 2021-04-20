using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IGalleryCategoryService
    {
        IDataResult<List<GalleryCategory>> GetAll();
        IDataResult<GalleryCategory> GetById(int id);
        IResult Add(GalleryCategory galleryCategory);
        IResult Delete(GalleryCategory galleryCategory);
        IResult Update(GalleryCategory galleryCategory);

        IDataResult<List<GalleryCategory>> GetGalleryId(int id);
        IDataResult<List<GalleryCategory>> GetCategoryId(int id);
        IDataResult<GalleryCategory> GetGalleyIdAndCategoryId(int galleryId,int categoryId);
    }
}