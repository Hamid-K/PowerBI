using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001382 RID: 4994
	public struct a : IProgramNodeBuilder, IEquatable<a>
	{
		// Token: 0x17001A97 RID: 6807
		// (get) Token: 0x06009B07 RID: 39687 RVA: 0x0020BD0A File Offset: 0x00209F0A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009B08 RID: 39688 RVA: 0x0020BD12 File Offset: 0x00209F12
		private a(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009B09 RID: 39689 RVA: 0x0020BD1B File Offset: 0x00209F1B
		public static a CreateUnsafe(ProgramNode node)
		{
			return new a(node);
		}

		// Token: 0x06009B0A RID: 39690 RVA: 0x0020BD24 File Offset: 0x00209F24
		public static a? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.a)
			{
				return null;
			}
			return new a?(a.CreateUnsafe(node));
		}

		// Token: 0x06009B0B RID: 39691 RVA: 0x0020BD5E File Offset: 0x00209F5E
		public static a CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new a(new Hole(g.Symbol.a, holeId));
		}

		// Token: 0x06009B0C RID: 39692 RVA: 0x0020BD76 File Offset: 0x00209F76
		public a(GrammarBuilders g, string value)
		{
			this = new a(new LiteralNode(g.Symbol.a, value));
		}

		// Token: 0x17001A98 RID: 6808
		// (get) Token: 0x06009B0D RID: 39693 RVA: 0x0020BD8F File Offset: 0x00209F8F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009B0E RID: 39694 RVA: 0x0020BDA6 File Offset: 0x00209FA6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009B0F RID: 39695 RVA: 0x0020BDBC File Offset: 0x00209FBC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009B10 RID: 39696 RVA: 0x0020BDE6 File Offset: 0x00209FE6
		public bool Equals(a other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DF9 RID: 15865
		private ProgramNode _node;
	}
}
