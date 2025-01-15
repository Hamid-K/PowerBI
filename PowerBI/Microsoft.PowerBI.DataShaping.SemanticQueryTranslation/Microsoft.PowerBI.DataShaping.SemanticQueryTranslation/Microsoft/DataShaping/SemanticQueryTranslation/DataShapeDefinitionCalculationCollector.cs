using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x02000006 RID: 6
	internal sealed class DataShapeDefinitionCalculationCollector : DataShapeDefinitionVisitor
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002319 File Offset: 0x00000519
		internal DataShapeDefinitionCalculationCollector()
		{
			this._calculationExpressions = new Dictionary<string, ExpressionNode>(StringComparer.Ordinal);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002331 File Offset: 0x00000531
		internal static Dictionary<string, ExpressionNode> MapCalculations(DataShape dataShape)
		{
			DataShapeDefinitionCalculationCollector dataShapeDefinitionCalculationCollector = new DataShapeDefinitionCalculationCollector();
			dataShapeDefinitionCalculationCollector.Visit(dataShape);
			return dataShapeDefinitionCalculationCollector._calculationExpressions;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002344 File Offset: 0x00000544
		internal override void Visit(Calculation calculation)
		{
			this._calculationExpressions.Add(calculation.Id, calculation.Value);
		}

		// Token: 0x0400002F RID: 47
		private readonly Dictionary<string, ExpressionNode> _calculationExpressions;
	}
}
