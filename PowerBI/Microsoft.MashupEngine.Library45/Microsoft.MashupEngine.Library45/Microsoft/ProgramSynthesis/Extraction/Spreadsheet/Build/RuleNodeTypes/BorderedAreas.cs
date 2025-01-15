using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E30 RID: 3632
	public struct BorderedAreas : IProgramNodeBuilder, IEquatable<BorderedAreas>
	{
		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06006127 RID: 24871 RVA: 0x0013EB8A File Offset: 0x0013CD8A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006128 RID: 24872 RVA: 0x0013EB92 File Offset: 0x0013CD92
		private BorderedAreas(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006129 RID: 24873 RVA: 0x0013EB9B File Offset: 0x0013CD9B
		public static BorderedAreas CreateUnsafe(ProgramNode node)
		{
			return new BorderedAreas(node);
		}

		// Token: 0x0600612A RID: 24874 RVA: 0x0013EBA4 File Offset: 0x0013CDA4
		public static BorderedAreas? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.BorderedAreas)
			{
				return null;
			}
			return new BorderedAreas?(BorderedAreas.CreateUnsafe(node));
		}

		// Token: 0x0600612B RID: 24875 RVA: 0x0013EBD9 File Offset: 0x0013CDD9
		public BorderedAreas(GrammarBuilders g, wholeSheet value0)
		{
			this._node = g.Rule.BorderedAreas.BuildASTNode(value0.Node);
		}

		// Token: 0x0600612C RID: 24876 RVA: 0x0013EBF8 File Offset: 0x0013CDF8
		public static implicit operator sheetSplits(BorderedAreas arg)
		{
			return sheetSplits.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x0600612D RID: 24877 RVA: 0x0013EC06 File Offset: 0x0013CE06
		public wholeSheet wholeSheet
		{
			get
			{
				return wholeSheet.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600612E RID: 24878 RVA: 0x0013EC1A File Offset: 0x0013CE1A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600612F RID: 24879 RVA: 0x0013EC30 File Offset: 0x0013CE30
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006130 RID: 24880 RVA: 0x0013EC5A File Offset: 0x0013CE5A
		public bool Equals(BorderedAreas other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BDA RID: 11226
		private ProgramNode _node;
	}
}
