using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x02000101 RID: 257
	internal sealed class LimitWindowExpansionInstanceBuilder<TParent> : BuilderBase<LimitWindowExpansionInstance, TParent>
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x0000F076 File Offset: 0x0000D276
		internal LimitWindowExpansionInstanceBuilder(TParent parent, LimitWindowExpansionInstance activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0000F080 File Offset: 0x0000D280
		public LimitWindowExpansionInstanceBuilder<TParent> WithValues(List<Expression> pathValues)
		{
			base.ActiveObject.Values = pathValues;
			return this;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0000F08F File Offset: 0x0000D28F
		public LimitWindowExpansionInstanceBuilder<TParent> WithValues(params Expression[] pathValues)
		{
			if (base.ActiveObject.Values == null)
			{
				base.ActiveObject.Values = new List<Expression>();
			}
			base.ActiveObject.Values.AddRange(pathValues);
			return this;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		public LimitWindowExpansionInstanceBuilder<TParent> WithWindowValue(List<Expression> values, WindowKind windowKind)
		{
			if (base.ActiveObject.WindowValues == null)
			{
				base.ActiveObject.WindowValues = new List<LimitWindowExpansionValue>();
			}
			base.ActiveObject.WindowValues.Add(new LimitWindowExpansionValue
			{
				Values = values,
				WindowKind = windowKind
			});
			return this;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0000F110 File Offset: 0x0000D310
		public LimitWindowExpansionInstanceBuilder<TParent> WithWindowValue(WindowKind windowKind, params Expression[] values)
		{
			if (base.ActiveObject.WindowValues == null)
			{
				base.ActiveObject.WindowValues = new List<LimitWindowExpansionValue>();
			}
			List<Expression> list = new List<Expression>();
			list.AddRange(values);
			base.ActiveObject.WindowValues.Add(new LimitWindowExpansionValue
			{
				Values = list,
				WindowKind = windowKind
			});
			return this;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0000F16C File Offset: 0x0000D36C
		public LimitWindowExpansionInstanceBuilder<LimitWindowExpansionInstanceBuilder<TParent>> WithWindowInstance()
		{
			LimitWindowExpansionInstance limitWindowExpansionInstance = new LimitWindowExpansionInstance();
			if (base.ActiveObject.Children == null)
			{
				base.ActiveObject.Children = new List<LimitWindowExpansionInstance>();
			}
			base.ActiveObject.Children.Add(limitWindowExpansionInstance);
			return new LimitWindowExpansionInstanceBuilder<LimitWindowExpansionInstanceBuilder<TParent>>(this, limitWindowExpansionInstance);
		}
	}
}
