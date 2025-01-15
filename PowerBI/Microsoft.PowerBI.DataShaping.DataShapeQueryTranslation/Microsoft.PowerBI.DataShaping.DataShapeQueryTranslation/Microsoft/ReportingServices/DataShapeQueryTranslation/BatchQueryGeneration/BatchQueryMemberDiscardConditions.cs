using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000134 RID: 308
	internal sealed class BatchQueryMemberDiscardConditions : Dictionary<DataMember, BatchQueryMemberDiscardCondition>
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x0002DD97 File Offset: 0x0002BF97
		internal BatchQueryMemberDiscardConditions(int capacity)
			: base(capacity)
		{
		}
	}
}
