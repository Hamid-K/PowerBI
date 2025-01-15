using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F36 RID: 3894
	public struct output : IProgramNodeBuilder, IEquatable<output>
	{
		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06006BF5 RID: 27637 RVA: 0x00161D16 File Offset: 0x0015FF16
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006BF6 RID: 27638 RVA: 0x00161D1E File Offset: 0x0015FF1E
		private output(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006BF7 RID: 27639 RVA: 0x00161D27 File Offset: 0x0015FF27
		public static output CreateUnsafe(ProgramNode node)
		{
			return new output(node);
		}

		// Token: 0x06006BF8 RID: 27640 RVA: 0x00161D30 File Offset: 0x0015FF30
		public static output? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.output)
			{
				return null;
			}
			return new output?(output.CreateUnsafe(node));
		}

		// Token: 0x06006BF9 RID: 27641 RVA: 0x00161D6A File Offset: 0x0015FF6A
		public static output CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new output(new Hole(g.Symbol.output, holeId));
		}

		// Token: 0x06006BFA RID: 27642 RVA: 0x00161D82 File Offset: 0x0015FF82
		public Table Cast_Table()
		{
			return Table.CreateUnsafe(this.Node);
		}

		// Token: 0x06006BFB RID: 27643 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Table(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06006BFC RID: 27644 RVA: 0x00161D8F File Offset: 0x0015FF8F
		public bool Is_Table(GrammarBuilders g, out Table value)
		{
			value = Table.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06006BFD RID: 27645 RVA: 0x00161DA3 File Offset: 0x0015FFA3
		public Table? As_Table(GrammarBuilders g)
		{
			return new Table?(Table.CreateUnsafe(this.Node));
		}

		// Token: 0x06006BFE RID: 27646 RVA: 0x00161DB5 File Offset: 0x0015FFB5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006BFF RID: 27647 RVA: 0x00161DC8 File Offset: 0x0015FFC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006C00 RID: 27648 RVA: 0x00161DF2 File Offset: 0x0015FFF2
		public bool Equals(output other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F21 RID: 12065
		private ProgramNode _node;
	}
}
