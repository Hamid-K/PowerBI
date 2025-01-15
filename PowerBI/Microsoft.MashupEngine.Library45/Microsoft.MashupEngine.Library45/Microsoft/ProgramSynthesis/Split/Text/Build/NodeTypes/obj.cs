using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001385 RID: 4997
	public struct obj : IProgramNodeBuilder, IEquatable<obj>
	{
		// Token: 0x17001A9D RID: 6813
		// (get) Token: 0x06009B25 RID: 39717 RVA: 0x0020BFDE File Offset: 0x0020A1DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009B26 RID: 39718 RVA: 0x0020BFE6 File Offset: 0x0020A1E6
		private obj(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009B27 RID: 39719 RVA: 0x0020BFEF File Offset: 0x0020A1EF
		public static obj CreateUnsafe(ProgramNode node)
		{
			return new obj(node);
		}

		// Token: 0x06009B28 RID: 39720 RVA: 0x0020BFF8 File Offset: 0x0020A1F8
		public static obj? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.obj)
			{
				return null;
			}
			return new obj?(obj.CreateUnsafe(node));
		}

		// Token: 0x06009B29 RID: 39721 RVA: 0x0020C032 File Offset: 0x0020A232
		public static obj CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new obj(new Hole(g.Symbol.obj, holeId));
		}

		// Token: 0x06009B2A RID: 39722 RVA: 0x0020C04A File Offset: 0x0020A24A
		public obj(GrammarBuilders g, object value)
		{
			this = new obj(new LiteralNode(g.Symbol.obj, value));
		}

		// Token: 0x17001A9E RID: 6814
		// (get) Token: 0x06009B2B RID: 39723 RVA: 0x0020C063 File Offset: 0x0020A263
		public object Value
		{
			get
			{
				return ((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009B2C RID: 39724 RVA: 0x0020C075 File Offset: 0x0020A275
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009B2D RID: 39725 RVA: 0x0020C088 File Offset: 0x0020A288
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009B2E RID: 39726 RVA: 0x0020C0B2 File Offset: 0x0020A2B2
		public bool Equals(obj other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DFC RID: 15868
		private ProgramNode _node;
	}
}
