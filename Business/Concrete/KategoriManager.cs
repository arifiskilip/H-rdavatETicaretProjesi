using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class KategoriManager : IKategoriService
    {
        private readonly IUoW _uoW;
        private readonly IKategoriDal _kategoriDal;

        public KategoriManager(IUoW uoW, IKategoriDal kategoriDal)
        {
            _uoW = uoW;
            _kategoriDal = kategoriDal;
        }

        public async Task<IResult> AddAsync(Kategori entity)
        {
            await _kategoriDal.AddAsync(entity);
            await _uoW.CommitAsync();
            return new SuccessResult("Ekleme işlemi başarılı!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var checkEntity = await _kategoriDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _kategoriDal.Delete(checkEntity); });
                await _uoW.CommitAsync();
                return new SuccessResult("SKategorime işlemi başarılı!");
            }
            return new ErrorResult("SKategorime işlemi başarısız!");
        }

        public async Task<IDataResult<List<Kategori>>> GetAllAsync()
        {
            var datas = await _kategoriDal.GetAllAsync(x => x.Durum == true);
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<Kategori>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<Kategori>>(datas.OrderBy(x=> x.Ad).ToList(), "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<Kategori>> GetByIdAsync(int id)
        {
            var checkEntity = await _kategoriDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<Kategori>(checkEntity, "İşlem başarılı!");
            }
            return new ErrorDataResult<Kategori>($"{id}'li veri mevcut değKategori!");
        }

        public async Task<IResult> MakeStatusPassive(int id)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var checkEntity = await _kategoriDal.GetByIdAsync(id);
                if (checkEntity != null)
                {
                    checkEntity.Durum = false;
                    await Task.Run(() => { _kategoriDal.Update(checkEntity); });
                    await _uoW.CommitAsync();
                    return new SuccessResult("İşlem başarılı!");
                }
                return new ErrorResult("İşlem başarısız!");
            }
        }

        public async Task<IResult> UpdateAsync(Kategori entity)
        {
            var checkEntity = await _kategoriDal.GetByIdAsync(entity.Id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _kategoriDal.Update(entity); });
                await _uoW.CommitAsync();
                return new SuccessDataResult<Kategori>(checkEntity, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<Kategori>("Güncelleme işlemi başarısız!");
        }

    }
}
