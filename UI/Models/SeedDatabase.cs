using System.Linq;
using System.Security.Policy;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.MicrosoftIdentity;
using Entities.Concrete;
using Entities.Concrete.MicrosoftIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace UI.Models
{
    public static class SeedDatabase
    {
        
        public static void Seed(DbContext context)
        {

            if (context is KurumsalDbContext)
            {

                KurumsalDbContext _context = context as KurumsalDbContext;

                if (_context.Settings.Count() == 0)
                {
                    _context.Settings.Add(Setting);
                }

            }


            context.SaveChanges();
        }

        private static Setting Setting = new Setting
        {
            Title = "Site Başlık",
            Phone = "0 555 555 55 55",
            Email = "mail@mail.com",
            Address = "Açık Adres",
            CompanyName = "Şirket Adı",
            Description = "Açıklama",
            Maps = "Harita konum",
            Meta = "Meta,1,2",
            WorkTime = "09:00-17:00",
            WhatsAppPhone= "055555555",
            WhatsAppText = "Bizimle iletişime geçiniz"
        };

    }
}