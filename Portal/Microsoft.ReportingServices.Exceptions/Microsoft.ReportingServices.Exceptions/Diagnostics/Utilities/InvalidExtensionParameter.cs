using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	internal sealed class InvalidExtensionParameter : ReportCatalogException
	{
		// Token: 0x06000201 RID: 513 RVA: 0x000048A3 File Offset: 0x00002AA3
		public InvalidExtensionParameter(string reason)
			: base(ErrorCode.rsInvalidExtensionParameter, ErrorStringsWrapper.rsInvalidExtensionParameter(reason), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000048BA File Offset: 0x00002ABA
		private InvalidExtensionParameter(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
