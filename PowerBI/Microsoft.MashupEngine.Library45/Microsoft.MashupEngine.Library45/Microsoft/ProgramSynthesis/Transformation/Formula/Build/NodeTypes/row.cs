using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015F6 RID: 5622
	public struct row : IProgramNodeBuilder, IEquatable<row>
	{
		// Token: 0x17002043 RID: 8259
		// (get) Token: 0x0600BAC0 RID: 47808 RVA: 0x0028318E File Offset: 0x0028138E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BAC1 RID: 47809 RVA: 0x00283196 File Offset: 0x00281396
		private row(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BAC2 RID: 47810 RVA: 0x0028319F File Offset: 0x0028139F
		public static row CreateUnsafe(ProgramNode node)
		{
			return new row(node);
		}

		// Token: 0x0600BAC3 RID: 47811 RVA: 0x002831A8 File Offset: 0x002813A8
		public static row? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.row)
			{
				return null;
			}
			return new row?(row.CreateUnsafe(node));
		}

		// Token: 0x0600BAC4 RID: 47812 RVA: 0x002831E2 File Offset: 0x002813E2
		public static row CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new row(new Hole(g.Symbol.row, holeId));
		}

		// Token: 0x0600BAC5 RID: 47813 RVA: 0x002831FA File Offset: 0x002813FA
		public row(GrammarBuilders g)
		{
			this = new row(new VariableNode(g.Symbol.row));
		}

		// Token: 0x17002044 RID: 8260
		// (get) Token: 0x0600BAC6 RID: 47814 RVA: 0x00283212 File Offset: 0x00281412
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600BAC7 RID: 47815 RVA: 0x0028321F File Offset: 0x0028141F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BAC8 RID: 47816 RVA: 0x00283234 File Offset: 0x00281434
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BAC9 RID: 47817 RVA: 0x0028325E File Offset: 0x0028145E
		public bool Equals(row other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040046A4 RID: 18084
		private ProgramNode _node;
	}
}
