using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E37 RID: 3639
	public struct MTrimBottomSingleCellRows : IProgramNodeBuilder, IEquatable<MTrimBottomSingleCellRows>
	{
		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x0600616E RID: 24942 RVA: 0x0013F1DE File Offset: 0x0013D3DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x0013F1E6 File Offset: 0x0013D3E6
		private MTrimBottomSingleCellRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x0013F1EF File Offset: 0x0013D3EF
		public static MTrimBottomSingleCellRows CreateUnsafe(ProgramNode node)
		{
			return new MTrimBottomSingleCellRows(node);
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x0013F1F8 File Offset: 0x0013D3F8
		public static MTrimBottomSingleCellRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MTrimBottomSingleCellRows)
			{
				return null;
			}
			return new MTrimBottomSingleCellRows?(MTrimBottomSingleCellRows.CreateUnsafe(node));
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x0013F22D File Offset: 0x0013D42D
		public MTrimBottomSingleCellRows(GrammarBuilders g, mTable value0)
		{
			this._node = g.Rule.MTrimBottomSingleCellRows.BuildASTNode(value0.Node);
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x0013F24C File Offset: 0x0013D44C
		public static implicit operator mTable(MTrimBottomSingleCellRows arg)
		{
			return mTable.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06006174 RID: 24948 RVA: 0x0013F25A File Offset: 0x0013D45A
		public mTable mTable
		{
			get
			{
				return mTable.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x0013F26E File Offset: 0x0013D46E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x0013F284 File Offset: 0x0013D484
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006177 RID: 24951 RVA: 0x0013F2AE File Offset: 0x0013D4AE
		public bool Equals(MTrimBottomSingleCellRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BE1 RID: 11233
		private ProgramNode _node;
	}
}
