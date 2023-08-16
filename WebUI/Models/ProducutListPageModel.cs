using Business.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class ProducutListPageModel
    {
        public IDataResult<List<Urun>> ProductResult { get; set; }
        public IDataResult<List<Kategori>> CategoryResult { get; set; }
        public IDataResult<List<Marka>> BrandResult { get; set; }
        public List<Sepet> Sepet { get; set; }
    }
}
