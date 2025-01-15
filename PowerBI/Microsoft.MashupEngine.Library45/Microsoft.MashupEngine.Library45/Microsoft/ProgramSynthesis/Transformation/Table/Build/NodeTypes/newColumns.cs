using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001AB7 RID: 6839
	public struct newColumns : IProgramNodeBuilder, IEquatable<newColumns>
	{
		// Token: 0x170025D6 RID: 9686
		// (get) Token: 0x0600E227 RID: 57895 RVA: 0x0030147A File Offset: 0x002FF67A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E228 RID: 57896 RVA: 0x00301482 File Offset: 0x002FF682
		private newColumns(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E229 RID: 57897 RVA: 0x0030148B File Offset: 0x002FF68B
		public static newColumns CreateUnsafe(ProgramNode node)
		{
			return new newColumns(node);
		}

		// Token: 0x0600E22A RID: 57898 RVA: 0x00301494 File Offset: 0x002FF694
		public static newColumns? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.newColumns)
			{
				return null;
			}
			return new newColumns?(newColumns.CreateUnsafe(node));
		}

		// Token: 0x0600E22B RID: 57899 RVA: 0x003014CE File Offset: 0x002FF6CE
		public static newColumns CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new newColumns(new Hole(g.Symbol.newColumns, holeId));
		}

		// Token: 0x0600E22C RID: 57900 RVA: 0x003014E6 File Offset: 0x002FF6E6
		public SplitColumn Cast_SplitColumn()
		{
			return SplitColumn.CreateUnsafe(this.Node);
		}

		// Token: 0x0600E22D RID: 57901 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SplitColumn(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600E22E RID: 57902 RVA: 0x003014F3 File Offset: 0x002FF6F3
		public bool Is_SplitColumn(GrammarBuilders g, out SplitColumn value)
		{
			value = SplitColumn.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600E22F RID: 57903 RVA: 0x00301507 File Offset: 0x002FF707
		public SplitColumn? As_SplitColumn(GrammarBuilders g)
		{
			return new SplitColumn?(SplitColumn.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E230 RID: 57904 RVA: 0x00301519 File Offset: 0x002FF719
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E231 RID: 57905 RVA: 0x0030152C File Offset: 0x002FF72C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E232 RID: 57906 RVA: 0x00301556 File Offset: 0x002FF756
		public bool Equals(newColumns other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005576 RID: 21878
		private ProgramNode _node;
	}
}
