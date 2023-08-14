using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MusteriRolManager : IMusteriRolService
    {
        private readonly IUoW _uoW;
        private readonly IMusteriRolDal _musteriMusteriRolDal;

        public MusteriRolManager(IUoW uoW, IMusteriRolDal musteriMusteriRolDal)
        {
            _uoW = uoW;
            _musteriMusteriRolDal = musteriMusteriRolDal;
        }

        public async Task<IResult> AddAsync(MusteriRol entity)
        {
            await _musteriMusteriRolDal.AddAsync(entity);
            await _uoW.CommitAsync();
            return new SuccessResult("Ekleme işlemi başarılı!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var checkEntity = await _musteriMusteriRolDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _musteriMusteriRolDal.Delete(checkEntity); });
                await _uoW.CommitAsync();
                return new SuccessResult("Silme işlemi başarılı!");
            }
            return new ErrorResult("Silme işlemi başarısız!");
        }

        public async Task<IDataResult<List<MusteriRol>>> GetAllAsync()
        {
            var datas = await _musteriMusteriRolDal.GetAllAsync();
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<MusteriRol>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<MusteriRol>>(datas, "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<MusteriRol>> GetByIdAsync(int id)
        {
            var checkEntity = await _musteriMusteriRolDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<MusteriRol>(checkEntity, "İşlem başarılı!");
            }
            return new ErrorDataResult<MusteriRol>($"{id}'li veri mevcut değMusteriRol!");
        }

        public async Task<IResult> UpdateAsync(MusteriRol entity)
        {
            var checkEntity = await _musteriMusteriRolDal.GetByIdAsync(entity.Id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _musteriMusteriRolDal.Update(entity); });
                await _uoW.CommitAsync();
                return new SuccessDataResult<MusteriRol>(checkEntity, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<MusteriRol>("Güncelleme işlemi başarısız!");
        }

    }
}
