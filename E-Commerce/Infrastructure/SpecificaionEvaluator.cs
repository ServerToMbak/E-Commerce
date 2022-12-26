using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class SpecificaionEvaluator<TEntity> where TEntity:BaseEntity//burada TEntity generiğinin bir BaseEntity olacağını belirttik.
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, //IQueryable olarak tanımlandı metot çünkü buraya TEntity 
        //olan IQueryable dönüyoruz.
        ISpecification<TEntity> spec)
        {
            var query =inputQuery;

            if(spec.Criteria !=null)//Filtreleme işlemi var mı kontrol edilir.
            {
                query = query.Where(spec.Criteria);//eğer varsa burada linq where sorgusuyla olan işlem gönderilir ve sonuç query'ye atanır
            } 
            
            if(spec.OrderBy !=null)//order by işlemi var mı diye kontrole dilir.
            {
                query = query.OrderBy(spec.OrderBy);//eğer order by işlemi varsa Linq metodu olan OrderBy ile işlem parametre olarak yollanarak uygulanır. 
            } 
            
            if(spec.OrderByDescending !=null)//order by işleminin tersi olan descending var mı kontrol ediliyor.
            {
                query = query.OrderByDescending(spec.OrderByDescending);//eğer varsa Linq metodlarıdan biri olan OrderByDescending ile uygulanıyor.
            } 
            if(spec.IsPagingEnabled)//eğer gönderilecek veriyi kontrol işlemi varsa burada kontrol edilir.
            {
                query = query.Skip(spec.Skip).Take(spec.Take);//her sayfada kaç veri olacağı bilgisi verilir sayfalama işlemi için.
                //burada kullanılan Skip ve Take metotları da Lİnq'den gelir.
            }

            query = spec.Includes.Aggregate(query, (current, include)=>current.Include(include));
            return query;//Burda oluşan query metodumuz olan get query olur ve buraeda onu metoda döndürürüz.
            //Burada System.Linq den faydalanarak Specification pattern için generic repositoryki veriyi getirme ve filtreleme işlemi için
            //filtreleme işleminin uygulayacak metodumuzu yazdık. Bu metodu sürekli kullanılacağı için Static olarak oluşturduk.
            //eğer bunun nerede ve nasıl kullanıldığını daha iyi anlamak istiyorsan IgenericRepositoryde tanımlanmış ve GenereicRepositoryde 
            //implemente edilmiş metotları inceleyebilirsin.
        }
    }
}