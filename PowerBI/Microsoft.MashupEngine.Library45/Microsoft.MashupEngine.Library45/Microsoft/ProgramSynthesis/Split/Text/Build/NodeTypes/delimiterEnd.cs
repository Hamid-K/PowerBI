using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200137A RID: 4986
	public struct delimiterEnd : IProgramNodeBuilder, IEquatable<delimiterEnd>
	{
		// Token: 0x17001A87 RID: 6791
		// (get) Token: 0x06009AB7 RID: 39607 RVA: 0x0020B57E File Offset: 0x0020977E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009AB8 RID: 39608 RVA: 0x0020B586 File Offset: 0x00209786
		private delimiterEnd(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009AB9 RID: 39609 RVA: 0x0020B58F File Offset: 0x0020978F
		public static delimiterEnd CreateUnsafe(ProgramNode node)
		{
			return new delimiterEnd(node);
		}

		// Token: 0x06009ABA RID: 39610 RVA: 0x0020B598 File Offset: 0x00209798
		public static delimiterEnd? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.delimiterEnd)
			{
				return null;
			}
			return new delimiterEnd?(delimiterEnd.CreateUnsafe(node));
		}

		// Token: 0x06009ABB RID: 39611 RVA: 0x0020B5D2 File Offset: 0x002097D2
		public static delimiterEnd CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new delimiterEnd(new Hole(g.Symbol.delimiterEnd, holeId));
		}

		// Token: 0x06009ABC RID: 39612 RVA: 0x0020B5EA File Offset: 0x002097EA
		public delimiterEnd(GrammarBuilders g, bool value)
		{
			this = new delimiterEnd(new LiteralNode(g.Symbol.delimiterEnd, value));
		}

		// Token: 0x17001A88 RID: 6792
		// (get) Token: 0x06009ABD RID: 39613 RVA: 0x0020B608 File Offset: 0x00209808
		public bool Value
		{
			get
			{
				return (bool)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009ABE RID: 39614 RVA: 0x0020B61F File Offset: 0x0020981F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009ABF RID: 39615 RVA: 0x0020B634 File Offset: 0x00209834
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009AC0 RID: 39616 RVA: 0x0020B65E File Offset: 0x0020985E
		public bool Equals(delimiterEnd other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DF1 RID: 15857
		private ProgramNode _node;
	}
}
