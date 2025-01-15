using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Build;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Split.Translation;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B54 RID: 6996
	internal class DataWranglingOperationTranslator
	{
		// Token: 0x0600E5B5 RID: 58805 RVA: 0x0030A798 File Offset: 0x00308998
		public static DataWranglingOperationTranslation Translate(Program program, ITable<object> inputTable)
		{
			DataWranglingOperation dataWranglingOperation = null;
			TTableProgram ttableProgram;
			if (!Language.Build.Node.IsRule.TTableProgram(program.ProgramNode, out ttableProgram))
			{
				return null;
			}
			LabelEncode labelEncode;
			CastColumn castColumn;
			FillMissingValues fillMissingValues;
			AddSplitColumns addSplitColumns;
			OneHotEncode oneHotEncode;
			Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer multiLabelBinarizer;
			DropColumn dropColumn;
			DropRows dropRows;
			Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson addColumnsFromJson;
			if (Language.Build.Node.IsRule.LabelEncode(ttableProgram.table.Node, out labelEncode))
			{
				dataWranglingOperation = DataWranglingOperationTranslator.TranslateLabelEncode(labelEncode, inputTable);
			}
			else if (Language.Build.Node.IsRule.CastColumn(ttableProgram.table.Node, out castColumn))
			{
				dataWranglingOperation = DataWranglingOperationTranslator.TranslateCastColumn(castColumn);
			}
			else if (Language.Build.Node.IsRule.FillMissingValues(ttableProgram.table.Node, out fillMissingValues))
			{
				dataWranglingOperation = DataWranglingOperationTranslator.TranslateFillMissingValues(fillMissingValues);
			}
			else if (Language.Build.Node.IsRule.AddSplitColumns(ttableProgram.table.Node, out addSplitColumns))
			{
				dataWranglingOperation = DataWranglingOperationTranslator.TranslateAddSplitColumns(addSplitColumns, inputTable);
			}
			else if (Language.Build.Node.IsRule.OneHotEncode(ttableProgram.table.Node, out oneHotEncode))
			{
				dataWranglingOperation = DataWranglingOperationTranslator.TranslateOneHotEncode(oneHotEncode, inputTable);
			}
			else if (Language.Build.Node.IsRule.MultiLabelBinarizer(ttableProgram.table.Node, out multiLabelBinarizer))
			{
				dataWranglingOperation = DataWranglingOperationTranslator.TranslateMultiLabelBinarizer(multiLabelBinarizer, inputTable);
			}
			else if (Language.Build.Node.IsRule.DropColumn(ttableProgram.table.Node, out dropColumn))
			{
				dataWranglingOperation = DataWranglingOperationTranslator.TranslateDropColumn(dropColumn);
			}
			else if (Language.Build.Node.IsRule.DropRows(ttableProgram.table.Node, out dropRows))
			{
				dataWranglingOperation = DataWranglingOperationTranslator.TranslateDropRows(dropRows, inputTable);
			}
			else if (Language.Build.Node.IsRule.AddColumnsFromJson(ttableProgram.table.Node, out addColumnsFromJson))
			{
				dataWranglingOperation = DataWranglingOperationTranslator.TranslateAddColumnsFromJson(addColumnsFromJson, inputTable);
			}
			return new DataWranglingOperationTranslation(program, dataWranglingOperation);
		}

		// Token: 0x0600E5B6 RID: 58806 RVA: 0x0030A99C File Offset: 0x00308B9C
		private static DataWranglingOperation TranslateLabelEncode(LabelEncode labelEncode, ITable<object> inputTable)
		{
			string text = "_";
			string text2 = "label_encoded";
			while (inputTable.ColumnNames.Contains(labelEncode.sourceColumnName.Value + text + text2))
			{
				text += "_";
			}
			string text3 = labelEncode.sourceColumnName.Value + text + text2;
			return new DataWranglingOperation(OperationId.LabelEncode, null, new string[] { labelEncode.sourceColumnName.Value }, new string[] { text3 });
		}

		// Token: 0x0600E5B7 RID: 58807 RVA: 0x0030AA28 File Offset: 0x00308C28
		private static DataWranglingOperation TranslateAddSplitColumns(AddSplitColumns AddSplitColumns, ITable<object> inputTable)
		{
			SplitColumn splitColumn;
			Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split split;
			SelectColumnToSplit selectColumnToSplit;
			SplitRegion splitRegion;
			if (!Language.Build.Node.IsRule.SplitColumn(AddSplitColumns.newColumns.Node, out splitColumn) || !Language.Build.Node.IsRule.Split(splitColumn.splitCell.Node, out split) || !Language.Build.Node.IsRule.SelectColumnToSplit(splitColumn.columnToSplit.Node, out selectColumnToSplit) || !DataWranglingOperationTranslator.SplitBuilder.Node.IsRule.SplitRegion(split.regionSplit.Node, out splitRegion))
			{
				return null;
			}
			SplitParameters splitParameters = DataWranglingOperationTranslator.TranslateSplitRegion(splitRegion);
			if (splitParameters != null)
			{
				return new DataWranglingOperation(OperationId.Split, splitParameters, new string[] { selectColumnToSplit.sourceColumnName.Value }, null);
			}
			return new DataWranglingOperation(OperationId.CustomSplit, new CustomSplitParameters(), new string[] { selectColumnToSplit.sourceColumnName.Value }, null);
		}

		// Token: 0x0600E5B8 RID: 58808 RVA: 0x0030AB34 File Offset: 0x00308D34
		private static DataWranglingOperation TranslateAddColumnsFromJson(Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson addColumnsFromJson, ITable<object> inputTable)
		{
			IEnumerable<string> enumerable = addColumnsFromJson.ejsonProgram.Value.ColumnNames.Select((string c) => Utilities.Uniquify(c, inputTable.ColumnNames.ToList<string>()));
			return new DataWranglingOperation(OperationId.AddColumnsFromJson, null, new string[] { addColumnsFromJson.sourceColumnName.Value }, enumerable);
		}

		// Token: 0x0600E5B9 RID: 58809 RVA: 0x0030AB94 File Offset: 0x00308D94
		private static DataWranglingOperation TranslateOneHotEncode(OneHotEncode oneHotEncode, ITable<object> inputTable)
		{
			string sourceColumnName = oneHotEncode.sourceColumnName.Value;
			IEnumerable<object> enumerable = (from d in inputTable.Column(sourceColumnName).Distinct<object>()
				where d != null
				select d).OrderBy((object x) => x.ToString(), StringComparer.Ordinal);
			string mid_sep = "_";
			while (inputTable.ColumnNames.Any((string colName) => colName.StartsWith(sourceColumnName + mid_sep)))
			{
				mid_sep += "_";
			}
			IEnumerable<string> enumerable2 = enumerable.Select((object category) => sourceColumnName + mid_sep + category.ToString());
			return new DataWranglingOperation(OperationId.OneHotEncode, null, new string[] { sourceColumnName }, enumerable2);
		}

		// Token: 0x0600E5BA RID: 58810 RVA: 0x0030AC84 File Offset: 0x00308E84
		private static DataWranglingOperation TranslateMultiLabelBinarizer(Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer multiLabelBinarizer, ITable<object> inputTable)
		{
			string sourceColumnName = multiLabelBinarizer.sourceColumnName.Value;
			IEnumerable<string> enumerable = (from d in inputTable.Column(sourceColumnName)
				where d != null
				select d).SelectMany((object d) => from a in d.ToString().Split(new string[] { multiLabelBinarizer.delimiter.Value }, StringSplitOptions.RemoveEmptyEntries)
				select a.Trim() into a
				where a.Length > 0
				select a).Distinct<string>().OrderBy((string d) => d.ToString(), StringComparer.Ordinal);
			string mid_sep = "_";
			while (inputTable.ColumnNames.Any((string colName) => colName.StartsWith(sourceColumnName + mid_sep)))
			{
				mid_sep += "_";
			}
			IEnumerable<string> enumerable2 = enumerable.Select((string category) => sourceColumnName + mid_sep + category.ToString());
			return new DataWranglingOperation(OperationId.MultiLabelBinarizer, new SplitParameters(multiLabelBinarizer.delimiter.Value, -1, false), new string[] { sourceColumnName }, enumerable2);
		}

		// Token: 0x0600E5BB RID: 58811 RVA: 0x0030ADA8 File Offset: 0x00308FA8
		private static DataWranglingOperation TranslateDropColumn(DropColumn dropColumn)
		{
			return new DataWranglingOperation(OperationId.Drop, new DropParameter(DataWranglingOperationTranslator.TranslateDropCondition(dropColumn.dropCondition.Value)), new string[] { dropColumn.sourceColumnName.Value }, null);
		}

		// Token: 0x0600E5BC RID: 58812 RVA: 0x0030ADF0 File Offset: 0x00308FF0
		private static SplitParameters TranslateSplitRegion(SplitRegion splitRegion)
		{
			if (splitRegion.ignoreIndexes.Value.Length != 0)
			{
				return null;
			}
			int num = splitRegion.numSplits.Value - 1;
			splitMatches_constantDelimiterMatches splitMatches_constantDelimiterMatches;
			splitMatches_multipleMatches splitMatches_multipleMatches;
			if (DataWranglingOperationTranslator.SplitBuilder.Node.IsRule.splitMatches_constantDelimiterMatches(splitRegion.splitMatches.Node, out splitMatches_constantDelimiterMatches))
			{
				ConstantDelimiter constantDelimiter;
				if (splitMatches_constantDelimiterMatches.constantDelimiterMatches.Is_ConstantDelimiter(DataWranglingOperationTranslator.SplitBuilder, out constantDelimiter))
				{
					return new SplitParameters(constantDelimiter.s.Value, num, false);
				}
			}
			else if (DataWranglingOperationTranslator.SplitBuilder.Node.IsRule.splitMatches_multipleMatches(splitRegion.splitMatches.Node, out splitMatches_multipleMatches))
			{
				Optional<IReadOnlyList<Delimiter>> optional = DelimiterCollector.MaybeCollectConstantDelimiters(splitMatches_multipleMatches.Node);
				if (!optional.HasValue)
				{
					return null;
				}
				IReadOnlyList<Delimiter> value = optional.Value;
				if (splitRegion.delimiterStart.Value || splitRegion.delimiterEnd.Value)
				{
					return null;
				}
				string text;
				bool flag;
				if (value.Count > 1)
				{
					IEnumerable<string> enumerable = value.Select(delegate(Delimiter delimiter)
					{
						if (!delimiter.IsRegex)
						{
							return Regex.Escape(delimiter.DelimiterString);
						}
						return delimiter.DelimiterString;
					});
					text = string.Join("|", enumerable);
					flag = true;
				}
				else
				{
					Delimiter delimiter2 = value[0];
					text = delimiter2.DelimiterString;
					flag = delimiter2.IsRegex;
				}
				return new SplitParameters(text, num, flag);
			}
			return null;
		}

		// Token: 0x0600E5BD RID: 58813 RVA: 0x0030AF5C File Offset: 0x0030915C
		private static DataWranglingOperation TranslateDropRows(DropRows dropRows, ITable<object> input)
		{
			MissingCondition missingCondition = dropRows.dropCondition.Value as MissingCondition;
			if (missingCondition != null)
			{
				if (missingCondition.MissingValueFraction == 1.0)
				{
					return new DataWranglingOperation(OperationId.DropNa, new DropNaParameters(Axis.Index, new DropNaHow?(DropNaHow.All), null), null, null);
				}
				if (missingCondition.MissingValueFraction == 0.0)
				{
					return new DataWranglingOperation(OperationId.DropNa, new DropNaParameters(Axis.Index, new DropNaHow?(DropNaHow.Any), null), null, null);
				}
				if (missingCondition.MissingValueFraction > 0.0 && missingCondition.MissingValueFraction < 1.0)
				{
					return new DataWranglingOperation(OperationId.DropNa, new DropNaParameters(Axis.Index, null, new int?((int)Math.Floor(missingCondition.MissingValueFraction * (double)input.ColumnNames.Count<string>()))), null, null);
				}
			}
			if (dropRows.dropCondition.Value is DuplicateCondition)
			{
				return new DataWranglingOperation(OperationId.DropDuplicates, null, null, null);
			}
			OutlierCondition outlierCondition = dropRows.dropCondition.Value as OutlierCondition;
			if (outlierCondition != null)
			{
				return new DataWranglingOperation(OperationId.DropOutliers, new DropOutlierParameters(outlierCondition.SourceColumnName, outlierCondition.ValidBoundExclusive), null, null);
			}
			return null;
		}

		// Token: 0x0600E5BE RID: 58814 RVA: 0x0030B090 File Offset: 0x00309290
		private static DropCondition TranslateDropCondition(DropCondition dropCondition)
		{
			MissingCondition missingCondition = dropCondition as MissingCondition;
			DropCondition dropCondition2;
			if (missingCondition == null)
			{
				DuplicateCondition duplicateCondition = dropCondition as DuplicateCondition;
				if (duplicateCondition == null)
				{
					ConstantCondition constantCondition = dropCondition as ConstantCondition;
					if (constantCondition == null)
					{
						dropCondition2 = new DropCondition((DropReason)dropCondition.DropReason);
					}
					else
					{
						dropCondition2 = new ConstantCondition(constantCondition.ConstantValue);
					}
				}
				else
				{
					dropCondition2 = new DuplicateCondition(duplicateCondition.OriginalValueId);
				}
			}
			else
			{
				dropCondition2 = new MissingCondition(missingCondition.MissingValueFraction, (MissingValueType)missingCondition.MissingValueTypes);
			}
			return dropCondition2;
		}

		// Token: 0x0600E5BF RID: 58815 RVA: 0x0030B0FC File Offset: 0x003092FC
		private static DataWranglingOperation TranslateFillMissingValues(FillMissingValues fillMissingValues)
		{
			string value = fillMissingValues.sourceColumnName.Value;
			if (value == null)
			{
				return null;
			}
			return new DataWranglingOperation(OperationId.FillNa, new FillNaParameters(fillMissingValues.fillValue.Value, fillMissingValues.missingValueMarkers.Value, (FillMethod)fillMissingValues.fillMethod.Value), new string[] { value }, null);
		}

		// Token: 0x0600E5C0 RID: 58816 RVA: 0x0030B164 File Offset: 0x00309364
		private static DataWranglingOperation TranslateCastColumn(CastColumn castColumn)
		{
			IRichDataType value = castColumn.richDataType.Value;
			RichNumericType richNumericType = value as RichNumericType;
			DataType? dataType;
			if (richNumericType == null)
			{
				if (!(value is RichDateType))
				{
					if (!(value is RichBooleanType))
					{
						if (!(value is RichCategoricalType))
						{
							dataType = null;
						}
						else
						{
							dataType = new DataType?(DataType.Category);
						}
					}
					else
					{
						dataType = new DataType?(DataType.Bool);
					}
				}
				else
				{
					dataType = new DataType?(DataType.DateTime64);
				}
			}
			else
			{
				dataType = new DataType?(richNumericType.ContainsRealSubtype ? DataType.Float : DataType.Int);
			}
			DataType? dataType2 = dataType;
			if (dataType2 == null)
			{
				return null;
			}
			return new DataWranglingOperation(OperationId.ChangeType, new ChangeTypeParameters(dataType2.Value), new string[] { castColumn.sourceColumnName.Value }, null);
		}

		// Token: 0x04005738 RID: 22328
		private static readonly GrammarBuilders SplitBuilder = GrammarBuilders.Instance(Language.Grammar);
	}
}
