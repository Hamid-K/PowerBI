using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	internal sealed class ReadOnlyDataSetParameterException : ReportCatalogException
	{
		// Token: 0x06000198 RID: 408 RVA: 0x000041C6 File Offset: 0x000023C6
		public ReadOnlyDataSetParameterException(string parameterName)
			: base(ErrorCode.rsReadOnlyDataSetParameter, ErrorStringsWrapper.rsReadOnlyDataSetParameter(parameterName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000041E0 File Offset: 0x000023E0
		private ReadOnlyDataSetParameterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
