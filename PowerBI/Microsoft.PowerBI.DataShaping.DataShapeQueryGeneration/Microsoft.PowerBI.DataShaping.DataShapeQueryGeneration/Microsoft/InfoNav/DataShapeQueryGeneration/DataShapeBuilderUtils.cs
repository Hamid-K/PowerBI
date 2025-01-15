using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200000C RID: 12
	internal static class DataShapeBuilderUtils
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00004574 File Offset: 0x00002774
		internal static TParent AddCalculations<TParent>(DataShapeIdGenerator ids, SelectBindingsBuilder selectBindingsBuilder, Dictionary<ProjectedDsqExpression, string> expressionToIdMapping, TParent container, ProjectedDsqExpression generatedDsqExpr, string id) where TParent : class, ICalculationContainer<TParent>
		{
			container = DataShapeBuilderUtils.AddCalculation<TParent>(container, generatedDsqExpr, id, null);
			container = DataShapeBuilderUtils.AddDynamicFormattingCalculation<TParent>(ids, selectBindingsBuilder, expressionToIdMapping, container, generatedDsqExpr.Value.DynamicFormatString);
			container = DataShapeBuilderUtils.AddDynamicFormattingCalculation<TParent>(ids, selectBindingsBuilder, expressionToIdMapping, container, generatedDsqExpr.Value.DynamicFormatCulture);
			return container;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000045CC File Offset: 0x000027CC
		internal static TParent AddCalculation<TParent>(ICalculationContainer<TParent> container, ProjectedDsqExpression generatedDsqExpr, string id, bool? suppressJoinPredicate = null)
		{
			Expression expression = new Expression(generatedDsqExpr.Value.DsqExpression);
			bool flag = suppressJoinPredicate ?? generatedDsqExpr.SuppressJoinPredicate;
			string nativeReferenceName = generatedDsqExpr.NativeReferenceName;
			bool isContextOnly = generatedDsqExpr.IsContextOnly;
			return container.WithCalculation(id, expression, flag, null, nativeReferenceName, isContextOnly);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004624 File Offset: 0x00002824
		internal static TParent AddDynamicFormattingCalculation<TParent>(DataShapeIdGenerator ids, SelectBindingsBuilder selectBindingsBuilder, Dictionary<ProjectedDsqExpression, string> expressionToIdMapping, TParent container, ProjectedDsqExpression dynamicFormattingDsqExpr) where TParent : class, ICalculationContainer<TParent>
		{
			if (dynamicFormattingDsqExpr == null)
			{
				return container;
			}
			string orCreateMeasureId = DataShapeBuilderUtils.GetOrCreateMeasureId(ids, selectBindingsBuilder, expressionToIdMapping, dynamicFormattingDsqExpr, dynamicFormattingDsqExpr.SemanticQuerySelectIndex);
			return DataShapeBuilderUtils.AddCalculation<TParent>(container, dynamicFormattingDsqExpr, orCreateMeasureId, null);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004660 File Offset: 0x00002860
		internal static string GetOrCreateMeasureId(DataShapeIdGenerator ids, SelectBindingsBuilder selectBindingsBuilder, Dictionary<ProjectedDsqExpression, string> expressionToIdMapping, ProjectedDsqExpression generatedDsqExpr, int? selectIdx)
		{
			string text = null;
			if (selectIdx != null)
			{
				text = selectBindingsBuilder.GetCalcIdForSelect(selectIdx.Value);
				if (text == null)
				{
					text = ids.CreateMeasureId();
					selectBindingsBuilder.SetCalcIdForSelect(selectIdx.Value, text);
				}
				return text;
			}
			if (!expressionToIdMapping.TryGetValue(generatedDsqExpr, out text))
			{
				text = ids.CreateMeasureId();
				expressionToIdMapping.Add(generatedDsqExpr, text);
			}
			return text;
		}
	}
}
