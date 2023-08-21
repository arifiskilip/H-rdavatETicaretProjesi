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
    public class SiparisDurumManager : ISiparisDurumService
    {
        private readonly IUoW _uoW;
        private readonly ISiparisDurumDal _siparisDurumDal;

        public SiparisDurumManager(IUoW uoW, ISiparisDurumDal siparisDurumDal)
        {
            _uoW = uoW;
            _siparisDurumDal = siparisDurumDal;
        }

        public async Task<IResult> AddAsync(SiparisDurum entity)
        {
            await _siparisDurumDal.AddAsync(entity);
            await _uoW.CommitAsync();
            return new SuccessResult("Ekleme işlemi başarılı!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var checkEntity = await _siparisDurumDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _siparisDurumDal.Delete(checkEntity); });
                await _uoW.CommitAsync();
                return new SuccessResult("Silme işlemi başarılı!");
            }
            return new ErrorResult("Silme işlemi başarısız!");
        }

        public async Task<IDataResult<List<SiparisDurum>>> GetAllAsync()
        {
            var datas = await _siparisDurumDal.GetAllAsync();
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<SiparisDurum>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<SiparisDurum>>(datas.OrderByDescending(x => x.Id).ToList(), "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<SiparisDurum>> GetByIdAsync(int id)
        {
            var checkEntity = await _siparisDurumDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<SiparisDurum>(checkEntity, "İşlem başarılı!");
            }
            return new ErrorDataResult<SiparisDurum>($"{id}'li veri mevcut değil!");
        }

        public async Task<IResult> UpdateAsync(SiparisDurum entity)
        {
            var checkEntity = await _siparisDurumDal.GetByIdAsync(entity.Id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _siparisDurumDal.Update(entity); });
                await _uoW.CommitAsync();
                return new SuccessDataResult<SiparisDurum>(checkEntity, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<SiparisDurum>("Güncelleme işlemi başarısız!");
        }

    }
}
