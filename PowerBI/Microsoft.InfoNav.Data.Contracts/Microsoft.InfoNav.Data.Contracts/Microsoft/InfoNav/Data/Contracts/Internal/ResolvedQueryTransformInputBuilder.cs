using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000226 RID: 550
	public sealed class ResolvedQueryTransformInputBuilder<TParent>
	{
		// Token: 0x06000FED RID: 4077 RVA: 0x0001E30E File Offset: 0x0001C50E
		internal ResolvedQueryTransformInputBuilder(TParent parent, Action<ResolvedQueryTransformInput> addToParent)
		{
			this._parent = parent;
			this._addToParent = addToParent;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0001E324 File Offset: 0x0001C524
		public ResolvedQueryTransformInputBuilder<TParent> WithParameter(string name, ResolvedQueryExpression expression)
		{
			return this.WithParameter(new ResolvedQueryTransformParameter(name, expression));
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0001E333 File Offset: 0x0001C533
		public ResolvedQueryTransformInputBuilder<TParent> WithParameter(ResolvedQueryTransformParameter parameter)
		{
			Util.EnsureList<ResolvedQueryTransformParameter>(ref this._parameters);
			this._parameters.Add(parameter);
			return this;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0001E34E File Offset: 0x0001C54E
		public ResolvedQueryTransformTableBuilder<ResolvedQueryTransformInputBuilder<TParent>> WithTable(string name)
		{
			return new ResolvedQueryTransformTableBuilder<ResolvedQueryTransformInputBuilder<TParent>>(this, delegate(ResolvedQueryTransformTable table)
			{
				this.WithTable(table);
			}, name);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0001E363 File Offset: 0x0001C563
		public ResolvedQueryTransformInputBuilder<TParent> WithTable(ResolvedQueryTransformTable table)
		{
			this._table = table;
			return this;
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x0001E36D File Offset: 0x0001C56D
		public TParent Parent
		{
			get
			{
				this._addToParent(this.Build());
				return this._parent;
			}
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0001E386 File Offset: 0x0001C586
		public ResolvedQueryTransformInput Build()
		{
			return new ResolvedQueryTransformInput(this._parameters, this._table);
		}

		// Token: 0x0400075F RID: 1887
		private readonly TParent _parent;

		// Token: 0x04000760 RID: 1888
		private readonly Action<ResolvedQueryTransformInput> _addToParent;

		// Token: 0x04000761 RID: 1889
		private List<ResolvedQueryTransformParameter> _parameters;

		// Token: 0x04000762 RID: 1890
		private ResolvedQueryTransformTable _table;
	}
}
