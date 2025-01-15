using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200000A RID: 10
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public struct AsyncResult<[global::System.Runtime.CompilerServices.Nullable(2)] T>
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000278C File Offset: 0x0000098C
		public AsyncResult(T value)
		{
			this.value = value;
			this.exception = null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000279C File Offset: 0x0000099C
		public AsyncResult(Exception exception)
		{
			this.value = default(T);
			this.exception = exception;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000027B1 File Offset: 0x000009B1
		public T Value
		{
			get
			{
				if (this.exception != null)
				{
					throw this.exception;
				}
				return this.value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000027C8 File Offset: 0x000009C8
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04000013 RID: 19
		private readonly T value;

		// Token: 0x04000014 RID: 20
		private readonly Exception exception;
	}
}
