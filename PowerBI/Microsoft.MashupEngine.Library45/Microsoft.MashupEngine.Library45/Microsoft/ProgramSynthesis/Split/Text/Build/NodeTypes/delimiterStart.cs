using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001379 RID: 4985
	public struct delimiterStart : IProgramNodeBuilder, IEquatable<delimiterStart>
	{
		// Token: 0x17001A85 RID: 6789
		// (get) Token: 0x06009AAD RID: 39597 RVA: 0x0020B48A File Offset: 0x0020968A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009AAE RID: 39598 RVA: 0x0020B492 File Offset: 0x00209692
		private delimiterStart(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009AAF RID: 39599 RVA: 0x0020B49B File Offset: 0x0020969B
		public static delimiterStart CreateUnsafe(ProgramNode node)
		{
			return new delimiterStart(node);
		}

		// Token: 0x06009AB0 RID: 39600 RVA: 0x0020B4A4 File Offset: 0x002096A4
		public static delimiterStart? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.delimiterStart)
			{
				return null;
			}
			return new delimiterStart?(delimiterStart.CreateUnsafe(node));
		}

		// Token: 0x06009AB1 RID: 39601 RVA: 0x0020B4DE File Offset: 0x002096DE
		public static delimiterStart CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new delimiterStart(new Hole(g.Symbol.delimiterStart, holeId));
		}

		// Token: 0x06009AB2 RID: 39602 RVA: 0x0020B4F6 File Offset: 0x002096F6
		public delimiterStart(GrammarBuilders g, bool value)
		{
			this = new delimiterStart(new LiteralNode(g.Symbol.delimiterStart, value));
		}

		// Token: 0x17001A86 RID: 6790
		// (get) Token: 0x06009AB3 RID: 39603 RVA: 0x0020B514 File Offset: 0x00209714
		public bool Value
		{
			get
			{
				return (bool)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009AB4 RID: 39604 RVA: 0x0020B52B File Offset: 0x0020972B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009AB5 RID: 39605 RVA: 0x0020B540 File Offset: 0x00209740
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009AB6 RID: 39606 RVA: 0x0020B56A File Offset: 0x0020976A
		public bool Equals(delimiterStart other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DF0 RID: 15856
		private ProgramNode _node;
	}
}
