using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context=context;
        }

        public async Task<T> GetByIdAsync(int id)//Getirilmek istenen ENTİTY'nin id si alınıyor ve bu işlem asenlron yapılıyor.
        {
           return await _context.Set<T>().FindAsync(id);//gönderilen entity türüne context.setle ulaşılıp FindAsync meotduyla entity bulunup dödürülüyor.
           //Buradaki Set ve FindAsync metotları Microsoft.EntityFrameworkCore'dan geliyor.
        }

       

        public async Task<IReadOnlyList<T>> ListAllAsync()//buradaki ListAllSunc mtoduyla T generic tipine Entity türümüzde
        //IreadOnlyList şeklinde listee olarak verileri döndürüyoruz.
        {
           return await _context.Set<T>().ToListAsync();//burada T türünde verilerimizi Microsoft.EntityFramework'den gelen
           //ToListAsync şeklinde geri döndürüyoruz.
        }
        // Aşağıdaki 3 farklı filtreleme işlemlerini SpecificationEvaluatorda oluşturduğumuz GetQery metodunu kullanarak gerçekleştirdik
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)//Tek bir entity döneceği için Microsoft.EntityFrameworkCore'dan gelen
        //FirstOrDefault metodunu kullandık.
        {
           return  await ApplySpecification(spec).FirstOrDefaultAsync();//filtre uygulanıp veri eğer varsa döndürüldü.
        }
        //Microsoft.EntityFrameworkCore'dan gelen ToList metoduyla filtrelenen veriyi listeledik.
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();//filtre uygulanıp veri listelenip döndürüldü.
        }
        //geçirilen filtrelemede dönecek veri sayısını hesapladığımız metot bruada Microsoft.EntityFrameworkCore'dan gelen CountAsync
        //metodunu kullandık.
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T>  spec)//Buradaki ApplySpecification metoduyla SpecificationEvaluatorda
        //Linq kullanarak yazdığımız filtreleme işlemini yapan GetQuery metodumuzu çağırıp uyguluyoruz.   
        {
            return SpecificaionEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);//ApplySpecfication metoduna return edip
            //yukarıda metotlarımızda bu metod üzerinden işlemleri yapabilir hale getirdik.
        }
    }
}