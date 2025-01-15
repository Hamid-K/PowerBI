using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001387 RID: 4999
	public struct v : IProgramNodeBuilder, IEquatable<v>
	{
		// Token: 0x17001AA1 RID: 6817
		// (get) Token: 0x06009B39 RID: 39737 RVA: 0x0020C1BA File Offset: 0x0020A3BA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009B3A RID: 39738 RVA: 0x0020C1C2 File Offset: 0x0020A3C2
		private v(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009B3B RID: 39739 RVA: 0x0020C1CB File Offset: 0x0020A3CB
		public static v CreateUnsafe(ProgramNode node)
		{
			return new v(node);
		}

		// Token: 0x06009B3C RID: 39740 RVA: 0x0020C1D4 File Offset: 0x0020A3D4
		public static v? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.v)
			{
				return null;
			}
			return new v?(v.CreateUnsafe(node));
		}

		// Token: 0x06009B3D RID: 39741 RVA: 0x0020C20E File Offset: 0x0020A40E
		public static v CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new v(new Hole(g.Symbol.v, holeId));
		}

		// Token: 0x06009B3E RID: 39742 RVA: 0x0020C226 File Offset: 0x0020A426
		public v(GrammarBuilders g)
		{
			this = new v(new VariableNode(g.Symbol.v));
		}

		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x06009B3F RID: 39743 RVA: 0x0020C23E File Offset: 0x0020A43E
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06009B40 RID: 39744 RVA: 0x0020C24B File Offset: 0x0020A44B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009B41 RID: 39745 RVA: 0x0020C260 File Offset: 0x0020A460
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009B42 RID: 39746 RVA: 0x0020C28A File Offset: 0x0020A48A
		public bool Equals(v other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DFE RID: 15870
		private ProgramNode _node;
	}
}
