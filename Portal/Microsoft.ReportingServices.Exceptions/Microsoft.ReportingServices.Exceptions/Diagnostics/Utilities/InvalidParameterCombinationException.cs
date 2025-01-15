using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	internal sealed class InvalidParameterCombinationException : ReportCatalogException
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00003C73 File Offset: 0x00001E73
		public InvalidParameterCombinationException()
			: base(ErrorCode.rsInvalidParameterCombination, ErrorStringsWrapper.rsInvalidParameterCombination, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00003C88 File Offset: 0x00001E88
		private InvalidParameterCombinationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
