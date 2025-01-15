using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000145 RID: 325
	internal sealed class BatchQueryMemberMatchConditions : Dictionary<DataMember, BatchQueryMemberMatchCondition>
	{
		// Token: 0x06000BFD RID: 3069 RVA: 0x00030719 File Offset: 0x0002E919
		internal BatchQueryMemberMatchConditions(int capacity)
			: base(capacity)
		{
		}
	}
}
