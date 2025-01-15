using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001890 RID: 6288
	internal static class NormalizationVisitor
	{
		// Token: 0x06009F86 RID: 40838 RVA: 0x0020F0DE File Offset: 0x0020D2DE
		public static IExpression Normalize(IFunctionExpression function, TypeValue[] parameterTypes, bool normalizeInvocation = true)
		{
			return NormalizationVisitor.Normalize(function, parameterTypes, normalizeInvocation);
		}

		// Token: 0x06009F87 RID: 40839 RVA: 0x0020F0E8 File Offset: 0x0020D2E8
		public static IExpression Normalize(IExpression node, bool normalizeInvocation = true)
		{
			return NormalizationVisitor.Normalize(node, null, normalizeInvocation);
		}

		// Token: 0x06009F88 RID: 40840 RVA: 0x0020F0F4 File Offset: 0x0020D2F4
		private static IExpression Normalize(IExpression node, TypeValue[] parameterTypes, bool normalizeInvocation)
		{
			int num = 0;
			int num2 = 0;
			while (num2 < 4 && num <= 500)
			{
				IExpression expression = node;
				if (normalizeInvocation)
				{
					if (parameterTypes != null)
					{
						node = ConstantFoldingVisitor2.Fold((IFunctionExpression)expression, parameterTypes);
					}
					else
					{
						node = ConstantFoldingVisitor2.Fold(expression);
					}
				}
				else if (parameterTypes != null)
				{
					node = ConstantFoldingVisitor.Fold((IFunctionExpression)expression, parameterTypes);
				}
				else
				{
					node = ConstantFoldingVisitor.Fold(expression);
				}
				node = InliningVisitor.Inline(Engine.Instance, node, Math.Min(500 - num, 500));
				node = CollapseNestedFunctionsVisitor.Instance.CollapseNestedFunctions(node);
				if (node == expression)
				{
					break;
				}
				int num3 = ComplexityVisitor.AnalyzeComplexity(expression);
				int num4 = ComplexityVisitor.AnalyzeComplexity(node);
				num += num4 - num3;
				num2++;
			}
			return node;
		}

		// Token: 0x040053A9 RID: 21417
		private const int maxIterations = 4;

		// Token: 0x040053AA RID: 21418
		private const int complexityLimit = 500;
	}
}
