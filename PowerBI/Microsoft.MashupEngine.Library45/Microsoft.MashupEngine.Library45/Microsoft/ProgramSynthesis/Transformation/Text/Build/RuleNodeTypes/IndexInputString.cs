using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001BFE RID: 7166
	public struct IndexInputString : IProgramNodeBuilder, IEquatable<IndexInputString>
	{
		// Token: 0x17002822 RID: 10274
		// (get) Token: 0x0600F0F1 RID: 61681 RVA: 0x0033EDBA File Offset: 0x0033CFBA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F0F2 RID: 61682 RVA: 0x0033EDC2 File Offset: 0x0033CFC2
		private IndexInputString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F0F3 RID: 61683 RVA: 0x0033EDCB File Offset: 0x0033CFCB
		public static IndexInputString CreateUnsafe(ProgramNode node)
		{
			return new IndexInputString(node);
		}

		// Token: 0x0600F0F4 RID: 61684 RVA: 0x0033EDD4 File Offset: 0x0033CFD4
		public static IndexInputString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IndexInputString)
			{
				return null;
			}
			return new IndexInputString?(IndexInputString.CreateUnsafe(node));
		}

		// Token: 0x0600F0F5 RID: 61685 RVA: 0x0033EE09 File Offset: 0x0033D009
		public IndexInputString(GrammarBuilders g, vs value0, columnIdx value1)
		{
			this._node = g.Rule.IndexInputString.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F0F6 RID: 61686 RVA: 0x0033EE2F File Offset: 0x0033D02F
		public static implicit operator indexInputString(IndexInputString arg)
		{
			return indexInputString.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002823 RID: 10275
		// (get) Token: 0x0600F0F7 RID: 61687 RVA: 0x0033EE3D File Offset: 0x0033D03D
		public vs vs
		{
			get
			{
				return vs.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002824 RID: 10276
		// (get) Token: 0x0600F0F8 RID: 61688 RVA: 0x0033EE51 File Offset: 0x0033D051
		public columnIdx columnIdx
		{
			get
			{
				return columnIdx.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F0F9 RID: 61689 RVA: 0x0033EE65 File Offset: 0x0033D065
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F0FA RID: 61690 RVA: 0x0033EE78 File Offset: 0x0033D078
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F0FB RID: 61691 RVA: 0x0033EEA2 File Offset: 0x0033D0A2
		public bool Equals(IndexInputString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AED RID: 23277
		private ProgramNode _node;
	}
}
