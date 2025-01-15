using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	internal sealed class DataSourceDisabledException : ReportCatalogException
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x000043BD File Offset: 0x000025BD
		public DataSourceDisabledException()
			: base(ErrorCode.rsDataSourceDisabled, ErrorStringsWrapper.rsDataSourceDisabled, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000043D3 File Offset: 0x000025D3
		private DataSourceDisabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
