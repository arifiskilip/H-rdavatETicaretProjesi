using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Contexts;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UrunManager : IUrunService
    {
        private readonly IUoW _uoW;
        private readonly IUrunDal _urunDal;

        public UrunManager(IUoW uoW, IUrunDal urunDal)
        {
            _uoW = uoW;
            _urunDal = urunDal;
        }
        public async Task<IResult> MakeStatusPassive(int id)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var checkEntity = await _urunDal.GetByIdAsync(id);
                if (checkEntity != null)
                {
                    checkEntity.Durum = false;
                    await Task.Run(() => { _urunDal.Update(checkEntity); });
                    await _uoW.CommitAsync();
                    return new SuccessResult("İşlem başarılı!");
                }
                return new ErrorResult("İşlem başarısız!");
            }
        }
        public async Task<IResult> AddAsync(Urun entity)
        {
            await _urunDal.AddAsync(entity);
            await _uoW.CommitAsync();
            return new SuccessResult("Ekleme işlemi başarılı!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var checkEntity = await _urunDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _urunDal.Delete(checkEntity); });
                await _uoW.CommitAsync();
                return new SuccessResult("Silme işlemi başarılı!");
            }
            return new ErrorResult("Silme işlemi başarısız!");
        }

        public async Task<IDataResult<List<Urun>>> GetAllAsync()
        {
            var datas = await _urunDal.GetAllAsync(x=> x.Durum ==  true,false);
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<Urun>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<Urun>>(datas.OrderByDescending(x => x.Id).ToList(), "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<Urun>> GetByIdAsync(int id)
        {
            var checkEntity = await _urunDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<Urun>(checkEntity, "İşlem başarılı!");
            }
            return new ErrorDataResult<Urun>($"{id}'li veri mevcut değil!");
        }

        public IDataResult<UrunDto> GetProductDto(int id)
        {
            var checkEntity = _urunDal.GetProductDto(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<UrunDto>(checkEntity, "İşlem başarılı!");
            }
            return new ErrorDataResult<UrunDto>($"Id={id}'sine sahip ürün mevcut değil!");
        }

        public IDataResult<List<UrunDto>> GetProductListDto()
        {
            var checkEntity = _urunDal.GetProductListDto();
            if (checkEntity != null)
            {
                return new SuccessDataResult<List<UrunDto>>(checkEntity.OrderByDescending(x => x.Id).ToList(), "İşlem başarılı!");
            }
            return new ErrorDataResult<List<UrunDto>>("İşlem başarısız!");
        }

        public async Task<IDataResult<List<Urun>>> ProductsWithBrandIdGet(int brandId)
        {
            var datas = await _urunDal.GetAllAsync(x => x.MarkaId == brandId && x.Durum == true, false);
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<Urun>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<Urun>>(datas.OrderByDescending(x => x.Id).ToList(), "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<List<Urun>>> ProductsWithCategoryIdGet(int categoryId)
        {
            var datas = await _urunDal.GetAllAsync(x => x.KategoriId == categoryId && x.Durum == true, false);
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<Urun>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<Urun>>(datas.OrderByDescending(x => x.Id).ToList(), "Listeleme işlemi başarılı!");
        }

        public async Task<IResult> UpdateAsync(Urun entity)
        {
            var checkEntity = await _urunDal.GetByIdAsync(entity.Id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _urunDal.Update(entity); });
                await _uoW.CommitAsync();
                return new SuccessDataResult<Urun>(checkEntity, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<Urun>("Güncelleme işlemi başarısız!");
        }

    }
}
