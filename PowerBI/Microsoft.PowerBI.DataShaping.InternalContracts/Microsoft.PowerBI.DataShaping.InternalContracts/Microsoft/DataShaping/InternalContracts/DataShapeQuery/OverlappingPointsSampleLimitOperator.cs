using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000AB RID: 171
	internal sealed class OverlappingPointsSampleLimitOperator : LimitOperator
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00007690 File Offset: 0x00005890
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00007698 File Offset: 0x00005898
		public LimitPlotAxis X { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x000076A1 File Offset: 0x000058A1
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x000076A9 File Offset: 0x000058A9
		public LimitPlotAxis Y { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x000076B2 File Offset: 0x000058B2
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.OverlappingPointsSampleLimitOperator;
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000076B6 File Offset: 0x000058B6
		public override TResult Accept<TResult>(LimitVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
