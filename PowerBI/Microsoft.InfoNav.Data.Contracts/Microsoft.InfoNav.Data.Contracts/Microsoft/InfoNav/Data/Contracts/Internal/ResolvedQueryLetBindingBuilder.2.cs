using System;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000222 RID: 546
	public class ResolvedQueryLetBindingBuilder<TParent>
	{
		// Token: 0x06000FD7 RID: 4055 RVA: 0x0001E14B File Offset: 0x0001C34B
		internal ResolvedQueryLetBindingBuilder(TParent parent, Action<ResolvedQueryLetBinding> addToParent, ResolvedQueryReferenceContext refContext, string name)
		{
			this._parent = parent;
			this._addToParent = addToParent;
			this._refContext = refContext;
			this._name = name;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0001E170 File Offset: 0x0001C370
		public ResolvedQueryLetBindingBuilder<TParent> WithExpression(ResolvedQueryExpression expression)
		{
			this._expression = expression;
			return this;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0001E17A File Offset: 0x0001C37A
		public ResolvedQueryDefinitionBuilder<ResolvedQueryLetBindingBuilder<TParent>> WithSubquery(string name = null)
		{
			return new ResolvedQueryDefinitionBuilder<ResolvedQueryLetBindingBuilder<TParent>>(this, delegate(ResolvedQueryDefinition queryDefinition)
			{
				this.WithExpression(queryDefinition.Subquery());
			}, this._refContext, name);
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0001E195 File Offset: 0x0001C395
		public TParent Parent
		{
			get
			{
				this._addToParent(this.Build());
				return this._parent;
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0001E1AE File Offset: 0x0001C3AE
		public ResolvedQueryLetBinding Build()
		{
			return this._expression.LetBinding(this._name);
		}

		// Token: 0x0400074F RID: 1871
		private readonly TParent _parent;

		// Token: 0x04000750 RID: 1872
		private readonly Action<ResolvedQueryLetBinding> _addToParent;

		// Token: 0x04000751 RID: 1873
		private readonly ResolvedQueryReferenceContext _refContext;

		// Token: 0x04000752 RID: 1874
		private string _name;

		// Token: 0x04000753 RID: 1875
		private ResolvedQueryExpression _expression;
	}
}
