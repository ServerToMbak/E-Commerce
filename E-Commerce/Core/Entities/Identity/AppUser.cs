using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppUser :IdentityUser//identityUser'dan string olarak id geliyor bizim oluşturmamıza
     //gerek yok. password hashletip saltlayıp tutuyor ve emailide yani sadece IdentityUsr'da birkaç özellik dahaa vardı ctr+tıkalyıp görebilirsin.
    //olmayan propertyleri ekleyeceğiz buraya 
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}