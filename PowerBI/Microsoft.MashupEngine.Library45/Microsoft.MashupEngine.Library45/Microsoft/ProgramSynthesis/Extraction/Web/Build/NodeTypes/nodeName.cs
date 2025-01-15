using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001091 RID: 4241
	public struct nodeName : IProgramNodeBuilder, IEquatable<nodeName>
	{
		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x06007FB1 RID: 32689 RVA: 0x001AC5CA File Offset: 0x001AA7CA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007FB2 RID: 32690 RVA: 0x001AC5D2 File Offset: 0x001AA7D2
		private nodeName(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007FB3 RID: 32691 RVA: 0x001AC5DB File Offset: 0x001AA7DB
		public static nodeName CreateUnsafe(ProgramNode node)
		{
			return new nodeName(node);
		}

		// Token: 0x06007FB4 RID: 32692 RVA: 0x001AC5E4 File Offset: 0x001AA7E4
		public static nodeName? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.nodeName)
			{
				return null;
			}
			return new nodeName?(nodeName.CreateUnsafe(node));
		}

		// Token: 0x06007FB5 RID: 32693 RVA: 0x001AC61E File Offset: 0x001AA81E
		public static nodeName CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new nodeName(new Hole(g.Symbol.nodeName, holeId));
		}

		// Token: 0x06007FB6 RID: 32694 RVA: 0x001AC636 File Offset: 0x001AA836
		public nodeName(GrammarBuilders g, string value)
		{
			this = new nodeName(new LiteralNode(g.Symbol.nodeName, value));
		}

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x06007FB7 RID: 32695 RVA: 0x001AC64F File Offset: 0x001AA84F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007FB8 RID: 32696 RVA: 0x001AC666 File Offset: 0x001AA866
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007FB9 RID: 32697 RVA: 0x001AC67C File Offset: 0x001AA87C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007FBA RID: 32698 RVA: 0x001AC6A6 File Offset: 0x001AA8A6
		public bool Equals(nodeName other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033AA RID: 13226
		private ProgramNode _node;
	}
}
