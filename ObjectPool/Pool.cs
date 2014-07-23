using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.ObjectPool
{
    /// <summary>
    /// Reference: Pro .NET Performance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pool<T>
    {
        private ConcurrentBag<T> pool = new ConcurrentBag<T>();
        private Func<T> objectFactory;
        public Pool(Func<T> factory)
        {
            objectFactory = factory;
        }
        public T GetInstance()
        {
            T result;
            if (!pool.TryTake(out result))
            {
                result = objectFactory();
            }
            return result;
        }
        public void ReturnToPool(T instance)
        {
            pool.Add(instance);
        }
    }

    public class PoolableObjectBase<T> where T : class, IDisposable, new()
    {
        private static Pool<T> pool = new Pool<T>(()=>new T());
        public void Dispose()
        {
            pool.ReturnToPool(this as T);
        }
        ~PoolableObjectBase()
        {
            GC.ReRegisterForFinalize(this);
            pool.ReturnToPool(this as T);
        }
    }

    public class MyPoolableObjectExample : PoolableObjectBase<MyPoolableObjectExample>, IDisposable
    {
        //...
    }
}
