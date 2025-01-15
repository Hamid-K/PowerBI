using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000F1 RID: 241
	internal class FilterConditionBuilder<TParent> : FilterBaseBuilder<FilterConditionBuilder<TParent>, FilterCondition, TParent>
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		internal FilterConditionBuilder(TParent parent, Action<FilterCondition> addFilterCondition)
			: base(parent, null)
		{
			this.m_addFilterCondition = addFilterCondition;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000E7B1 File Offset: 0x0000C9B1
		public override FilterConditionBuilder<TParent> WithFilterCondition(FilterCondition condition)
		{
			this.m_addFilterCondition(condition);
			return this;
		}

		// Token: 0x040002C0 RID: 704
		private Action<FilterCondition> m_addFilterCondition;
	}
}
