using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000B3 RID: 179
	internal abstract class LimitVisitor<TResult>
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x000078A2 File Offset: 0x00005AA2
		protected virtual TResult Visit(Limit limit)
		{
			return this.Visit(limit.Operator);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000078B0 File Offset: 0x00005AB0
		protected virtual TResult Visit(LimitOperator limitOperator)
		{
			return limitOperator.Accept<TResult>(this);
		}

		// Token: 0x0600042D RID: 1069
		internal abstract TResult Visit(TopLimitOperator limitOperator);

		// Token: 0x0600042E RID: 1070
		internal abstract TResult Visit(SampleLimitOperator limitOperator);

		// Token: 0x0600042F RID: 1071
		internal abstract TResult Visit(FirstLimitOperator limitOperator);

		// Token: 0x06000430 RID: 1072
		internal abstract TResult Visit(LastLimitOperator limitOperator);

		// Token: 0x06000431 RID: 1073
		internal abstract TResult Visit(BottomLimitOperator limitOperator);

		// Token: 0x06000432 RID: 1074
		internal abstract TResult Visit(BinnedLineSampleLimitOperator limitOperator);

		// Token: 0x06000433 RID: 1075
		internal abstract TResult Visit(OverlappingPointsSampleLimitOperator limitOperator);

		// Token: 0x06000434 RID: 1076
		internal abstract TResult Visit(TopNPerLevelLimitOperator limitOperator);

		// Token: 0x06000435 RID: 1077
		internal abstract TResult Visit(WindowLimitOperator limitOperator);
	}
}
