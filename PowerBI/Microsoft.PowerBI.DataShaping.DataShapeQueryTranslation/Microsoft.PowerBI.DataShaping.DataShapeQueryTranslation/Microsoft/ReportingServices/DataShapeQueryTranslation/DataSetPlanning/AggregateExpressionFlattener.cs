using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000D2 RID: 210
	internal static class AggregateExpressionFlattener
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x000223F8 File Offset: 0x000205F8
		public static ExpressionNode Rewrite(FunctionCallExpressionNode aggregateFunction)
		{
			if (aggregateFunction.UsageKind != FunctionUsageKind.Query)
			{
				return aggregateFunction;
			}
			ExpressionNode expressionNode = aggregateFunction.Arguments[0];
			FunctionDescriptor descriptor = aggregateFunction.Descriptor;
			string backingFunctionName = descriptor.BackingFunctionName;
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(backingFunctionName);
			if (num > 1320056494U)
			{
				if (num <= 2490518351U)
				{
					if (num != 1382488607U)
					{
						if (num != 1408978916U)
						{
							if (num != 2490518351U)
							{
								goto IL_0190;
							}
							if (!(backingFunctionName == "Core.DistinctCount"))
							{
								goto IL_0190;
							}
							return LiteralExpressionNode.OneInt64;
						}
						else if (!(backingFunctionName == "Core.StandardDeviation"))
						{
							goto IL_0190;
						}
					}
					else
					{
						if (!(backingFunctionName == "Core.Count"))
						{
							goto IL_0190;
						}
						return AggregateExpressionFlattener.FlattenCount(expressionNode);
					}
				}
				else if (num != 3037031955U)
				{
					if (num != 3797372689U)
					{
						if (num != 4119138911U)
						{
							goto IL_0190;
						}
						if (!(backingFunctionName == "Core.PercentileExc"))
						{
							goto IL_0190;
						}
						return expressionNode;
					}
					else
					{
						if (!(backingFunctionName == "Core.PercentileInc"))
						{
							goto IL_0190;
						}
						return expressionNode;
					}
				}
				else if (!(backingFunctionName == "Core.Variance"))
				{
					goto IL_0190;
				}
				return AggregateExpressionFlattener.FlattenStatFuncs(expressionNode);
			}
			if (num <= 326939756U)
			{
				if (num != 47960491U)
				{
					if (num != 326939756U)
					{
						goto IL_0190;
					}
					if (!(backingFunctionName == "Core.Min"))
					{
						goto IL_0190;
					}
				}
				else if (!(backingFunctionName == "Core.Sum"))
				{
					goto IL_0190;
				}
			}
			else if (num != 560443326U)
			{
				if (num != 1317937847U)
				{
					if (num != 1320056494U)
					{
						goto IL_0190;
					}
					if (!(backingFunctionName == "Core.Median"))
					{
						goto IL_0190;
					}
				}
				else if (!(backingFunctionName == "Core.Average"))
				{
					goto IL_0190;
				}
			}
			else if (!(backingFunctionName == "Core.Max"))
			{
				goto IL_0190;
			}
			return expressionNode;
			IL_0190:
			throw new InvalidOperationException(TranslationMessagePhrases.InvalidExpressionSyntax(descriptor.Name));
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000225AA File Offset: 0x000207AA
		private static ExpressionNode FlattenStatFuncs(ExpressionNode innerExpr)
		{
			return ExprNodes.IsNull(innerExpr).If(LiteralExpressionNode.NullInt64, LiteralExpressionNode.ZeroInt64);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x000225C1 File Offset: 0x000207C1
		private static ExpressionNode FlattenCount(ExpressionNode innerExpr)
		{
			return ExprNodes.IsNull(innerExpr).If(LiteralExpressionNode.ZeroInt64, LiteralExpressionNode.OneInt64);
		}
	}
}
