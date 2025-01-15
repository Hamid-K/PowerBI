using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000F9 RID: 249
	internal sealed class ExistsFilterItemBuilder<TParent> : BuilderBase<ExistsFilterItem, TParent>
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x0000EA48 File Offset: 0x0000CC48
		internal ExistsFilterItemBuilder(TParent parent, ExistsFilterItem activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0000EA54 File Offset: 0x0000CC54
		public ExistsFilterItemBuilder<TParent> WithTarget(Expression target)
		{
			ExistsFilterItem activeObject = base.ActiveObject;
			if (activeObject.Targets == null)
			{
				activeObject.Targets = new List<Expression>();
			}
			activeObject.Targets.Add(target);
			return this;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0000EA88 File Offset: 0x0000CC88
		public ExistsFilterItemBuilder<TParent> WithExists(Expression exists)
		{
			base.ActiveObject.Exists = exists;
			return this;
		}
	}
}
