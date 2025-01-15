using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000BE RID: 190
	public sealed class FaultedDataSourceException : ReportCatalogException
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000591F File Offset: 0x00003B1F
		public FaultedDataSourceException(ErrorCode errorCode, string errorString)
			: base(errorCode, errorString, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00005930 File Offset: 0x00003B30
		private FaultedDataSourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
