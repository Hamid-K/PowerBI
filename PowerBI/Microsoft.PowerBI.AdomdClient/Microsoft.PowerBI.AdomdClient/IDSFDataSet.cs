using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000051 RID: 81
	internal interface IDSFDataSet : ICollection, IEnumerable
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000508 RID: 1288
		string DataSetName { get; }

		// Token: 0x17000136 RID: 310
		DataTable this[int index] { get; }

		// Token: 0x17000137 RID: 311
		DataTable this[string index] { get; }

		// Token: 0x0600050B RID: 1291
		bool Contains(string tableName);
	}
}
