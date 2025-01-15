using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	internal sealed class SubreportFromSnapshotException : ReportCatalogException
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00004145 File Offset: 0x00002345
		public SubreportFromSnapshotException()
			: base(ErrorCode.rsSubreportFromSnapshot, ErrorStringsWrapper.rsSubreportFromSnapshot, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000415B File Offset: 0x0000235B
		private SubreportFromSnapshotException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
