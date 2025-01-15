using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	public sealed class DataSourceNotFoundException : ReportCatalogException
	{
		// Token: 0x060001CC RID: 460 RVA: 0x00004534 File Offset: 0x00002734
		public DataSourceNotFoundException(string dataSource)
			: base(ErrorCode.rsDataSourceNotFound, ErrorStringsWrapper.rsDataSourceNotFound(dataSource), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000454B File Offset: 0x0000274B
		private DataSourceNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
