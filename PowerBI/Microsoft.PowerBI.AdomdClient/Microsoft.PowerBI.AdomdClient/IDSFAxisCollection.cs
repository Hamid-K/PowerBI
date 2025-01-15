using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000050 RID: 80
	internal interface IDSFAxisCollection : ICollection, IEnumerable
	{
		// Token: 0x17000134 RID: 308
		IDSFDataSet this[int index] { get; }
	}
}
