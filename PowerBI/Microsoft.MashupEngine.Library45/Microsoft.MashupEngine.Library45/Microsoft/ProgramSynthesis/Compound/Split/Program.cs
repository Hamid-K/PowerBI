using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Compound.Split.Build;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.Compound.Split.Semantics;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Build;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split
{
	// Token: 0x02000912 RID: 2322
	public class Program : TableProgram<StringRegion>
	{
		// Token: 0x060031F9 RID: 12793 RVA: 0x00093372 File Offset: 0x00091572
		public Program(topSplit program)
			: this(program, null, null, null, 0, false)
		{
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x00093380 File Offset: 0x00091580
		public Program(ProgramProperties prop)
			: this(Program.ProgramFromProperties(prop), prop.RawColumnNames, prop.NewLineStrings, prop.Telemetry, prop.SkipEmptyAndCommentLinesCount, prop.HasEmptyLines)
		{
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000933AC File Offset: 0x000915AC
		internal Program(topSplit topSplit, IReadOnlyList<string> columnNames, IReadOnlyList<string> newLines, IReadOnlyList<InputLineTelemetry> telemetry, int skipEmptyAndCommentLinesCount, bool hasEmptyLines)
			: base(topSplit.Node, 0.0)
		{
			splitLines splitLines;
			SplitProgram splitProgram;
			splitRecords splitRecords;
			this.Properties = Program.GetProgramPartsAndProperties(this.TopSplitProgram, columnNames, skipEmptyAndCommentLinesCount, hasEmptyLines, newLines, telemetry, out splitLines, out splitProgram, out splitRecords);
			this.SplitFileProgram = splitLines;
			this.SplitTextProgram = splitProgram;
			this.MultiRecordSplitProgram = splitRecords;
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060031FC RID: 12796 RVA: 0x00093403 File Offset: 0x00091603
		public topSplit TopSplitProgram
		{
			get
			{
				return Language.Build.Node.Cast.topSplit(base.ProgramNode);
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x0009341F File Offset: 0x0009161F
		public splitLines SplitFileProgram { get; }

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x00093427 File Offset: 0x00091627
		public SplitProgram SplitTextProgram { get; }

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060031FF RID: 12799 RVA: 0x0009342F File Offset: 0x0009162F
		public splitRecords MultiRecordSplitProgram { get; }

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x00093437 File Offset: 0x00091637
		public ProgramProperties Properties { get; }

		// Token: 0x06003201 RID: 12801 RVA: 0x00093440 File Offset: 0x00091640
		private static topSplit ProgramFromProperties(ProgramProperties prop)
		{
			if (prop.ProgramType == ProgramType.MultiRecord || prop.ProgramType == ProgramType.Other)
			{
				throw new NotSupportedException(string.Format("{0} programs cannot be constructed from properties", prop.ProgramType));
			}
			Microsoft.ProgramSynthesis.Compound.Split.Build.GrammarBuilders build = Language.Build;
			splitRecords splitRecords;
			if (prop.ColumnDelimiter == null && prop.FieldPositions == null)
			{
				splitRecords = build.Node.Rule.NoSplit(build.Node.Variable.records, build.Node.Rule.hasHeader(prop.HasHeader));
			}
			else
			{
				if (prop.ColumnDelimiter != null && prop.FieldPositions != null)
				{
					throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot have both {0} and {1}", new object[] { "ColumnDelimiter", "FieldPositions" })), "prop");
				}
				regionSplit regionSplit = default(regionSplit);
				if (prop.ColumnDelimiter != null)
				{
					constantDelimiterMatches constantDelimiterMatches = ((prop.QuotingConf.Style == QuotingStyle.Adaptive) ? Program.SplitTextBuilder.Node.Rule.ConstantDelimiter(Program.SplitTextBuilder.Node.Variable.v, Program.SplitTextBuilder.Node.Rule.s(prop.ColumnDelimiter)) : Program.SplitTextBuilder.Node.Rule.ConstantDelimiterWithQuoting(Program.SplitTextBuilder.Node.Variable.v, Program.SplitTextBuilder.Node.Rule.s(prop.ColumnDelimiter), Program.SplitTextBuilder.Node.Rule.quotingConf(prop.QuotingConf)));
					splitMatches splitMatches = Program.SplitTextBuilder.Node.UnnamedConversion.splitMatches_constantDelimiterMatches(constantDelimiterMatches);
					regionSplit = Program.SplitTextBuilder.Node.Rule.SplitRegion(Program.SplitTextBuilder.Node.Variable.v, splitMatches, Program.SplitTextBuilder.Node.Rule.ignoreIndexes(new int[0]), Program.SplitTextBuilder.Node.Rule.numSplits(prop.ColumnCount), Program.SplitTextBuilder.Node.Rule.delimiterStart(false), Program.SplitTextBuilder.Node.Rule.delimiterEnd(false), Program.SplitTextBuilder.Node.Rule.includeDelimiters(false), Program.SplitTextBuilder.Node.Rule.fillStrategy(FillStrategy.LeftToRight));
				}
				else if (prop.FieldPositions != null)
				{
					regionSplit = Witnesses.CreateRegionSplitFromFieldPositions(Program.SplitTextBuilder, prop.FieldPositions);
				}
				splitTextProg splitTextProg = build.Node.Rule.SplitTextProg(regionSplit);
				delimiterSplit delimiterSplit = build.Node.Rule.SplitToCells(splitTextProg, build.Node.Variable.records);
				splitRecords = build.Node.Rule.TableFromCells(delimiterSplit, build.Node.Rule.hasHeader(prop.HasHeader));
			}
			Microsoft.ProgramSynthesis.Compound.Split.Build.GrammarBuilders.Nodes.NodeRules rule = build.Node.Rule;
			string columnDelimiter = prop.ColumnDelimiter;
			Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter delimiter = rule.delimiter((columnDelimiter != null) ? columnDelimiter.Some<string>() : Optional<string>.Nothing);
			allRecords allRecords = ((prop.QuotingConf.Style == QuotingStyle.Adaptive) ? build.Node.UnnamedConversion.allRecords_allLines(build.Node.Variable.allLines) : build.Node.Rule.QuoteRecords(build.Node.Rule.quotingConfig(prop.QuotingConf), delimiter, build.Node.Variable.allLines));
			skippedFooter skippedFooter = ((prop.SkipFooterLinesCount > 0) ? build.Node.Rule.SkipFooter(build.Node.Rule.k(prop.SkipFooterLinesCount), allRecords) : build.Node.UnnamedConversion.skippedFooter_allRecords(allRecords));
			skippedRecords skippedRecords = ((prop.SkipLinesCount > 0) ? build.Node.Rule.Skip(build.Node.Rule.k(prop.SkipLinesCount), build.Node.Rule.headerIndex(prop.HeaderIndex), skippedFooter) : build.Node.UnnamedConversion.skippedRecords_skippedFooter(skippedFooter));
			string commentStr = prop.CommentStr;
			Optional<string> optional = ((commentStr != null) ? commentStr.Some<string>() : Optional<string>.Nothing);
			dataLines dataLines = ((prop.FilterEmptyLines || optional.HasValue) ? build.Node.Rule.FilterRecords(build.Node.Rule.skipEmpty(prop.FilterEmptyLines), delimiter, build.Node.Rule.commentStr(optional), build.Node.Rule.hasCommentHeader(prop.HasCommentHeader), skippedRecords) : build.Node.UnnamedConversion.dataLines_skippedRecords(skippedRecords));
			splitLines splitLines = build.Node.Rule.SplitSequenceLet(dataLines, build.Node.Rule.Sequence(build.Node.Variable.ls));
			splitFile splitFile = build.Node.Rule.LetSplitFile(build.Node.Rule.SplitFile(build.Node.Variable.file), build.Node.Rule.MergeRecordLines(splitLines));
			splitRecordsSelect splitRecordsSelect = ((prop.SelectedColumns == null) ? build.Node.UnnamedConversion.splitRecordsSelect_splitRecords(splitRecords) : build.Node.Rule.SelectColumns(build.Node.Rule.columnList(prop.SelectedColumns.ToArray<int>()), splitRecords));
			return build.Node.Rule.LetFileRecordSplit(splitFile, splitRecordsSelect);
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000939B8 File Offset: 0x00091BB8
		private static ProgramProperties GetProgramPartsAndProperties(topSplit topSplit, IReadOnlyList<string> columnNames, int skipEmptyAndCommentLinesCount, bool hasEmptyLines, IReadOnlyList<string> newLines, IReadOnlyList<InputLineTelemetry> lineTelemetry, out splitLines splitFileProgram, out SplitProgram splitTextProgram, out splitRecords multiRecordProgram)
		{
			Microsoft.ProgramSynthesis.Compound.Split.Build.GrammarBuilders build = Language.Build;
			ProgramType programType = ProgramType.Other;
			splitTextProgram = null;
			multiRecordProgram = default(splitRecords);
			LetFileRecordSplit letFileRecordSplit = topSplit.Cast_LetFileRecordSplit();
			splitFileProgram = letFileRecordSplit.splitFile.Cast_LetSplitFile()._LetB1.Cast_MergeRecordLines().splitLines;
			IReadOnlyList<int> readOnlyList = null;
			SelectColumns selectColumns;
			splitRecords splitRecords;
			if (letFileRecordSplit.splitRecordsSelect.Is_SelectColumns(Language.Build, out selectColumns))
			{
				readOnlyList = selectColumns.columnList.Value;
				splitRecords = selectColumns.splitRecords;
			}
			else
			{
				splitRecordsSelect_splitRecords splitRecordsSelect_splitRecords;
				if (!letFileRecordSplit.splitRecordsSelect.Is_splitRecordsSelect_splitRecords(Language.Build, out splitRecordsSelect_splitRecords))
				{
					throw new Exception(string.Format("Unhandled case: {0}", letFileRecordSplit.Node.GrammarRule));
				}
				splitRecords = splitRecordsSelect_splitRecords.splitRecords;
			}
			string text = null;
			IReadOnlyList<Record<int, int?>> readOnlyList2 = null;
			int num = 1;
			TableFromCells tableFromCells;
			MultiRecordSplit multiRecordSplit;
			if (splitRecords.Is_TableFromCells(Language.Build, out tableFromCells))
			{
				splitTextProgram = new SplitProgram(tableFromCells.delimiterSplit.Cast_SplitToCells().splitTextProg.Cast_SplitTextProg().regionSplit);
				text = splitTextProgram.Properties.Delimiter;
				if (text != null)
				{
					programType = ProgramType.SimpleDelimiter;
				}
				readOnlyList2 = splitTextProgram.Properties.FieldPositions;
				if (readOnlyList2 != null)
				{
					programType = ProgramType.FixedWidth;
				}
				num = splitTextProgram.Properties.ColumnCount;
			}
			else if (splitRecords.Is_MultiRecordSplit(Language.Build, out multiRecordSplit))
			{
				multiRecordProgram = multiRecordSplit;
				programType = ProgramType.MultiRecord;
				columnNames = Program.GetColumnNames(multiRecordProgram.Cast_MultiRecordSplit(build).multiRecordSplit.Cast_LetMultiRecordSplit().mapColumnSelectors.Cast_MapColumnSelector().columnSelectorList);
				num = columnNames.Count;
			}
			else if (splitRecords.Is_NoSplit(build))
			{
				programType = ProgramType.SimpleOneColumn;
			}
			RegularExpression regularExpression = null;
			SplitSequenceLet splitSequenceLet = splitFileProgram.Cast_SplitSequenceLet();
			if (!splitSequenceLet.splitSequence.Is_Sequence(build))
			{
				SplitSequence splitSequence;
				if (!splitSequenceLet.splitSequence.Is_SplitSequence(build, out splitSequence))
				{
					throw new Exception(string.Format("Unhandled case: {0}", splitSequenceLet.splitSequence));
				}
				programType = ProgramType.Other;
				regularExpression = splitSequence.r.Value;
			}
			dataLines dataLines = splitSequenceLet.dataLines;
			RegularExpression regularExpression2 = null;
			RegularExpression regularExpression3 = null;
			bool flag = false;
			string text2 = null;
			bool flag2 = false;
			FilterHeader filterHeader;
			skippedRecords skippedRecords;
			SelectData selectData;
			FilterRecords filterRecords;
			if (dataLines.Is_FilterHeader(build, out filterHeader))
			{
				if (programType != ProgramType.SimpleDelimiter)
				{
					programType = ProgramType.Other;
				}
				skippedRecords = filterHeader.skippedRecords;
				regularExpression2 = filterHeader.basicLinePredicate.Cast_StartsWith().r.Value;
			}
			else if (dataLines.Is_SelectData(build, out selectData))
			{
				if (programType != ProgramType.SimpleDelimiter)
				{
					programType = ProgramType.Other;
				}
				skippedRecords = selectData.skippedRecords;
				regularExpression3 = selectData.basicLinePredicate.Cast_StartsWith().r.Value;
			}
			else if (dataLines.Is_FilterRecords(build, out filterRecords))
			{
				flag = filterRecords.skipEmpty.Value;
				text2 = (filterRecords.commentStr.Value.HasValue ? filterRecords.commentStr.Value.Value : null);
				flag2 = filterRecords.hasCommentHeader.Value;
				skippedRecords = filterRecords.skippedRecords;
			}
			else
			{
				dataLines_skippedRecords dataLines_skippedRecords;
				if (!dataLines.Is_dataLines_skippedRecords(build, out dataLines_skippedRecords))
				{
					throw new Exception(string.Format("Unhandled case: {0}", dataLines.Node.GrammarRule));
				}
				skippedRecords = dataLines_skippedRecords.skippedRecords;
			}
			Optional<int> optional = Optional<int>.Nothing;
			int num2 = 0;
			Skip skip;
			skippedFooter skippedFooter;
			if (skippedRecords.Is_Skip(build, out skip))
			{
				optional = skip.headerIndex.Value.Some<Optional<int>>();
				num2 = skip.k.Value;
				skippedFooter = skip.skippedFooter;
			}
			else
			{
				skippedRecords_skippedFooter skippedRecords_skippedFooter;
				if (!skippedRecords.Is_skippedRecords_skippedFooter(build, out skippedRecords_skippedFooter))
				{
					throw new Exception(string.Format("Unhandled case: {0}", skippedRecords.Node.GrammarRule));
				}
				skippedFooter = skippedRecords_skippedFooter.skippedFooter;
			}
			int num3 = 0;
			SkipFooter skipFooter;
			allRecords allRecords;
			if (skippedFooter.Is_SkipFooter(build, out skipFooter))
			{
				allRecords = skipFooter.allRecords;
				num3 = skipFooter.k.Value;
			}
			else
			{
				skippedFooter_allRecords skippedFooter_allRecords;
				if (!skippedFooter.Is_skippedFooter_allRecords(build, out skippedFooter_allRecords))
				{
					throw new Exception(string.Format("Unhandled case: {0}", skippedFooter.Node.GrammarRule));
				}
				allRecords = skippedFooter_allRecords.allRecords;
			}
			QuoteRecords quoteRecords;
			QuotingConfiguration quotingConfiguration;
			if (allRecords.Is_QuoteRecords(build, out quoteRecords))
			{
				quotingConfiguration = quoteRecords.quotingConfig.Value;
				SplitProgram splitProgram = splitTextProgram;
				if (((splitProgram != null) ? splitProgram.Properties.Delimiter : null) != null && !quotingConfiguration.Equals(splitTextProgram.Properties.QuotingConfiguration))
				{
					throw new Exception("Mismatching quoting configurations");
				}
			}
			else
			{
				if (!allRecords.Is_allRecords_allLines(build))
				{
					throw new Exception(string.Format("Unhandled case: {0}", allRecords.Node.GrammarRule));
				}
				SplitProgram splitProgram2 = splitTextProgram;
				QuotingConfiguration? quotingConfiguration2 = ((splitProgram2 != null) ? new QuotingConfiguration?(splitProgram2.Properties.QuotingConfiguration) : null);
				if (quotingConfiguration2 != null)
				{
					if (quotingConfiguration2.Value.QuoteChar != null || quotingConfiguration2.Value.EscapeChar != null)
					{
						throw new Exception("Mismatching quoting configurations");
					}
					quotingConfiguration = quotingConfiguration2.Value;
				}
				else
				{
					quotingConfiguration = new QuotingConfiguration(null, false, null, QuotingStyle.Standard);
				}
			}
			if ((quotingConfiguration.QuoteChar != null || quotingConfiguration.EscapeChar != null) && (programType == ProgramType.SimpleOneColumn || regularExpression3 != null || regularExpression2 != null))
			{
				programType = ProgramType.Other;
			}
			return new ProgramProperties(programType, num2, skipEmptyAndCommentLinesCount, num3, flag, hasEmptyLines, text2, flag2, optional, quotingConfiguration, regularExpression2, regularExpression3, regularExpression, columnNames, readOnlyList, num, text, readOnlyList2, newLines, lineTelemetry);
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x00093F88 File Offset: 0x00092188
		private static string[] GetColumnNames(columnSelectorList columnSelectorList)
		{
			Microsoft.ProgramSynthesis.Compound.Split.Build.GrammarBuilders build = Language.Build;
			List<string> list = new List<string>();
			SelectorList selectorList;
			while (columnSelectorList.Is_SelectorList(build, out selectorList))
			{
				columnSelectorList = selectorList.columnSelectorList;
				columnSelector columnSelector = selectorList.columnSelector;
				KthKeyValue kthKeyValue;
				string text;
				KthTwoLineKeyValue kthTwoLineKeyValue;
				KthKeyQuote kthKeyQuote;
				KthKeyValueFw kthKeyValueFw;
				if (columnSelector.Is_KthKeyValue(build, out kthKeyValue))
				{
					text = kthKeyValue.key.Value;
				}
				else if (columnSelector.Is_KthTwoLineKeyValue(build, out kthTwoLineKeyValue))
				{
					text = kthTwoLineKeyValue.key.Value;
				}
				else if (columnSelector.Is_KthKeyQuote(build, out kthKeyQuote))
				{
					text = kthKeyQuote.key.Value;
				}
				else if (columnSelector.Is_KthKeyValueFw(build, out kthKeyValueFw))
				{
					text = kthKeyValueFw.key.Value;
				}
				else
				{
					if (!columnSelector.Is_KthLine(build))
					{
						throw new Exception(string.Format("Unhandled case: {0}", columnSelector.Node.GrammarRule));
					}
					text = string.Empty;
				}
				list.Add(text);
			}
			return list.ToArray();
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x00094088 File Offset: 0x00092288
		public override ITable<StringRegion> Run(StringRegion input)
		{
			State state = State.CreateForExecution(Language.Grammar.InputSymbol, input);
			ITable<StringRegion> table = base.ProgramNode.Invoke(state) as ITable<StringRegion>;
			if (table == null)
			{
				return null;
			}
			IEnumerable<string> enumerable;
			if (this.Properties.ProgramType != ProgramType.MultiRecord)
			{
				IReadOnlyList<string> readOnlyList = table.ColumnNames.ToList<string>();
				enumerable = readOnlyList;
			}
			else
			{
				enumerable = this.Properties.RawColumnNames;
			}
			return new Table<StringRegion>(enumerable, table.Rows.ToArray<IEnumerable<StringRegion>>(), null);
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x000940F8 File Offset: 0x000922F8
		public ITable<string> RunClean(StringRegion input, ColumnNamesCleaning columnNameCleaning = ColumnNamesCleaning.AsciiAlphaNumeric)
		{
			ProgramType programType = this.Properties.ProgramType;
			QuotingConfiguration? quotingConfiguration = ((programType == ProgramType.FixedWidth || programType == ProgramType.MultiRecord || programType == ProgramType.SimpleOneColumn) ? null : new QuotingConfiguration?(this.Properties.QuotingConf));
			return this.Run(input).WithCleanedColumnNames(columnNameCleaning, this.Properties.SelectedColumns).WithCleanCells(quotingConfiguration);
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x00094158 File Offset: 0x00092358
		public override ITable<StringRegion> Run(TextReader inputReader)
		{
			IEnumerable<StringRegion> enumerable = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.SplitLines(inputReader);
			IEnumerable<StringRegion> enumerable2 = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.MergeRecordLines(this.RunFileProg(enumerable));
			if (this.Properties.ProgramType != ProgramType.MultiRecord)
			{
				ITable<StringRegion> table = ((this.SplitTextProgram == null) ? Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.NoSplit(enumerable2, this.Properties.HasHeader) : Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.TableFromCells(enumerable2.Select(new Func<StringRegion, SplitCell[]>(this.RunTextProg)), this.Properties.HasHeader));
				if (this.Properties.SelectedColumns != null)
				{
					table = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.SelectColumns(this.Properties.SelectedColumns.ToArray<int>(), table);
				}
				return table;
			}
			State state = State.CreateForExecution(Language.Build.Symbol.records, enumerable2);
			ITable<StringRegion> table2 = this.MultiRecordSplitProgram.Node.Invoke(state) as ITable<StringRegion>;
			if (table2 == null)
			{
				return null;
			}
			return new Table<StringRegion>(this.Properties.RawColumnNames, table2.Rows, null);
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x00094244 File Offset: 0x00092444
		public ITable<string> RunClean(TextReader input, ColumnNamesCleaning columnNameCleaning = ColumnNamesCleaning.AsciiAlphaNumeric)
		{
			ProgramType programType = this.Properties.ProgramType;
			QuotingConfiguration? quotingConfiguration = ((programType == ProgramType.FixedWidth || programType == ProgramType.MultiRecord || programType == ProgramType.SimpleOneColumn) ? null : new QuotingConfiguration?(this.Properties.QuotingConf));
			return this.Run(input).WithCleanedColumnNames(columnNameCleaning, this.Properties.SelectedColumns).WithCleanCells(quotingConfiguration);
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x000942A4 File Offset: 0x000924A4
		private IEnumerable<IEnumerable<StringRegion>> RunFileProg(IEnumerable<StringRegion> input)
		{
			State state = State.CreateForExecution(Language.Build.Symbol.allLines, input);
			object obj = this.SplitFileProgram.Node.Invoke(state);
			if (obj == null)
			{
				return null;
			}
			return obj.ToEnumerable<object>().Cast<IEnumerable<StringRegion>>();
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x000942EB File Offset: 0x000924EB
		internal SplitCell[] RunTextProg(StringRegion record)
		{
			return this.SplitTextProgram.Run(record);
		}

		// Token: 0x040018E6 RID: 6374
		private static readonly Microsoft.ProgramSynthesis.Split.Text.Build.GrammarBuilders SplitTextBuilder = Language.Build;
	}
}
