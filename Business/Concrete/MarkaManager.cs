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
    public class MarkaManager : IMarkaService
    {
        private readonly IUoW _uoW;
        private readonly IMarkaDal _markaDal;

        public MarkaManager(IUoW uoW, IMarkaDal markaDal)
        {
            _uoW = uoW;
            _markaDal = markaDal;
        }

        public async Task<IResult> AddAsync(Marka entity)
        {
            await _markaDal.AddAsync(entity);
            await _uoW.CommitAsync();
            return new SuccessResult("Ekleme işlemi başarılı!");
        }
        public async Task<IResult> MakeStatusPassive(int id)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var checkEntity = await _markaDal.GetByIdAsync(id);
                if (checkEntity != null)
                {
                    checkEntity.Durum = false;
                    await Task.Run(() => { _markaDal.Update(checkEntity); });
                    await _uoW.CommitAsync();
                    return new SuccessResult("İşlem başarılı!");
                }
                return new ErrorResult("İşlem başarısız!");
            }
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            var checkEntity = await _markaDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _markaDal.Delete(checkEntity); });
                await _uoW.CommitAsync();
                return new SuccessResult("Silme işlemi başarılı!");
            }
            return new ErrorResult("Silme işlemi başarısız!");
        }

        public async Task<IDataResult<List<Marka>>> GetAllAsync()
        {
            var datas = await _markaDal.GetAllAsync(x=> x.Durum == true);
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<Marka>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<Marka>>(datas.OrderBy(x => x.Ad).ToList(), "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<Marka>> GetByIdAsync(int id)
        {
            var checkEntity = await _markaDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<Marka>(checkEntity, "İşlem başarılı!");
            }
            return new ErrorDataResult<Marka>($"Id={id}'li veri mevcut değil!");
        }

        public async Task<IResult> UpdateAsync(Marka entity)
        {
            var checkEntity = await _markaDal.GetByIdAsync(entity.Id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _markaDal.Update(entity); });
                await _uoW.CommitAsync();
                return new SuccessDataResult<Marka>(checkEntity, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<Marka>("Güncelleme işlemi başarısız!");
        }

    }
}
