using System;
using Microsoft.Mashup.ScriptDom;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x02002029 RID: 8233
	internal static class SqlExpressionHelper
	{
		// Token: 0x0600C86D RID: 51309 RVA: 0x0027DF40 File Offset: 0x0027C140
		public static bool TryGetSelectStatement(TSqlFragment fragment, out SelectStatement select)
		{
			TSqlScript tsqlScript = fragment as TSqlScript;
			if (tsqlScript != null && tsqlScript.Batches.Count == 1 && tsqlScript.Batches[0].Statements.Count == 1)
			{
				select = tsqlScript.Batches[0].Statements[0] as SelectStatement;
				if (select != null && select.ComputeClauses.Count == 0 && select.Into == null)
				{
					return true;
				}
			}
			select = null;
			return false;
		}

		// Token: 0x0600C86E RID: 51310 RVA: 0x0027DFC0 File Offset: 0x0027C1C0
		public static bool IsPassthrough(SelectStatement select)
		{
			QuerySpecification querySpecification = select.QueryExpression as QuerySpecification;
			return querySpecification != null && select.WithCtesAndXmlNamespaces == null && querySpecification.SelectElements != null && querySpecification.SelectElements.Count == 1 && querySpecification.FromClause != null && querySpecification.FromClause.TableReferences.Count == 1 && querySpecification.GroupByClause == null && querySpecification.HavingClause == null && querySpecification.WhereClause == null && querySpecification.OrderByClause == null && querySpecification.UniqueRowFilter == UniqueRowFilter.NotSpecified && querySpecification.TopRowFilter == null && querySpecification.FromClause.TableReferences[0] is NamedTableReference && querySpecification.SelectElements[0] is SelectStarExpression;
		}
	}
}
