using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000986 RID: 2438
	public struct s : IProgramNodeBuilder, IEquatable<s>
	{
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000B3712 File Offset: 0x000B1912
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x000B371A File Offset: 0x000B191A
		private s(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x000B3723 File Offset: 0x000B1923
		public static s CreateUnsafe(ProgramNode node)
		{
			return new s(node);
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000B372C File Offset: 0x000B192C
		public static s? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.s)
			{
				return null;
			}
			return new s?(s.CreateUnsafe(node));
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x000B3766 File Offset: 0x000B1966
		public static s CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new s(new Hole(g.Symbol.s, holeId));
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x000B377E File Offset: 0x000B197E
		public s(GrammarBuilders g)
		{
			this = new s(new VariableNode(g.Symbol.s));
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06003A68 RID: 14952 RVA: 0x000B3796 File Offset: 0x000B1996
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x000B37A3 File Offset: 0x000B19A3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x000B37B8 File Offset: 0x000B19B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x000B37E2 File Offset: 0x000B19E2
		public bool Equals(s other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001AA6 RID: 6822
		private ProgramNode _node;
	}
}
