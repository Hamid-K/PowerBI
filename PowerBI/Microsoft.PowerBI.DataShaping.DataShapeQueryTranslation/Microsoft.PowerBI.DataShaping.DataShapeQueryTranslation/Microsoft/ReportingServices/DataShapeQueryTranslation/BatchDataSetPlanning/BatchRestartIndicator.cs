using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200017B RID: 379
	internal sealed class BatchRestartIndicator
	{
		// Token: 0x06000D71 RID: 3441 RVA: 0x000375B3 File Offset: 0x000357B3
		internal BatchRestartIndicator(string restartIndicatorName, IReadOnlyList<DataMember> dataMembersToRestart)
		{
			this.RestartIndicatorName = restartIndicatorName;
			this.DataMembersToRestart = dataMembersToRestart;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x000375C9 File Offset: 0x000357C9
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x000375D1 File Offset: 0x000357D1
		internal string OutputTableName { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x000375DA File Offset: 0x000357DA
		internal string RestartIndicatorName { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x000375E2 File Offset: 0x000357E2
		internal IReadOnlyList<DataMember> DataMembersToRestart { get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x000375EA File Offset: 0x000357EA
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x000375F2 File Offset: 0x000357F2
		internal BatchColumnReference RestartIndicatorColumn { get; private set; }

		// Token: 0x06000D78 RID: 3448 RVA: 0x000375FC File Offset: 0x000357FC
		internal void Bind(BatchDataSetPlan plan, OutputTableMapping outputTableMapping)
		{
			int num = outputTableMapping.IndexOf(this.OutputTableName);
			BatchDataBinding batchDataBinding = new BatchDataBinding(plan, num);
			this.RestartIndicatorColumn = new BatchColumnReference(batchDataBinding, this.RestartIndicatorName);
		}
	}
}
