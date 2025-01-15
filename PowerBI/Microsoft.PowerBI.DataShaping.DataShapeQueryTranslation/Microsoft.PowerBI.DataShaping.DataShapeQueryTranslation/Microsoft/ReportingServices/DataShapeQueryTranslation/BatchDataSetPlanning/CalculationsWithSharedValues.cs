using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000181 RID: 385
	internal sealed class CalculationsWithSharedValues
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x00037905 File Offset: 0x00035B05
		internal CalculationsWithSharedValues()
		{
			this.CalculationsList = new List<IList<string>>();
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00037918 File Offset: 0x00035B18
		internal IList<IList<string>> CalculationsList { get; }
	}
}
