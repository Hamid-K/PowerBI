using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000FC RID: 252
	internal sealed class DynamicLimitRecommendationBuilder<TParent> : BuilderBase<DynamicLimitRecommendation, TParent>
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x0000EE68 File Offset: 0x0000D068
		internal DynamicLimitRecommendationBuilder(TParent parent, DynamicLimitRecommendation activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0000EE72 File Offset: 0x0000D072
		public DynamicLimitRecommendationBuilder<TParent> WithMin(int count)
		{
			base.ActiveObject.Min = count;
			return this;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0000EE86 File Offset: 0x0000D086
		public DynamicLimitRecommendationBuilder<TParent> WithMax(int count)
		{
			base.ActiveObject.Max = count;
			return this;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0000EE9A File Offset: 0x0000D09A
		public DynamicLimitRecommendationBuilder<TParent> WithIsMandatoryConstraint(bool isMandatoryConstraint)
		{
			base.ActiveObject.IsMandatoryConstraint = isMandatoryConstraint;
			return this;
		}
	}
}
