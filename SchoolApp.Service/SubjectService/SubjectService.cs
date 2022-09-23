using SchoolApp.Data;
using SchoolApp.DataTable.Search;
using SchoolApp.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Service.SubjectService
{

    public class SubjectService : ISubjectService
    {

        private readonly IRepository<SubjectMaster> _SubjectMasterRepo;

        public SubjectService(IRepository<SubjectMaster> SubjectMasterRepo)
        {
            _SubjectMasterRepo = SubjectMasterRepo;

        }
        public List<SubjectMaster> GetSubjectMaster()
        {
            return _SubjectMasterRepo.Query().Get().ToList();
        }

        public IEnumerable<SubjectMaster> GetSubjectMasterPresenter()
        {
            return _SubjectMasterRepo.Query().Get();
        }

        public bool DeleteSubjectMaster(int id)
        {
            try
            {
                _SubjectMasterRepo.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SubjectMaster SaveSubjectMaster(SubjectMaster subjectmaster)
        {
            if (subjectmaster.SubjectId > 0)
            {
                _SubjectMasterRepo.Update(subjectmaster);
            }
            else
            {
                _SubjectMasterRepo.Insert(subjectmaster);
            }
            return subjectmaster;
        }

        public SubjectMaster FindById(int id)
        {
            return _SubjectMasterRepo.FindById(id);
        }
        public SubjectMaster GetSubjectMasterPresenter(int id)
        {
            return _SubjectMasterRepo.FindById(id);
        }
        public SubjectMaster SaveSubjectMasterPresenter(SubjectMaster subjectmaster)
        {
            _SubjectMasterRepo.Insert(subjectmaster);
            return subjectmaster;
        }

        public SubjectMaster UpdateSubjectMasterPresenter(SubjectMaster subjectmaster)
        {
            _SubjectMasterRepo.Update(subjectmaster);
            return subjectmaster;
        }

        public int DeleteSubjectMasterPresenter(int id)
        {
            _SubjectMasterRepo.Delete(id);
            return id;
        }


        public PagedListResult<SubjectMaster> GetSubjectMasterList(SearchQuery<SubjectMaster> query, out int totalItems)
        {
            return _SubjectMasterRepo.Search(query, out totalItems);
        }

        public List<SubjectMaster> GetAllSubjectList(bool IsAllSubject = false) {
            return IsAllSubject ? _SubjectMasterRepo.Query().Get().OrderBy(x=>x.Subject).ToList(): _SubjectMasterRepo.Query().Filter(x=>x.IsActive==true).Get().OrderBy(x => x.Subject).ToList();
        }

    }
}
    
