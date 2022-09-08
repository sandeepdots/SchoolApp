using SchoolApp.Data;
using SchoolApp.DataTable.Search;
using SchoolApp.Repo;
using SchoolApp.Service.DepartmentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Service.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IRepository<DepartmentMaster> _DepartmentMasterRepo;
        
        public DepartmentServices(IRepository<DepartmentMaster> DepartmentMasterRepo)
        {
            _DepartmentMasterRepo = DepartmentMasterRepo;
           
        }
        public List<DepartmentMaster> GetDepartment()
        {
            return _DepartmentMasterRepo.Query().Get().ToList();
        }

        public IEnumerable<DepartmentMaster> GetDepartmentPresenters()
        {
            return _DepartmentMasterRepo.Query().Get();
        }

        public bool DeleteDepartment(int id)
        {
            try
            {
                _DepartmentMasterRepo.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DepartmentMaster SaveDepartment(DepartmentMaster departmentMaster)
        {
            if (departmentMaster.DepartmentId > 0)
            {
                _DepartmentMasterRepo.Update(departmentMaster);
            }
            else
            {
                _DepartmentMasterRepo.Insert(departmentMaster);
            }
            return departmentMaster;
        }

        public DepartmentMaster FindById(int id)
        {
            return _DepartmentMasterRepo.FindById(id);
        }
        public DepartmentMaster GetDepartmentPresenter(int id)
        {
            return _DepartmentMasterRepo.FindById(id);
        }
        public DepartmentMaster SaveDepartmentPresenter(DepartmentMaster departmentMaster)
        {
            _DepartmentMasterRepo.Insert(departmentMaster);
            return departmentMaster;
        }

        public DepartmentMaster UpdateDepartmentPresenter(DepartmentMaster departmentMaster)
        {
            _DepartmentMasterRepo.Update(departmentMaster);
            return departmentMaster;
        }

        public int DeleteDepartmentPresenter(int id)
        {
            _DepartmentMasterRepo.Delete(id);
            return id;
        }
  

        public PagedListResult<DepartmentMaster> GetDepartmentList(SearchQuery<DepartmentMaster> query, out int totalItems)
        {
            return _DepartmentMasterRepo.Search(query, out totalItems);
        }


    }
}


