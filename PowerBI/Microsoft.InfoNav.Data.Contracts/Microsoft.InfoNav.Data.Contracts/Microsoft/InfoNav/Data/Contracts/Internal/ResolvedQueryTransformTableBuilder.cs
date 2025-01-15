using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000228 RID: 552
	public sealed class ResolvedQueryTransformTableBuilder<TParent>
	{
		// Token: 0x06000FFB RID: 4091 RVA: 0x0001E408 File Offset: 0x0001C608
		internal ResolvedQueryTransformTableBuilder(TParent parent, Action<ResolvedQueryTransformTable> addToParent, string name)
		{
			this._parent = parent;
			this._addToParent = addToParent;
			this._name = name;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0001E428 File Offset: 0x0001C628
		public ResolvedQueryTransformTableBuilder<TParent> WithColumn(string name, string role, ResolvedQueryExpression expression)
		{
			ResolvedQueryTransformTableColumn resolvedQueryTransformTableColumn = new ResolvedQueryTransformTableColumn(name, role, expression);
			Util.EnsureList<ResolvedQueryTransformTableColumn>(ref this._columns);
			this._columns.Add(resolvedQueryTransformTableColumn);
			return this;
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0001E457 File Offset: 0x0001C657
		public TParent Parent
		{
			get
			{
				this._addToParent(this.Build());
				return this._parent;
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0001E470 File Offset: 0x0001C670
		public ResolvedQueryTransformTable Build()
		{
			return new ResolvedQueryTransformTable(this._name, this._columns);
		}

		// Token: 0x04000766 RID: 1894
		private readonly TParent _parent;

		// Token: 0x04000767 RID: 1895
		private readonly Action<ResolvedQueryTransformTable> _addToParent;

		// Token: 0x04000768 RID: 1896
		private readonly string _name;

		// Token: 0x04000769 RID: 1897
		private List<ResolvedQueryTransformTableColumn> _columns;
	}
}
