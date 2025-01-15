using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000136 RID: 310
	internal static class DaxInTupleExpressionTranslator
	{
		// Token: 0x0600112D RID: 4397 RVA: 0x0002FD1C File Offset: 0x0002DF1C
		internal static DaxExpression Translate(QueryInExpression expression, DaxTransform daxTransform, bool forceLogicalTree, CancellationToken cancellationToken)
		{
			if (daxTransform.DaxCapabilities.IsSupported(ModelCapabilitiesKind.InOperator) && daxTransform.DaxCapabilities.IsSupported(ModelCapabilitiesKind.TableConstructor) && !forceLogicalTree && expression.CanBePreservedAsTuples)
			{
				return DaxInTupleExpressionTranslator.TranslateUsingInOperator(expression, daxTransform, cancellationToken);
			}
			if (expression.Values.Count > 500)
			{
				throw new CommandTreeTranslationException("The count of expressions in the Values argument to the In operator exceeds the maximum number allowed when rewriting to a binary tree of ORs and ANDs.", CommandTreeTranslationErrorCode.InvalidFilterExceedsMaxNumberOfValuesForInFilterTreeRewrite);
			}
			return DaxInTupleExpressionTranslator.TranslateToLogicalTree(expression, daxTransform, cancellationToken);
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0002FD80 File Offset: 0x0002DF80
		private static DaxExpression TranslateToLogicalTree(QueryInExpression expression, DaxTransform daxTransform, CancellationToken cancellationToken)
		{
			IReadOnlyList<QueryExpression> expressions = expression.Expressions;
			IReadOnlyList<DaxExpression> readOnlyList = DaxInTupleExpressionTranslator.TranslateAll(expressions, daxTransform);
			IReadOnlyList<IReadOnlyList<QueryExpression>> values = expression.Values;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2 = values.Count - 1;
			for (int i = 0; i < num2; i++)
			{
				stringBuilder.Append("OR(");
				num++;
				stringBuilder.NewlineAndIndent(num);
			}
			for (int j = 0; j < values.Count; j++)
			{
				IReadOnlyList<QueryExpression> readOnlyList2 = values[j];
				int num3 = readOnlyList2.Count - 1;
				for (int k = 0; k < num3; k++)
				{
					stringBuilder.Append("AND(");
					num++;
					stringBuilder.NewlineAndIndent(num);
				}
				for (int l = 0; l < readOnlyList2.Count; l++)
				{
					cancellationToken.ThrowIfCancellationRequested();
					DaxInTupleExpressionTranslator.AppendDaxExpressionText(stringBuilder, expressions, l, readOnlyList, readOnlyList2, daxTransform, expression, values.Count > 1 || readOnlyList2.Count > 1);
					if (l > 0)
					{
						num--;
						stringBuilder.NewlineAndIndent(num);
						stringBuilder.Append(")");
					}
					if (l < num3)
					{
						stringBuilder.Append(",");
						stringBuilder.NewlineAndIndent(num);
					}
				}
				if (j != 0)
				{
					num--;
					stringBuilder.NewlineAndIndent(num);
					stringBuilder.Append(")");
				}
				if (j < num2)
				{
					stringBuilder.Append(",");
					stringBuilder.NewlineAndIndent(num);
				}
			}
			return DaxExpression.Scalar(stringBuilder.ToString());
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0002FEFC File Offset: 0x0002E0FC
		private static void AppendDaxExpressionText(StringBuilder stringBuilder, IReadOnlyList<QueryExpression> expressions, int valueIndex, IReadOnlyList<DaxExpression> daxExpressions, IReadOnlyList<QueryExpression> valueTuple, DaxTransform daxTransform, QueryInExpression expression, bool hasMultipleValues)
		{
			QueryExpression queryExpression = expressions[valueIndex];
			DaxExpression daxExpression = daxExpressions[valueIndex];
			QueryExpression queryExpression2 = valueTuple[valueIndex];
			string text = DaxInTupleExpressionTranslator.ApplyEqual(daxTransform, queryExpression, daxExpression, queryExpression2, expression.IsStrict).Text;
			if (hasMultipleValues && text.Length > 2 && text[0] == '(' && text[text.Length - 1] == ')')
			{
				stringBuilder.Append(text, 1, text.Length - 2);
				return;
			}
			stringBuilder.Append(text);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0002FF80 File Offset: 0x0002E180
		private static void NewlineAndIndent(this StringBuilder stringBuilder, int count)
		{
			stringBuilder.Append(DaxFormat.NewLine);
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append('\t');
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0002FFB0 File Offset: 0x0002E1B0
		private static DaxExpression TranslateUsingInOperator(QueryInExpression expression, DaxTransform daxTransform, CancellationToken cancellationToken)
		{
			QueryInExpression queryInExpression = (expression.IsStrict ? expression : QueryAlgorithms.RewriteWithEqualitySemantics(expression, cancellationToken));
			IReadOnlyList<IReadOnlyList<QueryExpression>> values = queryInExpression.Values;
			IReadOnlyList<QueryExpression> expressions = queryInExpression.Expressions;
			DaxDataTableStringBuilder daxDataTableStringBuilder = new DaxDataTableStringBuilder(expressions.Count);
			daxDataTableStringBuilder.Begin();
			foreach (IEnumerable<QueryExpression> enumerable in values)
			{
				daxDataTableStringBuilder.BeginRow();
				foreach (QueryExpression queryExpression in enumerable)
				{
					DaxExpression daxExpression = queryExpression.Accept<DaxExpression>(daxTransform);
					daxDataTableStringBuilder.AppendColumn(daxExpression.Text);
				}
				daxDataTableStringBuilder.EndRow();
			}
			daxDataTableStringBuilder.End();
			List<DaxResultColumn> list = (from c in DataTableBuilder.AutoGenQueryTupleNames(expressions.Count)
				select new DaxResultColumn(c, new DaxColumnRef(c, DaxTableRef.Empty))).ToList<DaxResultColumn>();
			DaxExpression daxExpression2 = DaxExpression.Table(daxDataTableStringBuilder.ToDax(), list, false);
			return DaxOperators.In(daxTransform.TransformTuple<QueryExpression>(expressions, (QueryExpression expr) => expr), daxExpression2);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000300F4 File Offset: 0x0002E2F4
		private static DaxExpression ApplyEqual(DaxTransform daxTransform, QueryExpression expr, DaxExpression daxExpr, QueryExpression valueExpr, bool useStrictComparison)
		{
			DaxExpression daxExpression;
			if (valueExpr is QueryNullExpression)
			{
				daxExpression = DaxFunctions.IsBlank(daxExpr);
			}
			else
			{
				DaxExpression daxExpression2 = valueExpr.Accept<DaxExpression>(daxTransform);
				if (expr is QueryNullExpression)
				{
					daxExpression = DaxFunctions.IsBlank(daxExpression2);
				}
				else if (useStrictComparison || QueryAlgorithms.ShouldUseDaxIdentityComparison(expr, valueExpr))
				{
					daxExpression = DaxIdentityOperatorTranslator.EqualsIdentity(expr, daxExpr, valueExpr, daxExpression2);
				}
				else
				{
					daxExpression = DaxOperators.Equal(daxExpr, daxExpression2);
				}
			}
			return daxExpression;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00030150 File Offset: 0x0002E350
		private static IReadOnlyList<DaxExpression> TranslateAll(IReadOnlyList<QueryExpression> queryExprs, DaxTransform daxTransform)
		{
			List<DaxExpression> list = new List<DaxExpression>(queryExprs.Count);
			for (int i = 0; i < queryExprs.Count; i++)
			{
				list.Add(queryExprs[i].Accept<DaxExpression>(daxTransform));
			}
			return list;
		}

		// Token: 0x04000AAB RID: 2731
		private const string OrBegin = "OR(";

		// Token: 0x04000AAC RID: 2732
		private const string AndBegin = "AND(";

		// Token: 0x04000AAD RID: 2733
		private const string CloseParen = ")";

		// Token: 0x04000AAE RID: 2734
		private const string Comma = ",";
	}
}
