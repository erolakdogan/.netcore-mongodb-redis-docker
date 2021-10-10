using NetCoreMRD.Shared.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMRD.Shared.Business.Contract
{
    public interface IMongoRepository<T> where T : IBaseModel
    {
        IEnumerable<T> FilterBy(Expression<Func<T, bool>> filterExpression);
        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, TProjected>> projectionExpression);
        T FindById(ObjectId id);
        Task<T> FindByIdAsync(ObjectId id);
        Task InsertOneAsync(T document);
        Task ReplaceOneAsync(T document);
        Task DeleteByIdAsync(string id);
    }
}
