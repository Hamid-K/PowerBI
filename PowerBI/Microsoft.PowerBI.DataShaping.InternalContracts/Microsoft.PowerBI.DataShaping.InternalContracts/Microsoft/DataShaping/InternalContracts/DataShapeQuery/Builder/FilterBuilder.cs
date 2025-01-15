using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000F0 RID: 240
	internal class FilterBuilder<TParent> : FilterBaseBuilder<FilterBuilder<TParent>, Filter, TParent>
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x0000E774 File Offset: 0x0000C974
		internal FilterBuilder(TParent parent, Filter activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000E77E File Offset: 0x0000C97E
		public override FilterBuilder<TParent> WithFilterCondition(FilterCondition condition)
		{
			Filter activeObject = base.ActiveObject;
			Contract.RetailAssert(activeObject.Condition == null, "Filter has existing condition.");
			activeObject.Condition = condition;
			return this;
		}
	}
}
