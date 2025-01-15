using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C7B RID: 7291
	public struct pl2p : IProgramNodeBuilder, IEquatable<pl2p>
	{
		// Token: 0x1700292E RID: 10542
		// (get) Token: 0x0600F706 RID: 63238 RVA: 0x0034A1A6 File Offset: 0x003483A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F707 RID: 63239 RVA: 0x0034A1AE File Offset: 0x003483AE
		private pl2p(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F708 RID: 63240 RVA: 0x0034A1B7 File Offset: 0x003483B7
		public static pl2p CreateUnsafe(ProgramNode node)
		{
			return new pl2p(node);
		}

		// Token: 0x0600F709 RID: 63241 RVA: 0x0034A1C0 File Offset: 0x003483C0
		public static pl2p? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.pl2p)
			{
				return null;
			}
			return new pl2p?(pl2p.CreateUnsafe(node));
		}

		// Token: 0x0600F70A RID: 63242 RVA: 0x0034A1FA File Offset: 0x003483FA
		public static pl2p CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new pl2p(new Hole(g.Symbol.pl2p, holeId));
		}

		// Token: 0x0600F70B RID: 63243 RVA: 0x0034A212 File Offset: 0x00348412
		public pl2p(GrammarBuilders g)
		{
			this = new pl2p(new VariableNode(g.Symbol.pl2p));
		}

		// Token: 0x1700292F RID: 10543
		// (get) Token: 0x0600F70C RID: 63244 RVA: 0x0034A22A File Offset: 0x0034842A
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600F70D RID: 63245 RVA: 0x0034A237 File Offset: 0x00348437
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F70E RID: 63246 RVA: 0x0034A24C File Offset: 0x0034844C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F70F RID: 63247 RVA: 0x0034A276 File Offset: 0x00348476
		public bool Equals(pl2p other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B6A RID: 23402
		private ProgramNode _node;
	}
}
