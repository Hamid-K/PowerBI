using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001381 RID: 4993
	public struct s : IProgramNodeBuilder, IEquatable<s>
	{
		// Token: 0x17001A95 RID: 6805
		// (get) Token: 0x06009AFD RID: 39677 RVA: 0x0020BC1A File Offset: 0x00209E1A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009AFE RID: 39678 RVA: 0x0020BC22 File Offset: 0x00209E22
		private s(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009AFF RID: 39679 RVA: 0x0020BC2B File Offset: 0x00209E2B
		public static s CreateUnsafe(ProgramNode node)
		{
			return new s(node);
		}

		// Token: 0x06009B00 RID: 39680 RVA: 0x0020BC34 File Offset: 0x00209E34
		public static s? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.s)
			{
				return null;
			}
			return new s?(s.CreateUnsafe(node));
		}

		// Token: 0x06009B01 RID: 39681 RVA: 0x0020BC6E File Offset: 0x00209E6E
		public static s CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new s(new Hole(g.Symbol.s, holeId));
		}

		// Token: 0x06009B02 RID: 39682 RVA: 0x0020BC86 File Offset: 0x00209E86
		public s(GrammarBuilders g, string value)
		{
			this = new s(new LiteralNode(g.Symbol.s, value));
		}

		// Token: 0x17001A96 RID: 6806
		// (get) Token: 0x06009B03 RID: 39683 RVA: 0x0020BC9F File Offset: 0x00209E9F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009B04 RID: 39684 RVA: 0x0020BCB6 File Offset: 0x00209EB6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009B05 RID: 39685 RVA: 0x0020BCCC File Offset: 0x00209ECC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009B06 RID: 39686 RVA: 0x0020BCF6 File Offset: 0x00209EF6
		public bool Equals(s other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DF8 RID: 15864
		private ProgramNode _node;
	}
}
