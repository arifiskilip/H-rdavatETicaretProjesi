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
    public class IlManager : IIlService
    {
        private readonly IUoW _uoW;
        private readonly IIlDal _ilDal;

        public IlManager(IUoW uoW, IIlDal ilDal)
        {
            _uoW = uoW;
            _ilDal = ilDal;
        }

        public async Task<IResult> AddAsync(Il entity)
        {
            await _ilDal.AddAsync(entity);
            await _uoW.CommitAsync();
            return new SuccessResult("Ekleme işlemi başarılı!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var checkEntity = await _ilDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _ilDal.Delete(checkEntity); });
                await _uoW.CommitAsync();
                return new SuccessResult("Silme işlemi başarılı!");
            }
            return new ErrorResult("Silme işlemi başarısız!");
        }

        public async Task<IDataResult<List<Il>>> GetAllAsync()
        {
            var datas = await _ilDal.GetAllAsync();
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<Il>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<Il>>(datas, "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<Il>> GetByIdAsync(int id)
        {
            var checkEntity = await _ilDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<Il>(checkEntity, "İşlem başarılı!");
            }
            return new ErrorDataResult<Il>($"{id}'li veri mevcut değil!");
        }

        public async Task<IResult> UpdateAsync(Il entity)
        {
            var checkEntity = await _ilDal.GetByIdAsync(entity.Id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _ilDal.Update(entity); });
                await _uoW.CommitAsync();
                return new SuccessDataResult<Il>(checkEntity, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<Il>("Güncelleme işlemi başarısız!");
        }
  
    }
}
