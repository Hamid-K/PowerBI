using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000066 RID: 102
	internal sealed class DataSetCache
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x00012553 File Offset: 0x00010753
		public DataSetCache(string dataSetId, string dataSetPath)
		{
			this.DataSetId = dataSetId;
			this.DataSetPath = dataSetPath;
		}

		// Token: 0x040001FD RID: 509
		public readonly string DataSetId;

		// Token: 0x040001FE RID: 510
		public readonly string DataSetPath;

		// Token: 0x040001FF RID: 511
		public IDictionary<string, string[]> Cache = new Dictionary<string, string[]>();
	}
}
