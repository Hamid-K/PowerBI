using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes
{
	// Token: 0x0200127D RID: 4733
	public struct ETextOutput : IProgramNodeBuilder, IEquatable<ETextOutput>
	{
		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x06008F05 RID: 36613 RVA: 0x001E1E82 File Offset: 0x001E0082
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F06 RID: 36614 RVA: 0x001E1E8A File Offset: 0x001E008A
		private ETextOutput(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F07 RID: 36615 RVA: 0x001E1E93 File Offset: 0x001E0093
		public static ETextOutput CreateUnsafe(ProgramNode node)
		{
			return new ETextOutput(node);
		}

		// Token: 0x06008F08 RID: 36616 RVA: 0x001E1E9C File Offset: 0x001E009C
		public static ETextOutput? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ETextOutput)
			{
				return null;
			}
			return new ETextOutput?(ETextOutput.CreateUnsafe(node));
		}

		// Token: 0x06008F09 RID: 36617 RVA: 0x001E1ED1 File Offset: 0x001E00D1
		public ETextOutput(GrammarBuilders g, output value0)
		{
			this._node = g.Rule.ETextOutput.BuildASTNode(value0.Node);
		}

		// Token: 0x06008F0A RID: 36618 RVA: 0x001E1EF0 File Offset: 0x001E00F0
		public static implicit operator Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1(ETextOutput arg)
		{
			return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1.CreateUnsafe(arg.Node);
		}

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x06008F0B RID: 36619 RVA: 0x001E1EFE File Offset: 0x001E00FE
		public output output
		{
			get
			{
				return output.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06008F0C RID: 36620 RVA: 0x001E1F12 File Offset: 0x001E0112
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F0D RID: 36621 RVA: 0x001E1F28 File Offset: 0x001E0128
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F0E RID: 36622 RVA: 0x001E1F52 File Offset: 0x001E0152
		public bool Equals(ETextOutput other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A6E RID: 14958
		private ProgramNode _node;
	}
}
