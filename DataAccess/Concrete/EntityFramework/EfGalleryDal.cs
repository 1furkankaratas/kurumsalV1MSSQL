using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfGalleryDal : EfEntityRepositoryBase<GalleryImage, KurumsalDbContext>, IGalleryDal
    {
        public List<GalleryPageListDto> GetGalleries()
        {
            using (KurumsalDbContext context = new KurumsalDbContext())
            {
                var result =
                    from g in context.GalleryImages
                    join cq in context.GalleryCategories on g.Id equals cq.GalleryImageId
                    join cI in context.CategoryImages on cq.CategoryImageId equals cI.Id
                    select new GalleryPageDto
                    {
                        Id = g.Id, Description = g.Description, Source = g.Source,
                        CategoryName = cI.Name

                    };
                var a = result.ToList();

                List<GalleryPageDto> n = new List<GalleryPageDto>();
                List<GalleryPageListDto> n2 = new List<GalleryPageListDto>();
                foreach (var b in a)
                {
                    var id = b.Id;
                    if (n.Any(x=>x.Id==id))
                    {
                        var c = n.FirstOrDefault(x => x.Id == id);
                        c.CategoryName = c.CategoryName +","+ b.CategoryName;
                    }
                    else
                    {
                        n.Add(b);
                    }
                    


                }

                foreach (var f in n)
                {
                    var d = f.CategoryName.ToLower().Replace(" ", "").Split(',');
                    var g = f.CategoryName.Split(',');
                    GalleryPageListDto dto = new GalleryPageListDto
                    {
                        Id = f.Id,
                        Description = f.Description,
                        Source = f.Source
                    };
                    List<string> er = new List<string>();
                    List<string> et = new List<string>();
                    foreach (var h in g)
                    {
                        et.Add(h);
                    }
                    foreach (var h in d)
                    {
                        er.Add(h);
                    }
                    dto.CategoryNameList = et;
                    dto.CategoryName = er;
                    n2.Add(dto);
                }



                return n2;


            }
        }
    }
}