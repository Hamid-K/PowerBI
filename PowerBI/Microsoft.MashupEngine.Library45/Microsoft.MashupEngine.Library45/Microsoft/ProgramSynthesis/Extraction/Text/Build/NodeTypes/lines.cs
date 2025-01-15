using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F3F RID: 3903
	public struct lines : IProgramNodeBuilder, IEquatable<lines>
	{
		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06006C95 RID: 27797 RVA: 0x0016377E File Offset: 0x0016197E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006C96 RID: 27798 RVA: 0x00163786 File Offset: 0x00161986
		private lines(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006C97 RID: 27799 RVA: 0x0016378F File Offset: 0x0016198F
		public static lines CreateUnsafe(ProgramNode node)
		{
			return new lines(node);
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x00163798 File Offset: 0x00161998
		public static lines? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.lines)
			{
				return null;
			}
			return new lines?(lines.CreateUnsafe(node));
		}

		// Token: 0x06006C99 RID: 27801 RVA: 0x001637D2 File Offset: 0x001619D2
		public static lines CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new lines(new Hole(g.Symbol.lines, holeId));
		}

		// Token: 0x06006C9A RID: 27802 RVA: 0x001637EA File Offset: 0x001619EA
		public SplitLines Cast_SplitLines()
		{
			return SplitLines.CreateUnsafe(this.Node);
		}

		// Token: 0x06006C9B RID: 27803 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SplitLines(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06006C9C RID: 27804 RVA: 0x001637F7 File Offset: 0x001619F7
		public bool Is_SplitLines(GrammarBuilders g, out SplitLines value)
		{
			value = SplitLines.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06006C9D RID: 27805 RVA: 0x0016380B File Offset: 0x00161A0B
		public SplitLines? As_SplitLines(GrammarBuilders g)
		{
			return new SplitLines?(SplitLines.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C9E RID: 27806 RVA: 0x0016381D File Offset: 0x00161A1D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006C9F RID: 27807 RVA: 0x00163830 File Offset: 0x00161A30
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006CA0 RID: 27808 RVA: 0x0016385A File Offset: 0x00161A5A
		public bool Equals(lines other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F2A RID: 12074
		private ProgramNode _node;
	}
}
