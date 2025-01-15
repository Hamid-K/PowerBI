using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000FD RID: 253
	internal sealed class DynamicLimitEvenDistributionBlockBuilder<TParent> : BuilderBase<DynamicLimitEvenDistributionBlock, TParent>
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x0000EEA9 File Offset: 0x0000D0A9
		internal DynamicLimitEvenDistributionBlockBuilder(TParent parent, DynamicLimitEvenDistributionBlock activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
		internal DynamicLimitRecommendationBuilder<DynamicLimitEvenDistributionBlockBuilder<TParent>> WithCount()
		{
			DynamicLimitRecommendation dynamicLimitRecommendation = new DynamicLimitRecommendation();
			base.ActiveObject.Count = dynamicLimitRecommendation;
			return new DynamicLimitRecommendationBuilder<DynamicLimitEvenDistributionBlockBuilder<TParent>>(this, dynamicLimitRecommendation);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		internal DynamicLimitBuilder<DynamicLimitEvenDistributionBlockBuilder<TParent>> WithDynamicLimit()
		{
			DynamicLimit dynamicLimit = new DynamicLimit();
			if (base.ActiveObject.Limits == null)
			{
				base.ActiveObject.Limits = new List<DynamicLimit>();
			}
			base.ActiveObject.Limits.Add(dynamicLimit);
			return new DynamicLimitBuilder<DynamicLimitEvenDistributionBlockBuilder<TParent>>(this, dynamicLimit);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0000EF24 File Offset: 0x0000D124
		internal DynamicLimitBuilder<DynamicLimitEvenDistributionBlockBuilder<TParent>> WithDynamicLimit(Expression limitRef)
		{
			DynamicLimitBuilder<DynamicLimitEvenDistributionBlockBuilder<TParent>> dynamicLimitBuilder = this.WithDynamicLimit();
			dynamicLimitBuilder.WithLimitRef(limitRef);
			return dynamicLimitBuilder;
		}
	}
}
