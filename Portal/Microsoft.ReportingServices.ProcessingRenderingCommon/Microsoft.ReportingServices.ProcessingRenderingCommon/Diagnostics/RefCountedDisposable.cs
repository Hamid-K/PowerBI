using System;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000084 RID: 132
	internal abstract class RefCountedDisposable : IDisposable
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x0000BEAA File Offset: 0x0000A0AA
		public void Dispose()
		{
			this.m_selfReference.Dispose();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000BEB7 File Offset: 0x0000A0B7
		protected RefCountedDisposable()
		{
			this.m_selfReference = new RefCountedDisposable.RefCountedDisposableReference(this);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000BECC File Offset: 0x0000A0CC
		public IDisposable AddReference()
		{
			IDisposable disposable = new RefCountedDisposable.RefCountedDisposableReference(this);
			return this.Get(disposable);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000BEE7 File Offset: 0x0000A0E7
		private int RemoveReference()
		{
			return Interlocked.Decrement(ref this.m_refCount);
		}

		// Token: 0x060003B5 RID: 949
		protected abstract void DiposeInternal();

		// Token: 0x060003B6 RID: 950
		protected abstract IDisposable Get(IDisposable referenceDisposer);

		// Token: 0x040001F9 RID: 505
		private int m_refCount;

		// Token: 0x040001FA RID: 506
		private IDisposable m_selfReference;

		// Token: 0x020000F1 RID: 241
		private class RefCountedDisposableReference : IDisposable
		{
			// Token: 0x060007CC RID: 1996 RVA: 0x00014866 File Offset: 0x00012A66
			public RefCountedDisposableReference(RefCountedDisposable disposable)
			{
				if (disposable == null)
				{
					throw new ArgumentNullException();
				}
				this.m_obj = disposable;
				Interlocked.Increment(ref this.m_obj.m_refCount);
			}

			// Token: 0x060007CD RID: 1997 RVA: 0x00014890 File Offset: 0x00012A90
			public void Dispose()
			{
				if (this.m_disposed)
				{
					return;
				}
				try
				{
					if (this.m_obj.RemoveReference() <= 0)
					{
						this.m_obj.DiposeInternal();
					}
				}
				finally
				{
					this.m_disposed = true;
				}
			}

			// Token: 0x040004BC RID: 1212
			private bool m_disposed;

			// Token: 0x040004BD RID: 1213
			private RefCountedDisposable m_obj;
		}
	}
}
