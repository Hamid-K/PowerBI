using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	internal sealed class UnknownDataSetParameterException : ReportCatalogException
	{
		// Token: 0x0600019C RID: 412 RVA: 0x0000420B File Offset: 0x0000240B
		public UnknownDataSetParameterException(string parameterName)
			: base(ErrorCode.rsUnknownDataSetParameter, ErrorStringsWrapper.rsUnknownDataSetParameter(parameterName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00004225 File Offset: 0x00002425
		private UnknownDataSetParameterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
