using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000227 RID: 551
	public sealed class ResolvedQueryTransformOutputBuilder<TParent>
	{
		// Token: 0x06000FF5 RID: 4085 RVA: 0x0001E3A3 File Offset: 0x0001C5A3
		internal ResolvedQueryTransformOutputBuilder(TParent parent, Action<ResolvedQueryTransformOutput> addToParent)
		{
			this._parent = parent;
			this._addToParent = addToParent;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0001E3B9 File Offset: 0x0001C5B9
		public ResolvedQueryTransformTableBuilder<ResolvedQueryTransformOutputBuilder<TParent>> WithTable(string name)
		{
			return new ResolvedQueryTransformTableBuilder<ResolvedQueryTransformOutputBuilder<TParent>>(this, delegate(ResolvedQueryTransformTable table)
			{
				this.WithTable(table);
			}, name);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0001E3CE File Offset: 0x0001C5CE
		public ResolvedQueryTransformOutputBuilder<TParent> WithTable(ResolvedQueryTransformTable table)
		{
			this._table = table;
			return this;
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0001E3D8 File Offset: 0x0001C5D8
		public TParent Parent
		{
			get
			{
				this._addToParent(this.Build());
				return this._parent;
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0001E3F1 File Offset: 0x0001C5F1
		public ResolvedQueryTransformOutput Build()
		{
			return new ResolvedQueryTransformOutput(this._table);
		}

		// Token: 0x04000763 RID: 1891
		private readonly TParent _parent;

		// Token: 0x04000764 RID: 1892
		private readonly Action<ResolvedQueryTransformOutput> _addToParent;

		// Token: 0x04000765 RID: 1893
		private ResolvedQueryTransformTable _table;
	}
}
