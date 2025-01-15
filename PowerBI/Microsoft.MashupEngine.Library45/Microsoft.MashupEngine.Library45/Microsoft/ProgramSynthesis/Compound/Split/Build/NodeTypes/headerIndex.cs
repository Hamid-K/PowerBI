using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200097B RID: 2427
	public struct headerIndex : IProgramNodeBuilder, IEquatable<headerIndex>
	{
		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060039F4 RID: 14836 RVA: 0x000B2D06 File Offset: 0x000B0F06
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000B2D0E File Offset: 0x000B0F0E
		private headerIndex(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000B2D17 File Offset: 0x000B0F17
		public static headerIndex CreateUnsafe(ProgramNode node)
		{
			return new headerIndex(node);
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000B2D20 File Offset: 0x000B0F20
		public static headerIndex? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.headerIndex)
			{
				return null;
			}
			return new headerIndex?(headerIndex.CreateUnsafe(node));
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000B2D5A File Offset: 0x000B0F5A
		public static headerIndex CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new headerIndex(new Hole(g.Symbol.headerIndex, holeId));
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000B2D72 File Offset: 0x000B0F72
		public headerIndex(GrammarBuilders g, Optional<int> value)
		{
			this = new headerIndex(new LiteralNode(g.Symbol.headerIndex, value));
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060039FA RID: 14842 RVA: 0x000B2D90 File Offset: 0x000B0F90
		public Optional<int> Value
		{
			get
			{
				return (Optional<int>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000B2DA7 File Offset: 0x000B0FA7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000B2DBC File Offset: 0x000B0FBC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000B2DE6 File Offset: 0x000B0FE6
		public bool Equals(headerIndex other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A9B RID: 6811
		private ProgramNode _node;
	}
}
