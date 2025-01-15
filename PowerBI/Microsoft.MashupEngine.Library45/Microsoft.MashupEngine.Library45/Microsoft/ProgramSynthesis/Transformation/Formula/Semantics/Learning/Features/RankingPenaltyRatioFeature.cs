using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Features
{
	// Token: 0x020016F8 RID: 5880
	public class RankingPenaltyRatioFeature : RankingRatioFeatureBase
	{
		// Token: 0x0600C416 RID: 50198 RVA: 0x002A36B0 File Offset: 0x002A18B0
		public RankingPenaltyRatioFeature(Grammar grammar, RankingDebugTrace debugTrace)
			: base(grammar, "PenaltyRatio", debugTrace)
		{
			GrammarBuilders.GrammarSymbols symbol = base.Builder.Symbol;
			base.WithDefaultSymbolRatio(null);
			Symbol constStr = symbol.constStr;
			Func<string, LearningInfo, double?> func;
			if ((func = RankingPenaltyRatioFeature.<>O.<0>__ConstStr) == null)
			{
				func = (RankingPenaltyRatioFeature.<>O.<0>__ConstStr = new Func<string, LearningInfo, double?>(RankingPenaltyRatioFeature.ConstStr));
			}
			base.WithSymbolRatio<string>(constStr, func);
			base.WithDefaultRuleRatio(new double?(1.0));
			base.WithRuleRatio("Split", new double?(0.9));
			base.WithRuleRatio(RuleId.AllCase, new double?(0.0));
			base.WithRuleRatio("LetX", new double?(0.01));
			base.WithRuleRatio("Trim", new double?(0.01));
			base.WithRuleRatio("TrimFull", new double?(0.01));
			base.WithRuleRatio("TrimSplit", new double?(0.0));
			base.WithRuleRatio("TrimSlice", new double?(1.0));
			base.WithRuleRatio("Str", new double?(0.0));
			base.WithRuleRatio(new string[] { "Number", "AddRightNumber", "SubtractRightNumber", "MultiplyRightNumber", "DivideRightNumber" }, new double?(81.0));
			base.WithRuleRatio("Date", new double?(81.0));
			base.WithRuleRatio("ParseDateTime", new Func<LearningInfo, double?>(this.ParseDateTime));
			base.WithRuleRatio("FormatDateTime", new double?(0.5));
			base.WithRuleRatio("DateTimePart", new double?(1.1));
			base.WithRuleRatio("TimePart", new double?(1.2));
			base.WithRuleRatio("RoundDateTime", new double?(1.3));
			base.WithRuleRatio("ParseNumber", new double?(1.1));
			base.WithRuleRatio("FormatNumber", new double?(1.1));
			base.WithRuleRatio("Replace", new Func<LearningInfo, double?>(this.Replace));
			base.WithRuleRatio("Add", new Func<LearningInfo, double?>(this.Add));
			base.WithRuleRatio("Subtract", new Func<LearningInfo, double?>(this.Subtract));
			base.WithRuleRatio("Multiply", new Func<LearningInfo, double?>(this.Multiply));
			base.WithRuleRatio("Divide", new Func<LearningInfo, double?>(this.Divide));
			base.WithRuleRatio("Sum", new double?(3.0));
			base.WithRuleRatio("Product", new double?(4.0));
			base.WithRuleRatio("Average", new double?(5.0));
			base.WithRuleRatio("FromNumberStr", new double?(10.0));
			base.WithRuleRatio("Match", new double?(10.0));
			base.WithRuleRatio("MatchEnd", new double?(10.1));
			base.WithRuleRatio("MatchFull", new double?(10.2));
			base.WithRuleRatio("If", new double?(100.0));
			base.WithRuleRatio("Or", new double?(1.0));
			base.WithRuleRatio("IsString", new double?(1.0));
			base.WithRuleRatio("IsNumber", new double?(1.05));
			base.WithRuleRatio("IsBlank", new double?(1.1));
			base.WithRuleRatio("IsNotBlank", new double?(1.25));
			base.WithRuleRatio("StartsWith", new double?(1.4));
			base.WithRuleRatio("Contains", new double?(1.5));
			base.WithRuleRatio("StartsWithDigit", new double?(1.6));
			base.WithRuleRatio("EndsWithDigit", new double?(1.65));
			base.WithRuleRatio("EndsWith", new double?(1.7));
			base.WithRuleRatio("StringEquals", new double?(5.0));
			base.WithRuleRatio("NumberGreaterThan", new double?(1.2));
			base.WithRuleRatio("NumberLessThan", new double?(1.3));
			base.WithRuleRatio("NumberEquals", new double?(5.0));
			base.WithRuleRatio("IsMatch", new double?(10.0));
			base.WithRuleRatio("ContainsMatch", new double?(11.0));
		}

		// Token: 0x17002171 RID: 8561
		// (get) Token: 0x0600C417 RID: 50199 RVA: 0x0000A5FD File Offset: 0x000087FD
		protected override RankingRatioKind Kind
		{
			get
			{
				return RankingRatioKind.Penalty;
			}
		}

		// Token: 0x0600C418 RID: 50200 RVA: 0x002A3BBB File Offset: 0x002A1DBB
		protected override IEnumerable<double> GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0.Yield<double>();
		}

		// Token: 0x0600C419 RID: 50201 RVA: 0x002A3BCC File Offset: 0x002A1DCC
		private static double? ConstStr(string value, LearningInfo info)
		{
			RankingScoreFeatureOptions rankingScoreFeatureOptions = info.Options as RankingScoreFeatureOptions;
			if (rankingScoreFeatureOptions == null)
			{
				return new double?(1.0);
			}
			if (value.AllDelimiters())
			{
				return new double?(2.0 - RankingRatioFeatureBase.Proportion((double)value.Length, 50.0));
			}
			IReadOnlyList<string> allSubstrings = (from s in value.AllSubstrings()
				where !string.IsNullOrEmpty(s)
				select s).ToDistinctReadOnlyList<string>();
			int num = rankingScoreFeatureOptions.AllInputs.Select(delegate(IRow input)
			{
				Func<string, string> <>9__3;
				return allSubstrings.Count(delegate(string substring)
				{
					IEnumerable<string> columnNames = input.ColumnNames;
					if (columnNames == null)
					{
						return false;
					}
					Func<string, string> func;
					if ((func = <>9__3) == null)
					{
						func = (<>9__3 = (string columnName) => Operators.FromStr(input, columnName));
					}
					return columnNames.Collect(func).Any((string inputString) => inputString.Contains(substring));
				});
			}).Max();
			double num2 = 10.0 * RankingRatioFeatureBase.Proportion((double)value.Length, 50.0);
			double num3 = (double)(Math.Min(num, 20) * 5);
			return new double?(30.0 - num2 + num3);
		}

		// Token: 0x0600C41A RID: 50202 RVA: 0x002A3CB8 File Offset: 0x002A1EB8
		private double? ParseDateTime(LearningInfo info)
		{
			return new double?(base.Builder.Node.CastRule.ParseDateTime(info.ProgramNode).dateTimeParseDesc.Value.IsPartial ? 85.0 : 0.5);
		}

		// Token: 0x0600C41B RID: 50203 RVA: 0x002A3D10 File Offset: 0x002A1F10
		private double? Replace(LearningInfo info)
		{
			RankingScoreFeatureOptions rankingScoreFeatureOptions = info.Options as RankingScoreFeatureOptions;
			if (rankingScoreFeatureOptions == null)
			{
				return new double?(60.0);
			}
			if (rankingScoreFeatureOptions.Examples.Count > 1)
			{
				return new double?(5.0);
			}
			Replace replace = base.Builder.Node.CastRule.Replace(info.ProgramNode);
			string value = replace.replaceFindText.Value;
			string value2 = replace.replaceText.Value;
			if (value.AllDelimiters() && value2.AllDelimiters())
			{
				return new double?(0.5);
			}
			if (value.Length > 1 && value2.Length == 0)
			{
				return new double?(5.0);
			}
			if ((value2.Length <= 1 || !value2.ToCharArray().Distinct<char>().HasAtMost(1)) && value.Length > 1 && value2.Length > 1)
			{
				return new double?(0.5);
			}
			return new double?(60.0);
		}

		// Token: 0x0600C41C RID: 50204 RVA: 0x002A3E24 File Offset: 0x002A2024
		private double? Add(LearningInfo info)
		{
			Add add = base.Builder.Node.CastRule.Add(info.ProgramNode);
			double num = 0.5;
			double? num2 = this.ArithmeticPairFactor(add.arithmeticLeft.Node, add.addRight.Node);
			if (num2 == null)
			{
				return null;
			}
			return new double?(num + num2.GetValueOrDefault());
		}

		// Token: 0x0600C41D RID: 50205 RVA: 0x002A3EA0 File Offset: 0x002A20A0
		private double? Divide(LearningInfo info)
		{
			Divide divide = base.Builder.Node.CastRule.Divide(info.ProgramNode);
			double num = 0.6;
			double? num2 = this.ArithmeticPairFactor(divide.arithmeticLeft.Node, divide.divideRight.Node);
			if (num2 == null)
			{
				return null;
			}
			return new double?(num + num2.GetValueOrDefault());
		}

		// Token: 0x0600C41E RID: 50206 RVA: 0x002A3F1C File Offset: 0x002A211C
		private double? Multiply(LearningInfo info)
		{
			Multiply multiply = base.Builder.Node.CastRule.Multiply(info.ProgramNode);
			double num = 0.6;
			double? num2 = this.ArithmeticPairFactor(multiply.arithmeticLeft.Node, multiply.multiplyRight.Node);
			if (num2 == null)
			{
				return null;
			}
			return new double?(num + num2.GetValueOrDefault());
		}

		// Token: 0x0600C41F RID: 50207 RVA: 0x002A3F98 File Offset: 0x002A2198
		private double? Subtract(LearningInfo info)
		{
			Subtract subtract = base.Builder.Node.CastRule.Subtract(info.ProgramNode);
			double num = 0.5;
			double? num2 = this.ArithmeticPairFactor(subtract.arithmeticLeft.Node, subtract.subtractRight.Node);
			if (num2 == null)
			{
				return null;
			}
			return new double?(num + num2.GetValueOrDefault());
		}

		// Token: 0x0600C420 RID: 50208 RVA: 0x002A4014 File Offset: 0x002A2214
		private double? ArithmeticPairFactor(ProgramNode left, ProgramNode right)
		{
			columnName? columnName = (from n in ProgramExtractVisitor.ExtractNodes(right, (ProgramNode n) => n.Is<columnName>(), false)
				select columnName.CreateSafe(base.Builder, n)).FirstOrDefault<columnName?>();
			string text = ((columnName != null) ? columnName.GetValueOrDefault().Value : null);
			columnName = (from n in ProgramExtractVisitor.ExtractNodes(left, (ProgramNode n) => n.Is<columnName>(), false)
				select columnName.CreateSafe(base.Builder, n)).FirstOrDefault<columnName?>();
			return new double?((((columnName != null) ? columnName.GetValueOrDefault().Value : null) == text) ? 82.0 : 0.0);
		}

		// Token: 0x020016F9 RID: 5881
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04004C7C RID: 19580
			public static Func<string, LearningInfo, double?> <0>__ConstStr;
		}
	}
}
