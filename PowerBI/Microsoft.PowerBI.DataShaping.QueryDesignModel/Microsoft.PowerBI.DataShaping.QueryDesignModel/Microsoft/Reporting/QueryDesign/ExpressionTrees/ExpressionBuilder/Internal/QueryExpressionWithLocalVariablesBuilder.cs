using System;
using System.Collections.Generic;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal
{
	// Token: 0x02000158 RID: 344
	internal sealed class QueryExpressionWithLocalVariablesBuilder : QueryVariableDeclarationScopeBuilder
	{
		// Token: 0x0600137C RID: 4988 RVA: 0x00037DB2 File Offset: 0x00035FB2
		internal QueryExpressionWithLocalVariablesBuilder()
		{
			this._variableDeclarations = new List<QueryVariableDeclarationExpression>();
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00037DC5 File Offset: 0x00035FC5
		public QueryExpressionWithLocalVariables ToQueryExpression()
		{
			return QueryExpressionBuilder.CreateExpressionWithLocalVariables(this._variableDeclarations, this._result);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00037DD8 File Offset: 0x00035FD8
		public void SetResult(QueryExpression result)
		{
			this._result = result;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00037DE1 File Offset: 0x00035FE1
		protected override void AddVariableDeclaration(QueryVariableDeclarationExpression declaration)
		{
			this._variableDeclarations.Add(declaration);
		}

		// Token: 0x04000AF8 RID: 2808
		private readonly List<QueryVariableDeclarationExpression> _variableDeclarations;

		// Token: 0x04000AF9 RID: 2809
		private QueryExpression _result;
	}
}
