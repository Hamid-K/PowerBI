using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001378 RID: 4984
	public struct quotingConf : IProgramNodeBuilder, IEquatable<quotingConf>
	{
		// Token: 0x17001A83 RID: 6787
		// (get) Token: 0x06009AA3 RID: 39587 RVA: 0x0020B396 File Offset: 0x00209596
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009AA4 RID: 39588 RVA: 0x0020B39E File Offset: 0x0020959E
		private quotingConf(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009AA5 RID: 39589 RVA: 0x0020B3A7 File Offset: 0x002095A7
		public static quotingConf CreateUnsafe(ProgramNode node)
		{
			return new quotingConf(node);
		}

		// Token: 0x06009AA6 RID: 39590 RVA: 0x0020B3B0 File Offset: 0x002095B0
		public static quotingConf? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.quotingConf)
			{
				return null;
			}
			return new quotingConf?(quotingConf.CreateUnsafe(node));
		}

		// Token: 0x06009AA7 RID: 39591 RVA: 0x0020B3EA File Offset: 0x002095EA
		public static quotingConf CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new quotingConf(new Hole(g.Symbol.quotingConf, holeId));
		}

		// Token: 0x06009AA8 RID: 39592 RVA: 0x0020B402 File Offset: 0x00209602
		public quotingConf(GrammarBuilders g, QuotingConfiguration value)
		{
			this = new quotingConf(new LiteralNode(g.Symbol.quotingConf, value));
		}

		// Token: 0x17001A84 RID: 6788
		// (get) Token: 0x06009AA9 RID: 39593 RVA: 0x0020B420 File Offset: 0x00209620
		public QuotingConfiguration Value
		{
			get
			{
				return (QuotingConfiguration)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009AAA RID: 39594 RVA: 0x0020B437 File Offset: 0x00209637
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009AAB RID: 39595 RVA: 0x0020B44C File Offset: 0x0020964C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009AAC RID: 39596 RVA: 0x0020B476 File Offset: 0x00209676
		public bool Equals(quotingConf other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DEF RID: 15855
		private ProgramNode _node;
	}
}
