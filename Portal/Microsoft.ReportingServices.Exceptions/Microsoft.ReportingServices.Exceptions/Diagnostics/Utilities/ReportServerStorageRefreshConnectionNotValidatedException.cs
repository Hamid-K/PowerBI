using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200007F RID: 127
	[Serializable]
	internal sealed class ReportServerStorageRefreshConnectionNotValidatedException : ReportCatalogException
	{
		// Token: 0x0600023B RID: 571 RVA: 0x00004CB9 File Offset: 0x00002EB9
		public ReportServerStorageRefreshConnectionNotValidatedException(long modelId, long refreshConnectionId)
			: base(ErrorCode.rsReportServerStorageRefreshConnectionNotValidated, ErrorStringsWrapper.rsReportServerStorageRefreshConnectionNotValidated(modelId.ToString(), refreshConnectionId.ToString()), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00004CE0 File Offset: 0x00002EE0
		private ReportServerStorageRefreshConnectionNotValidatedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
