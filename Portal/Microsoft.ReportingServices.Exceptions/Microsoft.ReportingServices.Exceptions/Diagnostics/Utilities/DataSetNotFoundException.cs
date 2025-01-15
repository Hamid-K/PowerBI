using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	internal sealed class DataSetNotFoundException : ReportCatalogException
	{
		// Token: 0x060001DD RID: 477 RVA: 0x00004658 File Offset: 0x00002858
		public DataSetNotFoundException(string dataSet)
			: base(ErrorCode.rsDataSetNotFound, ErrorStringsWrapper.rsDataSetNotFound(dataSet), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00004672 File Offset: 0x00002872
		private DataSetNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
