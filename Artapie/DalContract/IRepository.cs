namespace DalContract
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    
    public interface IRepository
    {
        IEnumerable<T> GetMany<T>(Expression<Func<T, bool>> condition = null);

        T Get<T>(Expression<Func<T, bool>> condition = null);
    }
}