using SchoolApp.Data;
using SchoolApp.DataTable.Search;
using SchoolApp.Repo;
using SchoolApp.Service.DepartmentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SchoolApp.Service.ClassMasterServices
{
   public  class ClassMasterService:IClassMasterService
    {

        private readonly IRepository<ClassMaster> _ClassMasterRepo;

        public ClassMasterService(IRepository<ClassMaster> ClassMasterRepo)
        {
            _ClassMasterRepo = ClassMasterRepo;

        }
        public List<ClassMaster> GetClassMaster()
        {
            return _ClassMasterRepo.Query().Get().ToList();
        }

        public IEnumerable<ClassMaster> GetClassMasterPresenters()
        {
            return _ClassMasterRepo.Query().Get();
        }

        public bool DeleteClassMaster(int id)
        {
            try
            {
                _ClassMasterRepo.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ClassMaster SaveClassMaster(ClassMaster classmaster)
        {
            if (classmaster.ClassId > 0)
            {
                _ClassMasterRepo.Update(classmaster);
            }
            else
            {
                _ClassMasterRepo.Insert(classmaster);
            }
            return classmaster;
        }

        public ClassMaster FindById(int id)
        {
            return _ClassMasterRepo.FindById(id);
        }
        public ClassMaster GetClassMasterPresenter(int id)
        {
            return _ClassMasterRepo.FindById(id);
        }
        public ClassMaster SaveClassMasterPresenter(ClassMaster classmaster)
        {
            _ClassMasterRepo.Insert(classmaster);
            return classmaster;
        }

        public ClassMaster UpdateClassMasterPresenter(ClassMaster classmaster)
        {
            _ClassMasterRepo.Update(classmaster);
            return classmaster;
        }

        public int DeleteClassMasterPresenter(int id)
        {
            _ClassMasterRepo.Delete(id);
            return id;
        }


        public PagedListResult<ClassMaster> GetClassMasterList(SearchQuery<ClassMaster> query, out int totalItems)
        {
            return _ClassMasterRepo.Search(query, out totalItems);
        }

        public List<ClassMaster> GetAllClassList(bool IsAllClass = false) {
            return IsAllClass ? _ClassMasterRepo.Query().Get().OrderBy(x => x.Class).ToList() : _ClassMasterRepo.Query().Filter(x => x.IsActive == true).Get().OrderBy(x => x.Class).ToList();
        }

    }
}

