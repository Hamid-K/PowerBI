using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000051 RID: 81
	internal interface IDSFDataSet : ICollection, IEnumerable
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000515 RID: 1301
		string DataSetName { get; }

		// Token: 0x1700013C RID: 316
		DataTable this[int index] { get; }

		// Token: 0x1700013D RID: 317
		DataTable this[string index] { get; }

		// Token: 0x06000518 RID: 1304
		bool Contains(string tableName);
	}
}
