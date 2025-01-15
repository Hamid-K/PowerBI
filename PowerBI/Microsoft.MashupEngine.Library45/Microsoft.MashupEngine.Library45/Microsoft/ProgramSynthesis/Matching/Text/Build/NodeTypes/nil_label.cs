using System;
using System.Collections.Immutable;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011FF RID: 4607
	public struct nil_label : IProgramNodeBuilder, IEquatable<nil_label>
	{
		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x06008AE6 RID: 35558 RVA: 0x001D1BE6 File Offset: 0x001CFDE6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008AE7 RID: 35559 RVA: 0x001D1BEE File Offset: 0x001CFDEE
		private nil_label(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008AE8 RID: 35560 RVA: 0x001D1BF7 File Offset: 0x001CFDF7
		public static nil_label CreateUnsafe(ProgramNode node)
		{
			return new nil_label(node);
		}

		// Token: 0x06008AE9 RID: 35561 RVA: 0x001D1C00 File Offset: 0x001CFE00
		public static nil_label? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.nil_label)
			{
				return null;
			}
			return new nil_label?(nil_label.CreateUnsafe(node));
		}

		// Token: 0x06008AEA RID: 35562 RVA: 0x001D1C3A File Offset: 0x001CFE3A
		public static nil_label CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new nil_label(new Hole(g.Symbol.nil_label, holeId));
		}

		// Token: 0x06008AEB RID: 35563 RVA: 0x001D1C52 File Offset: 0x001CFE52
		public nil_label(GrammarBuilders g, ImmutableList<MatchingLabel> value)
		{
			this = new nil_label(new LiteralNode(g.Symbol.nil_label, value));
		}

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x06008AEC RID: 35564 RVA: 0x001D1C6B File Offset: 0x001CFE6B
		public ImmutableList<MatchingLabel> Value
		{
			get
			{
				return (ImmutableList<MatchingLabel>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008AED RID: 35565 RVA: 0x001D1C82 File Offset: 0x001CFE82
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008AEE RID: 35566 RVA: 0x001D1C98 File Offset: 0x001CFE98
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008AEF RID: 35567 RVA: 0x001D1CC2 File Offset: 0x001CFEC2
		public bool Equals(nil_label other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038B3 RID: 14515
		private ProgramNode _node;
	}
}
