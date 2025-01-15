using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Simplification
{
	// Token: 0x02001D99 RID: 7577
	internal class GeneralizedErreRewrite : TTextAlternativeSelector
	{
		// Token: 0x0600FE72 RID: 65138 RVA: 0x00364E7E File Offset: 0x0036307E
		private GeneralizedErreRewrite()
		{
		}

		// Token: 0x0600FE73 RID: 65139 RVA: 0x0036523D File Offset: 0x0036343D
		protected override IEnumerable<ProgramNode> GetAlternatives(ProgramNode p)
		{
			return GeneralizedErreRewrite.MaybeErreProgram(p).AsEnumerable<ProgramNode>();
		}

		// Token: 0x17002A63 RID: 10851
		// (get) Token: 0x0600FE74 RID: 65140 RVA: 0x0036524A File Offset: 0x0036344A
		public static GeneralizedErreRewrite Instance { get; } = new GeneralizedErreRewrite();

		// Token: 0x17002A64 RID: 10852
		// (get) Token: 0x0600FE75 RID: 65141 RVA: 0x00365254 File Offset: 0x00363454
		private static ProgramNode SubstrPosPairPattern
		{
			get
			{
				return GeneralizedErreRewrite.Rule.PosPair(GeneralizedErreRewrite.Rule.RegexPositionRelative(GeneralizedErreRewrite.Hole.x, GeneralizedErreRewrite.Rule.RegexPair(GeneralizedErreRewrite.RHole1, GeneralizedErreRewrite.RHole2), GeneralizedErreRewrite.KHole1), GeneralizedErreRewrite.Rule.RegexPositionRelative(GeneralizedErreRewrite.Hole.x, GeneralizedErreRewrite.Rule.RegexPair(GeneralizedErreRewrite.RHole3, GeneralizedErreRewrite.RHole4), GeneralizedErreRewrite.KHole2)).Node;
			}
		}

		// Token: 0x0600FE76 RID: 65142 RVA: 0x003652D0 File Offset: 0x003634D0
		private static bool TryGetTokens(IReadOnlyDictionary<Hole, ProgramNode> substitution, r rHole, out AbstractRegexToken[] tokens)
		{
			RegularExpression value = r.CreateUnsafe(substitution[(Hole)rHole.Node]).Value;
			tokens = value.Tokens.OfType<AbstractRegexToken>().ToArray<AbstractRegexToken>();
			return tokens.Length == value.Tokens.Length;
		}

		// Token: 0x0600FE77 RID: 65143 RVA: 0x00365320 File Offset: 0x00363520
		private static int GetInt(IReadOnlyDictionary<Hole, ProgramNode> substitution, k kHole)
		{
			return k.CreateUnsafe(substitution[(Hole)kHole.Node]).Value;
		}

		// Token: 0x0600FE78 RID: 65144 RVA: 0x0036534C File Offset: 0x0036354C
		private static Optional<ProgramNode> MaybeErreProgram(ProgramNode p)
		{
			IReadOnlyDictionary<Hole, ProgramNode> readOnlyDictionary = ProgramSetRewriter.ExtractMappings(p, GeneralizedErreRewrite.SubstrPosPairPattern);
			if (readOnlyDictionary == null)
			{
				return default(Optional<ProgramNode>);
			}
			int @int = GeneralizedErreRewrite.GetInt(readOnlyDictionary, GeneralizedErreRewrite.KHole1);
			int int2 = GeneralizedErreRewrite.GetInt(readOnlyDictionary, GeneralizedErreRewrite.KHole2);
			if (@int != int2)
			{
				return default(Optional<ProgramNode>);
			}
			AbstractRegexToken[] array;
			AbstractRegexToken[] array2;
			AbstractRegexToken[] array3;
			AbstractRegexToken[] array4;
			if (!GeneralizedErreRewrite.TryGetTokens(readOnlyDictionary, GeneralizedErreRewrite.RHole1, out array) || !GeneralizedErreRewrite.TryGetTokens(readOnlyDictionary, GeneralizedErreRewrite.RHole2, out array2) || !GeneralizedErreRewrite.TryGetTokens(readOnlyDictionary, GeneralizedErreRewrite.RHole3, out array3) || !GeneralizedErreRewrite.TryGetTokens(readOnlyDictionary, GeneralizedErreRewrite.RHole4, out array4))
			{
				return default(Optional<ProgramNode>);
			}
			if (!array.Concat(array2).SequenceEqual(array3.Concat(array4)))
			{
				return default(Optional<ProgramNode>);
			}
			AbstractRegexToken[] array5 = array2.Take(array2.Length - array4.Length).ToArray<AbstractRegexToken>();
			string text = ((array.Length != 0) ? ("(?<=" + ReadablePythonTranslator.TokenArray2PythonRegEx(array) + ")") : string.Empty);
			string text2 = ReadablePythonTranslator.TokenArray2PythonRegEx(array5);
			string text3 = ((array4.Length != 0) ? ("(?=" + ReadablePythonTranslator.TokenArray2PythonRegEx(array4) + ")") : string.Empty);
			string text4 = text + text2 + text3;
			string text5 = ReadablePythonTranslator.TokenArray2PythonVariableName(array5);
			RegexToken regexToken = new RegexToken(text4, text5, 0, 1.0, null, true, true, null);
			r r = Language.Build.Node.Rule.r(new RegularExpression(new RegexToken[] { regexToken }, 0));
			return GeneralizedErreRewrite.Rule.RegexPositionPair(x.CreateUnsafe(readOnlyDictionary[(Hole)GeneralizedErreRewrite.Hole.x.Node]), r, k.CreateUnsafe(readOnlyDictionary[(Hole)GeneralizedErreRewrite.KHole1.Node])).Node.Some<ProgramNode>();
		}

		// Token: 0x04005F3C RID: 24380
		private static readonly GrammarBuilders.Nodes.NodeRules Rule = Language.Build.Node.Rule;

		// Token: 0x04005F3D RID: 24381
		private static readonly GrammarBuilders.Nodes.NodeHoles Hole = Language.Build.Node.Hole;

		// Token: 0x04005F3E RID: 24382
		private static readonly r RHole1 = GeneralizedErreRewrite.Hole.r;

		// Token: 0x04005F3F RID: 24383
		private static readonly r RHole2 = r.CreateHole(Language.Build, "2");

		// Token: 0x04005F40 RID: 24384
		private static readonly r RHole3 = r.CreateHole(Language.Build, "3");

		// Token: 0x04005F41 RID: 24385
		private static readonly r RHole4 = r.CreateHole(Language.Build, "4");

		// Token: 0x04005F42 RID: 24386
		private static readonly k KHole1 = GeneralizedErreRewrite.Hole.k;

		// Token: 0x04005F43 RID: 24387
		private static readonly k KHole2 = k.CreateHole(Language.Build, "2");
	}
}
