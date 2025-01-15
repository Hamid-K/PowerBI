using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001AB8 RID: 6840
	public struct columnToSplit : IProgramNodeBuilder, IEquatable<columnToSplit>
	{
		// Token: 0x170025D7 RID: 9687
		// (get) Token: 0x0600E233 RID: 57907 RVA: 0x0030156A File Offset: 0x002FF76A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E234 RID: 57908 RVA: 0x00301572 File Offset: 0x002FF772
		private columnToSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E235 RID: 57909 RVA: 0x0030157B File Offset: 0x002FF77B
		public static columnToSplit CreateUnsafe(ProgramNode node)
		{
			return new columnToSplit(node);
		}

		// Token: 0x0600E236 RID: 57910 RVA: 0x00301584 File Offset: 0x002FF784
		public static columnToSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.columnToSplit)
			{
				return null;
			}
			return new columnToSplit?(columnToSplit.CreateUnsafe(node));
		}

		// Token: 0x0600E237 RID: 57911 RVA: 0x003015BE File Offset: 0x002FF7BE
		public static columnToSplit CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new columnToSplit(new Hole(g.Symbol.columnToSplit, holeId));
		}

		// Token: 0x0600E238 RID: 57912 RVA: 0x003015D6 File Offset: 0x002FF7D6
		public SelectColumnToSplit Cast_SelectColumnToSplit()
		{
			return SelectColumnToSplit.CreateUnsafe(this.Node);
		}

		// Token: 0x0600E239 RID: 57913 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SelectColumnToSplit(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600E23A RID: 57914 RVA: 0x003015E3 File Offset: 0x002FF7E3
		public bool Is_SelectColumnToSplit(GrammarBuilders g, out SelectColumnToSplit value)
		{
			value = SelectColumnToSplit.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600E23B RID: 57915 RVA: 0x003015F7 File Offset: 0x002FF7F7
		public SelectColumnToSplit? As_SelectColumnToSplit(GrammarBuilders g)
		{
			return new SelectColumnToSplit?(SelectColumnToSplit.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E23C RID: 57916 RVA: 0x00301609 File Offset: 0x002FF809
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E23D RID: 57917 RVA: 0x0030161C File Offset: 0x002FF81C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E23E RID: 57918 RVA: 0x00301646 File Offset: 0x002FF846
		public bool Equals(columnToSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005577 RID: 21879
		private ProgramNode _node;
	}
}
