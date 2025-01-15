using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E33 RID: 3635
	public struct MWholeSheet : IProgramNodeBuilder, IEquatable<MWholeSheet>
	{
		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06006145 RID: 24901 RVA: 0x0013EE36 File Offset: 0x0013D036
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x0013EE3E File Offset: 0x0013D03E
		private MWholeSheet(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x0013EE47 File Offset: 0x0013D047
		public static MWholeSheet CreateUnsafe(ProgramNode node)
		{
			return new MWholeSheet(node);
		}

		// Token: 0x06006148 RID: 24904 RVA: 0x0013EE50 File Offset: 0x0013D050
		public static MWholeSheet? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MWholeSheet)
			{
				return null;
			}
			return new MWholeSheet?(MWholeSheet.CreateUnsafe(node));
		}

		// Token: 0x06006149 RID: 24905 RVA: 0x0013EE85 File Offset: 0x0013D085
		public MWholeSheet(GrammarBuilders g, withoutFormatting value0)
		{
			this._node = g.Rule.MWholeSheet.BuildASTNode(value0.Node);
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x0013EEA4 File Offset: 0x0013D0A4
		public static implicit operator mTable(MWholeSheet arg)
		{
			return mTable.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x0600614B RID: 24907 RVA: 0x0013EEB2 File Offset: 0x0013D0B2
		public withoutFormatting withoutFormatting
		{
			get
			{
				return withoutFormatting.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x0013EEC6 File Offset: 0x0013D0C6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x0013EEDC File Offset: 0x0013D0DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x0013EF06 File Offset: 0x0013D106
		public bool Equals(MWholeSheet other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BDD RID: 11229
		private ProgramNode _node;
	}
}
