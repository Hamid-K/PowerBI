using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	internal sealed class UnSupportedHigherApiVersion : ReportCatalogException
	{
		// Token: 0x06000132 RID: 306 RVA: 0x0000805F File Offset: 0x0000625F
		public UnSupportedHigherApiVersion(string serverVersion, string clientVersion)
			: base(ErrorCode.rsApiVersionNotRecognized, ErrorStringsWrapper.rsApiVersionNotRecognized(serverVersion, clientVersion), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00007D2D File Offset: 0x00005F2D
		private UnSupportedHigherApiVersion(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
