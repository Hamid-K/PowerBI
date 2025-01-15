using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	internal sealed class CannotBuildExternalConnectionStringException : ReportCatalogException
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00004575 File Offset: 0x00002775
		public CannotBuildExternalConnectionStringException(string dataSource)
			: this(dataSource, null)
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000457F File Offset: 0x0000277F
		public CannotBuildExternalConnectionStringException(string dataSource, Exception innerException)
			: base(ErrorCode.rsCannotBuildExternalConnectionString, ErrorStringsWrapper.cannotBuildExternalConnectionString(dataSource), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00004596 File Offset: 0x00002796
		private CannotBuildExternalConnectionStringException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
