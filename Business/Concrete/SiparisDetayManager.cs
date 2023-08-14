using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SiparisDetayManager : ISiparisDetayService
    {
        private readonly IUoW _uoW;
        private readonly ISiparisDetayDal _siparisDetayDal;

        public SiparisDetayManager(IUoW uoW, ISiparisDetayDal siparisDetayDal)
        {
            _uoW = uoW;
            _siparisDetayDal = siparisDetayDal;
        }

        public async Task<IResult> AddAsync(SiparisDetay entity)
        {
            await _siparisDetayDal.AddAsync(entity);
            await _uoW.CommitAsync();
            return new SuccessResult("Ekleme işlemi başarılı!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var checkEntity = await _siparisDetayDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _siparisDetayDal.Delete(checkEntity); });
                await _uoW.CommitAsync();
                return new SuccessResult("Silme işlemi başarılı!");
            }
            return new ErrorResult("Silme işlemi başarısız!");
        }

        public async Task<IDataResult<List<SiparisDetay>>> GetAllAsync()
        {
            var datas = await _siparisDetayDal.GetAllAsync();
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<SiparisDetay>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<SiparisDetay>>(datas, "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<SiparisDetay>> GetByIdAsync(int id)
        {
            var checkEntity = await _siparisDetayDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<SiparisDetay>(checkEntity, "İşlem başarılı!");
            }
            return new ErrorDataResult<SiparisDetay>($"{id}'li veri mevcut değil!");
        }

        public async Task<IResult> UpdateAsync(SiparisDetay entity)
        {
            var checkEntity = await _siparisDetayDal.GetByIdAsync(entity.Id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _siparisDetayDal.Update(entity); });
                await _uoW.CommitAsync();
                return new SuccessDataResult<SiparisDetay>(checkEntity, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<SiparisDetay>("Güncelleme işlemi başarısız!");
        }

    }
}
