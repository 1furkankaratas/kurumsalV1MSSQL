using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IGalleryService
    {

        IDataResult<List<GalleryImage>> GetAll();
        IDataResult<GalleryImage> GetById(int id);
        IResult Add(GalleryImage galleryImage);
        IResult Delete(GalleryImage galleryImage);
        IResult Update(GalleryImage galleryImage,string source);
        IResult UpdateIsActive(GalleryImage galleryImage);

        IDataResult<List<GalleryPageListDto>> GetGalleries();

        IDataResult<string> UploadGalleryImage(IFormFile file,string savingPath);

        IResult DeleteGalleryImage(string source);
    }
}