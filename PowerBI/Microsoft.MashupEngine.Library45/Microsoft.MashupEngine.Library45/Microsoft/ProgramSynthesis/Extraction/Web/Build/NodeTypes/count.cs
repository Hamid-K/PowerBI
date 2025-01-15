using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001096 RID: 4246
	public struct count : IProgramNodeBuilder, IEquatable<count>
	{
		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x06007FE3 RID: 32739 RVA: 0x001ACA82 File Offset: 0x001AAC82
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007FE4 RID: 32740 RVA: 0x001ACA8A File Offset: 0x001AAC8A
		private count(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007FE5 RID: 32741 RVA: 0x001ACA93 File Offset: 0x001AAC93
		public static count CreateUnsafe(ProgramNode node)
		{
			return new count(node);
		}

		// Token: 0x06007FE6 RID: 32742 RVA: 0x001ACA9C File Offset: 0x001AAC9C
		public static count? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.count)
			{
				return null;
			}
			return new count?(count.CreateUnsafe(node));
		}

		// Token: 0x06007FE7 RID: 32743 RVA: 0x001ACAD6 File Offset: 0x001AACD6
		public static count CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new count(new Hole(g.Symbol.count, holeId));
		}

		// Token: 0x06007FE8 RID: 32744 RVA: 0x001ACAEE File Offset: 0x001AACEE
		public count(GrammarBuilders g, int value)
		{
			this = new count(new LiteralNode(g.Symbol.count, value));
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x06007FE9 RID: 32745 RVA: 0x001ACB0C File Offset: 0x001AAD0C
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007FEA RID: 32746 RVA: 0x001ACB23 File Offset: 0x001AAD23
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007FEB RID: 32747 RVA: 0x001ACB38 File Offset: 0x001AAD38
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007FEC RID: 32748 RVA: 0x001ACB62 File Offset: 0x001AAD62
		public bool Equals(count other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033AF RID: 13231
		private ProgramNode _node;
	}
}
