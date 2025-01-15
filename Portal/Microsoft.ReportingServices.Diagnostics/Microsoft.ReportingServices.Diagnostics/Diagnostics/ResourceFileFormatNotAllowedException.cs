using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal sealed class ResourceFileFormatNotAllowedException : ReportCatalogException
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000022B1 File Offset: 0x000004B1
		public ResourceFileFormatNotAllowedException(string message)
			: base(ErrorCode.rsWrongItemType, message, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000022A7 File Offset: 0x000004A7
		private ResourceFileFormatNotAllowedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
