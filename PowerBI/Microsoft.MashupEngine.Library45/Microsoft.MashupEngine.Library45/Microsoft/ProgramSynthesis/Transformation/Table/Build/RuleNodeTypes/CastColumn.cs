using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AAB RID: 6827
	public struct CastColumn : IProgramNodeBuilder, IEquatable<CastColumn>
	{
		// Token: 0x170025B0 RID: 9648
		// (get) Token: 0x0600E175 RID: 57717 RVA: 0x002FFD06 File Offset: 0x002FDF06
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E176 RID: 57718 RVA: 0x002FFD0E File Offset: 0x002FDF0E
		private CastColumn(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E177 RID: 57719 RVA: 0x002FFD17 File Offset: 0x002FDF17
		public static CastColumn CreateUnsafe(ProgramNode node)
		{
			return new CastColumn(node);
		}

		// Token: 0x0600E178 RID: 57720 RVA: 0x002FFD20 File Offset: 0x002FDF20
		public static CastColumn? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.CastColumn)
			{
				return null;
			}
			return new CastColumn?(CastColumn.CreateUnsafe(node));
		}

		// Token: 0x0600E179 RID: 57721 RVA: 0x002FFD58 File Offset: 0x002FDF58
		public CastColumn(GrammarBuilders g, table value0, sourceColumnName value1, richDataType value2, isMixedColumn value3)
		{
			this._node = g.Rule.CastColumn.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node });
		}

		// Token: 0x0600E17A RID: 57722 RVA: 0x002FFDA9 File Offset: 0x002FDFA9
		public static implicit operator table(CastColumn arg)
		{
			return table.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025B1 RID: 9649
		// (get) Token: 0x0600E17B RID: 57723 RVA: 0x002FFDB7 File Offset: 0x002FDFB7
		public table table
		{
			get
			{
				return table.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170025B2 RID: 9650
		// (get) Token: 0x0600E17C RID: 57724 RVA: 0x002FFDCB File Offset: 0x002FDFCB
		public sourceColumnName sourceColumnName
		{
			get
			{
				return sourceColumnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170025B3 RID: 9651
		// (get) Token: 0x0600E17D RID: 57725 RVA: 0x002FFDDF File Offset: 0x002FDFDF
		public richDataType richDataType
		{
			get
			{
				return richDataType.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x170025B4 RID: 9652
		// (get) Token: 0x0600E17E RID: 57726 RVA: 0x002FFDF3 File Offset: 0x002FDFF3
		public isMixedColumn isMixedColumn
		{
			get
			{
				return isMixedColumn.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x0600E17F RID: 57727 RVA: 0x002FFE07 File Offset: 0x002FE007
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E180 RID: 57728 RVA: 0x002FFE1C File Offset: 0x002FE01C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E181 RID: 57729 RVA: 0x002FFE46 File Offset: 0x002FE046
		public bool Equals(CastColumn other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400556A RID: 21866
		private ProgramNode _node;
	}
}
