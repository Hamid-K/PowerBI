using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000178 RID: 376
	internal sealed class BatchMemberMatchConditions : Dictionary<DataMember, BatchMatchCondition>
	{
		// Token: 0x06000D6F RID: 3439 RVA: 0x00037556 File Offset: 0x00035756
		internal BatchMemberMatchConditions(int capacity)
			: base(capacity)
		{
		}
	}
}
