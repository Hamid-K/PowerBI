using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Tree.Learning;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree
{
	// Token: 0x02001E32 RID: 7730
	[DebuggerDisplay("ProgramNode")]
	public class MatchProgram : TransformationProgram<MatchProgram, Node, bool>
	{
		// Token: 0x0601020A RID: 66058 RVA: 0x00375B88 File Offset: 0x00373D88
		public MatchProgram(ProgramNode node, double score)
			: base(node, score, null)
		{
		}

		// Token: 0x0601020B RID: 66059 RVA: 0x00375B93 File Offset: 0x00373D93
		public MatchProgram(ProgramNode node, Feature<double> scoreFeature = null)
			: base(node, node.GetFeatureValue<double>(scoreFeature ?? new RankingScore(Language.Grammar, null), null), null)
		{
		}

		// Token: 0x0601020C RID: 66060 RVA: 0x00375BB4 File Offset: 0x00373DB4
		public override bool Run(Node input)
		{
			KeyValuePair<Symbol, object> keyValuePair = new KeyValuePair<Symbol, object>(Language.Grammar.InputSymbol, input);
			KeyValuePair<Symbol, object> keyValuePair2 = new KeyValuePair<Symbol, object>(Language.Build.Symbol.x, input);
			State state = State.CreateForExecution(new KeyValuePair<Symbol, object>[] { keyValuePair, keyValuePair2 });
			return (bool)base.ProgramNode.Invoke(state);
		}

		// Token: 0x0601020D RID: 66061 RVA: 0x00375C17 File Offset: 0x00373E17
		public string Serialize()
		{
			return base.ProgramNode.PrintAST(ASTSerializationFormat.XML);
		}

		// Token: 0x0601020E RID: 66062 RVA: 0x00375C25 File Offset: 0x00373E25
		public bool Equals(MatchProgram other)
		{
			return other != null && (this == other || object.Equals(base.ProgramNode, other.ProgramNode));
		}

		// Token: 0x0601020F RID: 66063 RVA: 0x00375C43 File Offset: 0x00373E43
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((MatchProgram)obj)));
		}

		// Token: 0x06010210 RID: 66064 RVA: 0x00375C71 File Offset: 0x00373E71
		public override int GetHashCode()
		{
			ProgramNode programNode = base.ProgramNode;
			if (programNode == null)
			{
				return 0;
			}
			return programNode.GetHashCode();
		}

		// Token: 0x06010211 RID: 66065 RVA: 0x00375C84 File Offset: 0x00373E84
		public override string ToString()
		{
			return string.Join(Environment.NewLine, GuardPrinter.Print(base.ProgramNode));
		}
	}
}
