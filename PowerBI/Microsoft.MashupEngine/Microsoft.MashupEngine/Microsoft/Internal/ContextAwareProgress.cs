using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Internal
{
	// Token: 0x02000189 RID: 393
	internal class ContextAwareProgress<T, U> : IProgress where T : struct, IContext<U> where U : struct, IDisposable
	{
		// Token: 0x06000792 RID: 1938 RVA: 0x0000DC04 File Offset: 0x0000BE04
		public ContextAwareProgress(T context, IProgress progress)
		{
			this.context = context;
			this.progress = progress;
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		public long Rows
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				long rows;
				try
				{
					rows = this.progress.Rows;
				}
				finally
				{
					u.Dispose();
				}
				return rows;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		public long ExceptionRows
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				long exceptionRows;
				try
				{
					exceptionRows = this.progress.ExceptionRows;
				}
				finally
				{
					u.Dispose();
				}
				return exceptionRows;
			}
		}

		// Token: 0x04000493 RID: 1171
		protected readonly T context;

		// Token: 0x04000494 RID: 1172
		protected readonly IProgress progress;
	}
}
