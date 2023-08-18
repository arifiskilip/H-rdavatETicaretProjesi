using Business.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class ProducutListPageModel
    {
        public IDataResult<List<Urun>> ProductResult { get; set; }
        public IDataResult<List<Kategori>> CategoryResult { get; set; }
        public IDataResult<List<Marka>> BrandResult { get; set; }
        public IDataResult<UrunDto> UrunDto { get; set; }
        public IDataResult<List<UrunDto>> UrunlerDto { get; set; }
        public List<Sepet> Sepet { get; set; }
    }
}
