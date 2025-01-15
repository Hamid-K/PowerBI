using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001BA5 RID: 7077
	internal static class Rewriter
	{
		// Token: 0x0600E7E3 RID: 59363 RVA: 0x003123FC File Offset: 0x003105FC
		public static ProgramSet Rewrite(ProgramSet set)
		{
			IEnumerable<RewriteRule> rewriteRules = Rewriter.RewriteRules;
			Func<ProgramSet, RewriteRule, ProgramSet> func;
			if ((func = Rewriter.<>O.<0>__Rewrite) == null)
			{
				func = (Rewriter.<>O.<0>__Rewrite = new Func<ProgramSet, RewriteRule, ProgramSet>(ProgramSetRewriter.Rewrite));
			}
			return rewriteRules.Aggregate(set, func);
		}

		// Token: 0x0600E7E4 RID: 59364 RVA: 0x00312424 File Offset: 0x00310624
		public static ProgramNode Rewrite(ProgramNode node)
		{
			IEnumerable<RewriteRule> rewriteRules = Rewriter.RewriteRules;
			Func<ProgramNode, RewriteRule, ProgramNode> func;
			if ((func = Rewriter.<>O.<1>__Rewrite) == null)
			{
				func = (Rewriter.<>O.<1>__Rewrite = new Func<ProgramNode, RewriteRule, ProgramNode>(ProgramSetRewriter.Rewrite));
			}
			ProgramNode programNode = ProgramSetRewriter.FoldConstants(rewriteRules.Aggregate(node, func));
			ProgramNode programNode2;
			do
			{
				programNode2 = programNode;
				IEnumerable<IFuncRewriteRule> constStrRewriteRules = Rewriter.ConstStrRewriteRules;
				ProgramNode programNode3 = programNode;
				Func<ProgramNode, IFuncRewriteRule, ProgramNode> func2;
				if ((func2 = Rewriter.<>O.<2>__Rewrite) == null)
				{
					func2 = (Rewriter.<>O.<2>__Rewrite = new Func<ProgramNode, IFuncRewriteRule, ProgramNode>(ProgramSetRewriter.Rewrite));
				}
				programNode = constStrRewriteRules.Aggregate(programNode3, func2);
			}
			while (programNode2 != programNode);
			return programNode;
		}

		// Token: 0x0600E7E5 RID: 59365 RVA: 0x00312490 File Offset: 0x00310690
		private static RewriteRule GenerateRangeRewriteRule<TRangeInput, TFormat, TRoundingSpec, TSharedParsed, TLet, TSharedFormat, TString, TSubstring, TValue>(Func<TRangeInput, TFormat, s, TRoundingSpec, TRoundingSpec, conv> formatRange, TRangeInput inputValue, TFormat format, TRoundingSpec lowerRoundingSpec, TRoundingSpec upperRoundingSpec, Func<TRangeInput, TLet, conv> letSharedInput, TSharedParsed sharedParsed, Func<TFormat, TString, TLet> letSharedFormat, TSharedFormat sharedFormat, Func<TSubstring, TString, TString> concat, Func<TValue, TSharedFormat, TSubstring> formatValue, Func<TSharedParsed, TRoundingSpec, TValue> roundValue, Func<s, TSubstring> constStr, Func<TSubstring, TString> convertStringSubstring)
		{
			s s = Rewriter.Hole.s;
			conv conv = formatRange(inputValue, format, s, lowerRoundingSpec, upperRoundingSpec);
			TSubstring tsubstring = formatValue(roundValue(sharedParsed, lowerRoundingSpec), sharedFormat);
			TSubstring tsubstring2 = formatValue(roundValue(sharedParsed, upperRoundingSpec), sharedFormat);
			TString tstring = concat(tsubstring, concat(constStr(s), convertStringSubstring(tsubstring2)));
			TLet tlet = letSharedFormat(format, tstring);
			conv conv2 = letSharedInput(inputValue, tlet);
			return new RewriteRule(conv.Node, conv2.Node);
		}

		// Token: 0x0600E7E6 RID: 59366 RVA: 0x0031252C File Offset: 0x0031072C
		internal static RewriteRule GenerateRegexPositionPairRewriteRule()
		{
			ProgramNode node = Rewriter.Rule.RegexPositionPair(Rewriter.Var.x, Rewriter.Hole.r, Rewriter.Hole.k).Node;
			RegularExpression regularExpression = new RegularExpression(0);
			r r = Rewriter.Rule.r(regularExpression);
			ProgramNode node2 = Rewriter.Rule.PosPair(Rewriter.Rule.RegexPositionRelative(Rewriter.Var.x, Rewriter.Rule.RegexPair(r, Rewriter.Hole.r), Rewriter.Hole.k), Rewriter.Rule.RegexPositionRelative(Rewriter.Var.x, Rewriter.Rule.RegexPair(Rewriter.Hole.r, r), Rewriter.Hole.k)).Node;
			return new RewriteRule(node, node2);
		}

		// Token: 0x0600E7E7 RID: 59367 RVA: 0x003125FC File Offset: 0x003107FC
		private static RewriteRule GenerateNumericRangeRewriteRule()
		{
			return Rewriter.GenerateRangeRewriteRule<inputNumber, numberFormat, roundingSpec, sharedParsedNumber, _LetB0, sharedNumberFormat, rangeString, rangeSubstring, rangeNumber>(new Func<inputNumber, numberFormat, s, roundingSpec, roundingSpec, conv>(Rewriter.Rule.FormatNumericRange), Rewriter.Hole.inputNumber, Rewriter.Hole.numberFormat, roundingSpec.CreateHole(Rewriter.Build, "lower"), roundingSpec.CreateHole(Rewriter.Build, "upper"), new Func<inputNumber, _LetB0, conv>(Rewriter.Rule.LetSharedParsedNumber), Rewriter.Var.sharedParsedNumber, new Func<numberFormat, rangeString, _LetB0>(Rewriter.Rule.LetSharedNumberFormat), Rewriter.Var.sharedNumberFormat, new Func<rangeSubstring, rangeString, rangeString>(Rewriter.Rule.RangeConcat), new Func<rangeNumber, sharedNumberFormat, rangeSubstring>(Rewriter.Rule.RangeFormatNumber), new Func<sharedParsedNumber, roundingSpec, rangeNumber>(Rewriter.Rule.RangeRoundNumber), new Func<s, rangeSubstring>(Rewriter.Rule.RangeConstStr), new Func<rangeSubstring, rangeString>(Rewriter.Build.Node.UnnamedConversion.rangeString_rangeSubstring));
		}

		// Token: 0x0600E7E8 RID: 59368 RVA: 0x003126E0 File Offset: 0x003108E0
		private static RewriteRule GenerateDateTimeRangeRewriteRule()
		{
			return Rewriter.GenerateRangeRewriteRule<inputDateTime, outputDtFormat, dtRoundingSpec, sharedParsedDt, _LetB1, sharedDtFormat, dtRangeString, dtRangeSubstring, rangeDateTime>(new Func<inputDateTime, outputDtFormat, s, dtRoundingSpec, dtRoundingSpec, conv>(Rewriter.Rule.FormatDateTimeRange), Rewriter.Hole.inputDateTime, Rewriter.Hole.outputDtFormat, dtRoundingSpec.CreateHole(Rewriter.Build, "lower"), dtRoundingSpec.CreateHole(Rewriter.Build, "upper"), new Func<inputDateTime, _LetB1, conv>(Rewriter.Rule.LetSharedParsedDateTime), Rewriter.Var.sharedParsedDt, new Func<outputDtFormat, dtRangeString, _LetB1>(Rewriter.Rule.LetSharedDateTimeFormat), Rewriter.Var.sharedDtFormat, new Func<dtRangeSubstring, dtRangeString, dtRangeString>(Rewriter.Rule.DtRangeConcat), new Func<rangeDateTime, sharedDtFormat, dtRangeSubstring>(Rewriter.Rule.RangeFormatDateTime), new Func<sharedParsedDt, dtRoundingSpec, rangeDateTime>(Rewriter.Rule.RangeRoundDateTime), new Func<s, dtRangeSubstring>(Rewriter.Rule.DtRangeConstStr), new Func<dtRangeSubstring, dtRangeString>(Rewriter.Build.Node.UnnamedConversion.dtRangeString_dtRangeSubstring));
		}

		// Token: 0x0600E7E9 RID: 59369 RVA: 0x003127C4 File Offset: 0x003109C4
		private static FuncRewriteRule<e, GrammarBuilders> GenerateConcatConstStrAtEndRewriteRule()
		{
			Rewriter.<>c__DisplayClass13_0 CS$<>8__locals1 = new Rewriter.<>c__DisplayClass13_0();
			CS$<>8__locals1.s1 = s.CreateHole(Rewriter.Build, "s1");
			CS$<>8__locals1.s2 = s.CreateHole(Rewriter.Build, "s2");
			e e = Rewriter.Rule.Concat(Rewriter.Rule.ConstStr(CS$<>8__locals1.s1), Rewriter.Rule.Atom(Rewriter.Rule.ConstStr(CS$<>8__locals1.s2)));
			Func<e, IReadOnlyDictionary<Hole, ProgramNode>, e> func = new Func<e, IReadOnlyDictionary<Hole, ProgramNode>, e>(CS$<>8__locals1.<GenerateConcatConstStrAtEndRewriteRule>g__MergeConstants|0);
			Func<GrammarBuilders, ProgramNode, e?> func2;
			if ((func2 = Rewriter.<>O.<3>__CreateSafe) == null)
			{
				func2 = (Rewriter.<>O.<3>__CreateSafe = new Func<GrammarBuilders, ProgramNode, e?>(e.CreateSafe));
			}
			return new FuncRewriteRule<e, GrammarBuilders>(e, func, func2, Rewriter.Build);
		}

		// Token: 0x0600E7EA RID: 59370 RVA: 0x00312868 File Offset: 0x00310A68
		private static FuncRewriteRule<e, GrammarBuilders> GenerateConcatConstStrRewriteRule()
		{
			Rewriter.<>c__DisplayClass14_0 CS$<>8__locals1 = new Rewriter.<>c__DisplayClass14_0();
			CS$<>8__locals1.s1 = s.CreateHole(Rewriter.Build, "s1");
			CS$<>8__locals1.s2 = s.CreateHole(Rewriter.Build, "s2");
			e e = Rewriter.Rule.Concat(Rewriter.Rule.ConstStr(CS$<>8__locals1.s1), Rewriter.Rule.Concat(Rewriter.Rule.ConstStr(CS$<>8__locals1.s2), Rewriter.Hole.e));
			Func<e, IReadOnlyDictionary<Hole, ProgramNode>, e> func = new Func<e, IReadOnlyDictionary<Hole, ProgramNode>, e>(CS$<>8__locals1.<GenerateConcatConstStrRewriteRule>g__MergeConstants|0);
			Func<GrammarBuilders, ProgramNode, e?> func2;
			if ((func2 = Rewriter.<>O.<3>__CreateSafe) == null)
			{
				func2 = (Rewriter.<>O.<3>__CreateSafe = new Func<GrammarBuilders, ProgramNode, e?>(e.CreateSafe));
			}
			return new FuncRewriteRule<e, GrammarBuilders>(e, func, func2, Rewriter.Build);
		}

		// Token: 0x04005840 RID: 22592
		private static readonly GrammarBuilders Build = Language.Build;

		// Token: 0x04005841 RID: 22593
		private static readonly GrammarBuilders.Nodes.NodeRules Rule = Language.Build.Node.Rule;

		// Token: 0x04005842 RID: 22594
		private static readonly GrammarBuilders.Nodes.NodeVariables Var = Language.Build.Node.Variable;

		// Token: 0x04005843 RID: 22595
		private static readonly GrammarBuilders.Nodes.NodeHoles Hole = Language.Build.Node.Hole;

		// Token: 0x04005844 RID: 22596
		private static readonly GrammarBuilders.GrammarSymbols Sym = Language.Build.Symbol;

		// Token: 0x04005845 RID: 22597
		private static readonly RewriteRule[] RewriteRules = new RewriteRule[]
		{
			Rewriter.GenerateNumericRangeRewriteRule(),
			Rewriter.GenerateDateTimeRangeRewriteRule(),
			Rewriter.GenerateRegexPositionPairRewriteRule()
		};

		// Token: 0x04005846 RID: 22598
		private static readonly IFuncRewriteRule[] ConstStrRewriteRules = new IFuncRewriteRule[]
		{
			Rewriter.GenerateConcatConstStrRewriteRule(),
			Rewriter.GenerateConcatConstStrAtEndRewriteRule()
		};

		// Token: 0x02001BA6 RID: 7078
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005847 RID: 22599
			public static Func<ProgramSet, RewriteRule, ProgramSet> <0>__Rewrite;

			// Token: 0x04005848 RID: 22600
			public static Func<ProgramNode, RewriteRule, ProgramNode> <1>__Rewrite;

			// Token: 0x04005849 RID: 22601
			public static Func<ProgramNode, IFuncRewriteRule, ProgramNode> <2>__Rewrite;

			// Token: 0x0400584A RID: 22602
			public static Func<GrammarBuilders, ProgramNode, e?> <3>__CreateSafe;
		}
	}
}
