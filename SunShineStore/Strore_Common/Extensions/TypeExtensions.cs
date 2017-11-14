﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.Extensions
{
    public static class TypeExtensions
    {
        [DebuggerStepThrough]
        public static IEnumerable<Type> PublicTypes(this Assembly instance)
        {
            IEnumerable<Type> types = null;

            if (instance != null)
            {
                try
                {
                    types = instance.GetTypes().Where(type => (type != null) && type.IsPublic && type.IsVisible).ToList();
                }
                catch (ReflectionTypeLoadException e)
                {
                    types = e.Types;
                }
            }

            return types ?? Enumerable.Empty<Type>();
        }

        [DebuggerStepThrough]
        public static IEnumerable<Type> PublicTypes(this  IEnumerable<Assembly> instance)
        {
            return instance == null ? Enumerable.Empty<Type>() : instance.SelectMany(assembly => assembly.PublicTypes());
        }


        [DebuggerStepThrough]
        public static IEnumerable<Type> ConcreteTypes(this Assembly instance)
        {
            return instance == null ? Enumerable.Empty<Type>() : instance.PublicTypes().Where(type => (type != null) && type.IsClass && !type.IsAbstract && !type.IsInterface && !type.IsGenericType);
        }


        [DebuggerStepThrough]
        public static IEnumerable<Type> ConcreteTypes(this IEnumerable<Assembly> instance)
        {
            return (instance == null) ?
                   Enumerable.Empty<Type>() :
                   instance.SelectMany(assembly => assembly.ConcreteTypes());
        }
    }
}
