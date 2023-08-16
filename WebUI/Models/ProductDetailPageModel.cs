using Business.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class ProductDetailPageModel
    {
        public IDataResult<UrunDto> UrunDto { get; set; }
        public IDataResult<List<UrunDto>> UrunlerDto { get; set; }
    }
}
