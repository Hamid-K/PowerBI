using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000F2 RID: 242
	internal sealed class CompoundFilterConditionBuilder<TParent> : FilterBaseBuilder<CompoundFilterConditionBuilder<TParent>, CompoundFilterCondition, TParent>
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x0000E7C0 File Offset: 0x0000C9C0
		internal CompoundFilterConditionBuilder(TParent parent, CompoundFilterCondition activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		public override CompoundFilterConditionBuilder<TParent> WithFilterCondition(FilterCondition condition)
		{
			CompoundFilterCondition activeObject = base.ActiveObject;
			List<FilterCondition> list = activeObject.Conditions;
			if (list == null)
			{
				list = new List<FilterCondition>();
				activeObject.Conditions = list;
			}
			list.Add(condition);
			return this;
		}
	}
}
