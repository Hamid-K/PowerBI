using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001094 RID: 4244
	public struct idx2 : IProgramNodeBuilder, IEquatable<idx2>
	{
		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x06007FCF RID: 32719 RVA: 0x001AC89E File Offset: 0x001AAA9E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007FD0 RID: 32720 RVA: 0x001AC8A6 File Offset: 0x001AAAA6
		private idx2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007FD1 RID: 32721 RVA: 0x001AC8AF File Offset: 0x001AAAAF
		public static idx2 CreateUnsafe(ProgramNode node)
		{
			return new idx2(node);
		}

		// Token: 0x06007FD2 RID: 32722 RVA: 0x001AC8B8 File Offset: 0x001AAAB8
		public static idx2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.idx2)
			{
				return null;
			}
			return new idx2?(idx2.CreateUnsafe(node));
		}

		// Token: 0x06007FD3 RID: 32723 RVA: 0x001AC8F2 File Offset: 0x001AAAF2
		public static idx2 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new idx2(new Hole(g.Symbol.idx2, holeId));
		}

		// Token: 0x06007FD4 RID: 32724 RVA: 0x001AC90A File Offset: 0x001AAB0A
		public idx2(GrammarBuilders g, int value)
		{
			this = new idx2(new LiteralNode(g.Symbol.idx2, value));
		}

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06007FD5 RID: 32725 RVA: 0x001AC928 File Offset: 0x001AAB28
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007FD6 RID: 32726 RVA: 0x001AC93F File Offset: 0x001AAB3F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007FD7 RID: 32727 RVA: 0x001AC954 File Offset: 0x001AAB54
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007FD8 RID: 32728 RVA: 0x001AC97E File Offset: 0x001AAB7E
		public bool Equals(idx2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033AD RID: 13229
		private ProgramNode _node;
	}
}
