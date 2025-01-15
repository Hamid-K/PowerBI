using System;
using System.Runtime.ExceptionServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001E55 RID: 7765
	public struct AsyncResult<T>
	{
		// Token: 0x0600BEB2 RID: 48818 RVA: 0x00269422 File Offset: 0x00267622
		public AsyncResult(T value)
		{
			this.value = value;
			this.exception = null;
		}

		// Token: 0x0600BEB3 RID: 48819 RVA: 0x00269432 File Offset: 0x00267632
		public AsyncResult(Exception exception)
		{
			this.value = default(T);
			this.exception = exception;
		}

		// Token: 0x17002EE0 RID: 12000
		// (get) Token: 0x0600BEB4 RID: 48820 RVA: 0x00269447 File Offset: 0x00267647
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17002EE1 RID: 12001
		// (get) Token: 0x0600BEB5 RID: 48821 RVA: 0x0026944F File Offset: 0x0026764F
		public T Value
		{
			get
			{
				if (this.exception != null)
				{
					ExceptionDispatchInfo.Capture(this.exception).Throw();
				}
				return this.value;
			}
		}

		// Token: 0x0400611B RID: 24859
		private T value;

		// Token: 0x0400611C RID: 24860
		private Exception exception;
	}
}
