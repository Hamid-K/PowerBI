using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200007A RID: 122
	internal static class QueryExpressionAnalyzer
	{
		// Token: 0x06000262 RID: 610 RVA: 0x0000BD9C File Offset: 0x00009F9C
		public static Formula CreateFormula(IRdmQueryExpression queryExpression)
		{
			FormulaParserContext formulaParserContext = new FormulaParserContext();
			queryExpression.FindFormulaComponents(formulaParserContext);
			return QueryExpressionAnalyzer.CreateFormula(formulaParserContext);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000BDBC File Offset: 0x00009FBC
		public static Formula CreateFormula(FormulaParserContext context)
		{
			string text = null;
			List<string> list = new List<string>();
			Formula formula = QueryExpressionAnalyzer.CreateFormulaEdmReference(context, list);
			if (context.FunctionName != null)
			{
				string functionName = context.FunctionName;
				if (functionName == "Core.Sum")
				{
					text = "Sum";
				}
				else if (functionName == "Core.Average")
				{
					text = "Average";
				}
				else if (functionName == "Core.Count")
				{
					text = "Count";
				}
				else if (functionName == "Core.DistinctCount")
				{
					text = "DistinctCount";
				}
				else if (functionName == "Core.Min")
				{
					text = "Min";
				}
				else if (functionName == "Core.Max")
				{
					text = "Max";
				}
				return new Formula
				{
					Arguments = new List<Formula>
					{
						new Formula
						{
							QualifiedName = list
						}
					},
					Function = text
				};
			}
			return formula;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000BE94 File Offset: 0x0000A094
		private static Formula CreateFormulaEdmReference(FormulaParserContext context, List<string> qualifiedName)
		{
			string entityName = context.EntityName;
			qualifiedName.Add(entityName);
			if (!string.IsNullOrEmpty(context.PropertyName))
			{
				if (!string.IsNullOrEmpty(context.ContainerName))
				{
					string containerName = context.ContainerName;
					qualifiedName.Add(containerName);
				}
				string propertyName = context.PropertyName;
				qualifiedName.Add(propertyName);
			}
			return new Formula
			{
				QualifiedName = qualifiedName,
				EdmReferenceKind = context.EdmReferenceKind
			};
		}
	}
}
