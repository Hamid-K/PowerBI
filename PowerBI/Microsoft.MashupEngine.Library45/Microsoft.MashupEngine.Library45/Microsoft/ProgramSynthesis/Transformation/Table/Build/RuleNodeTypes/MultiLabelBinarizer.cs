using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AAA RID: 6826
	public struct MultiLabelBinarizer : IProgramNodeBuilder, IEquatable<MultiLabelBinarizer>
	{
		// Token: 0x170025AC RID: 9644
		// (get) Token: 0x0600E169 RID: 57705 RVA: 0x002FFBEE File Offset: 0x002FDDEE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E16A RID: 57706 RVA: 0x002FFBF6 File Offset: 0x002FDDF6
		private MultiLabelBinarizer(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E16B RID: 57707 RVA: 0x002FFBFF File Offset: 0x002FDDFF
		public static MultiLabelBinarizer CreateUnsafe(ProgramNode node)
		{
			return new MultiLabelBinarizer(node);
		}

		// Token: 0x0600E16C RID: 57708 RVA: 0x002FFC08 File Offset: 0x002FDE08
		public static MultiLabelBinarizer? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MultiLabelBinarizer)
			{
				return null;
			}
			return new MultiLabelBinarizer?(MultiLabelBinarizer.CreateUnsafe(node));
		}

		// Token: 0x0600E16D RID: 57709 RVA: 0x002FFC3D File Offset: 0x002FDE3D
		public MultiLabelBinarizer(GrammarBuilders g, table value0, sourceColumnName value1, delimiter value2)
		{
			this._node = g.Rule.MultiLabelBinarizer.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600E16E RID: 57710 RVA: 0x002FFC6A File Offset: 0x002FDE6A
		public static implicit operator table(MultiLabelBinarizer arg)
		{
			return table.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025AD RID: 9645
		// (get) Token: 0x0600E16F RID: 57711 RVA: 0x002FFC78 File Offset: 0x002FDE78
		public table table
		{
			get
			{
				return table.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170025AE RID: 9646
		// (get) Token: 0x0600E170 RID: 57712 RVA: 0x002FFC8C File Offset: 0x002FDE8C
		public sourceColumnName sourceColumnName
		{
			get
			{
				return sourceColumnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170025AF RID: 9647
		// (get) Token: 0x0600E171 RID: 57713 RVA: 0x002FFCA0 File Offset: 0x002FDEA0
		public delimiter delimiter
		{
			get
			{
				return delimiter.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600E172 RID: 57714 RVA: 0x002FFCB4 File Offset: 0x002FDEB4
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E173 RID: 57715 RVA: 0x002FFCC8 File Offset: 0x002FDEC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E174 RID: 57716 RVA: 0x002FFCF2 File Offset: 0x002FDEF2
		public bool Equals(MultiLabelBinarizer other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005569 RID: 21865
		private ProgramNode _node;
	}
}
