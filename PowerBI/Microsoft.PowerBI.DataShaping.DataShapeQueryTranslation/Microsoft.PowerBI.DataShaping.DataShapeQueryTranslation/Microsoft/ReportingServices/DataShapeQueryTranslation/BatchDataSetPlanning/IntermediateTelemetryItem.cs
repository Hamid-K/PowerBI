using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200019A RID: 410
	internal sealed class IntermediateTelemetryItem
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0003B5C3 File Offset: 0x000397C3
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x0003B5CB File Offset: 0x000397CB
		internal string ColumnName { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0003B5D4 File Offset: 0x000397D4
		// (set) Token: 0x06000E7D RID: 3709 RVA: 0x0003B5DC File Offset: 0x000397DC
		internal string TelemetryItemName { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0003B5E5 File Offset: 0x000397E5
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x0003B5ED File Offset: 0x000397ED
		internal ExpressionNode Value { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0003B5F6 File Offset: 0x000397F6
		// (set) Token: 0x06000E81 RID: 3713 RVA: 0x0003B5FE File Offset: 0x000397FE
		internal ExpressionId ValueId { get; set; }
	}
}
