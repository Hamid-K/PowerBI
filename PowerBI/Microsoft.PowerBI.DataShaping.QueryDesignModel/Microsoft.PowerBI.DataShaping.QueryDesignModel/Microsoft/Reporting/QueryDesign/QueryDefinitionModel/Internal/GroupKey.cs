using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000DA RID: 218
	internal class GroupKey : INamedProjection, INamedItem
	{
		// Token: 0x06000DBE RID: 3518 RVA: 0x00023378 File Offset: 0x00021578
		internal GroupKey(string name, QueryExpression expression)
		{
			Contracts.CheckValue<string>(name, "name");
			Contracts.CheckNonEmpty(name, "name");
			Contracts.CheckValue<QueryExpression>(expression, "expression");
			Contracts.CheckParam(expression.IsModelFieldReference(), "expression");
			this._name = name;
			this._expression = expression;
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000233CA File Offset: 0x000215CA
		public QueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x000233D2 File Offset: 0x000215D2
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x0400099D RID: 2461
		private readonly string _name;

		// Token: 0x0400099E RID: 2462
		private readonly QueryExpression _expression;
	}
}
