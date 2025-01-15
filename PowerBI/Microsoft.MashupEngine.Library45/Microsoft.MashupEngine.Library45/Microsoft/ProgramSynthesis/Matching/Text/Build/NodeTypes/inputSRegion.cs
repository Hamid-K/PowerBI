using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x02001200 RID: 4608
	public struct inputSRegion : IProgramNodeBuilder, IEquatable<inputSRegion>
	{
		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x06008AF0 RID: 35568 RVA: 0x001D1CD6 File Offset: 0x001CFED6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008AF1 RID: 35569 RVA: 0x001D1CDE File Offset: 0x001CFEDE
		private inputSRegion(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008AF2 RID: 35570 RVA: 0x001D1CE7 File Offset: 0x001CFEE7
		public static inputSRegion CreateUnsafe(ProgramNode node)
		{
			return new inputSRegion(node);
		}

		// Token: 0x06008AF3 RID: 35571 RVA: 0x001D1CF0 File Offset: 0x001CFEF0
		public static inputSRegion? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.inputSRegion)
			{
				return null;
			}
			return new inputSRegion?(inputSRegion.CreateUnsafe(node));
		}

		// Token: 0x06008AF4 RID: 35572 RVA: 0x001D1D2A File Offset: 0x001CFF2A
		public static inputSRegion CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new inputSRegion(new Hole(g.Symbol.inputSRegion, holeId));
		}

		// Token: 0x06008AF5 RID: 35573 RVA: 0x001D1D42 File Offset: 0x001CFF42
		public inputSRegion(GrammarBuilders g)
		{
			this = new inputSRegion(new VariableNode(g.Symbol.inputSRegion));
		}

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x06008AF6 RID: 35574 RVA: 0x001D1D5A File Offset: 0x001CFF5A
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06008AF7 RID: 35575 RVA: 0x001D1D67 File Offset: 0x001CFF67
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008AF8 RID: 35576 RVA: 0x001D1D7C File Offset: 0x001CFF7C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008AF9 RID: 35577 RVA: 0x001D1DA6 File Offset: 0x001CFFA6
		public bool Equals(inputSRegion other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038B4 RID: 14516
		private ProgramNode _node;
	}
}
