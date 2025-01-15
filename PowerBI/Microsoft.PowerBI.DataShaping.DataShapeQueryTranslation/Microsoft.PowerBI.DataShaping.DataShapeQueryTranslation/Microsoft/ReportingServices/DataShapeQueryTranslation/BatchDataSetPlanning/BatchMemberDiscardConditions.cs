using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000174 RID: 372
	internal sealed class BatchMemberDiscardConditions : Dictionary<DataMember, BatchDiscardCondition>
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x00037399 File Offset: 0x00035599
		internal BatchMemberDiscardConditions(int capacity)
			: base(capacity)
		{
		}
	}
}
