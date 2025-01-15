using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x02001284 RID: 4740
	public struct skip : IProgramNodeBuilder, IEquatable<skip>
	{
		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x06008F5E RID: 36702 RVA: 0x001E286A File Offset: 0x001E0A6A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F5F RID: 36703 RVA: 0x001E2872 File Offset: 0x001E0A72
		private skip(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F60 RID: 36704 RVA: 0x001E287B File Offset: 0x001E0A7B
		public static skip CreateUnsafe(ProgramNode node)
		{
			return new skip(node);
		}

		// Token: 0x06008F61 RID: 36705 RVA: 0x001E2884 File Offset: 0x001E0A84
		public static skip? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.skip)
			{
				return null;
			}
			return new skip?(skip.CreateUnsafe(node));
		}

		// Token: 0x06008F62 RID: 36706 RVA: 0x001E28BE File Offset: 0x001E0ABE
		public static skip CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new skip(new Hole(g.Symbol.skip, holeId));
		}

		// Token: 0x06008F63 RID: 36707 RVA: 0x001E28D6 File Offset: 0x001E0AD6
		public skip(GrammarBuilders g, int value)
		{
			this = new skip(new LiteralNode(g.Symbol.skip, value));
		}

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x06008F64 RID: 36708 RVA: 0x001E28F4 File Offset: 0x001E0AF4
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008F65 RID: 36709 RVA: 0x001E290B File Offset: 0x001E0B0B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F66 RID: 36710 RVA: 0x001E2920 File Offset: 0x001E0B20
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F67 RID: 36711 RVA: 0x001E294A File Offset: 0x001E0B4A
		public bool Equals(skip other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A75 RID: 14965
		private ProgramNode _node;
	}
}
