using System;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000223 RID: 547
	public sealed class ResolvedQuerySourceBuilder<TParent>
	{
		// Token: 0x06000FDD RID: 4061 RVA: 0x0001E1D0 File Offset: 0x0001C3D0
		internal ResolvedQuerySourceBuilder(TParent parent, Action<ResolvedQuerySource> addToParent, ResolvedQueryReferenceContext refContext, string name)
		{
			this._parent = parent;
			this._addToParent = addToParent;
			this._refContext = refContext;
			this._name = name;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0001E1F5 File Offset: 0x0001C3F5
		public ResolvedQuerySourceBuilder<TParent> WithExpression(ResolvedQueryExpression expression)
		{
			this._expression = expression;
			return this;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0001E1FF File Offset: 0x0001C3FF
		public ResolvedQueryDefinitionBuilder<ResolvedQuerySourceBuilder<TParent>> WithSubquery(string name = null)
		{
			return new ResolvedQueryDefinitionBuilder<ResolvedQuerySourceBuilder<TParent>>(this, delegate(ResolvedQueryDefinition queryDefinition)
			{
				this.WithExpression(queryDefinition.Subquery());
			}, this._refContext, name);
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0001E21A File Offset: 0x0001C41A
		public TParent Parent
		{
			get
			{
				this._addToParent(this.Build());
				return this._parent;
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0001E233 File Offset: 0x0001C433
		public ResolvedQuerySource Build()
		{
			return this._expression.ExpressionSource(this._name);
		}

		// Token: 0x04000754 RID: 1876
		private readonly TParent _parent;

		// Token: 0x04000755 RID: 1877
		private readonly Action<ResolvedQuerySource> _addToParent;

		// Token: 0x04000756 RID: 1878
		private readonly ResolvedQueryReferenceContext _refContext;

		// Token: 0x04000757 RID: 1879
		private string _name;

		// Token: 0x04000758 RID: 1880
		private ResolvedQueryExpression _expression;
	}
}
