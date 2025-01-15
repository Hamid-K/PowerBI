using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000FB RID: 251
	internal sealed class DynamicLimitsBuilder<TParent> : BuilderBase<DynamicLimits, TParent>
	{
		// Token: 0x060006DD RID: 1757 RVA: 0x0000ED5B File Offset: 0x0000CF5B
		internal DynamicLimitsBuilder(TParent parent, DynamicLimits activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0000ED65 File Offset: 0x0000CF65
		public DynamicLimitsBuilder<TParent> WithTargetIntersectionCount(int count)
		{
			base.ActiveObject.TargetIntersectionCount = count;
			return this;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0000ED79 File Offset: 0x0000CF79
		public DynamicLimitsBuilder<TParent> WithIntersectionLimit(Expression expression)
		{
			base.ActiveObject.IntersectionLimit = expression;
			return this;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0000ED88 File Offset: 0x0000CF88
		public DynamicLimitRecommendationBuilder<DynamicLimitsBuilder<TParent>> WithPrimary()
		{
			DynamicLimitRecommendation dynamicLimitRecommendation = new DynamicLimitRecommendation();
			base.ActiveObject.Primary = dynamicLimitRecommendation;
			return new DynamicLimitRecommendationBuilder<DynamicLimitsBuilder<TParent>>(this, dynamicLimitRecommendation);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
		public DynamicLimitRecommendationBuilder<DynamicLimitsBuilder<TParent>> WithSecondary()
		{
			DynamicLimitRecommendation dynamicLimitRecommendation = new DynamicLimitRecommendation();
			base.ActiveObject.Secondary = dynamicLimitRecommendation;
			return new DynamicLimitRecommendationBuilder<DynamicLimitsBuilder<TParent>>(this, dynamicLimitRecommendation);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		public DynamicLimitEvenDistributionBlockBuilder<DynamicLimitsBuilder<TParent>> WithEvenDistributionBlock()
		{
			if (base.ActiveObject.Blocks == null)
			{
				base.ActiveObject.Blocks = new List<DynamicLimitBlock>();
			}
			DynamicLimitEvenDistributionBlock dynamicLimitEvenDistributionBlock = new DynamicLimitEvenDistributionBlock();
			base.ActiveObject.Blocks.Add(dynamicLimitEvenDistributionBlock);
			return new DynamicLimitEvenDistributionBlockBuilder<DynamicLimitsBuilder<TParent>>(this, dynamicLimitEvenDistributionBlock);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0000EE20 File Offset: 0x0000D020
		public DynamicLimitPrimarySecondaryBlockBuilder<DynamicLimitsBuilder<TParent>> WithPrimarySecondaryBlock()
		{
			if (base.ActiveObject.Blocks == null)
			{
				base.ActiveObject.Blocks = new List<DynamicLimitBlock>();
			}
			DynamicLimitPrimarySecondaryBlock dynamicLimitPrimarySecondaryBlock = new DynamicLimitPrimarySecondaryBlock();
			base.ActiveObject.Blocks.Add(dynamicLimitPrimarySecondaryBlock);
			return new DynamicLimitPrimarySecondaryBlockBuilder<DynamicLimitsBuilder<TParent>>(this, dynamicLimitPrimarySecondaryBlock);
		}
	}
}
