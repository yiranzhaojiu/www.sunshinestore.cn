using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;
using Strore_Common.Extensions;

namespace Strore_Common.Utility
{
    public class BuildManagerWrapper
    {
        private static readonly BuildManagerWrapper current = new BuildManagerWrapper();
        private IEnumerable<Assembly> referencedAssemblies;
        private IEnumerable<Type> publicTypes;
        private IEnumerable<Type> concreteTypes;

        public static BuildManagerWrapper Current
        {
            [DebuggerStepThrough]
            get
            {
                return current;
            }
        }

        public virtual IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (referencedAssemblies != null)
                    return referencedAssemblies;

                if (HttpContext.Current == null)
                {
                    List<Assembly> allAssemblies = new List<Assembly>();
                    allAssemblies.Add(Assembly.GetEntryAssembly());
                    string path = AppDomain.CurrentDomain.BaseDirectory;

                    foreach (string dll in Directory.GetFiles(path, "*.dll"))
                    {
                        try
                        {
                            allAssemblies.Add(Assembly.LoadFrom(dll));
                        }
                        catch
                        {

                        }
                    }

                    var list = allAssemblies.Where(assembly => assembly != null && !assembly.GlobalAssemblyCache).ToList();
                    var _list = list.Where((x, y) => list.FindIndex(a => a.FullName==x.FullName) == y);

                    return referencedAssemblies ?? (referencedAssemblies = .Distinct(new LambdaComparer<Assembly>((x, y) => x.FullName == y.FullName)).ToList());

                }
                else
                { 
                    return referencedAssemblies ?? (referencedAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(assembly => !assembly.GlobalAssemblyCache && assembly.FullName.StartsWith("SunShineStore")));
                }
            }
        }

        public IEnumerable<Type> PublicTypes
        {
            get {
                return publicTypes ?? (publicTypes = Assemblies.PublicTypes().ToList());
            }
        }

        public IEnumerable<Type> ConcreteTypes
        {
            get
            {
                return concreteTypes ?? (concreteTypes = Assemblies.ConcreteTypes().ToList());
            }
        }


    }
}
