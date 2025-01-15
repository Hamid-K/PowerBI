using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AA8 RID: 6824
	public struct LabelEncode : IProgramNodeBuilder, IEquatable<LabelEncode>
	{
		// Token: 0x170025A6 RID: 9638
		// (get) Token: 0x0600E153 RID: 57683 RVA: 0x002FF9F6 File Offset: 0x002FDBF6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E154 RID: 57684 RVA: 0x002FF9FE File Offset: 0x002FDBFE
		private LabelEncode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E155 RID: 57685 RVA: 0x002FFA07 File Offset: 0x002FDC07
		public static LabelEncode CreateUnsafe(ProgramNode node)
		{
			return new LabelEncode(node);
		}

		// Token: 0x0600E156 RID: 57686 RVA: 0x002FFA10 File Offset: 0x002FDC10
		public static LabelEncode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LabelEncode)
			{
				return null;
			}
			return new LabelEncode?(LabelEncode.CreateUnsafe(node));
		}

		// Token: 0x0600E157 RID: 57687 RVA: 0x002FFA45 File Offset: 0x002FDC45
		public LabelEncode(GrammarBuilders g, table value0, sourceColumnName value1)
		{
			this._node = g.Rule.LabelEncode.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600E158 RID: 57688 RVA: 0x002FFA6B File Offset: 0x002FDC6B
		public static implicit operator table(LabelEncode arg)
		{
			return table.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025A7 RID: 9639
		// (get) Token: 0x0600E159 RID: 57689 RVA: 0x002FFA79 File Offset: 0x002FDC79
		public table table
		{
			get
			{
				return table.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170025A8 RID: 9640
		// (get) Token: 0x0600E15A RID: 57690 RVA: 0x002FFA8D File Offset: 0x002FDC8D
		public sourceColumnName sourceColumnName
		{
			get
			{
				return sourceColumnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600E15B RID: 57691 RVA: 0x002FFAA1 File Offset: 0x002FDCA1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E15C RID: 57692 RVA: 0x002FFAB4 File Offset: 0x002FDCB4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E15D RID: 57693 RVA: 0x002FFADE File Offset: 0x002FDCDE
		public bool Equals(LabelEncode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005567 RID: 21863
		private ProgramNode _node;
	}
}
