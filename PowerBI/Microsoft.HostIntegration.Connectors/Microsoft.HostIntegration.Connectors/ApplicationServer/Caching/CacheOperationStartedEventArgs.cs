using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000AC RID: 172
	public sealed class CacheOperationStartedEventArgs : EventArgs
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0001420B File Offset: 0x0001240B
		private CacheOperationStartedEventArgs(object operationContext, CacheOperationType operationType)
		{
			this._cacheOperationType = operationType;
			this._operationContext = operationContext;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00014221 File Offset: 0x00012421
		public object OperationContext
		{
			get
			{
				return this._operationContext;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00014229 File Offset: 0x00012429
		public CacheOperationType OperationType
		{
			get
			{
				return this._cacheOperationType;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00014231 File Offset: 0x00012431
		internal static CacheOperationStartedEventArgs FromCompletedEventArgs(CacheOperationCompletedEventArgs eventArgs)
		{
			return new CacheOperationStartedEventArgs(eventArgs.OperationContext, eventArgs.OperationType);
		}

		// Token: 0x04000310 RID: 784
		private object _operationContext;

		// Token: 0x04000311 RID: 785
		private CacheOperationType _cacheOperationType;
	}
}
