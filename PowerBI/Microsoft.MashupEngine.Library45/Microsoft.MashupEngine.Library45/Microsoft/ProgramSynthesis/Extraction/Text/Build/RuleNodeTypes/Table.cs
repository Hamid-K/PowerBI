using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F22 RID: 3874
	public struct Table : IProgramNodeBuilder, IEquatable<Table>
	{
		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06006B1A RID: 27418 RVA: 0x0016095A File Offset: 0x0015EB5A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B1B RID: 27419 RVA: 0x00160962 File Offset: 0x0015EB62
		private Table(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B1C RID: 27420 RVA: 0x0016096B File Offset: 0x0015EB6B
		public static Table CreateUnsafe(ProgramNode node)
		{
			return new Table(node);
		}

		// Token: 0x06006B1D RID: 27421 RVA: 0x00160974 File Offset: 0x0015EB74
		public static Table? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Table)
			{
				return null;
			}
			return new Table?(Table.CreateUnsafe(node));
		}

		// Token: 0x06006B1E RID: 27422 RVA: 0x001609A9 File Offset: 0x0015EBA9
		public Table(GrammarBuilders g, columnNames value0, table value1)
		{
			this._node = g.Rule.Table.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06006B1F RID: 27423 RVA: 0x001609CF File Offset: 0x0015EBCF
		public static implicit operator output(Table arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06006B20 RID: 27424 RVA: 0x001609DD File Offset: 0x0015EBDD
		public columnNames columnNames
		{
			get
			{
				return columnNames.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06006B21 RID: 27425 RVA: 0x001609F1 File Offset: 0x0015EBF1
		public table table
		{
			get
			{
				return table.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006B22 RID: 27426 RVA: 0x00160A05 File Offset: 0x0015EC05
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B23 RID: 27427 RVA: 0x00160A18 File Offset: 0x0015EC18
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B24 RID: 27428 RVA: 0x00160A42 File Offset: 0x0015EC42
		public bool Equals(Table other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F0D RID: 12045
		private ProgramNode _node;
	}
}
