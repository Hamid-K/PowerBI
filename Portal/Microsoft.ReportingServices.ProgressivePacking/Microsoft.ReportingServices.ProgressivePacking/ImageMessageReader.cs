using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000003 RID: 3
	internal abstract class ImageMessageReader<T> : IImageMessageReader, IEnumerable, IDisposable, IEnumerable<T> where T : ImageMessageElement
	{
		// Token: 0x06000001 RID: 1
		public abstract IEnumerator<T> GetEnumerator();

		// Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x06000003 RID: 3
		public abstract void InternalDispose();

		// Token: 0x06000004 RID: 4 RVA: 0x00002058 File Offset: 0x00000258
		public void Dispose()
		{
			if (!this.m_disposed)
			{
				this.InternalDispose();
				this.m_disposed = true;
			}
		}

		// Token: 0x04000001 RID: 1
		private bool m_disposed;
	}
}
