using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000F5 RID: 245
	internal sealed class DefaultValueFilterConditionBuilder<TParent> : BuilderBase<DefaultValueFilterCondition, TParent>
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x0000E8D7 File Offset: 0x0000CAD7
		internal DefaultValueFilterConditionBuilder(TParent parent, DefaultValueFilterCondition activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000E8E4 File Offset: 0x0000CAE4
		public DefaultValueFilterConditionBuilder<TParent> WithTarget(Expression target)
		{
			DefaultValueFilterCondition activeObject = base.ActiveObject;
			if (activeObject.Targets == null)
			{
				activeObject.Targets = new List<Expression>();
			}
			activeObject.Targets.Add(target);
			return this;
		}
	}
}
