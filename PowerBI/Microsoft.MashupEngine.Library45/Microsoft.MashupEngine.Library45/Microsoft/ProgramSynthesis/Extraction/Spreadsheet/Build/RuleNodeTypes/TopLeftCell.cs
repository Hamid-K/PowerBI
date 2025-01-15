using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E3F RID: 3647
	public struct TopLeftCell : IProgramNodeBuilder, IEquatable<TopLeftCell>
	{
		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x060061BE RID: 25022 RVA: 0x0013F8FE File Offset: 0x0013DAFE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060061BF RID: 25023 RVA: 0x0013F906 File Offset: 0x0013DB06
		private TopLeftCell(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x0013F90F File Offset: 0x0013DB0F
		public static TopLeftCell CreateUnsafe(ProgramNode node)
		{
			return new TopLeftCell(node);
		}

		// Token: 0x060061C1 RID: 25025 RVA: 0x0013F918 File Offset: 0x0013DB18
		public static TopLeftCell? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TopLeftCell)
			{
				return null;
			}
			return new TopLeftCell?(TopLeftCell.CreateUnsafe(node));
		}

		// Token: 0x060061C2 RID: 25026 RVA: 0x0013F94D File Offset: 0x0013DB4D
		public TopLeftCell(GrammarBuilders g, aboveOrLeftmost value0)
		{
			this._node = g.Rule.TopLeftCell.BuildASTNode(value0.Node);
		}

		// Token: 0x060061C3 RID: 25027 RVA: 0x0013F96C File Offset: 0x0013DB6C
		public static implicit operator title(TopLeftCell arg)
		{
			return title.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x060061C4 RID: 25028 RVA: 0x0013F97A File Offset: 0x0013DB7A
		public aboveOrLeftmost aboveOrLeftmost
		{
			get
			{
				return aboveOrLeftmost.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060061C5 RID: 25029 RVA: 0x0013F98E File Offset: 0x0013DB8E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060061C6 RID: 25030 RVA: 0x0013F9A4 File Offset: 0x0013DBA4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060061C7 RID: 25031 RVA: 0x0013F9CE File Offset: 0x0013DBCE
		public bool Equals(TopLeftCell other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BE9 RID: 11241
		private ProgramNode _node;
	}
}
