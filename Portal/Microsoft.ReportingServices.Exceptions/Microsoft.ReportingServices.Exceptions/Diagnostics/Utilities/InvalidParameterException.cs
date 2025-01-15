using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	internal sealed class InvalidParameterException : ReportCatalogException
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00003C00 File Offset: 0x00001E00
		public InvalidParameterException(string parameterName)
			: base(ErrorCode.rsInvalidParameter, ErrorStringsWrapper.rsInvalidParameter(parameterName), null, null, Array.Empty<object>())
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00003C1D File Offset: 0x00001E1D
		public InvalidParameterException(string parameterName, Exception innnerException)
			: base(ErrorCode.rsInvalidParameter, ErrorStringsWrapper.rsInvalidParameter(parameterName), innnerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00003C33 File Offset: 0x00001E33
		private InvalidParameterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000012 RID: 18
		public readonly string ParameterName;
	}
}
