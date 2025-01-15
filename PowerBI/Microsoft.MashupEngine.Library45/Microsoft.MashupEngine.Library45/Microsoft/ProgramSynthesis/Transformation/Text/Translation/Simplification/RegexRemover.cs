using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Simplification
{
	// Token: 0x02001D9A RID: 7578
	internal class RegexRemover
	{
		// Token: 0x0600FE7A RID: 65146 RVA: 0x003655C4 File Offset: 0x003637C4
		internal RegexRemover(IEnumerable<Constraint<IRow, object>> examples)
		{
			this._examples = ((examples != null) ? examples.ToList<Constraint<IRow, object>>() : null);
			this._substrPattern0 = RegexRemover.Rule.SingleBranch(RegexRemover.Rule.Transformation(RegexRemover.Rule.Atom(RegexRemover.Rule.LetColumnName(RegexRemover.Hole.idx, RegexRemover.Rule.LetX(RegexRemover.Rule.ChooseInput(RegexRemover.Var.vs, RegexRemover.Hole.columnName), RegexRemover.Hole.conv))))).Node;
			this._substrPatternList = new List<ProgramNode>
			{
				RegexRemover.Rule.SubString(RegexRemover.Hole.SS).Node,
				RegexRemover.Rule.ToLowercase(RegexRemover.Hole.SS).Node,
				RegexRemover.Rule.ToUppercase(RegexRemover.Hole.SS).Node,
				RegexRemover.Rule.ToSimpleTitleCase(RegexRemover.Hole.SS).Node
			};
			this._rHole1 = RegexRemover.Hole.r;
			this._rHole2 = r.CreateHole(RegexRemover.builders, "2");
			this._rHole3 = r.CreateHole(RegexRemover.builders, "3");
			this._rHole4 = r.CreateHole(RegexRemover.builders, "4");
			this._kHole1 = RegexRemover.Hole.k;
			this._kHole2 = k.CreateHole(RegexRemover.builders, "2");
			this._substrPattern1 = RegexRemover.Rule.SubStr(RegexRemover.Hole.x, RegexRemover.Rule.PosPair(RegexRemover.Rule.RegexPositionRelative(RegexRemover.Hole.x, RegexRemover.Rule.RegexPair(this._rHole1, this._rHole2), this._kHole1), RegexRemover.Rule.RegexPositionRelative(RegexRemover.Hole.x, RegexRemover.Rule.RegexPair(this._rHole3, this._rHole4), this._kHole2))).Node;
		}

		// Token: 0x0600FE7B RID: 65147 RVA: 0x003657F0 File Offset: 0x003639F0
		private bool IsASubStrProgram(ProgramNode p, out IReadOnlyDictionary<Hole, ProgramNode> substitution)
		{
			substitution = ProgramSetRewriter.ExtractMappings(p, this._substrPattern0);
			if (substitution == null)
			{
				return false;
			}
			ProgramNode convNode = substitution[(Hole)RegexRemover.Hole.conv.Node];
			IReadOnlyDictionary<Hole, ProgramNode> readOnlyDictionary = this._substrPatternList.Select((ProgramNode pattern) => ProgramSetRewriter.ExtractMappings(convNode, pattern)).FirstOrDefault((IReadOnlyDictionary<Hole, ProgramNode> x) => x != null);
			if (readOnlyDictionary == null)
			{
				return false;
			}
			readOnlyDictionary = ProgramSetRewriter.ExtractMappings(readOnlyDictionary[(Hole)RegexRemover.Hole.SS.Node], this._substrPattern1);
			if (readOnlyDictionary == null)
			{
				return false;
			}
			substitution = readOnlyDictionary.Concat(substitution).ToDictionary<Hole, ProgramNode>();
			return true;
		}

		// Token: 0x0600FE7C RID: 65148 RVA: 0x003658BC File Offset: 0x00363ABC
		internal Program GetAlternative(Program program)
		{
			ProgramNode programNode = program.ProgramNode;
			IReadOnlyDictionary<Hole, ProgramNode> readOnlyDictionary;
			if (this._examples == null || !this.IsASubStrProgram(programNode, out readOnlyDictionary))
			{
				return null;
			}
			List<Constraint<IRow, object>> list = new List<Constraint<IRow, object>>
			{
				new RegexTokenConstraint(RegexRemover._simpleTokens),
				new ForbidTransformation(TransformationKind.Concat | TransformationKind.Lookup | TransformationKind.ParseNumber | TransformationKind.ParseDateTime | TransformationKind.RoundNumber | TransformationKind.RoundDateTime | TransformationKind.FormatNumber | TransformationKind.FormatNumericRange | TransformationKind.FormatDateTime | TransformationKind.FormatDateTimeRange | TransformationKind.InputNumber | TransformationKind.InputDate | TransformationKind.IfThenElse)
			};
			list.AddRange(this._examples);
			return Learner.Instance.Learn(list, null, default(CancellationToken));
		}

		// Token: 0x0600FE7D RID: 65149 RVA: 0x0036592C File Offset: 0x00363B2C
		private static KeyValuePair<string, Token> mkTokenKVP(string name, string regex, bool useAsCanonicalRepresentation = true, int score = 15, string ws = "\\p{Zs}*", string canonicalRepresentation = null)
		{
			string text = (useAsCanonicalRepresentation ? Regex.Escape(regex) : regex);
			return new KeyValuePair<string, Token>(name, new RegexToken(ws + text + ws, name, score, -5.5, (string s) => -Math.Log(2.0), true, true, canonicalRepresentation ?? (useAsCanonicalRepresentation ? regex : null)));
		}

		// Token: 0x0600FE7E RID: 65150 RVA: 0x00365994 File Offset: 0x00363B94
		public static bool IsSeparatorWithWs(Token token, out string separator)
		{
			Token token2;
			if (RegexRemover._simpleTokens.TryGetValue(token.Name, out token2))
			{
				separator = token2.CanonicalRepresentation;
				return true;
			}
			separator = null;
			return false;
		}

		// Token: 0x0600FE7F RID: 65151 RVA: 0x003659C3 File Offset: 0x00363BC3
		public static bool IsZeroLengthToken(Token token)
		{
			return RegexRemover.ZeroLengthTokens.Contains(token.Name);
		}

		// Token: 0x04005F44 RID: 24388
		private static readonly GrammarBuilders builders = Language.Build;

		// Token: 0x04005F45 RID: 24389
		private static readonly GrammarBuilders.Nodes.NodeRules Rule = Language.Build.Node.Rule;

		// Token: 0x04005F46 RID: 24390
		private static readonly GrammarBuilders.Nodes.NodeVariables Var = Language.Build.Node.Variable;

		// Token: 0x04005F47 RID: 24391
		private static readonly GrammarBuilders.Nodes.NodeHoles Hole = Language.Build.Node.Hole;

		// Token: 0x04005F48 RID: 24392
		private static readonly r Epsilon = RegexRemover.Rule.r(new RegularExpression(0));

		// Token: 0x04005F49 RID: 24393
		private readonly ProgramNode _substrPattern0;

		// Token: 0x04005F4A RID: 24394
		private readonly List<ProgramNode> _substrPatternList;

		// Token: 0x04005F4B RID: 24395
		private readonly ProgramNode _substrPattern1;

		// Token: 0x04005F4C RID: 24396
		private readonly List<Constraint<IRow, object>> _examples;

		// Token: 0x04005F4D RID: 24397
		private readonly r _rHole1;

		// Token: 0x04005F4E RID: 24398
		private readonly r _rHole2;

		// Token: 0x04005F4F RID: 24399
		private readonly r _rHole3;

		// Token: 0x04005F50 RID: 24400
		private readonly r _rHole4;

		// Token: 0x04005F51 RID: 24401
		private readonly k _kHole1;

		// Token: 0x04005F52 RID: 24402
		private readonly k _kHole2;

		// Token: 0x04005F53 RID: 24403
		private const TransformationKind _forbiddenTransformations = TransformationKind.Concat | TransformationKind.Lookup | TransformationKind.ParseNumber | TransformationKind.ParseDateTime | TransformationKind.RoundNumber | TransformationKind.RoundDateTime | TransformationKind.FormatNumber | TransformationKind.FormatNumericRange | TransformationKind.FormatDateTime | TransformationKind.FormatDateTimeRange | TransformationKind.InputNumber | TransformationKind.InputDate | TransformationKind.IfThenElse;

		// Token: 0x04005F54 RID: 24404
		private const double LogDefaultStaticPrior = -5.5;

		// Token: 0x04005F55 RID: 24405
		private static IReadOnlyDictionary<string, Token> _simpleTokens = new KeyValuePair<string, Token>[]
		{
			RegexRemover.mkTokenKVP("WsCommaWs", ",", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsSemiColonWs", ";", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsColonWs", ":", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsExclamationWs", "!", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsLeftParenWs", "(", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsRightParenWs", ")", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsQuoteWs", "'", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsDoubleQuoteWs", "\"", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsBarWs", "|", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsAndWs", "and", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsSlashWs", "/", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsBackSlashWs", "\\", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsHyphenWs", "-", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsDotWs", ".", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsStarWs", "*", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsPlusWs", "+", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsUnderscoreWs", "_", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsEqualWs", "=", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsGreaterThanWs", ">", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsLessThanWs", "<", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsLeftBracketWs", "[", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsRightBracketWs", "]", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsLeftBraceWs", "{", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsRightBraceWs", "}", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsAmpersandWs", "&", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsHashWs", "#", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsDollarWs", "$", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsHatWs", "^", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsAtWs", "@", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsPercentageWs", "%", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsQuestionMarkWs", "?", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("WsTildeWs", "~", true, 15, "\\p{Zs}*", null),
			RegexRemover.mkTokenKVP("SpaceTabSpace", "\\t", false, 15, "[ ]*", "\t"),
			RegexRemover.mkTokenKVP("SpaceNewLineSpace", "\\n", false, 15, "[ ]*", "\n"),
			RegexRemover.mkTokenKVP("Begin", "^", false, 15, string.Empty, null),
			RegexRemover.mkTokenKVP("End", "$", false, 15, string.Empty, null),
			RegexRemover.mkTokenKVP("Space", " ", true, 15, "\\p{Zs}*", null)
		}.ToDictionary((KeyValuePair<string, Token> kv) => kv.Key, (KeyValuePair<string, Token> kv) => kv.Value);

		// Token: 0x04005F56 RID: 24406
		private static readonly HashSet<string> ZeroLengthTokens = new HashSet<string> { "Begin", "End" };
	}
}
