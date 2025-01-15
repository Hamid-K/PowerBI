using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000245 RID: 581
	internal enum ExpressionAggregateType
	{
		// Token: 0x0400067B RID: 1659
		Sum,
		// Token: 0x0400067C RID: 1660
		Avg,
		// Token: 0x0400067D RID: 1661
		Max,
		// Token: 0x0400067E RID: 1662
		Min,
		// Token: 0x0400067F RID: 1663
		Count,
		// Token: 0x04000680 RID: 1664
		CountDistinct,
		// Token: 0x04000681 RID: 1665
		CountRows,
		// Token: 0x04000682 RID: 1666
		StDev,
		// Token: 0x04000683 RID: 1667
		StDevP,
		// Token: 0x04000684 RID: 1668
		Var,
		// Token: 0x04000685 RID: 1669
		VarP,
		// Token: 0x04000686 RID: 1670
		First,
		// Token: 0x04000687 RID: 1671
		Last,
		// Token: 0x04000688 RID: 1672
		Previous,
		// Token: 0x04000689 RID: 1673
		RunningValue,
		// Token: 0x0400068A RID: 1674
		RowNumber,
		// Token: 0x0400068B RID: 1675
		Aggregate,
		// Token: 0x0400068C RID: 1676
		NoAggregate
	}
}
