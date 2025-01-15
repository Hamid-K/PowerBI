using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000965 RID: 2405
	public struct delimiterSplit : IProgramNodeBuilder, IEquatable<delimiterSplit>
	{
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x000B0ABE File Offset: 0x000AECBE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000B0AC6 File Offset: 0x000AECC6
		private delimiterSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000B0ACF File Offset: 0x000AECCF
		public static delimiterSplit CreateUnsafe(ProgramNode node)
		{
			return new delimiterSplit(node);
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x000B0AD8 File Offset: 0x000AECD8
		public static delimiterSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.delimiterSplit)
			{
				return null;
			}
			return new delimiterSplit?(delimiterSplit.CreateUnsafe(node));
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x000B0B12 File Offset: 0x000AED12
		public static delimiterSplit CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new delimiterSplit(new Hole(g.Symbol.delimiterSplit, holeId));
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x000B0B2A File Offset: 0x000AED2A
		public SplitToCells Cast_SplitToCells()
		{
			return SplitToCells.CreateUnsafe(this.Node);
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SplitToCells(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x000B0B37 File Offset: 0x000AED37
		public bool Is_SplitToCells(GrammarBuilders g, out SplitToCells value)
		{
			value = SplitToCells.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x000B0B4B File Offset: 0x000AED4B
		public SplitToCells? As_SplitToCells(GrammarBuilders g)
		{
			return new SplitToCells?(SplitToCells.CreateUnsafe(this.Node));
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x000B0B5D File Offset: 0x000AED5D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000B0B70 File Offset: 0x000AED70
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000B0B9A File Offset: 0x000AED9A
		public bool Equals(delimiterSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A85 RID: 6789
		private ProgramNode _node;
	}
}
