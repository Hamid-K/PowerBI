using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E96 RID: 7830
	public struct parent : IProgramNodeBuilder, IEquatable<parent>
	{
		// Token: 0x17002BF9 RID: 11257
		// (get) Token: 0x060108BA RID: 67770 RVA: 0x0038E07A File Offset: 0x0038C27A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060108BB RID: 67771 RVA: 0x0038E082 File Offset: 0x0038C282
		private parent(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060108BC RID: 67772 RVA: 0x0038E08B File Offset: 0x0038C28B
		public static parent CreateUnsafe(ProgramNode node)
		{
			return new parent(node);
		}

		// Token: 0x060108BD RID: 67773 RVA: 0x0038E094 File Offset: 0x0038C294
		public static parent? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.parent)
			{
				return null;
			}
			return new parent?(parent.CreateUnsafe(node));
		}

		// Token: 0x060108BE RID: 67774 RVA: 0x0038E0CE File Offset: 0x0038C2CE
		public static parent CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new parent(new Hole(g.Symbol.parent, holeId));
		}

		// Token: 0x060108BF RID: 67775 RVA: 0x0038E0E6 File Offset: 0x0038C2E6
		public parent(GrammarBuilders g)
		{
			this = new parent(new VariableNode(g.Symbol.parent));
		}

		// Token: 0x17002BFA RID: 11258
		// (get) Token: 0x060108C0 RID: 67776 RVA: 0x0038E0FE File Offset: 0x0038C2FE
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x060108C1 RID: 67777 RVA: 0x0038E10B File Offset: 0x0038C30B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060108C2 RID: 67778 RVA: 0x0038E120 File Offset: 0x0038C320
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060108C3 RID: 67779 RVA: 0x0038E14A File Offset: 0x0038C34A
		public bool Equals(parent other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062D5 RID: 25301
		private ProgramNode _node;
	}
}
