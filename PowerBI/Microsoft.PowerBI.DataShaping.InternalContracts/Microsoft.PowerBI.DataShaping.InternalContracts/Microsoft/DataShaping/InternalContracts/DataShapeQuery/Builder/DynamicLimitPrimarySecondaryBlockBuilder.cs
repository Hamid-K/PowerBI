using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000FE RID: 254
	internal sealed class DynamicLimitPrimarySecondaryBlockBuilder<TParent> : BuilderBase<DynamicLimitPrimarySecondaryBlock, TParent>
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x0000EF34 File Offset: 0x0000D134
		internal DynamicLimitPrimarySecondaryBlockBuilder(TParent parent, DynamicLimitPrimarySecondaryBlock activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0000EF40 File Offset: 0x0000D140
		internal DynamicLimitRecommendationBuilder<DynamicLimitPrimarySecondaryBlockBuilder<TParent>> WithCount()
		{
			DynamicLimitRecommendation dynamicLimitRecommendation = new DynamicLimitRecommendation();
			base.ActiveObject.Count = dynamicLimitRecommendation;
			return new DynamicLimitRecommendationBuilder<DynamicLimitPrimarySecondaryBlockBuilder<TParent>>(this, dynamicLimitRecommendation);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0000EF68 File Offset: 0x0000D168
		internal DynamicLimitBuilder<DynamicLimitPrimarySecondaryBlockBuilder<TParent>> WithPrimary()
		{
			DynamicLimit dynamicLimit = new DynamicLimit();
			base.ActiveObject.Primary = dynamicLimit;
			return new DynamicLimitBuilder<DynamicLimitPrimarySecondaryBlockBuilder<TParent>>(this, dynamicLimit);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0000EF8E File Offset: 0x0000D18E
		internal DynamicLimitBuilder<DynamicLimitPrimarySecondaryBlockBuilder<TParent>> WithPrimary(Expression limitRef)
		{
			DynamicLimitBuilder<DynamicLimitPrimarySecondaryBlockBuilder<TParent>> dynamicLimitBuilder = this.WithPrimary();
			dynamicLimitBuilder.WithLimitRef(limitRef);
			return dynamicLimitBuilder;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		internal DynamicLimitBuilder<DynamicLimitPrimarySecondaryBlockBuilder<TParent>> WithSecondary()
		{
			DynamicLimit dynamicLimit = new DynamicLimit();
			base.ActiveObject.Secondary = dynamicLimit;
			return new DynamicLimitBuilder<DynamicLimitPrimarySecondaryBlockBuilder<TParent>>(this, dynamicLimit);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0000EFC6 File Offset: 0x0000D1C6
		internal DynamicLimitBuilder<DynamicLimitPrimarySecondaryBlockBuilder<TParent>> WithSecondary(Expression limitRef)
		{
			DynamicLimitBuilder<DynamicLimitPrimarySecondaryBlockBuilder<TParent>> dynamicLimitBuilder = this.WithSecondary();
			dynamicLimitBuilder.WithLimitRef(limitRef);
			return dynamicLimitBuilder;
		}
	}
}
