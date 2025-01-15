using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.Common.Json;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x02000072 RID: 114
	internal sealed class OptimizedCalculationGenerator : CalculationGenerator
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x00007C84 File Offset: 0x00005E84
		internal OptimizedCalculationGenerator(DsrWriterOptions options, ExpressionEvaluator evaluator, ExpressionTypeEvaluator typeEvaluator, DictionaryEncodingCoordinator dictEncoding)
			: base(options, evaluator, typeEvaluator, new CalculationGenerationTelemetry())
		{
			this._dictEncoding = dictEncoding;
			this._parentIdToCalcTypes = new Dictionary<string, IList<Type>>(StringComparer.Ordinal);
			this._nullValueEncoding = (options.WriteCalcsNullValueEncoded ? new NullValueEncodingCoordinator() : null);
			this._repeatedValueEncoding = (options.WriteCalcsRepeatedValueEncoded ? new RepeatedValueEncodingCoordinator() : null);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00007CE4 File Offset: 0x00005EE4
		internal override void Process(string parentId, IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation> calculations, ICalculationContainerWriter writer)
		{
			IList<Type> list;
			if (!this._parentIdToCalcTypes.TryGetValue(parentId, out list))
			{
				list = this.CollectAndProcessCalculationTypes(calculations, writer.BeginCalculationsSchema());
				this._parentIdToCalcTypes.Add(parentId, list);
			}
			this.InitFlagSequenceEncodings(parentId, calculations.Count);
			if (this.ShouldWriteCalcsAsProperties(calculations))
			{
				base.ProcessCalculations<CalculationAsPropertyWriter>(calculations, list, writer.BeginCalculationsAsProperties(), base.Options.SkipNullIntersectionAndCalculations);
			}
			else
			{
				base.ProcessCalculations<CalculationAsValueWriter>(calculations, list, writer.BeginCalculationsAsValuesArray(), false);
			}
			this.WriteFlagSequenceEncodings(writer.BeginFlagSequence());
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00007D68 File Offset: 0x00005F68
		private void WriteFlagSequenceEncodings(FlagSequenceWriter flagSequenceWriter)
		{
			if (this._repeatedValueEncoding != null && this._repeatedValueEncoding.HasRepeatedValues)
			{
				flagSequenceWriter.WriteProperty(base.Options.Names.RepeatedValues, this._repeatedValueEncoding.Flags);
			}
			if (this._nullValueEncoding != null && this._nullValueEncoding.HasNullValues)
			{
				flagSequenceWriter.WriteProperty(base.Options.Names.NullValues, this._nullValueEncoding.Flags);
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00007DE1 File Offset: 0x00005FE1
		private bool ShouldWriteCalcsAsProperties(IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation> calculations)
		{
			return (base.Options.WriteCalcsAsParentProperties && calculations.Count == 1) || !base.Options.WriteCalcsAsArrays;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00007E0C File Offset: 0x0000600C
		private List<Type> CollectAndProcessCalculationTypes(IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation> calculations, CollectionWriter<CalculationMetadataWriter> collectionWriter)
		{
			List<Type> list = new List<Type>(calculations.Count);
			for (int i = 0; i < calculations.Count; i++)
			{
				Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation calculation = calculations[i];
				CalculationMetadataWriter calculationMetadataWriter = collectionWriter.BeginItem();
				Type expressionType = base.TypeEvaluator.GetExpressionType(calculation.Value);
				list.Add(expressionType);
				string text = null;
				if (this._dictEncoding != null)
				{
					this._dictEncoding.TryAddNewDictionary(calculation.Id, expressionType, out text);
				}
				int num = expressionType.ToConceptualTypeCode();
				calculationMetadataWriter.WriteCalculationMetadata(calculation.Id, num, text);
				calculationMetadataWriter.End();
				base.Telemetry.AddCalcSchemaInfo(num, calculation.Id, text);
			}
			collectionWriter.End();
			return list;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00007EB9 File Offset: 0x000060B9
		private void InitFlagSequenceEncodings(string parentId, int calcCount)
		{
			if (this._repeatedValueEncoding != null)
			{
				this._repeatedValueEncoding.InitializeFlagSequence(parentId, calcCount);
			}
			if (this._nullValueEncoding != null)
			{
				this._nullValueEncoding.InitializeFlagSequence(calcCount);
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00007EE4 File Offset: 0x000060E4
		protected override void RegisterSkippedCalculation(int calcIndex, object evaluatedCalcValue)
		{
			if (this._repeatedValueEncoding != null)
			{
				this._repeatedValueEncoding.UpdatePreviousValue(calcIndex, evaluatedCalcValue);
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00007EFC File Offset: 0x000060FC
		protected override void WriteCalculation(int calcIndex, string calcId, Type calcColumnType, object evaluatedCalcValue, ICalculationWriter calcWriter)
		{
			if (this.TryHandleRepeatedValueEncoding(calcIndex, evaluatedCalcValue))
			{
				base.Telemetry.IncrementRepeatedValueCount(calcColumnType.ToConceptualTypeCode());
				return;
			}
			if (this.TryHandleNullValueEncoding(calcIndex, evaluatedCalcValue))
			{
				base.Telemetry.IncrementNullValueCount(calcColumnType.ToConceptualTypeCode());
				return;
			}
			if (evaluatedCalcValue == null)
			{
				calcWriter.WriteIdAndJsonEncodedValue(calcId, "null");
				return;
			}
			if (this.TryWriteWithDictionaryEncoding(calcId, evaluatedCalcValue, calcWriter))
			{
				return;
			}
			this.WriteWithTypeOptimizedEncoding(calcId, calcColumnType, evaluatedCalcValue, calcWriter);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00007F6F File Offset: 0x0000616F
		private bool TryHandleNullValueEncoding(int calcIndex, object evaluatedCalcValue)
		{
			return this._nullValueEncoding != null && this._nullValueEncoding.TryHandleValue(calcIndex, evaluatedCalcValue);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00007F88 File Offset: 0x00006188
		private bool TryHandleRepeatedValueEncoding(int calcIndex, object evaluatedCalcValue)
		{
			return this._repeatedValueEncoding != null && this._repeatedValueEncoding.TryHandleValue(calcIndex, evaluatedCalcValue);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00007FA4 File Offset: 0x000061A4
		private bool TryWriteWithDictionaryEncoding(string calcId, object evaluatedCalcValue, ICalculationWriter calcWriter)
		{
			if (this._dictEncoding == null)
			{
				return false;
			}
			int num;
			if (!this._dictEncoding.TryGetOrAddValue(calcId, evaluatedCalcValue, out num))
			{
				return false;
			}
			OptimizedCalculationGenerator.WriteIdAndValue(calcId, num, calcWriter);
			return true;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00007FD8 File Offset: 0x000061D8
		private void WriteWithTypeOptimizedEncoding(string calcId, Type calcColumnType, object evaluatedCalcValue, ICalculationWriter calcWriter)
		{
			switch (Type.GetTypeCode(calcColumnType))
			{
			case TypeCode.Object:
				if (calcColumnType == typeof(byte[]))
				{
					OptimizedCalculationGenerator.WriteCalculationAsSimpleOrTypeEncoded(calcId, evaluatedCalcValue, calcWriter, base.Options.CalculationValueEncodingOptions.ByteArrayEncoding);
					return;
				}
				if (calcColumnType == typeof(object))
				{
					calcWriter.WriteIdAndVariantValue(calcId, evaluatedCalcValue);
					return;
				}
				break;
			case TypeCode.Boolean:
			{
				DsrWriterValueEncodingOptions boolEncoding = base.Options.CalculationValueEncodingOptions.BoolEncoding;
				Action<string, bool, ICalculationWriter> action;
				if ((action = OptimizedCalculationGenerator.<>O.<0>__WriteIdAndOptimizedValue) == null)
				{
					action = (OptimizedCalculationGenerator.<>O.<0>__WriteIdAndOptimizedValue = new Action<string, bool, ICalculationWriter>(OptimizedCalculationGenerator.WriteIdAndOptimizedValue));
				}
				OptimizedCalculationGenerator.WriteCalculation<bool>(calcId, evaluatedCalcValue, calcWriter, boolEncoding, action);
				return;
			}
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
				OptimizedCalculationGenerator.WriteIdAndValue(calcId, (int)evaluatedCalcValue, calcWriter);
				return;
			case TypeCode.Int64:
				OptimizedCalculationGenerator.WriteCalculation<long>(calcId, evaluatedCalcValue, calcWriter, base.Options.CalculationValueEncodingOptions.LongEncoding, new Action<string, long, ICalculationWriter>(this.WriteIdAndOptimizedValue));
				return;
			case TypeCode.Double:
				OptimizedCalculationGenerator.WriteCalculation<double>(calcId, evaluatedCalcValue, calcWriter, base.Options.CalculationValueEncodingOptions.DoubleEncoding, new Action<string, double, ICalculationWriter>(this.WriteIdAndOptimizedValue));
				return;
			case TypeCode.Decimal:
				OptimizedCalculationGenerator.WriteCalculation<decimal>(calcId, evaluatedCalcValue, calcWriter, base.Options.CalculationValueEncodingOptions.DecimalEncoding, new Action<string, decimal, ICalculationWriter>(this.WriteIdAndOptimizedValue));
				return;
			case TypeCode.DateTime:
				OptimizedCalculationGenerator.WriteCalculation<DateTime>(calcId, evaluatedCalcValue, calcWriter, base.Options.CalculationValueEncodingOptions.DateTimeEncoding, new Action<string, DateTime, ICalculationWriter>(this.WriteIdAndOptimizedValue));
				return;
			case TypeCode.String:
				OptimizedCalculationGenerator.WriteCalculationAsSimpleOrTypeEncoded(calcId, evaluatedCalcValue, calcWriter, base.Options.CalculationValueEncodingOptions.TextEncoding);
				return;
			}
			Contract.RetailFail(string.Format("Attempt to write value of unsupported type {0}", calcColumnType));
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00008190 File Offset: 0x00006390
		private static void WriteCalculation<T>(string calcId, object value, ICalculationWriter calcWriter, DsrWriterValueEncodingOptions encodingOption, Action<string, T, ICalculationWriter> writeIdAndOptimizedValue)
		{
			if (encodingOption == DsrWriterValueEncodingOptions.Optimized)
			{
				T t;
				try
				{
					t = (T)((object)value);
				}
				catch (InvalidCastException ex)
				{
					Type type = value.GetType();
					Type typeFromHandle = typeof(T);
					throw new ProcessingException("DataExtensionInvalidOutputError", ProcessingErrorMessages.WrongCalculationValueType(calcId, typeFromHandle, type), ex, ErrorSource.PowerBI);
				}
				writeIdAndOptimizedValue(calcId, t, calcWriter);
				return;
			}
			OptimizedCalculationGenerator.WriteCalculationAsSimpleOrTypeEncoded(calcId, value, calcWriter, encodingOption);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000081F8 File Offset: 0x000063F8
		private static void WriteCalculationAsSimpleOrTypeEncoded(string calcId, object value, ICalculationWriter calcWriter, DsrWriterValueEncodingOptions encodingOption)
		{
			if (encodingOption == DsrWriterValueEncodingOptions.TypeEncoded)
			{
				calcWriter.WriteIdAndVariantValue(calcId, value);
				return;
			}
			if (encodingOption == DsrWriterValueEncodingOptions.SimpleEncoded)
			{
				calcWriter.WriteIdAndSimpleValue(calcId, value);
				return;
			}
			Contract.RetailFail(string.Format("Attempt to write value with unsupported encoding option '{0}'", encodingOption));
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008228 File Offset: 0x00006428
		private void WriteIdAndOptimizedValue(string calcId, long value, ICalculationWriter calcWriter)
		{
			if (base.Options.CalculationValueEncodingOptions.LongFallbackAnalyzer.ShouldFallbackToSimple(value))
			{
				calcWriter.WriteIdAndSimpleValue(calcId, value);
				base.Telemetry.IncrementTypeFallbackCount(CalculationGenerationTelemetry.LongTypeCode);
				return;
			}
			calcWriter.WriteIdAndJsonEncodedValue(calcId, JsonValueUtils.ToString(value));
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008278 File Offset: 0x00006478
		private void WriteIdAndOptimizedValue(string calcId, double value, ICalculationWriter calcWriter)
		{
			string text = JsonValueUtils.ToString(value);
			if (base.Options.CalculationValueEncodingOptions.DoubleFallbackAnalyzer.ShouldFallbackToSimple(value, text))
			{
				calcWriter.WriteIdAndJsonEncodedString(calcId, text);
				base.Telemetry.IncrementTypeFallbackCount(CalculationGenerationTelemetry.DoubleTypeCode);
				return;
			}
			calcWriter.WriteIdAndJsonEncodedValue(calcId, text);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000082C8 File Offset: 0x000064C8
		private void WriteIdAndOptimizedValue(string calcId, decimal value, ICalculationWriter calcWriter)
		{
			string text = JsonValueUtils.ToString(value);
			if (base.Options.CalculationValueEncodingOptions.DecimalFallbackAnalyzer.ShouldFallbackToSimple(value, text))
			{
				calcWriter.WriteIdAndJsonEncodedString(calcId, text);
				base.Telemetry.IncrementTypeFallbackCount(CalculationGenerationTelemetry.DecimalTypeCode);
				return;
			}
			calcWriter.WriteIdAndJsonEncodedValue(calcId, text);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008318 File Offset: 0x00006518
		private static void WriteIdAndOptimizedValue(string calcId, bool value, ICalculationWriter calcWriter)
		{
			int num = (value ? 1 : 0);
			calcWriter.WriteIdAndJsonEncodedValue(calcId, JsonValueUtils.ToString(num));
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000833C File Offset: 0x0000653C
		private static void WriteIdAndValue(string calcId, int value, ICalculationWriter calcWriter)
		{
			string text = JsonValueUtils.ToString(value);
			calcWriter.WriteIdAndJsonEncodedValue(calcId, text);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008358 File Offset: 0x00006558
		private void WriteIdAndOptimizedValue(string calcId, DateTime value, ICalculationWriter calcWriter)
		{
			long num;
			if (DateTimeToUnixMillisecondsConverter.TryConvert(value, true, out num))
			{
				string text = JsonValueUtils.ToString(num);
				calcWriter.WriteIdAndJsonEncodedValue(calcId, text);
				return;
			}
			calcWriter.WriteIdAndSimpleValue(calcId, value);
			base.Telemetry.IncrementTypeFallbackCount(CalculationGenerationTelemetry.DateTimeTypeCode);
		}

		// Token: 0x0400019E RID: 414
		private readonly IDictionary<string, IList<Type>> _parentIdToCalcTypes;

		// Token: 0x0400019F RID: 415
		private readonly RepeatedValueEncodingCoordinator _repeatedValueEncoding;

		// Token: 0x040001A0 RID: 416
		private readonly NullValueEncodingCoordinator _nullValueEncoding;

		// Token: 0x040001A1 RID: 417
		private readonly DictionaryEncodingCoordinator _dictEncoding;

		// Token: 0x020000EA RID: 234
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000355 RID: 853
			public static Action<string, bool, ICalculationWriter> <0>__WriteIdAndOptimizedValue;
		}
	}
}
