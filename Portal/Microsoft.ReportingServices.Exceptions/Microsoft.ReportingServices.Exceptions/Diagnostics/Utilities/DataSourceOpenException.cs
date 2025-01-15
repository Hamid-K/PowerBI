using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000047 RID: 71
	[Serializable]
	internal sealed class DataSourceOpenException : ReportCatalogException
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x000043DD File Offset: 0x000025DD
		public DataSourceOpenException(string datasourceName, Exception innerException)
			: base(ErrorCode.rsErrorOpeningConnection, ErrorStringsWrapper.rsErrorOpeningConnection(datasourceName), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000043F7 File Offset: 0x000025F7
		private DataSourceOpenException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
