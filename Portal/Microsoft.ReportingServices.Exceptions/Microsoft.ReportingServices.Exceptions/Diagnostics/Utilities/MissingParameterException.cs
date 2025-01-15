using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	internal sealed class MissingParameterException : ReportCatalogException
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public MissingParameterException(string parameterName)
			: base(ErrorCode.rsMissingParameter, ErrorStringsWrapper.rsMissingParameter(parameterName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00003BF6 File Offset: 0x00001DF6
		private MissingParameterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
