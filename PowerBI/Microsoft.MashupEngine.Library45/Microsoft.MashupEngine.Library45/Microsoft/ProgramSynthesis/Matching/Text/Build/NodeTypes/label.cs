using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011FE RID: 4606
	public struct label : IProgramNodeBuilder, IEquatable<label>
	{
		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x06008ADC RID: 35548 RVA: 0x001D1AF6 File Offset: 0x001CFCF6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008ADD RID: 35549 RVA: 0x001D1AFE File Offset: 0x001CFCFE
		private label(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008ADE RID: 35550 RVA: 0x001D1B07 File Offset: 0x001CFD07
		public static label CreateUnsafe(ProgramNode node)
		{
			return new label(node);
		}

		// Token: 0x06008ADF RID: 35551 RVA: 0x001D1B10 File Offset: 0x001CFD10
		public static label? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.label)
			{
				return null;
			}
			return new label?(label.CreateUnsafe(node));
		}

		// Token: 0x06008AE0 RID: 35552 RVA: 0x001D1B4A File Offset: 0x001CFD4A
		public static label CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new label(new Hole(g.Symbol.label, holeId));
		}

		// Token: 0x06008AE1 RID: 35553 RVA: 0x001D1B62 File Offset: 0x001CFD62
		public label(GrammarBuilders g, MatchingLabel value)
		{
			this = new label(new LiteralNode(g.Symbol.label, value));
		}

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x06008AE2 RID: 35554 RVA: 0x001D1B7B File Offset: 0x001CFD7B
		public MatchingLabel Value
		{
			get
			{
				return (MatchingLabel)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008AE3 RID: 35555 RVA: 0x001D1B92 File Offset: 0x001CFD92
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008AE4 RID: 35556 RVA: 0x001D1BA8 File Offset: 0x001CFDA8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008AE5 RID: 35557 RVA: 0x001D1BD2 File Offset: 0x001CFDD2
		public bool Equals(label other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038B2 RID: 14514
		private ProgramNode _node;
	}
}
