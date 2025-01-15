using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E40 RID: 3648
	public struct TopSameFontCells : IProgramNodeBuilder, IEquatable<TopSameFontCells>
	{
		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x060061C8 RID: 25032 RVA: 0x0013F9E2 File Offset: 0x0013DBE2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060061C9 RID: 25033 RVA: 0x0013F9EA File Offset: 0x0013DBEA
		private TopSameFontCells(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060061CA RID: 25034 RVA: 0x0013F9F3 File Offset: 0x0013DBF3
		public static TopSameFontCells CreateUnsafe(ProgramNode node)
		{
			return new TopSameFontCells(node);
		}

		// Token: 0x060061CB RID: 25035 RVA: 0x0013F9FC File Offset: 0x0013DBFC
		public static TopSameFontCells? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TopSameFontCells)
			{
				return null;
			}
			return new TopSameFontCells?(TopSameFontCells.CreateUnsafe(node));
		}

		// Token: 0x060061CC RID: 25036 RVA: 0x0013FA31 File Offset: 0x0013DC31
		public TopSameFontCells(GrammarBuilders g, aboveOrLeftmost value0)
		{
			this._node = g.Rule.TopSameFontCells.BuildASTNode(value0.Node);
		}

		// Token: 0x060061CD RID: 25037 RVA: 0x0013FA50 File Offset: 0x0013DC50
		public static implicit operator title(TopSameFontCells arg)
		{
			return title.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x060061CE RID: 25038 RVA: 0x0013FA5E File Offset: 0x0013DC5E
		public aboveOrLeftmost aboveOrLeftmost
		{
			get
			{
				return aboveOrLeftmost.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060061CF RID: 25039 RVA: 0x0013FA72 File Offset: 0x0013DC72
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060061D0 RID: 25040 RVA: 0x0013FA88 File Offset: 0x0013DC88
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060061D1 RID: 25041 RVA: 0x0013FAB2 File Offset: 0x0013DCB2
		public bool Equals(TopSameFontCells other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BEA RID: 11242
		private ProgramNode _node;
	}
}
