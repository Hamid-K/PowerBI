using System;
using System.Collections.Generic;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000277 RID: 631
	internal sealed class QueryTableDefinition : QueryTable
	{
		// Token: 0x06001B38 RID: 6968 RVA: 0x0004C762 File Offset: 0x0004A962
		internal QueryTableDefinition(IReadOnlyList<QueryTableColumn> columns, QueryExpression expression, string bindingVariableNameSuggestion)
			: base(columns)
		{
			this._expression = expression;
			this._bindingVariableNameSuggestion = bindingVariableNameSuggestion;
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0004C779 File Offset: 0x0004A979
		internal override QueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0004C781 File Offset: 0x0004A981
		internal override string BindingVariableNameSuggestion
		{
			get
			{
				return this._bindingVariableNameSuggestion;
			}
		}

		// Token: 0x04000EEE RID: 3822
		private readonly QueryExpression _expression;

		// Token: 0x04000EEF RID: 3823
		private readonly string _bindingVariableNameSuggestion;
	}
}
