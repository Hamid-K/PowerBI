using System;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x0200000A RID: 10
	public sealed class DataShapeEngineErrorInfo
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002632 File Offset: 0x00000832
		public DataShapeEngineErrorInfo(string errorCode, ErrorSource errorSource)
		{
			this.ErrorCode = errorCode;
			this.ErrorSource = errorSource;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002648 File Offset: 0x00000848
		public string ErrorCode { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002650 File Offset: 0x00000850
		public ErrorSource ErrorSource { get; }
	}
}
