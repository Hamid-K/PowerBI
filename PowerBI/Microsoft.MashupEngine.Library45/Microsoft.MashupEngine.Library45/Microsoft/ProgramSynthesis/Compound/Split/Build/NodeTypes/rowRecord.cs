using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000982 RID: 2434
	public struct rowRecord : IProgramNodeBuilder, IEquatable<rowRecord>
	{
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06003A3A RID: 14906 RVA: 0x000B3382 File Offset: 0x000B1582
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000B338A File Offset: 0x000B158A
		private rowRecord(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x000B3393 File Offset: 0x000B1593
		public static rowRecord CreateUnsafe(ProgramNode node)
		{
			return new rowRecord(node);
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000B339C File Offset: 0x000B159C
		public static rowRecord? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.rowRecord)
			{
				return null;
			}
			return new rowRecord?(rowRecord.CreateUnsafe(node));
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x000B33D6 File Offset: 0x000B15D6
		public static rowRecord CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new rowRecord(new Hole(g.Symbol.rowRecord, holeId));
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000B33EE File Offset: 0x000B15EE
		public rowRecord(GrammarBuilders g)
		{
			this = new rowRecord(new VariableNode(g.Symbol.rowRecord));
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06003A40 RID: 14912 RVA: 0x000B3406 File Offset: 0x000B1606
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x000B3413 File Offset: 0x000B1613
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x000B3428 File Offset: 0x000B1628
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x000B3452 File Offset: 0x000B1652
		public bool Equals(rowRecord other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001AA2 RID: 6818
		private ProgramNode _node;
	}
}
