using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;//default olarak her sayfada görütülenecek veri sayısı
        public int PageIndex {get; set;} = 1;//sayfalamada başlanacak sayfanın verilmesi 
        private int _pageSize = 6;//her sayfada görüntğlecek veri sayısı aşağıdaki metotla bunud eğiştirebileceğimiz set metodu verilmiştir.
        public int PageSize
        {
            get =>  _pageSize;
            set =>  _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int? BrandId {get; set;}//sayfada görüntülemek istediğimiz ürünlerin brand ıd'nin tutulduğu değer burası
        // null da olabilir sonuçta her zaman filtreleme yapmak istemeyebiliriz.
        public int? TypeId {get; set;} //Burada da görüntülenecek ürünerlin type'A göre filtreleme değerinini verildiği değer 
        //burası da marka gibi null da olabilir.  
        public string Sort { get; set; }//Ascending veya descending sıralama parametresi yani yazdığımız OrderBy ve OrderByDescending metotlarının
        //hangi deer üzerinden belirleneceğini belirtiyoruz örneğin productName'e göre veta price'a göre.
        public string _search;//Ürünü search edebilmek için yazılan alan aşagıdaki search metoduyla da get ve set edilmiştir.
        public string Search
        {
            get => _search;
            set => _search =value.ToLower();//aranacak bilgi küçük harfe dönüştürülür ki büyük, küçük harf karmaşasıyla ürünün bulunmasında
            //problem yaşanmasın.
        }
    }
}