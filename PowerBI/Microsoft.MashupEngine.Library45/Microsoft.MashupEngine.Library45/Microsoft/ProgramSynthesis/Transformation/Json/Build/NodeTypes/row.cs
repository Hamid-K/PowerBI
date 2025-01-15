using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A4E RID: 6734
	public struct row : IProgramNodeBuilder, IEquatable<row>
	{
		// Token: 0x17002522 RID: 9506
		// (get) Token: 0x0600DDF8 RID: 56824 RVA: 0x002F25D2 File Offset: 0x002F07D2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DDF9 RID: 56825 RVA: 0x002F25DA File Offset: 0x002F07DA
		private row(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DDFA RID: 56826 RVA: 0x002F25E3 File Offset: 0x002F07E3
		public static row CreateUnsafe(ProgramNode node)
		{
			return new row(node);
		}

		// Token: 0x0600DDFB RID: 56827 RVA: 0x002F25EC File Offset: 0x002F07EC
		public static row? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.row)
			{
				return null;
			}
			return new row?(row.CreateUnsafe(node));
		}

		// Token: 0x0600DDFC RID: 56828 RVA: 0x002F2626 File Offset: 0x002F0826
		public static row CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new row(new Hole(g.Symbol.row, holeId));
		}

		// Token: 0x0600DDFD RID: 56829 RVA: 0x002F263E File Offset: 0x002F083E
		public row(GrammarBuilders g)
		{
			this = new row(new VariableNode(g.Symbol.row));
		}

		// Token: 0x17002523 RID: 9507
		// (get) Token: 0x0600DDFE RID: 56830 RVA: 0x002F2656 File Offset: 0x002F0856
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600DDFF RID: 56831 RVA: 0x002F2663 File Offset: 0x002F0863
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DE00 RID: 56832 RVA: 0x002F2678 File Offset: 0x002F0878
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DE01 RID: 56833 RVA: 0x002F26A2 File Offset: 0x002F08A2
		public bool Equals(row other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400543F RID: 21567
		private ProgramNode _node;
	}
}
