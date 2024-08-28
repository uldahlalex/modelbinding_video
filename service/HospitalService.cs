using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using videoprep;

namespace service;

public interface IGenericService
{
    IQueryable<T> GetAll<T>(
        Expression<Func<T, bool>>? filter = null,
        string? orderBy = null,
        string includeProperties = "") where T : class;

    T Get<T>(int id) where T : class;
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
}

public class GenericService(HospitalContext ctx) : IGenericService
{
    private IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> query, string? orderBy) where T : class
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return (IOrderedQueryable<T>)query;

        string[] orderParts = orderBy.Trim().Split(' ');
        string propertyName = orderParts[0];
        bool descending = orderParts.Length > 1 && orderParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

        var property = typeof(T).GetProperty(propertyName,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property == null)
            throw new ArgumentException($"'{propertyName}' is not a valid property of {typeof(T).Name}");

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);

        string methodName = descending ? "OrderByDescending" : "OrderBy";
        var resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), property.PropertyType },
            query.Expression, Expression.Quote(orderByExp));

        return (IOrderedQueryable<T>)query.Provider.CreateQuery<T>(resultExp);
    }

    public IQueryable<T> GetAll<T>(
        Expression<Func<T, bool>>? filter = null,
        string? orderBy = null,
        string includeProperties = "") where T : class
    {
        IQueryable<T> query = ctx.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (!string.IsNullOrEmpty(orderBy))
        {
            query = ApplyOrder(query, orderBy);
        }

        return query;
    }

    public T Get<T>(int id) where T : class
    {
        return ctx.Set<T>().Find(id);
    }

    public void Add<T>(T entity) where T : class
    {
        ctx.Set<T>().Add(entity);
        ctx.SaveChanges();
    }

    public void Update<T>(T entity) where T : class
    {
        ctx.Set<T>().Update(entity);
        ctx.SaveChanges();
    }

    public void Delete<T>(T entity) where T : class
    {
        ctx.Set<T>().Remove(entity);
        ctx.SaveChanges();
    }
}