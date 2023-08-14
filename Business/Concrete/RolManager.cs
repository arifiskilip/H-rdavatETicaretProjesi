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
    internal class RolManager : IRolService
    {
        private readonly IUoW _uoW;
        private readonly IRolDal _rolDal;
        private readonly IMusteriRolDal _musteriRolDal;

        public RolManager(IUoW uoW, IRolDal rolDal, IMusteriRolDal musteriRolDal)
        {
            _uoW = uoW;
            _rolDal = rolDal;
            _musteriRolDal = musteriRolDal;
        }

        public async Task<IResult> AddAsync(Rol entity)
        {
            await _rolDal.AddAsync(entity);
            await _uoW.CommitAsync();
            return new SuccessResult("Ekleme işlemi başarılı!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var checkEntity = await _rolDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _rolDal.Delete(checkEntity); });
                await _uoW.CommitAsync();
                return new SuccessResult("Silme işlemi başarılı!");
            }
            return new ErrorResult("Silme işlemi başarısız!");
        }

        public async Task<IDataResult<List<Rol>>> GetAllAsync()
        {
            var datas = await _rolDal.GetAllAsync();
            if (datas.Count < -1)
            {
                return new ErrorDataResult<List<Rol>>("Listeleme işlemi başarısız!");
            }
            return new SuccessDataResult<List<Rol>>(datas, "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<Rol>> GetByIdAsync(int id)
        {
            var checkEntity = await _rolDal.GetByIdAsync(id);
            if (checkEntity != null)
            {
                return new SuccessDataResult<Rol>(checkEntity, "İşlem başarılı!");
            }
            return new ErrorDataResult<Rol>($"{id}'li veri mevcut değRol!");
        }

        public async Task<List<Rol>> GetUserRolesAsync(int userId)
        {
            List<Rol> roles = new List<Rol>();
           var customerRoles =await _musteriRolDal.GetAllAsync(x => x.MusteriId == userId);
            foreach (var item in customerRoles)
            {
               var rol = await _rolDal.GetByIdAsync(item.RoleId);
                roles.Add(rol);
            }
            return roles;
        }

        public async Task<IResult> UpdateAsync(Rol entity)
        {
            var checkEntity = await _rolDal.GetByIdAsync(entity.Id);
            if (checkEntity != null)
            {
                await Task.Run(() => { _rolDal.Update(entity); });
                await _uoW.CommitAsync();
                return new SuccessDataResult<Rol>(checkEntity, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<Rol>("Güncelleme işlemi başarısız!");
        }

    }
}
