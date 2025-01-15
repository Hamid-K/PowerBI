using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	internal sealed class ResourceMimeTypeNotAllowedException : ReportCatalogException
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000022B1 File Offset: 0x000004B1
		public ResourceMimeTypeNotAllowedException(string message)
			: base(ErrorCode.rsWrongItemType, message, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000022A7 File Offset: 0x000004A7
		private ResourceMimeTypeNotAllowedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
