using SchoolApp.Data;
using SchoolApp.DataTable.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Service.SubjectService
{
  public  interface ISubjectService
    {

        List<SubjectMaster> GetSubjectMaster();
        SubjectMaster SaveSubjectMaster(SubjectMaster subjectmaster);
        bool DeleteSubjectMaster(int id);
        SubjectMaster FindById(int id);
        SubjectMaster GetSubjectMasterPresenter(int id);
        PagedListResult<SubjectMaster> GetSubjectMasterList(SearchQuery<SubjectMaster> query, out int totalItems);

        SubjectMaster SaveSubjectMasterPresenter(SubjectMaster subjectmaster);
        SubjectMaster UpdateSubjectMasterPresenter(SubjectMaster subjectmaster);

        int DeleteSubjectMasterPresenter(int id);
        List<SubjectMaster> GetAllSubjectList(bool IsAllSubject = false);
    }




}

