using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000A7 RID: 167
	public class CacheOperationCompletedEventArgs : EventArgs
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x00013FFF File Offset: 0x000121FF
		internal CacheOperationCompletedEventArgs(CacheOperationType operationType)
		{
			this._cacheOperationType = operationType;
			this._operationContext = CacheOperationContext.GetUnique();
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00014019 File Offset: 0x00012219
		public bool HasSucceeded
		{
			get
			{
				return this._exception == null;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00014024 File Offset: 0x00012224
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0001402C File Offset: 0x0001222C
		public Exception ExceptionObject
		{
			get
			{
				return this._exception;
			}
			internal set
			{
				this._exception = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00014035 File Offset: 0x00012235
		public CacheOperationContext OperationContext
		{
			get
			{
				return this._operationContext;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001403D File Offset: 0x0001223D
		public CacheOperationType OperationType
		{
			get
			{
				return this._cacheOperationType;
			}
		}

		// Token: 0x04000308 RID: 776
		private readonly CacheOperationContext _operationContext;

		// Token: 0x04000309 RID: 777
		private readonly CacheOperationType _cacheOperationType;

		// Token: 0x0400030A RID: 778
		private Exception _exception;
	}
}
