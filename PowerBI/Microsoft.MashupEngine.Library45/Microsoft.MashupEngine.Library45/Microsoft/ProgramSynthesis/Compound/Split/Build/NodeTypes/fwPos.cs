using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000976 RID: 2422
	public struct fwPos : IProgramNodeBuilder, IEquatable<fwPos>
	{
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060039C2 RID: 14786 RVA: 0x000B2846 File Offset: 0x000B0A46
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x000B284E File Offset: 0x000B0A4E
		private fwPos(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x000B2857 File Offset: 0x000B0A57
		public static fwPos CreateUnsafe(ProgramNode node)
		{
			return new fwPos(node);
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x000B2860 File Offset: 0x000B0A60
		public static fwPos? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fwPos)
			{
				return null;
			}
			return new fwPos?(fwPos.CreateUnsafe(node));
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000B289A File Offset: 0x000B0A9A
		public static fwPos CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fwPos(new Hole(g.Symbol.fwPos, holeId));
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000B28B2 File Offset: 0x000B0AB2
		public fwPos(GrammarBuilders g, int value)
		{
			this = new fwPos(new LiteralNode(g.Symbol.fwPos, value));
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060039C8 RID: 14792 RVA: 0x000B28D0 File Offset: 0x000B0AD0
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000B28E7 File Offset: 0x000B0AE7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000B28FC File Offset: 0x000B0AFC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000B2926 File Offset: 0x000B0B26
		public bool Equals(fwPos other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A96 RID: 6806
		private ProgramNode _node;
	}
}
