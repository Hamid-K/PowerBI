using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200109D RID: 4253
	public struct node : IProgramNodeBuilder, IEquatable<node>
	{
		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x06008029 RID: 32809 RVA: 0x001AD116 File Offset: 0x001AB316
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600802A RID: 32810 RVA: 0x001AD11E File Offset: 0x001AB31E
		private node(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600802B RID: 32811 RVA: 0x001AD127 File Offset: 0x001AB327
		public static node CreateUnsafe(ProgramNode node)
		{
			return new node(node);
		}

		// Token: 0x0600802C RID: 32812 RVA: 0x001AD130 File Offset: 0x001AB330
		public static node? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.node)
			{
				return null;
			}
			return new node?(node.CreateUnsafe(node));
		}

		// Token: 0x0600802D RID: 32813 RVA: 0x001AD16A File Offset: 0x001AB36A
		public static node CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new node(new Hole(g.Symbol.node, holeId));
		}

		// Token: 0x0600802E RID: 32814 RVA: 0x001AD182 File Offset: 0x001AB382
		public node(GrammarBuilders g)
		{
			this = new node(new VariableNode(g.Symbol.node));
		}

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x0600802F RID: 32815 RVA: 0x001AD19A File Offset: 0x001AB39A
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06008030 RID: 32816 RVA: 0x001AD1A7 File Offset: 0x001AB3A7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008031 RID: 32817 RVA: 0x001AD1BC File Offset: 0x001AB3BC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008032 RID: 32818 RVA: 0x001AD1E6 File Offset: 0x001AB3E6
		public bool Equals(node other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033B6 RID: 13238
		private ProgramNode _node;
	}
}
