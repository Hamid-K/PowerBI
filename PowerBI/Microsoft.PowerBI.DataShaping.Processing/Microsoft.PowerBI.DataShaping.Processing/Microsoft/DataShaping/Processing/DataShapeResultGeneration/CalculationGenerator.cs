using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x02000070 RID: 112
	internal abstract class CalculationGenerator
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x00007B58 File Offset: 0x00005D58
		protected CalculationGenerator(DsrWriterOptions options, ExpressionEvaluator evaluator, ExpressionTypeEvaluator typeEvaluator, CalculationGenerationTelemetry telemetry)
		{
			this.Options = options;
			this.Evaluator = evaluator;
			this.TypeEvaluator = typeEvaluator;
			this._telemetry = telemetry;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00007B7D File Offset: 0x00005D7D
		internal static CalculationGenerator Create(DsrWriterOptions options, ExpressionEvaluator evaluator, ExpressionTypeEvaluator typeEvaluator, DictionaryEncodingCoordinator dictionaryEncoding)
		{
			if (options.WriteOptimizedCalculations)
			{
				return new OptimizedCalculationGenerator(options, evaluator, typeEvaluator, dictionaryEncoding);
			}
			return new CalculationAsObjectGenerator(options, evaluator, typeEvaluator);
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00007B99 File Offset: 0x00005D99
		protected DsrWriterOptions Options { get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00007BA1 File Offset: 0x00005DA1
		protected ExpressionEvaluator Evaluator { get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00007BA9 File Offset: 0x00005DA9
		protected ExpressionTypeEvaluator TypeEvaluator { get; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00007BB1 File Offset: 0x00005DB1
		internal CalculationGenerationTelemetry Telemetry
		{
			get
			{
				return this._telemetry;
			}
		}

		// Token: 0x060002A9 RID: 681
		internal abstract void Process(string parentId, IList<Calculation> calculations, ICalculationContainerWriter writer);

		// Token: 0x060002AA RID: 682 RVA: 0x00007BBC File Offset: 0x00005DBC
		protected void ProcessCalculations<T>(IList<Calculation> calculations, IList<Type> calculationTypes, CollectionWriter<T> calculationsWriter, bool skipNullCalculations) where T : StreamingDsrWriterWrapperBase, ICalculationWriter, new()
		{
			for (int i = 0; i < calculations.Count; i++)
			{
				this.ProcessCalculation<T>(i, calculations[i], (calculationTypes != null) ? calculationTypes[i] : null, calculationsWriter, skipNullCalculations);
			}
			calculationsWriter.End();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00007C00 File Offset: 0x00005E00
		private void ProcessCalculation<T>(int calcIndex, Calculation calc, Type calcType, CollectionWriter<T> writer, bool skipNullCalculations) where T : StreamingDsrWriterWrapperBase, ICalculationWriter, new()
		{
			object obj = this.Evaluator.Evaluate(calc.Value);
			if (obj == null && skipNullCalculations)
			{
				this.RegisterSkippedCalculation(calcIndex, obj);
				return;
			}
			T t = writer.BeginItem();
			this.WriteCalculation(calcIndex, calc.Id, calcType, obj, t);
			t.End();
		}

		// Token: 0x060002AC RID: 684
		protected abstract void WriteCalculation(int calcIndex, string calcId, Type calcType, object evaluatedCalcValue, ICalculationWriter calcWriter);

		// Token: 0x060002AD RID: 685 RVA: 0x00007C59 File Offset: 0x00005E59
		protected virtual void RegisterSkippedCalculation(int calcIndex, object evaluatedCalcValue)
		{
		}

		// Token: 0x0400019A RID: 410
		private readonly CalculationGenerationTelemetry _telemetry;
	}
}
