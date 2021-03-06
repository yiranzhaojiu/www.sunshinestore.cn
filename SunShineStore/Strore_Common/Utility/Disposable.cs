﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.Utility
{
    public abstract class Disposable : IDisposable
    {
        private bool disposed;

        [DebuggerStepThrough]
        ~Disposable()
        {
            Dispose(false);
        }

        [DebuggerStepThrough]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [DebuggerStepThrough]
        protected virtual void InternalDispose()
        {
        }

        [DebuggerStepThrough]
        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                InternalDispose();
            }

            disposed = true;
        }
    }
}
