using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Features
{
	// Token: 0x020016F0 RID: 5872
	public class RankingAwardRatioFeature : RankingRatioFeatureBase
	{
		// Token: 0x0600C3D0 RID: 50128 RVA: 0x002A1D94 File Offset: 0x0029FF94
		public RankingAwardRatioFeature(Grammar grammar, RankingDebugTrace debugTrace)
			: base(grammar, "AwardRatio", debugTrace)
		{
			GrammarBuilders.GrammarSymbols symbol = base.Builder.Symbol;
			base.WithDefaultSymbolRatio(null);
			base.WithRuleRatio("LetX", new Func<LearningInfo, double?>(this.LetX));
			base.WithRuleRatio("Trim", new Func<LearningInfo, double?>(this.Trim));
			base.WithRuleRatio("TrimFull", new Func<LearningInfo, double?>(this.Trim));
			base.WithRuleRatio(RuleId.AllCase, new Func<LearningInfo, double?>(this.Case));
			Symbol columnName = symbol.columnName;
			Func<string, LearningInfo, double?> func;
			if ((func = RankingAwardRatioFeature.<>O.<0>__ColumnName) == null)
			{
				func = (RankingAwardRatioFeature.<>O.<0>__ColumnName = new Func<string, LearningInfo, double?>(RankingAwardRatioFeature.ColumnName));
			}
			base.WithSymbolRatio<string>(columnName, func);
			Symbol columnNames = symbol.columnNames;
			Func<string[], LearningInfo, double?> func2;
			if ((func2 = RankingAwardRatioFeature.<>O.<1>__ColumnNames) == null)
			{
				func2 = (RankingAwardRatioFeature.<>O.<1>__ColumnNames = new Func<string[], LearningInfo, double?>(RankingAwardRatioFeature.ColumnNames));
			}
			base.WithSymbolRatio<string[]>(columnNames, func2);
			Symbol absPos = symbol.absPos;
			Func<int, double?> func3;
			if ((func3 = RankingAwardRatioFeature.<>O.<2>__AbsPos) == null)
			{
				func3 = (RankingAwardRatioFeature.<>O.<2>__AbsPos = new Func<int, double?>(RankingAwardRatioFeature.AbsPos));
			}
			base.WithSymbolRatio<int>(absPos, func3);
			Symbol findDelimiter = symbol.findDelimiter;
			Func<string, double?> func4;
			if ((func4 = RankingAwardRatioFeature.<>O.<3>__FindDelimiter) == null)
			{
				func4 = (RankingAwardRatioFeature.<>O.<3>__FindDelimiter = new Func<string, double?>(RankingAwardRatioFeature.FindDelimiter));
			}
			base.WithSymbolRatio<string>(findDelimiter, func4);
			Symbol findInstance = symbol.findInstance;
			Func<int, LearningInfo, double?> func5;
			if ((func5 = RankingAwardRatioFeature.<>O.<4>__FindInstance) == null)
			{
				func5 = (RankingAwardRatioFeature.<>O.<4>__FindInstance = new Func<int, LearningInfo, double?>(RankingAwardRatioFeature.FindInstance));
			}
			base.WithSymbolRatio<int>(findInstance, func5);
			Symbol findOffset = symbol.findOffset;
			Func<int, double?> func6;
			if ((func6 = RankingAwardRatioFeature.<>O.<5>__FindOffset) == null)
			{
				func6 = (RankingAwardRatioFeature.<>O.<5>__FindOffset = new Func<int, double?>(RankingAwardRatioFeature.FindOffset));
			}
			base.WithSymbolRatio<int>(findOffset, func6);
			Symbol locale = symbol.locale;
			Func<string, LearningInfo, double?> func7;
			if ((func7 = RankingAwardRatioFeature.<>O.<6>__Locale) == null)
			{
				func7 = (RankingAwardRatioFeature.<>O.<6>__Locale = new Func<string, LearningInfo, double?>(RankingAwardRatioFeature.Locale));
			}
			base.WithSymbolRatio<string>(locale, func7);
			Symbol matchDesc = symbol.matchDesc;
			Func<MatchDescriptor, double?> func8;
			if ((func8 = RankingAwardRatioFeature.<>O.<7>__MatchDescriptor) == null)
			{
				func8 = (RankingAwardRatioFeature.<>O.<7>__MatchDescriptor = new Func<MatchDescriptor, double?>(RankingAwardRatioFeature.MatchDescriptor));
			}
			base.WithSymbolRatio<MatchDescriptor>(matchDesc, func8);
			Symbol matchInstance = symbol.matchInstance;
			Func<int, double?> func9;
			if ((func9 = RankingAwardRatioFeature.<>O.<8>__MatchInstance) == null)
			{
				func9 = (RankingAwardRatioFeature.<>O.<8>__MatchInstance = new Func<int, double?>(RankingAwardRatioFeature.MatchInstance));
			}
			base.WithSymbolRatio<int>(matchInstance, func9);
			Symbol splitDelimiter = symbol.splitDelimiter;
			Func<string, double?> func10;
			if ((func10 = RankingAwardRatioFeature.<>O.<9>__SplitDelimiter) == null)
			{
				func10 = (RankingAwardRatioFeature.<>O.<9>__SplitDelimiter = new Func<string, double?>(RankingAwardRatioFeature.SplitDelimiter));
			}
			base.WithSymbolRatio<string>(splitDelimiter, func10);
			Symbol splitInstance = symbol.splitInstance;
			Func<int, double?> func11;
			if ((func11 = RankingAwardRatioFeature.<>O.<10>__SplitInstance) == null)
			{
				func11 = (RankingAwardRatioFeature.<>O.<10>__SplitInstance = new Func<int, double?>(RankingAwardRatioFeature.SplitInstance));
			}
			base.WithSymbolRatio<int>(splitInstance, func11);
			Symbol numberFormatDesc = symbol.numberFormatDesc;
			Func<FormatNumberDescriptor, LearningInfo, double?> func12;
			if ((func12 = RankingAwardRatioFeature.<>O.<11>__NumberFormatDesc) == null)
			{
				func12 = (RankingAwardRatioFeature.<>O.<11>__NumberFormatDesc = new Func<FormatNumberDescriptor, LearningInfo, double?>(RankingAwardRatioFeature.NumberFormatDesc));
			}
			base.WithSymbolRatio<FormatNumberDescriptor>(numberFormatDesc, func12);
			Symbol numberRoundDesc = symbol.numberRoundDesc;
			Func<RoundNumberDescriptor, double?> func13;
			if ((func13 = RankingAwardRatioFeature.<>O.<12>__NumberRoundDesc) == null)
			{
				func13 = (RankingAwardRatioFeature.<>O.<12>__NumberRoundDesc = new Func<RoundNumberDescriptor, double?>(RankingAwardRatioFeature.NumberRoundDesc));
			}
			base.WithSymbolRatio<RoundNumberDescriptor>(numberRoundDesc, func13);
			Symbol dateTimeParseDesc = symbol.dateTimeParseDesc;
			Func<DateTimeDescriptor, LearningInfo, double?> func14;
			if ((func14 = RankingAwardRatioFeature.<>O.<13>__DateTimeParseDesc) == null)
			{
				func14 = (RankingAwardRatioFeature.<>O.<13>__DateTimeParseDesc = new Func<DateTimeDescriptor, LearningInfo, double?>(RankingAwardRatioFeature.DateTimeParseDesc));
			}
			base.WithSymbolRatio<DateTimeDescriptor>(dateTimeParseDesc, func14);
			Symbol dateTimeFormatDesc = symbol.dateTimeFormatDesc;
			Func<DateTimeDescriptor, LearningInfo, double?> func15;
			if ((func15 = RankingAwardRatioFeature.<>O.<14>__DateTimeFormatDesc) == null)
			{
				func15 = (RankingAwardRatioFeature.<>O.<14>__DateTimeFormatDesc = new Func<DateTimeDescriptor, LearningInfo, double?>(RankingAwardRatioFeature.DateTimeFormatDesc));
			}
			base.WithSymbolRatio<DateTimeDescriptor>(dateTimeFormatDesc, func15);
			Symbol dateTimePartKind = symbol.dateTimePartKind;
			Func<DateTimePartKind, LearningInfo, double?> func16;
			if ((func16 = RankingAwardRatioFeature.<>O.<15>__DateTimePartKind) == null)
			{
				func16 = (RankingAwardRatioFeature.<>O.<15>__DateTimePartKind = new Func<DateTimePartKind, LearningInfo, double?>(RankingAwardRatioFeature.DateTimePartKind));
			}
			base.WithSymbolRatio<DateTimePartKind>(dateTimePartKind, func16);
			Symbol dateTimeRoundDesc = symbol.dateTimeRoundDesc;
			Func<RoundDateTimeDescriptor, LearningInfo, double?> func17;
			if ((func17 = RankingAwardRatioFeature.<>O.<16>__RoundDateTimeDesc) == null)
			{
				func17 = (RankingAwardRatioFeature.<>O.<16>__RoundDateTimeDesc = new Func<RoundDateTimeDescriptor, LearningInfo, double?>(RankingAwardRatioFeature.RoundDateTimeDesc));
			}
			base.WithSymbolRatio<RoundDateTimeDescriptor>(dateTimeRoundDesc, func17);
			Symbol isMatchRegex = symbol.isMatchRegex;
			Func<Regex, double?> func18;
			if ((func18 = RankingAwardRatioFeature.<>O.<17>__IsMatchRegex) == null)
			{
				func18 = (RankingAwardRatioFeature.<>O.<17>__IsMatchRegex = new Func<Regex, double?>(RankingAwardRatioFeature.IsMatchRegex));
			}
			base.WithSymbolRatio<Regex>(isMatchRegex, func18);
			Symbol slicePrefixAbsPos = symbol.slicePrefixAbsPos;
			Func<int, double?> func19;
			if ((func19 = RankingAwardRatioFeature.<>O.<18>__SlicePrefixAbsPos) == null)
			{
				func19 = (RankingAwardRatioFeature.<>O.<18>__SlicePrefixAbsPos = new Func<int, double?>(RankingAwardRatioFeature.SlicePrefixAbsPos));
			}
			base.WithSymbolRatio<int>(slicePrefixAbsPos, func19);
			base.WithSymbolRatio(symbol.constStr, new double?(0.0));
			base.WithSymbolRatio(symbol.constDt, new double?(0.0));
			base.WithRuleRatio("ToStr", new double?(0.0));
			base.WithRuleRatio("ToDouble", new double?(0.0));
			base.WithRuleRatio("ToDecimal", new double?(0.0));
			base.WithRuleRatio("ToInt", new double?(0.0));
			base.WithRuleRatio("ToDateTime", new double?(0.0));
			base.WithRuleRatio("FromStr", new double?(0.0));
			base.WithRuleRatio("FromNumber", new double?(0.0));
			base.WithRuleRatio("FromNumbers", new double?(0.0));
			base.WithRuleRatio("FromNumberStr", new double?(0.0));
			base.WithRuleRatio("FromDateTime", new double?(0.0));
			base.WithRuleRatio("FromDateTimePart", new double?(0.0));
		}

		// Token: 0x17002169 RID: 8553
		// (get) Token: 0x0600C3D1 RID: 50129 RVA: 0x0000FA11 File Offset: 0x0000DC11
		protected override RankingRatioKind Kind
		{
			get
			{
				return RankingRatioKind.Award;
			}
		}

		// Token: 0x0600C3D2 RID: 50130 RVA: 0x002A2254 File Offset: 0x002A0454
		private static double? AbsPos(int value)
		{
			double num = 0.1 * RankingRatioFeatureBase.Proportion((double)Math.Abs(value), 100.0);
			double num2;
			if (value > 0)
			{
				if (value <= 6)
				{
					if (value != 1)
					{
						num2 = 0.9 + num;
					}
					else
					{
						num2 = 1.0;
					}
				}
				else
				{
					num2 = 0.5 + num;
				}
			}
			else if (value < -4)
			{
				num2 = 0.1 + num;
			}
			else
			{
				num2 = 0.6 + num;
			}
			return new double?(num2);
		}

		// Token: 0x0600C3D3 RID: 50131 RVA: 0x002A22E0 File Offset: 0x002A04E0
		private double? Case(LearningInfo info)
		{
			RankingScoreFeatureOptions rankingScoreFeatureOptions = info.Options as RankingScoreFeatureOptions;
			if (rankingScoreFeatureOptions == null)
			{
				return new double?(0.0);
			}
			return new double?(this.IsOutputEffected(info.ProgramNode, rankingScoreFeatureOptions.AllInputs, null) ? 1.0 : 0.0);
		}

		// Token: 0x0600C3D4 RID: 50132 RVA: 0x002A233C File Offset: 0x002A053C
		private static double? ColumnName(string columnName, LearningInfo info)
		{
			RankingScoreFeatureOptions rankingScoreFeatureOptions = info.Options as RankingScoreFeatureOptions;
			if (rankingScoreFeatureOptions == null || rankingScoreFeatureOptions.ColumnNamePriority == null || !rankingScoreFeatureOptions.ColumnNamePriority.Any<string>())
			{
				return new double?(0.0);
			}
			int? num = rankingScoreFeatureOptions.ColumnNamePriority.IndexOf(columnName);
			return new double?((num == null) ? 0.0 : RankingRatioFeatureBase.InverseProportion((double)num.Value, rankingScoreFeatureOptions.ColumnNamePriority.Count));
		}

		// Token: 0x0600C3D5 RID: 50133 RVA: 0x002A23BC File Offset: 0x002A05BC
		private static double? ColumnNames(string[] columnNames, LearningInfo info)
		{
			return new double?((from columnName in columnNames
				let ranking = RankingAwardRatioFeature.ColumnName(columnName, info).GetValueOrDefault()
				select ranking).Average() * 0.5 + RankingRatioFeatureBase.InverseProportion((double)columnNames.Length, 50) * 0.5);
		}

		// Token: 0x0600C3D6 RID: 50134 RVA: 0x002A2436 File Offset: 0x002A0636
		private static double? DateTimeFormatDesc(DateTimeDescriptor descriptor, LearningInfo info)
		{
			return RankingAwardRatioFeature.DateTimeDescriptor(descriptor, info, false);
		}

		// Token: 0x0600C3D7 RID: 50135 RVA: 0x002A2440 File Offset: 0x002A0640
		private static double? DateTimeParseDesc(DateTimeDescriptor descriptor, LearningInfo info)
		{
			return RankingAwardRatioFeature.DateTimeDescriptor(descriptor, info, true);
		}

		// Token: 0x0600C3D8 RID: 50136 RVA: 0x002A244A File Offset: 0x002A064A
		private static double? DateTimePartKind(DateTimePartKind kind, LearningInfo info)
		{
			return new double?(0.5 * RankingRatioFeatureBase.Proportion((double)kind, (double)EnumUtils.GetValues<DateTimePartKind>().Length));
		}

		// Token: 0x0600C3D9 RID: 50137 RVA: 0x002A246C File Offset: 0x002A066C
		private static double? FindDelimiter(string value)
		{
			double num;
			if (!(value == ":"))
			{
				if (!(value == " "))
				{
					if (!(value == ","))
					{
						if (!(value == "."))
						{
							num = 0.5;
						}
						else
						{
							num = 0.1;
						}
					}
					else
					{
						num = 0.8;
					}
				}
				else
				{
					num = 0.9;
				}
			}
			else
			{
				num = 1.0;
			}
			return new double?(num);
		}

		// Token: 0x0600C3DA RID: 50138 RVA: 0x002A24F0 File Offset: 0x002A06F0
		private static double? FindInstance(int instance, LearningInfo info)
		{
			double num;
			if (instance >= 0)
			{
				if (instance == 1)
				{
					num = 1.0;
				}
				else
				{
					num = 0.5 + 0.49 * RankingRatioFeatureBase.Proportion((double)instance, 20.0);
				}
			}
			else
			{
				num = 0.49 * RankingRatioFeatureBase.InverseProportion((double)Math.Abs(instance), 20);
			}
			return new double?(num);
		}

		// Token: 0x0600C3DB RID: 50139 RVA: 0x002A2558 File Offset: 0x002A0758
		private static double? FindOffset(int findOffset)
		{
			double num;
			if (findOffset <= 0)
			{
				if (findOffset == 0)
				{
					num = 1.0;
				}
				else
				{
					num = 0.4 * RankingRatioFeatureBase.InverseProportion((double)Math.Abs(findOffset), 5);
				}
			}
			else
			{
				num = 0.5 + 0.49 * RankingRatioFeatureBase.InverseProportion((double)findOffset, 5);
			}
			return new double?(num);
		}

		// Token: 0x0600C3DC RID: 50140 RVA: 0x002A25B8 File Offset: 0x002A07B8
		private static double? IsMatchRegex(Regex regex)
		{
			string text = regex.ToString();
			double num = (text.StartsWith("^") ? 0.5 : (text.EndsWith("$") ? 0.4 : 0.0));
			double num2 = 0.5 * RankingRatioFeatureBase.Proportion((double)text.Length, 50.0);
			return new double?(num + num2);
		}

		// Token: 0x0600C3DD RID: 50141 RVA: 0x002A262C File Offset: 0x002A082C
		private static double? SlicePrefixAbsPos(int position)
		{
			return new double?(RankingRatioFeatureBase.Proportion((double)position, 10.0));
		}

		// Token: 0x0600C3DE RID: 50142 RVA: 0x002A2644 File Offset: 0x002A0844
		private double? LetX(LearningInfo info)
		{
			IFeatureOptions options2 = info.Options;
			RankingScoreFeatureOptions options = options2 as RankingScoreFeatureOptions;
			if (options == null)
			{
				return new double?(0.0);
			}
			ProgramNode sourceNode = info.ProgramNode;
			Func<IRow, object> <>9__2;
			int num = ProgramExtractVisitor.ExtractNodes(sourceNode, (ProgramNode node) => node.IsTrim(), false).Count(delegate(ProgramNode node)
			{
				RankingAwardRatioFeature <>4__this = this;
				IEnumerable<IRow> allInputs = options.AllInputs;
				Func<IRow, object> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (IRow row) => sourceNode.Children[0].Run(row));
				}
				return <>4__this.IsOutputEffected(node, allInputs, func);
			});
			return new double?((num == 0) ? 0.0 : (0.8 + 0.2 * RankingRatioFeatureBase.Proportion((double)num, 2.0)));
		}

		// Token: 0x0600C3DF RID: 50143 RVA: 0x002A2708 File Offset: 0x002A0908
		private static double? Locale(string locale, LearningInfo info)
		{
			return new double?(RankingRatioFeatureBase.LocaleProportion(locale, info));
		}

		// Token: 0x0600C3E0 RID: 50144 RVA: 0x002A2716 File Offset: 0x002A0916
		private static double? MatchDescriptor(MatchDescriptor regexDesc)
		{
			return new double?(1.0 / (double)regexDesc.Pattern.Length);
		}

		// Token: 0x0600C3E1 RID: 50145 RVA: 0x002A2734 File Offset: 0x002A0934
		private static double? MatchInstance(int instance)
		{
			double num;
			if (instance > 0)
			{
				if (instance != 1)
				{
					num = 0.5;
				}
				else
				{
					num = 1.0;
				}
			}
			else if (instance != -1)
			{
				num = 0.3;
			}
			else
			{
				num = 0.8;
			}
			return new double?(num);
		}

		// Token: 0x0600C3E2 RID: 50146 RVA: 0x002A2788 File Offset: 0x002A0988
		private static double? NumberFormatDesc(FormatNumberDescriptor descriptor, LearningInfo info)
		{
			double num;
			double num2;
			if (descriptor.IncludeGroupSeparator)
			{
				num = 0.2 * RankingRatioFeatureBase.InverseProportion((double)descriptor.LeadingDigits, 10);
				num2 = 0.1;
			}
			else
			{
				num = 0.1 + 0.1 * RankingRatioFeatureBase.InverseProportion((double)descriptor.LeadingDigits, 10);
				num2 = 0.2;
			}
			double num3 = 0.1 * RankingRatioFeatureBase.Proportion((double)descriptor.TrailingDigits, 10.0);
			double num4 = (descriptor.IncludeDecimalSeparator ? 0.2 : 0.1);
			double num5 = 0.2 * RankingRatioFeatureBase.LocaleProportion(descriptor.Locale, info);
			return new double?(num + num3 + num4 + num2 + num5);
		}

		// Token: 0x0600C3E3 RID: 50147 RVA: 0x002A2854 File Offset: 0x002A0A54
		private static double? NumberRoundDesc(RoundNumberDescriptor descriptor)
		{
			double num = ((Math.Abs(descriptor.Delta - Math.Pow(10.0, (double)((int)Math.Log10(descriptor.Delta)))) < 1E-06) ? 0.1 : 0.0) + 0.8 * RankingRatioFeatureBase.InverseProportion(Convert.ToDouble(descriptor.Delta), 100000);
			double num2;
			switch (descriptor.Mode)
			{
			case RoundingMode.Nearest:
				num2 = 0.05;
				break;
			case RoundingMode.Down:
				num2 = 0.03;
				break;
			case RoundingMode.Up:
				num2 = 0.04;
				break;
			default:
				num2 = 0.0;
				break;
			}
			double num3 = num2;
			return new double?(num + num3);
		}

		// Token: 0x0600C3E4 RID: 50148 RVA: 0x002A2920 File Offset: 0x002A0B20
		private static double? RoundDateTimeDesc(RoundDateTimeDescriptor descriptor, LearningInfo info)
		{
			RoundingMode mode = descriptor.Mode;
			double num;
			if (mode != RoundingMode.Nearest)
			{
				if (mode == RoundingMode.Down)
				{
					num = 0.3;
				}
				else
				{
					num = 0.1;
				}
			}
			else
			{
				num = 0.2;
			}
			double num2 = num;
			double num3 = 0.5 * RankingRatioFeatureBase.Proportion((double)descriptor.Period, (double)EnumUtils.GetValues<DateTimePartKind>().Length);
			double num4 = 0.2 * RankingRatioFeatureBase.Proportion((double)descriptor.Ceiling, (double)EnumUtils.GetValues<RoundDatePeriodCeiling>().Length);
			return new double?(num2 + num3 + num4);
		}

		// Token: 0x0600C3E5 RID: 50149 RVA: 0x002A29A8 File Offset: 0x002A0BA8
		private static double? SplitDelimiter(string value)
		{
			double num;
			if (!(value == ",") && !(value == ":"))
			{
				if (!(value == " "))
				{
					if (!(value == "."))
					{
						num = 0.5;
					}
					else
					{
						num = 0.1;
					}
				}
				else
				{
					num = 0.25;
				}
			}
			else
			{
				num = 1.0;
			}
			return new double?(num);
		}

		// Token: 0x0600C3E6 RID: 50150 RVA: 0x002A2A20 File Offset: 0x002A0C20
		private static double? SplitInstance(int instance)
		{
			double num;
			if (instance < 0)
			{
				if (instance == -1)
				{
					num = 1.0;
				}
				else
				{
					num = 0.49 * RankingRatioFeatureBase.InverseProportion((double)Math.Abs(instance), 20);
				}
			}
			else
			{
				num = 0.5 + 0.49 * RankingRatioFeatureBase.Proportion((double)instance, 20.0);
			}
			return new double?(num);
		}

		// Token: 0x0600C3E7 RID: 50151 RVA: 0x002A2A88 File Offset: 0x002A0C88
		private double? Trim(LearningInfo info)
		{
			RankingScoreFeatureOptions rankingScoreFeatureOptions = info.Options as RankingScoreFeatureOptions;
			if (rankingScoreFeatureOptions == null)
			{
				return new double?(0.0);
			}
			return new double?(this.IsOutputEffected(info.ProgramNode, rankingScoreFeatureOptions.AllInputs, null) ? 1.0 : 0.0);
		}

		// Token: 0x0600C3E8 RID: 50152 RVA: 0x002A2AE4 File Offset: 0x002A0CE4
		private static double? DateTimeDescriptor(DateTimeDescriptor descriptor, LearningInfo info, bool forParse)
		{
			string mask = descriptor.Mask;
			double num = RankingRatioFeatureBase.LocaleProportion(descriptor.Locale, info);
			double num2 = (mask.Contains("yyyy") ? 0.05 : (mask.Contains("yy") ? 0.03 : 0.01));
			double num3 = (mask.Contains("MMMM") ? 1.0 : (mask.Contains("MMM") ? 0.8 : (mask.Contains("MM") ? 0.6 : (mask.Contains("M") ? 0.5 : 0.01))));
			double num4 = (mask.Contains("dddd") ? 0.3 : (mask.Contains("ddd") ? 0.6 : (mask.Contains("dd") ? 0.6 : (mask.Contains("d") ? 0.61 : 0.01))));
			double num5 = (mask.Contains("HH") ? 0.51 : (mask.Contains("H") ? 0.5 : (mask.Contains("hh") ? 0.9 : (mask.Contains("h") ? 0.8 : 0.01))));
			double num6 = (mask.Contains("mm") ? 0.01 : 1.0);
			double num7 = (mask.Contains("ss") ? 0.01 : 1.0);
			double num8 = ((mask.Contains("d/") || mask.Contains("yy/")) ? 1.0 : (mask.Contains("d ") ? 0.8 : ((mask.Contains("d-") || mask.Contains("yy-")) ? 0.6 : ((mask.Contains("d.") || mask.Contains("yy.")) ? 0.4 : 0.01))));
			if (descriptor.Locale == "en-US")
			{
				if (mask.Contains("/d/") || mask.Contains(" d,") || mask == "dd")
				{
					num4 += 0.02;
				}
			}
			else
			{
				if (mask.Contains("dd/") || mask.Contains("dd,") || mask == "dd")
				{
					num4 += 0.02;
				}
				if (mask.Contains("H:"))
				{
					num5 += 0.02;
				}
			}
			return new double?((forParse ? 0.8 : 0.5) * num + 0.011 * num2 + 0.012 * num3 + 0.025 * num4 + 0.027 * num5 + 0.015 * num6 + 0.016 * num7 + 0.01 * num8);
		}

		// Token: 0x0600C3E9 RID: 50153 RVA: 0x002A2E68 File Offset: 0x002A1068
		private bool IsOutputEffected(ProgramNode node, IEnumerable<IRow> inputs, Func<IRow, object> xselector = null)
		{
			ProgramNode programNode = node.Children[0];
			bool flag = ProgramExtractVisitor.Extract(node, (ProgramNode inode) => inode.IsProperCase(), false).Any<ProgramNodeDetail>();
			foreach (IRow row in inputs)
			{
				State state = State.CreateForExecution(node.Grammar.InputSymbol, row);
				if (xselector != null)
				{
					state = state.Bind(base.Builder.Symbol.x, xselector(row));
				}
				string text = programNode.Invoke(state) as string;
				if (text == null)
				{
					return false;
				}
				if (flag && RankingAwardRatioFeature._inlineCaptialLetterRegex.IsMatch(text))
				{
					return false;
				}
				string text2 = node.Invoke(state) as string;
				if (text2 != null)
				{
					if (!text2.All((char c) => !c.IsLetter()))
					{
						if (text != text2)
						{
							return true;
						}
						continue;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x04004C4F RID: 19535
		private static readonly Regex _inlineCaptialLetterRegex = "(?<!\\p{Zs}|^)\\p{Lu}".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x020016F1 RID: 5873
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04004C50 RID: 19536
			public static Func<string, LearningInfo, double?> <0>__ColumnName;

			// Token: 0x04004C51 RID: 19537
			public static Func<string[], LearningInfo, double?> <1>__ColumnNames;

			// Token: 0x04004C52 RID: 19538
			public static Func<int, double?> <2>__AbsPos;

			// Token: 0x04004C53 RID: 19539
			public static Func<string, double?> <3>__FindDelimiter;

			// Token: 0x04004C54 RID: 19540
			public static Func<int, LearningInfo, double?> <4>__FindInstance;

			// Token: 0x04004C55 RID: 19541
			public static Func<int, double?> <5>__FindOffset;

			// Token: 0x04004C56 RID: 19542
			public static Func<string, LearningInfo, double?> <6>__Locale;

			// Token: 0x04004C57 RID: 19543
			public static Func<MatchDescriptor, double?> <7>__MatchDescriptor;

			// Token: 0x04004C58 RID: 19544
			public static Func<int, double?> <8>__MatchInstance;

			// Token: 0x04004C59 RID: 19545
			public static Func<string, double?> <9>__SplitDelimiter;

			// Token: 0x04004C5A RID: 19546
			public static Func<int, double?> <10>__SplitInstance;

			// Token: 0x04004C5B RID: 19547
			public static Func<FormatNumberDescriptor, LearningInfo, double?> <11>__NumberFormatDesc;

			// Token: 0x04004C5C RID: 19548
			public static Func<RoundNumberDescriptor, double?> <12>__NumberRoundDesc;

			// Token: 0x04004C5D RID: 19549
			public static Func<DateTimeDescriptor, LearningInfo, double?> <13>__DateTimeParseDesc;

			// Token: 0x04004C5E RID: 19550
			public static Func<DateTimeDescriptor, LearningInfo, double?> <14>__DateTimeFormatDesc;

			// Token: 0x04004C5F RID: 19551
			public static Func<DateTimePartKind, LearningInfo, double?> <15>__DateTimePartKind;

			// Token: 0x04004C60 RID: 19552
			public static Func<RoundDateTimeDescriptor, LearningInfo, double?> <16>__RoundDateTimeDesc;

			// Token: 0x04004C61 RID: 19553
			public static Func<Regex, double?> <17>__IsMatchRegex;

			// Token: 0x04004C62 RID: 19554
			public static Func<int, double?> <18>__SlicePrefixAbsPos;
		}
	}
}
