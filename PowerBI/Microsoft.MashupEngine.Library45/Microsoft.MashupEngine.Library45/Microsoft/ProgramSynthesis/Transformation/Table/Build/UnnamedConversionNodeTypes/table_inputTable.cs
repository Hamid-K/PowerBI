using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001AA7 RID: 6823
	public struct table_inputTable : IProgramNodeBuilder, IEquatable<table_inputTable>
	{
		// Token: 0x170025A4 RID: 9636
		// (get) Token: 0x0600E149 RID: 57673 RVA: 0x002FF913 File Offset: 0x002FDB13
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E14A RID: 57674 RVA: 0x002FF91B File Offset: 0x002FDB1B
		private table_inputTable(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E14B RID: 57675 RVA: 0x002FF924 File Offset: 0x002FDB24
		public static table_inputTable CreateUnsafe(ProgramNode node)
		{
			return new table_inputTable(node);
		}

		// Token: 0x0600E14C RID: 57676 RVA: 0x002FF92C File Offset: 0x002FDB2C
		public static table_inputTable? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.table_inputTable)
			{
				return null;
			}
			return new table_inputTable?(table_inputTable.CreateUnsafe(node));
		}

		// Token: 0x0600E14D RID: 57677 RVA: 0x002FF961 File Offset: 0x002FDB61
		public table_inputTable(GrammarBuilders g, inputTable value0)
		{
			this._node = g.UnnamedConversion.table_inputTable.BuildASTNode(value0.Node);
		}

		// Token: 0x0600E14E RID: 57678 RVA: 0x002FF980 File Offset: 0x002FDB80
		public static implicit operator table(table_inputTable arg)
		{
			return table.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025A5 RID: 9637
		// (get) Token: 0x0600E14F RID: 57679 RVA: 0x002FF98E File Offset: 0x002FDB8E
		public inputTable inputTable
		{
			get
			{
				return inputTable.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600E150 RID: 57680 RVA: 0x002FF9A2 File Offset: 0x002FDBA2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E151 RID: 57681 RVA: 0x002FF9B8 File Offset: 0x002FDBB8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E152 RID: 57682 RVA: 0x002FF9E2 File Offset: 0x002FDBE2
		public bool Equals(table_inputTable other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005566 RID: 21862
		private ProgramNode _node;
	}
}
