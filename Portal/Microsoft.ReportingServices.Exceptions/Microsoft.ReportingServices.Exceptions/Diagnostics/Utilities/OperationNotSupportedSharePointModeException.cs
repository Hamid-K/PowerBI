using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000099 RID: 153
	[Serializable]
	internal sealed class OperationNotSupportedSharePointModeException : ReportCatalogException
	{
		// Token: 0x06000272 RID: 626 RVA: 0x000050B5 File Offset: 0x000032B5
		public OperationNotSupportedSharePointModeException()
			: base(ErrorCode.rsOperationNotSupportedSharePointMode, ErrorStringsWrapper.rsOperationNotSupportedSharePointMode, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000050CE File Offset: 0x000032CE
		private OperationNotSupportedSharePointModeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
