using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000985 RID: 2437
	public struct ls : IProgramNodeBuilder, IEquatable<ls>
	{
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x000B362E File Offset: 0x000B182E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x000B3636 File Offset: 0x000B1836
		private ls(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000B363F File Offset: 0x000B183F
		public static ls CreateUnsafe(ProgramNode node)
		{
			return new ls(node);
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000B3648 File Offset: 0x000B1848
		public static ls? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.ls)
			{
				return null;
			}
			return new ls?(ls.CreateUnsafe(node));
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x000B3682 File Offset: 0x000B1882
		public static ls CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new ls(new Hole(g.Symbol.ls, holeId));
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000B369A File Offset: 0x000B189A
		public ls(GrammarBuilders g)
		{
			this = new ls(new VariableNode(g.Symbol.ls));
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06003A5E RID: 14942 RVA: 0x000B36B2 File Offset: 0x000B18B2
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000B36BF File Offset: 0x000B18BF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000B36D4 File Offset: 0x000B18D4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000B36FE File Offset: 0x000B18FE
		public bool Equals(ls other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001AA5 RID: 6821
		private ProgramNode _node;
	}
}
