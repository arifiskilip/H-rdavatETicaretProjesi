using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Contexts;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MusteriManager : IMusteriService
    {
        private readonly IUoW _uoW;
        private readonly IMusteriDal _musteriDal;

        public MusteriManager(IUoW uoW, IMusteriDal musteriDal)
        {
            _uoW = uoW;
            _musteriDal = musteriDal;
        }

        public async Task<IResult> MakeStatusPassive(int id)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var checkEntity = await _musteriDal.GetByIdAsync(id);
                if (checkEntity != null)
                {
                    checkEntity.Durum = false;
                    await Task.Run(() => { _musteriDal.Update(checkEntity); });
                    await _uoW.CommitAsync();
                    return new SuccessResult("İşlem başarılı!");
                }
                return new ErrorResult("İşlem başarısız!");
            }
        }
        public async Task<IResult> AddAsync(Musteri entity)
        {
            await _musteriDal.AddAsync(entity);
            await _uoW.CommitAsync();
            return new SuccessResult("Ekleme işlemi başarılı!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var checkEntity = await _musteriDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _musteriDal.Delete(checkEntity); });
                await _uoW.CommitAsync();
                return new SuccessResult("Silme işlemi başarılı!");
            }
            return new ErrorResult("Silme işlemi başarısız!");
        }

        public async Task<IDataResult<List<Musteri>>> GetAllAsync()
        {
            var datas = await _musteriDal.GetAllAsync();
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<Musteri>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<Musteri>>(datas.OrderByDescending(x => x.Id).ToList(), "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<Musteri>> GetByIdAsync(int id)
        {
            var checkEntity = await _musteriDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<Musteri>(checkEntity, "İşlem başarılı!");
            }
            return new ErrorDataResult<Musteri>($"{id}'li veri mevcut değil!");
        }

        public Musteri GetByPhone(string phone)
        {
            return _musteriDal.GetAllAsync(u => u.Telefon == phone,false).Result.FirstOrDefault();
        }

        public async Task<IResult> UpdateAsync(Musteri entity)
        {
            var checkEntity = await _musteriDal.GetByIdAsync(entity.Id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _musteriDal.Update(entity); });
                await _uoW.CommitAsync();
                return new SuccessDataResult<Musteri>(checkEntity, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<Musteri>("Güncelleme işlemi başarısız!");
        }

        public async Task<IResult> MakeStatusActive(int id)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var checkEntity = await _musteriDal.GetByIdAsync(id);
                if (checkEntity != null)
                {
                    checkEntity.Durum = true;
                    await Task.Run(() => { _musteriDal.Update(checkEntity); });
                    await _uoW.CommitAsync();
                    return new SuccessResult("İşlem başarılı!");
                }
                return new ErrorResult("İşlem başarısız!");
            }
        }
    }
}
