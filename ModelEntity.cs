using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using yourapp.persistance;

namespace UM4RS.persistance
{
    /// <summary>
    /// Generic Class for Model Objects.
    /// This class has the CRUD basic operations
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public abstract class ModelEntity<TObject> where TObject : class, IBaseEntity 
    {
        static readonly YourContext ModelContext = new YourContext();

        #region -- CLASS METHODS -- 
        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        public static TObject Get(int id)
        {
            return ModelContext.Set<TObject>().Find(id);
        }

        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        public static async Task<TObject> GetAsync(int id)
        {
            return await ModelContext.Set<TObject>().FindAsync(id);
        }

        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <returns>An ICollection of every object in the database</returns>
        public static ICollection<TObject> GetAll()
        {
            return ModelContext.Set<TObject>().ToList();
        }

        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <returns>An ICollection of every object in the database</returns>
        public static async Task<ICollection<TObject>> GetAllAsync()
        {
            return await ModelContext.Set<TObject>().ToListAsync();
        }

        /// <summary>
        /// Returns a single object which matches the provided expression
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="match">A Linq expression filter to find a single result</param>
        /// <returns>A single object which matches the expression filter. 
        /// If more than one object is found or if zero are found, null is returned</returns>
        public static TObject Find(Expression<Func<TObject, bool>> match)
        {
            return ModelContext.Set<TObject>().SingleOrDefault(match);
        }

        /// <summary>
        /// Returns a single object which matches the provided expression
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="match">A Linq expression filter to find a single result</param>
        /// <returns>A single object which matches the expression filter. 
        /// If more than one object is found or if zero are found, null is returned</returns>
        public static async Task<TObject> FindAsync(Expression<Func<TObject, bool>> match)
        {
            return await ModelContext.Set<TObject>().SingleOrDefaultAsync(match);
        }

        /// <summary>
        /// Returns a collection of objects which match the provided expression
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="match">A linq expression filter to find one or more results</param>
        /// <returns>An ICollection of object which match the expression filter</returns>
        public static ICollection<TObject> FindAll(Expression<Func<TObject, bool>> match)
        {
            return ModelContext.Set<TObject>().Where(match).ToList();
        }

        /// <summary>
        /// Returns a collection of objects which match the provided expression
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="match">A linq expression filter to find one or more results</param>
        /// <returns>An ICollection of object which match the expression filter</returns>
        public static async Task<ICollection<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match)
        {
            return await ModelContext.Set<TObject>().Where(match).ToListAsync();
        }

        /// <summary>
        /// Saves a new or modified object into the database and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="t">The object to Save</param>
        /// <returns>The resulting object including its primary key after the Save</returns>
        public static TObject Save(TObject t) 
        {
            if (t == null) return null;
            if (t.Id > 0)
            {
                // update the object
                ModelContext.Entry(t).State = EntityState.Modified;                
            }
            else
            {
                // add the new object
                ModelContext.Set<TObject>().Add(t);
            }
            
            ModelContext.SaveChanges();
            return t;
        }

        /// <summary>
        /// Inserts a single object to the database and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="t">The object to insert</param>
        /// <returns>The resulting object including its primary key after the insert</returns>
        public static async Task<TObject> SaveAsync(TObject t)
        {
            if (t == null) return null;
            if (t.Id > 0)
            {
                // update the object
                ModelContext.Entry(t).State = EntityState.Modified;
            }
            else
            {
                // add the new object
                ModelContext.Set<TObject>().Add(t);
            }

            await ModelContext.SaveChangesAsync();
            return t;
        }

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="tList">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        public static IEnumerable<TObject> SavedAll(IEnumerable<TObject> tList)
        {
            ModelContext.Set<TObject>().AddRange(tList);
            ModelContext.SaveChanges();
            return tList;
        }

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="tList">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        public static async Task<IEnumerable<TObject>> SaveAllAsync(IEnumerable<TObject> tList)
        {
            ModelContext.Set<TObject>().AddRange(tList);
            await ModelContext.SaveChangesAsync();
            return tList;
        }

        /// <summary>
        /// Deletes a single object from the database and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="t">The object to delete</param>
        public static void Delete(TObject t)
        {
            ModelContext.Set<TObject>().Remove(t);
            ModelContext.SaveChanges();
        }

        /// <summary>
        /// Deletes a single object from the database and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="t">The object to delete</param>
        public async Task<int> DeleteAsync(TObject t)
        {
            ModelContext.Set<TObject>().Remove(t);
            return await ModelContext.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the count of the number of objects in the database
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <returns>The count of the number of objects</returns>
        public static int Count()
        {
            return ModelContext.Set<TObject>().Count();
        }

        /// <summary>
        /// Gets the count of the number of objects in the database
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <returns>The count of the number of objects</returns>
        public static async Task<int> CountAsync()
        {
            return await ModelContext.Set<TObject>().CountAsync();
        }
        #endregion

        #region -- INSTANCE METHODS --

        /// <summary>
        /// Save the current objecto to database
        /// </summary>
        public void Save()
        {
            var baseEntity = this as TObject;
            Debug.Assert(baseEntity != null);//assert that its not violated
            ModelContext.Set<TObject>().Add(baseEntity);
            ModelContext.SaveChanges();
        }

        #endregion
    }
}
