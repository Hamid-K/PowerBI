using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	internal sealed class ParameterTypeMismatchException : ReportCatalogException
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00003C3D File Offset: 0x00001E3D
		public ParameterTypeMismatchException(string parameterName)
			: base(ErrorCode.rsParameterTypeMismatch, ErrorStringsWrapper.rsParameterTypeMismatch(parameterName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00003C53 File Offset: 0x00001E53
		public ParameterTypeMismatchException(string parameterName, Exception innerException)
			: base(ErrorCode.rsParameterTypeMismatch, ErrorStringsWrapper.rsParameterTypeMismatch(parameterName), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00003C69 File Offset: 0x00001E69
		private ParameterTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
