using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001377 RID: 4983
	public struct pattern : IProgramNodeBuilder, IEquatable<pattern>
	{
		// Token: 0x17001A81 RID: 6785
		// (get) Token: 0x06009A99 RID: 39577 RVA: 0x0020B2A6 File Offset: 0x002094A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A9A RID: 39578 RVA: 0x0020B2AE File Offset: 0x002094AE
		private pattern(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A9B RID: 39579 RVA: 0x0020B2B7 File Offset: 0x002094B7
		public static pattern CreateUnsafe(ProgramNode node)
		{
			return new pattern(node);
		}

		// Token: 0x06009A9C RID: 39580 RVA: 0x0020B2C0 File Offset: 0x002094C0
		public static pattern? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.pattern)
			{
				return null;
			}
			return new pattern?(pattern.CreateUnsafe(node));
		}

		// Token: 0x06009A9D RID: 39581 RVA: 0x0020B2FA File Offset: 0x002094FA
		public static pattern CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new pattern(new Hole(g.Symbol.pattern, holeId));
		}

		// Token: 0x06009A9E RID: 39582 RVA: 0x0020B312 File Offset: 0x00209512
		public pattern(GrammarBuilders g, string value)
		{
			this = new pattern(new LiteralNode(g.Symbol.pattern, value));
		}

		// Token: 0x17001A82 RID: 6786
		// (get) Token: 0x06009A9F RID: 39583 RVA: 0x0020B32B File Offset: 0x0020952B
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009AA0 RID: 39584 RVA: 0x0020B342 File Offset: 0x00209542
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009AA1 RID: 39585 RVA: 0x0020B358 File Offset: 0x00209558
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009AA2 RID: 39586 RVA: 0x0020B382 File Offset: 0x00209582
		public bool Equals(pattern other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DEE RID: 15854
		private ProgramNode _node;
	}
}
