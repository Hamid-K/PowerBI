using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000F4 RID: 244
	internal sealed class AnyValueFilterConditionBuilder<TParent> : BuilderBase<AnyValueFilterCondition, TParent>
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x0000E887 File Offset: 0x0000CA87
		internal AnyValueFilterConditionBuilder(TParent parent, AnyValueFilterCondition activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000E894 File Offset: 0x0000CA94
		public AnyValueFilterConditionBuilder<TParent> WithTarget(Expression target)
		{
			AnyValueFilterCondition activeObject = base.ActiveObject;
			if (activeObject.Targets == null)
			{
				activeObject.Targets = new List<Expression>();
			}
			activeObject.Targets.Add(target);
			return this;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		public AnyValueFilterConditionBuilder<TParent> WithDefaultValueOverridesAncestors()
		{
			base.ActiveObject.DefaultValueOverridesAncestors = true;
			return this;
		}
	}
}
