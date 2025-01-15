using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000080 RID: 128
	[Serializable]
	internal sealed class DataExtensionNotFoundException : ReportCatalogException
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00004CEA File Offset: 0x00002EEA
		public DataExtensionNotFoundException(string extension)
			: base(ErrorCode.rsDataExtensionNotFound, ErrorStringsWrapper.rsDataExtensionNotFound(extension), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00004D04 File Offset: 0x00002F04
		private DataExtensionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
