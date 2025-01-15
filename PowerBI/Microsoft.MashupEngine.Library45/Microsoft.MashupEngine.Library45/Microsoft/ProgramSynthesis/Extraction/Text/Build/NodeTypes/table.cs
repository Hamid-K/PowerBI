using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F37 RID: 3895
	public struct table : IProgramNodeBuilder, IEquatable<table>
	{
		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06006C01 RID: 27649 RVA: 0x00161E06 File Offset: 0x00160006
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006C02 RID: 27650 RVA: 0x00161E0E File Offset: 0x0016000E
		private table(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006C03 RID: 27651 RVA: 0x00161E17 File Offset: 0x00160017
		public static table CreateUnsafe(ProgramNode node)
		{
			return new table(node);
		}

		// Token: 0x06006C04 RID: 27652 RVA: 0x00161E20 File Offset: 0x00160020
		public static table? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.table)
			{
				return null;
			}
			return new table?(table.CreateUnsafe(node));
		}

		// Token: 0x06006C05 RID: 27653 RVA: 0x00161E5A File Offset: 0x0016005A
		public static table CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new table(new Hole(g.Symbol.table, holeId));
		}

		// Token: 0x06006C06 RID: 27654 RVA: 0x00161E72 File Offset: 0x00160072
		public RowMap Cast_RowMap()
		{
			return RowMap.CreateUnsafe(this.Node);
		}

		// Token: 0x06006C07 RID: 27655 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_RowMap(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06006C08 RID: 27656 RVA: 0x00161E7F File Offset: 0x0016007F
		public bool Is_RowMap(GrammarBuilders g, out RowMap value)
		{
			value = RowMap.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06006C09 RID: 27657 RVA: 0x00161E93 File Offset: 0x00160093
		public RowMap? As_RowMap(GrammarBuilders g)
		{
			return new RowMap?(RowMap.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C0A RID: 27658 RVA: 0x00161EA5 File Offset: 0x001600A5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006C0B RID: 27659 RVA: 0x00161EB8 File Offset: 0x001600B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006C0C RID: 27660 RVA: 0x00161EE2 File Offset: 0x001600E2
		public bool Equals(table other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F22 RID: 12066
		private ProgramNode _node;
	}
}
