using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Utils;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x0200008A RID: 138
	internal static class QueryGenerationDevErrors
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x00017EBE File Offset: 0x000160BE
		internal static string DataTableInvalidColumnReferenceNameExpressionNode(string kind)
		{
			return StringUtil.FormatInvariant("The column reference name expression is an expression node of an invalid type '{0}'", new object[] { kind });
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00017ED4 File Offset: 0x000160D4
		internal static string MissingColumn(string name)
		{
			return StringUtil.FormatInvariant("Column '{0}' does not exist in the referenced table.", new object[] { name });
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00017EEA File Offset: 0x000160EA
		internal static string MissingColumn(ExpressionId exprId)
		{
			return StringUtil.FormatInvariant("Column by expression Id '{0}' does not exist in the referenced table.", new object[] { exprId });
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00017F08 File Offset: 0x00016108
		internal static IContainsTelemetryMarkup GetFieldNameForDetailError(ExpressionTable expressionTable, ExpressionId detailExprId)
		{
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = expressionTable.GetNodeOrDefault(detailExprId) as ResolvedPropertyExpressionNode;
			if (resolvedPropertyExpressionNode != null)
			{
				return TranslationMessageUtils.GetPropertyNameForError(resolvedPropertyExpressionNode);
			}
			return null;
		}

		// Token: 0x04000336 RID: 822
		internal const string DetailWithoutGroup = "Detail outside of a group";

		// Token: 0x04000337 RID: 823
		internal const string ExpressionTranslationFailed = "Expression translation failed";

		// Token: 0x04000338 RID: 824
		internal const string InvalidFilterCondition = "Could not generate query expression for filter condition";

		// Token: 0x04000339 RID: 825
		internal const string InvalidLiteral = "Literal validation failed";

		// Token: 0x0400033A RID: 826
		internal const string NoCompatibleGroupForDetail = "Did not find compatible group for query expression";

		// Token: 0x0400033B RID: 827
		internal const string SubQueryExpressionFailed = "Could not generate subquery expression";

		// Token: 0x0400033C RID: 828
		internal const string SubQueryGenerationFailed = "Could not generate subquery definition";

		// Token: 0x0400033D RID: 829
		internal const string SubQueryOverCompositeExpression = "Subquery over composite expression";
	}
}
