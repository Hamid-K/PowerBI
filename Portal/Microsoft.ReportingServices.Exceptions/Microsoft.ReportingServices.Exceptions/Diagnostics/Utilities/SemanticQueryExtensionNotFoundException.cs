using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000081 RID: 129
	[Serializable]
	internal sealed class SemanticQueryExtensionNotFoundException : ReportCatalogException
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00004D0E File Offset: 0x00002F0E
		public SemanticQueryExtensionNotFoundException(string extension)
			: base(ErrorCode.rsSemanticQueryExtensionNotFound, ErrorStringsWrapper.rsSemanticQueryExtensionNotFound(extension), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00004D25 File Offset: 0x00002F25
		private SemanticQueryExtensionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
