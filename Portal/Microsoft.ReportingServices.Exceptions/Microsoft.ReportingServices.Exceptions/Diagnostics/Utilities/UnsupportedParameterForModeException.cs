using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200009B RID: 155
	[Serializable]
	internal sealed class UnsupportedParameterForModeException : ReportCatalogException
	{
		// Token: 0x06000276 RID: 630 RVA: 0x000050FB File Offset: 0x000032FB
		public UnsupportedParameterForModeException(string mode, string parameterName)
			: base(ErrorCode.rsUnsupportedParameterForMode, ErrorStringsWrapper.rsUnsupportedParameterForMode(mode, parameterName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00005116 File Offset: 0x00003316
		private UnsupportedParameterForModeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
