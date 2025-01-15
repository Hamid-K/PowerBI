using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Split.Translation
{
	// Token: 0x020013FE RID: 5118
	internal class DelimiterCollector : ProgramNodeVisitor<bool>
	{
		// Token: 0x06009E10 RID: 40464 RVA: 0x00218673 File Offset: 0x00216873
		private DelimiterCollector()
		{
		}

		// Token: 0x06009E11 RID: 40465 RVA: 0x00218688 File Offset: 0x00216888
		public static Optional<IReadOnlyList<Delimiter>> MaybeCollectConstantDelimiters(ProgramNode node)
		{
			DelimiterCollector delimiterCollector = new DelimiterCollector();
			return node.AcceptVisitor<bool>(delimiterCollector).Then(delimiterCollector.Delimiters);
		}

		// Token: 0x06009E12 RID: 40466 RVA: 0x002186B0 File Offset: 0x002168B0
		public override bool VisitNonterminal(NonterminalNode node)
		{
			LookAround lookAround;
			if (Language.Build.Node.IsRule.LookAround(node, out lookAround))
			{
				if (lookAround.r1.Is_Empty(Language.Build) && lookAround.r2.Is_Empty(Language.Build))
				{
					ConstStr constStr;
					if (lookAround.c.Is_ConstStr(Language.Build, out constStr))
					{
						string text = constStr.s.Value;
						this._delimiters.Add(new Delimiter(text, false));
						return true;
					}
					ConstStrWithWhitespace constStrWithWhitespace;
					if (lookAround.c.Is_ConstStrWithWhitespace(Language.Build, out constStrWithWhitespace))
					{
						string text;
						if (string.IsNullOrEmpty(constStrWithWhitespace.s.Value))
						{
							text = "\\s*";
						}
						else if (constStrWithWhitespace.s.Value == " ")
						{
							text = "\\s+";
						}
						else
						{
							text = "\\s*" + Regex.Escape(constStrWithWhitespace.s.Value) + "\\s*";
						}
						this._delimiters.Add(new Delimiter(text, true));
						return true;
					}
					ConstAlphStr constAlphStr;
					if (lookAround.c.Is_ConstAlphStr(Language.Build, out constAlphStr))
					{
						string text = "\\s*(?<![A-Za-z])" + Regex.Escape(constAlphStr.a.Value) + "\\s*(?<![A-Za-z])";
						this._delimiters.Add(new Delimiter(text, true));
						return true;
					}
				}
				return false;
			}
			bool flag = true;
			foreach (ProgramNode programNode in node.Children)
			{
				flag &= programNode.AcceptVisitor<bool>(this);
			}
			return flag;
		}

		// Token: 0x06009E13 RID: 40467 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool VisitLet(LetNode node)
		{
			return false;
		}

		// Token: 0x06009E14 RID: 40468 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool VisitLambda(LambdaNode node)
		{
			return false;
		}

		// Token: 0x06009E15 RID: 40469 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool VisitLiteral(LiteralNode node)
		{
			return false;
		}

		// Token: 0x06009E16 RID: 40470 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool VisitVariable(VariableNode node)
		{
			return false;
		}

		// Token: 0x06009E17 RID: 40471 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool VisitHole(Hole node)
		{
			return false;
		}

		// Token: 0x17001AC5 RID: 6853
		// (get) Token: 0x06009E18 RID: 40472 RVA: 0x00218866 File Offset: 0x00216A66
		public IReadOnlyList<Delimiter> Delimiters
		{
			get
			{
				return this._delimiters;
			}
		}

		// Token: 0x04003FFE RID: 16382
		private readonly List<Delimiter> _delimiters = new List<Delimiter>();
	}
}
