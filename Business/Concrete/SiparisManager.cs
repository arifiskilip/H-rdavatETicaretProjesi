using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SiparisManager : ISiparisService
    {
        private readonly IUoW _uoW;
        private readonly ISiparisDal _siparisDal;

        public SiparisManager(IUoW uoW, ISiparisDal siparisDal)
        {
            _uoW = uoW;
            _siparisDal = siparisDal;
        }

        public async Task<IResult> AddAsync(Siparis entity)
        {
            await _siparisDal.AddAsync(entity);
            await _uoW.CommitAsync();
            return new SuccessResult("Ekleme işlemi başarılı!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var checkEntity = await _siparisDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _siparisDal.Delete(checkEntity); });
                await _uoW.CommitAsync();
                return new SuccessResult("Silme işlemi başarılı!");
            }
            return new ErrorResult("Silme işlemi başarısız!");
        }

        public async Task<IDataResult<List<Siparis>>> GetAllAsync()
        {
            var datas = await _siparisDal.GetAllAsync();
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<Siparis>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<Siparis>>(datas, "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<Siparis>> GetByIdAsync(int id)
        {
            var checkEntity = await _siparisDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<Siparis>(checkEntity, "İşlem başarılı!");
            }
            return new ErrorDataResult<Siparis>($"{id}'li veri mevcut değil!");
        }

        public async Task<IResult> UpdateAsync(Siparis entity)
        {
            var checkEntity = await _siparisDal.GetByIdAsync(entity.Id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _siparisDal.Update(entity); });
                await _uoW.CommitAsync();
                return new SuccessDataResult<Siparis>(checkEntity, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<Siparis>("Güncelleme işlemi başarısız!");
        }

    }
}
