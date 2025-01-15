using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A4C RID: 6732
	public struct v : IProgramNodeBuilder, IEquatable<v>
	{
		// Token: 0x1700251E RID: 9502
		// (get) Token: 0x0600DDE4 RID: 56804 RVA: 0x002F240A File Offset: 0x002F060A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DDE5 RID: 56805 RVA: 0x002F2412 File Offset: 0x002F0612
		private v(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DDE6 RID: 56806 RVA: 0x002F241B File Offset: 0x002F061B
		public static v CreateUnsafe(ProgramNode node)
		{
			return new v(node);
		}

		// Token: 0x0600DDE7 RID: 56807 RVA: 0x002F2424 File Offset: 0x002F0624
		public static v? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.v)
			{
				return null;
			}
			return new v?(v.CreateUnsafe(node));
		}

		// Token: 0x0600DDE8 RID: 56808 RVA: 0x002F245E File Offset: 0x002F065E
		public static v CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new v(new Hole(g.Symbol.v, holeId));
		}

		// Token: 0x0600DDE9 RID: 56809 RVA: 0x002F2476 File Offset: 0x002F0676
		public v(GrammarBuilders g)
		{
			this = new v(new VariableNode(g.Symbol.v));
		}

		// Token: 0x1700251F RID: 9503
		// (get) Token: 0x0600DDEA RID: 56810 RVA: 0x002F248E File Offset: 0x002F068E
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600DDEB RID: 56811 RVA: 0x002F249B File Offset: 0x002F069B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DDEC RID: 56812 RVA: 0x002F24B0 File Offset: 0x002F06B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DDED RID: 56813 RVA: 0x002F24DA File Offset: 0x002F06DA
		public bool Equals(v other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400543D RID: 21565
		private ProgramNode _node;
	}
}
