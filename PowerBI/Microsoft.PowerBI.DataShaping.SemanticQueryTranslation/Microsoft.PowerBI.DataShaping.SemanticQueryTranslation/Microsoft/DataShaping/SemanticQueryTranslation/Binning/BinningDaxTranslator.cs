using System;
using Microsoft.DataShaping.SemanticQueryTranslation.Utils;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation.Binning
{
	// Token: 0x0200002A RID: 42
	internal sealed class BinningDaxTranslator
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00007094 File Offset: 0x00005294
		internal static bool TryTranslate(SemanticQueryTranslatorContext context, ResolvedBinItem binItem, out SemanticQueryToDaxTranslationResult result)
		{
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression;
			if (!BinningDaxTranslator.TryBuildBinningExpression(context, binItem, out queryExpression))
			{
				SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.UnknownBinningTranslationError(EngineMessageSeverity.Error));
				result = null;
				return false;
			}
			string text;
			if (!SemanticQueryToDaxTranslator.TryTranslateQueryExpressionToDax(context, queryExpression, out text))
			{
				SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.UnknownBinningDaxTranslationError(EngineMessageSeverity.Error));
				result = null;
				return false;
			}
			result = SemanticQueryToDaxTranslationResultUtils.ForSingleExpression(text, context.ErrorContext);
			return true;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000070F4 File Offset: 0x000052F4
		private static bool TryBuildBinningExpression(SemanticQueryTranslatorContext context, ResolvedBinItem binItem, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression binningExpr)
		{
			ResolvedQueryExpression resolvedQueryExpression;
			if (!BinningDaxTranslator.TryValidateBinItem(binItem, out resolvedQueryExpression))
			{
				SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.UnknownBinningValidationError(EngineMessageSeverity.Error));
				binningExpr = null;
				return false;
			}
			binningExpr = SemanticQueryExpressionTranslator.TranslateExpression(context.ErrorContext, context.Model, context.Schema, context.FeatureSwitchProvider, resolvedQueryExpression, false);
			return binningExpr != null;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007147 File Offset: 0x00005347
		private static bool TryValidateBinItem(ResolvedBinItem binItem, out ResolvedQueryExpression binItemExpr)
		{
			binItemExpr = null;
			return binItem != null && !(binItem.Expression == null) && (BinningDaxTranslator.IsValidBinFloorExpression(binItem.Expression, out binItemExpr) || BinningDaxTranslator.IsValidBinMemberExpression(binItem.Expression, out binItemExpr));
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007180 File Offset: 0x00005380
		private static bool IsValidBinFloorExpression(ResolvedQueryExpression binItemExpression, out ResolvedQueryExpression binItemExpr)
		{
			binItemExpr = binItemExpression as ResolvedQueryFloorExpression;
			return !(binItemExpr == null);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007198 File Offset: 0x00005398
		private static bool IsValidBinMemberExpression(ResolvedQueryExpression binItemExpression, out ResolvedQueryExpression binItemExpr)
		{
			binItemExpr = null;
			ResolvedQueryMemberExpression resolvedQueryMemberExpression = binItemExpression as ResolvedQueryMemberExpression;
			if (resolvedQueryMemberExpression == null || resolvedQueryMemberExpression.Member == null || !resolvedQueryMemberExpression.Member.Equals("Min"))
			{
				return false;
			}
			binItemExpr = resolvedQueryMemberExpression.Expression as ResolvedQueryDiscretizeExpression;
			return !(binItemExpr == null);
		}

		// Token: 0x04000083 RID: 131
		private const string MinMember = "Min";
	}
}
