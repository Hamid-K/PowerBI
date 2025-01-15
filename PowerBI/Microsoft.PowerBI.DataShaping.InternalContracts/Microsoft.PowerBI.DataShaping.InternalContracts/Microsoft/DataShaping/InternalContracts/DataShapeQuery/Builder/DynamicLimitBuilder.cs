using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000FF RID: 255
	internal sealed class DynamicLimitBuilder<TParent> : BuilderBase<DynamicLimit, TParent>
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x0000EFD6 File Offset: 0x0000D1D6
		internal DynamicLimitBuilder(TParent parent, DynamicLimit activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		internal DynamicLimitBuilder<TParent> WithLimitRef(Expression limitRef)
		{
			base.ActiveObject.LimitRef = limitRef;
			return this;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000EFEF File Offset: 0x0000D1EF
		internal DynamicLimitRecommendationBuilder<DynamicLimitBuilder<TParent>> WithCount()
		{
			base.ActiveObject.Count = new DynamicLimitRecommendation();
			return new DynamicLimitRecommendationBuilder<DynamicLimitBuilder<TParent>>(this, base.ActiveObject.Count);
		}
	}
}
