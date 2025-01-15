using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	internal sealed class ExecuteQueriesFailureException : ReportCatalogException
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00004728 File Offset: 0x00002928
		public ExecuteQueriesFailureException(string dataSourceName, ErrorCode errorCode, Exception innerException)
			: base(errorCode, ErrorStringsWrapper.rsExecuteQueriesFailure(dataSourceName), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000473E File Offset: 0x0000293E
		private ExecuteQueriesFailureException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
