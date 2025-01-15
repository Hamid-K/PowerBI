using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001AC0 RID: 6848
	public struct isMixedColumn : IProgramNodeBuilder, IEquatable<isMixedColumn>
	{
		// Token: 0x170025E5 RID: 9701
		// (get) Token: 0x0600E287 RID: 57991 RVA: 0x00301CE6 File Offset: 0x002FFEE6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E288 RID: 57992 RVA: 0x00301CEE File Offset: 0x002FFEEE
		private isMixedColumn(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E289 RID: 57993 RVA: 0x00301CF7 File Offset: 0x002FFEF7
		public static isMixedColumn CreateUnsafe(ProgramNode node)
		{
			return new isMixedColumn(node);
		}

		// Token: 0x0600E28A RID: 57994 RVA: 0x00301D00 File Offset: 0x002FFF00
		public static isMixedColumn? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.isMixedColumn)
			{
				return null;
			}
			return new isMixedColumn?(isMixedColumn.CreateUnsafe(node));
		}

		// Token: 0x0600E28B RID: 57995 RVA: 0x00301D3A File Offset: 0x002FFF3A
		public static isMixedColumn CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new isMixedColumn(new Hole(g.Symbol.isMixedColumn, holeId));
		}

		// Token: 0x0600E28C RID: 57996 RVA: 0x00301D52 File Offset: 0x002FFF52
		public isMixedColumn(GrammarBuilders g, bool value)
		{
			this = new isMixedColumn(new LiteralNode(g.Symbol.isMixedColumn, value));
		}

		// Token: 0x170025E6 RID: 9702
		// (get) Token: 0x0600E28D RID: 57997 RVA: 0x00301D70 File Offset: 0x002FFF70
		public bool Value
		{
			get
			{
				return (bool)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600E28E RID: 57998 RVA: 0x00301D87 File Offset: 0x002FFF87
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E28F RID: 57999 RVA: 0x00301D9C File Offset: 0x002FFF9C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E290 RID: 58000 RVA: 0x00301DC6 File Offset: 0x002FFFC6
		public bool Equals(isMixedColumn other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400557F RID: 21887
		private ProgramNode _node;
	}
}
