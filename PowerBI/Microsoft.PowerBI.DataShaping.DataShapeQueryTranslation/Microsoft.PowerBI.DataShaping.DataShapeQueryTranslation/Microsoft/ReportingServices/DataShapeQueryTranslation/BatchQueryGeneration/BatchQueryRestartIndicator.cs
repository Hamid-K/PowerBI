using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000147 RID: 327
	internal sealed class BatchQueryRestartIndicator
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x000310B1 File Offset: 0x0002F2B1
		internal BatchQueryRestartIndicator(ExpressionId restartIndicatorId, IReadOnlyList<DataMember> dataMembersToRestart)
		{
			this.RestartIndicatorId = restartIndicatorId;
			this.DataMembersToRestart = dataMembersToRestart;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x000310C7 File Offset: 0x0002F2C7
		public ExpressionId RestartIndicatorId { get; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x000310CF File Offset: 0x0002F2CF
		internal IReadOnlyList<DataMember> DataMembersToRestart { get; }
	}
}
