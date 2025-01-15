using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200097D RID: 2429
	public struct skipEmpty : IProgramNodeBuilder, IEquatable<skipEmpty>
	{
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x000B2EEE File Offset: 0x000B10EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x000B2EF6 File Offset: 0x000B10F6
		private skipEmpty(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x000B2EFF File Offset: 0x000B10FF
		public static skipEmpty CreateUnsafe(ProgramNode node)
		{
			return new skipEmpty(node);
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x000B2F08 File Offset: 0x000B1108
		public static skipEmpty? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.skipEmpty)
			{
				return null;
			}
			return new skipEmpty?(skipEmpty.CreateUnsafe(node));
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000B2F42 File Offset: 0x000B1142
		public static skipEmpty CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new skipEmpty(new Hole(g.Symbol.skipEmpty, holeId));
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x000B2F5A File Offset: 0x000B115A
		public skipEmpty(GrammarBuilders g, bool value)
		{
			this = new skipEmpty(new LiteralNode(g.Symbol.skipEmpty, value));
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x000B2F78 File Offset: 0x000B1178
		public bool Value
		{
			get
			{
				return (bool)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000B2F8F File Offset: 0x000B118F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000B2FA4 File Offset: 0x000B11A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000B2FCE File Offset: 0x000B11CE
		public bool Equals(skipEmpty other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A9D RID: 6813
		private ProgramNode _node;
	}
}
