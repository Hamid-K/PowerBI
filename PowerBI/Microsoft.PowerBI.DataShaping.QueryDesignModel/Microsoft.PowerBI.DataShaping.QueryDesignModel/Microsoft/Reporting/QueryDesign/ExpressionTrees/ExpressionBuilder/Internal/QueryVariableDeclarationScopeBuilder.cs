using System;
using System.Collections.Generic;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal
{
	// Token: 0x02000159 RID: 345
	internal abstract class QueryVariableDeclarationScopeBuilder
	{
		// Token: 0x06001380 RID: 4992 RVA: 0x00037DEF File Offset: 0x00035FEF
		protected QueryVariableDeclarationScopeBuilder()
		{
			this.NamingContext = new QueryNamingContext(null);
			this._expressionToVariable = new Dictionary<QueryExpression, QueryVariableDeclarationExpression>();
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x00037E0E File Offset: 0x0003600E
		internal QueryNamingContext NamingContext { get; }

		// Token: 0x06001382 RID: 4994 RVA: 0x00037E18 File Offset: 0x00036018
		public QueryVariableReferenceExpression DeclareVariable(QueryExpression expression, string suggestedName)
		{
			QueryVariableDeclarationExpression queryVariableDeclarationExpression;
			if (this._expressionToVariable.TryGetValue(expression, out queryVariableDeclarationExpression))
			{
				return queryVariableDeclarationExpression.Variable;
			}
			string text = this.NamingContext.CreateNameForVariableDeclaration(suggestedName);
			QueryVariableDeclarationExpression queryVariableDeclarationExpression2 = expression.DeclareVariableAs(text);
			this.AddVariableDeclaration(queryVariableDeclarationExpression2);
			this._expressionToVariable.Add(expression, queryVariableDeclarationExpression2);
			return queryVariableDeclarationExpression2.Variable;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00037E6C File Offset: 0x0003606C
		public QueryTableReference DeclareVariable(QueryTable table, string suggestedName)
		{
			string text = this.NamingContext.CreateNameForVariableDeclaration(suggestedName);
			QueryVariableDeclarationExpression queryVariableDeclarationExpression = table.Expression.DeclareVariableAs(text);
			this.AddVariableDeclaration(queryVariableDeclarationExpression);
			return new QueryTableReference(table.Columns, queryVariableDeclarationExpression.Variable);
		}

		// Token: 0x06001384 RID: 4996
		protected abstract void AddVariableDeclaration(QueryVariableDeclarationExpression declaration);

		// Token: 0x04000AFA RID: 2810
		private readonly Dictionary<QueryExpression, QueryVariableDeclarationExpression> _expressionToVariable;
	}
}
