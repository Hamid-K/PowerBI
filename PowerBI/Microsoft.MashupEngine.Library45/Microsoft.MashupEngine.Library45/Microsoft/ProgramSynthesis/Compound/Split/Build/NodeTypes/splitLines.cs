using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000968 RID: 2408
	public struct splitLines : IProgramNodeBuilder, IEquatable<splitLines>
	{
		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060038FE RID: 14590 RVA: 0x000B0D8E File Offset: 0x000AEF8E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000B0D96 File Offset: 0x000AEF96
		private splitLines(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x000B0D9F File Offset: 0x000AEF9F
		public static splitLines CreateUnsafe(ProgramNode node)
		{
			return new splitLines(node);
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x000B0DA8 File Offset: 0x000AEFA8
		public static splitLines? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitLines)
			{
				return null;
			}
			return new splitLines?(splitLines.CreateUnsafe(node));
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x000B0DE2 File Offset: 0x000AEFE2
		public static splitLines CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitLines(new Hole(g.Symbol.splitLines, holeId));
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000B0DFA File Offset: 0x000AEFFA
		public SplitSequenceLet Cast_SplitSequenceLet()
		{
			return SplitSequenceLet.CreateUnsafe(this.Node);
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SplitSequenceLet(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000B0E07 File Offset: 0x000AF007
		public bool Is_SplitSequenceLet(GrammarBuilders g, out SplitSequenceLet value)
		{
			value = SplitSequenceLet.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000B0E1B File Offset: 0x000AF01B
		public SplitSequenceLet? As_SplitSequenceLet(GrammarBuilders g)
		{
			return new SplitSequenceLet?(SplitSequenceLet.CreateUnsafe(this.Node));
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000B0E2D File Offset: 0x000AF02D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000B0E40 File Offset: 0x000AF040
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000B0E6A File Offset: 0x000AF06A
		public bool Equals(splitLines other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A88 RID: 6792
		private ProgramNode _node;
	}
}
