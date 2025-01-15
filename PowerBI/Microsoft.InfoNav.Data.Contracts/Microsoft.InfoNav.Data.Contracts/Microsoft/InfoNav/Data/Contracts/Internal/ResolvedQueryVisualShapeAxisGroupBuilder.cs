using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200022A RID: 554
	public class ResolvedQueryVisualShapeAxisGroupBuilder<TParent>
	{
		// Token: 0x06001003 RID: 4099 RVA: 0x0001E4F1 File Offset: 0x0001C6F1
		internal ResolvedQueryVisualShapeAxisGroupBuilder(TParent parent, Action<ResolvedQueryAxisGroup> addToParent)
		{
			this.parent = parent;
			this.addToParent = addToParent;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0001E512 File Offset: 0x0001C712
		public ResolvedQueryVisualShapeAxisGroupBuilder<TParent> WithKey(ResolvedQueryExpression expression)
		{
			this.keys.Add(expression);
			return this;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0001E521 File Offset: 0x0001C721
		public ResolvedQueryVisualShapeAxisGroupBuilder<TParent> WithSubtotal()
		{
			this.includeSubtotal = true;
			return this;
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x0001E52B File Offset: 0x0001C72B
		public TParent Parent
		{
			get
			{
				this.addToParent(new ResolvedQueryAxisGroup(this.keys, this.includeSubtotal));
				return this.parent;
			}
		}

		// Token: 0x0400076E RID: 1902
		private readonly TParent parent;

		// Token: 0x0400076F RID: 1903
		private readonly Action<ResolvedQueryAxisGroup> addToParent;

		// Token: 0x04000770 RID: 1904
		private readonly List<ResolvedQueryExpression> keys = new List<ResolvedQueryExpression>();

		// Token: 0x04000771 RID: 1905
		private bool includeSubtotal;
	}
}
