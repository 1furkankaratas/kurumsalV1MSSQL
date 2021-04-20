using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryImageDal : EfEntityRepositoryBase<CategoryImage, KurumsalDbContext>, ICategoryImageDal
    {
        
    }
}