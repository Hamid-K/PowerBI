using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015EE RID: 5614
	public struct splitDelimiter : IProgramNodeBuilder, IEquatable<splitDelimiter>
	{
		// Token: 0x17002033 RID: 8243
		// (get) Token: 0x0600BA70 RID: 47728 RVA: 0x002829F6 File Offset: 0x00280BF6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA71 RID: 47729 RVA: 0x002829FE File Offset: 0x00280BFE
		private splitDelimiter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA72 RID: 47730 RVA: 0x00282A07 File Offset: 0x00280C07
		public static splitDelimiter CreateUnsafe(ProgramNode node)
		{
			return new splitDelimiter(node);
		}

		// Token: 0x0600BA73 RID: 47731 RVA: 0x00282A10 File Offset: 0x00280C10
		public static splitDelimiter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitDelimiter)
			{
				return null;
			}
			return new splitDelimiter?(splitDelimiter.CreateUnsafe(node));
		}

		// Token: 0x0600BA74 RID: 47732 RVA: 0x00282A4A File Offset: 0x00280C4A
		public static splitDelimiter CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitDelimiter(new Hole(g.Symbol.splitDelimiter, holeId));
		}

		// Token: 0x0600BA75 RID: 47733 RVA: 0x00282A62 File Offset: 0x00280C62
		public splitDelimiter(GrammarBuilders g, string value)
		{
			this = new splitDelimiter(new LiteralNode(g.Symbol.splitDelimiter, value));
		}

		// Token: 0x17002034 RID: 8244
		// (get) Token: 0x0600BA76 RID: 47734 RVA: 0x00282A7B File Offset: 0x00280C7B
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA77 RID: 47735 RVA: 0x00282A92 File Offset: 0x00280C92
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA78 RID: 47736 RVA: 0x00282AA8 File Offset: 0x00280CA8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA79 RID: 47737 RVA: 0x00282AD2 File Offset: 0x00280CD2
		public bool Equals(splitDelimiter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400469C RID: 18076
		private ProgramNode _node;
	}
}
