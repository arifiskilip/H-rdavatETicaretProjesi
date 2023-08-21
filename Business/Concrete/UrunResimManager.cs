using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UrunResimManager : IUrunResimService
    {
        private readonly IUrunResimDal _urunResimDal;
        private readonly IUoW _uoW;
        public UrunResimManager(IUoW uoW, IUrunResimDal urunResimDal)
        {
            _uoW = uoW;
            _urunResimDal = urunResimDal;
        }

        public async Task<IResult> AddAsync(UrunResim entity)
        {
            await _urunResimDal.AddAsync(entity);
            await _uoW.CommitAsync();
            return new SuccessResult("Ekleme işlemi başarılı!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var checkEntity = await _urunResimDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _urunResimDal.Delete(checkEntity); });
                await _uoW.CommitAsync();
                return new SuccessResult("Silme işlemi başarılı!");
            }
            return new ErrorResult("Silme işlemi başarısız!");
        }

        public async Task<IDataResult<List<UrunResim>>> GetAllAsync()
        {
            var datas = await _urunResimDal.GetAllAsync();
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<UrunResim>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<UrunResim>>(datas.OrderByDescending(x => x.Id).ToList(), "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<UrunResim>> GetByIdAsync(int id)
        {
            var checkEntity = await _urunResimDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<UrunResim>(checkEntity, "İşlem başarılı!");
            }
            return new ErrorDataResult<UrunResim>($"{id}'li veri mevcut değil!");
        }

        public async Task<IResult> UpdateAsync(UrunResim entity)
        {
            var checkEntity = await _urunResimDal.GetByIdAsync(entity.Id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _urunResimDal.Update(entity); });
                await _uoW.CommitAsync();
                return new SuccessDataResult<UrunResim>(checkEntity, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<UrunResim>("Güncelleme işlemi başarısız!");
        }
    }
}
