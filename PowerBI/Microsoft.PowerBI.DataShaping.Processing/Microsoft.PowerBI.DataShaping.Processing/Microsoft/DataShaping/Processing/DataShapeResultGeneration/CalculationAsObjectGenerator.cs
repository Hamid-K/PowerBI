using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x02000071 RID: 113
	internal sealed class CalculationAsObjectGenerator : CalculationGenerator
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00007C5B File Offset: 0x00005E5B
		internal CalculationAsObjectGenerator(DsrWriterOptions options, ExpressionEvaluator evaluator, ExpressionTypeEvaluator typeEvaluator)
			: base(options, evaluator, typeEvaluator, null)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00007C67 File Offset: 0x00005E67
		internal override void Process(string parentId, IList<Calculation> calculations, ICalculationContainerWriter writer)
		{
			base.ProcessCalculations<CalculationAsObjectWriter>(calculations, null, writer.BeginCalculationsAsObjects(), false);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00007C78 File Offset: 0x00005E78
		protected override void WriteCalculation(int calcIndex, string calcId, Type calcType, object evaluatedCalcValue, ICalculationWriter calcWriter)
		{
			calcWriter.WriteIdAndVariantValue(calcId, evaluatedCalcValue);
		}
	}
}
