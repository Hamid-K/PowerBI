using System;
using System.Collections.Generic;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200027D RID: 637
	internal sealed class QueryTableReference : QueryTable
	{
		// Token: 0x06001B63 RID: 7011 RVA: 0x0004CC35 File Offset: 0x0004AE35
		internal QueryTableReference(IReadOnlyList<QueryTableColumn> columns, QueryVariableReferenceExpression expression)
			: base(columns)
		{
			this._expression = expression;
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0004CC45 File Offset: 0x0004AE45
		internal override QueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x0004CC4D File Offset: 0x0004AE4D
		internal string VariableName
		{
			get
			{
				return this._expression.VariableName;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0004CC5A File Offset: 0x0004AE5A
		internal override string BindingVariableNameSuggestion
		{
			get
			{
				return this.VariableName;
			}
		}

		// Token: 0x04000EFA RID: 3834
		private readonly QueryVariableReferenceExpression _expression;
	}
}
