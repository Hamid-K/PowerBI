using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	internal sealed class InvalidDataSourceCountException : ReportCatalogException
	{
		// Token: 0x060001BE RID: 446 RVA: 0x00004446 File Offset: 0x00002646
		public InvalidDataSourceCountException(string reportPath)
			: base(ErrorCode.rsInvalidDataSourceCount, ErrorStringsWrapper.rsInvalidDataSourceCount(reportPath), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00004460 File Offset: 0x00002660
		private InvalidDataSourceCountException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
