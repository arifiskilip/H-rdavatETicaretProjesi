using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class IlceManager : IIlceService
    {
        private readonly IUoW _uoW;
        private readonly IIlceDal _ilceDal;

        public IlceManager(IUoW uoW, IIlceDal ilceDal)
        {
            _uoW = uoW;
            _ilceDal = ilceDal;
        }

        public async Task<IResult> AddAsync(Ilce entity)
        {
            await _ilceDal.AddAsync(entity);
            await _uoW.CommitAsync();
            return new SuccessDataResult<Ilce>("Ekleme işlemi başarılı!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var checkEntity = await _ilceDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _ilceDal.Delete(checkEntity); });
                await _uoW.CommitAsync();
                return new SuccessResult("Silme işlemi başarılı!");
            }
            return new ErrorResult("Silme işlemi başarısız!");
        }

        public async Task<IDataResult<List<Ilce>>> GetAllAsync()
        {
            var datas = await _ilceDal.GetAllAsync();
            if (datas.Count<-1)
            {
                return new ErrorDataResult<List<Ilce>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<Ilce>>(datas, "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<Ilce>> GetByIdAsync(int id)
        {
            var checkEntity = await _ilceDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<Ilce>(checkEntity,"İşlem başarılı!");
            }
            return new ErrorDataResult<Ilce>($"{id}'li veri mevcut değil!");
        }

        public async Task<IResult> UpdateAsync(Ilce entity)
        {
            var checkEntity = await _ilceDal.GetByIdAsync(entity.Id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _ilceDal.Update(entity); });
                await _uoW.CommitAsync();
                return new SuccessDataResult<Ilce>(checkEntity, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<Ilce>("Güncelleme işlemi başarısız!");
        }
    }
}
