
using Microsoft.Web.Administration;
using SchoolApp.DataTable.Search;
using System.Collections.Generic;


namespace SchoolApp.Repo
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity FindById(object id);
        void InsertGraph(TEntity entity);
        void InsertCollection(ICollection<TEntity> entityCollection);
        void Update(TEntity entity);
        TEntity Update(TEntity dbEntity, TEntity entity);
        void InsertCollection(List<TEntity> entityCollection);
        void UpdateCollection(List<TEntity> entityCollection);
        void Delete(object id);
        int SaveChanges();
        void Delete(TEntity entity);
        void Insert(TEntity entity);
        void ChangeEntityState<T>(T entity, ObjectState state) where T : class;
        void ChangeEntityCollectionState<T>(ICollection<T> entityCollection, ObjectState state) where T : class;
        RepositoryQuery<TEntity> Query();
        //void SaveBulk(List<TEntity> enityList);
        void Dispose();

        PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery);
        PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery, out int totalCount);

    }
}
