using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LightInject
{
    /// <summary>
    /// Ensures that a new instance is created for each request in addition to tracking disposable instances.
    /// </summary>
    public class HttpRequestLifetime : ILifetime
    {
        public string Key
        {
            get;
            set;
        }
        //public HttpRequestLifetime(string key)
        //{
        //    this._key = key;
        //}

        private HttpContextBase testContext;
        private readonly object _lock = new object();

        /// <summary>
        /// Return the HttpContext if running in a web application, the test 
        /// context otherwise.
        /// </summary>
        private HttpContextBase Context
        {
            get
            {
                HttpContextBase context = (HttpContext.Current != null)
                                ? new HttpContextWrapper(HttpContext.Current)
                                : testContext;
                return context;
            }
        }

        /// <summary>
        /// Returns a service instance according to the specific lifetime characteristics.
        /// </summary>
        /// <param name="createInstance">The function delegate used to create a new service instance.</param>
        /// <param name="scope">The <see cref="Scope"/> of the current service request.</param>
        /// <returns>The requested services instance.</returns>
        public object GetInstance(Func<object> createInstance, Scope scope)
        {
            object instance = Context.Items[Key];
            if (instance == null)
            {
                lock (_lock)
                {
                    instance = Context.Items[Key];
                    if (instance == null)
                    {
                        instance = createInstance();
                        Context.Items[Key] = instance;

                        var disposable = instance as IDisposable;
                        if (disposable != null)
                        {
                            TrackInstance(scope, disposable);
                        }
                    }
                }
            }


            return instance;
        }

        private static void TrackInstance(Scope scope, IDisposable disposable)
        {
            if (scope == null)
            {
                throw new InvalidOperationException("Attempt to create a disposable instance without a current scope.");
            }

            scope.TrackInstance(disposable);
        }
    }
}
