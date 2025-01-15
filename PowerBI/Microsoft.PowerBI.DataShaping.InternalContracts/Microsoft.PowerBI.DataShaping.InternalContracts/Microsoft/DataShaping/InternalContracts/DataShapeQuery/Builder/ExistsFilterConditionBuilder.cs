using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000F3 RID: 243
	internal sealed class ExistsFilterConditionBuilder<TParent> : BuilderBase<ExistsFilterCondition, TParent>
	{
		// Token: 0x060006B7 RID: 1719 RVA: 0x0000E7FF File Offset: 0x0000C9FF
		internal ExistsFilterConditionBuilder(TParent parent, ExistsFilterCondition activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000E809 File Offset: 0x0000CA09
		public ExistsFilterConditionBuilder<TParent> WithExistsItem(Expression target, Expression exists)
		{
			this.AddItem(new ExistsFilterItem
			{
				Targets = new List<Expression> { target },
				Exists = exists
			});
			return this;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000E830 File Offset: 0x0000CA30
		public ExistsFilterItemBuilder<ExistsFilterConditionBuilder<TParent>> WithExistsItem()
		{
			ExistsFilterItem existsFilterItem = new ExistsFilterItem();
			this.AddItem(existsFilterItem);
			return new ExistsFilterItemBuilder<ExistsFilterConditionBuilder<TParent>>(this, existsFilterItem);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000E854 File Offset: 0x0000CA54
		private void AddItem(ExistsFilterItem item)
		{
			ExistsFilterCondition activeObject = base.ActiveObject;
			if (activeObject.Items == null)
			{
				activeObject.Items = new List<ExistsFilterItem>();
			}
			activeObject.Items.Add(item);
		}
	}
}
