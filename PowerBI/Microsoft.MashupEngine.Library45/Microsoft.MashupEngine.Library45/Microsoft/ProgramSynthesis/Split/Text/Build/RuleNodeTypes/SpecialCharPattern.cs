using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001346 RID: 4934
	public struct SpecialCharPattern : IProgramNodeBuilder, IEquatable<SpecialCharPattern>
	{
		// Token: 0x17001A1D RID: 6685
		// (get) Token: 0x06009810 RID: 38928 RVA: 0x002063B2 File Offset: 0x002045B2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009811 RID: 38929 RVA: 0x002063BA File Offset: 0x002045BA
		private SpecialCharPattern(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009812 RID: 38930 RVA: 0x002063C3 File Offset: 0x002045C3
		public static SpecialCharPattern CreateUnsafe(ProgramNode node)
		{
			return new SpecialCharPattern(node);
		}

		// Token: 0x06009813 RID: 38931 RVA: 0x002063CC File Offset: 0x002045CC
		public static SpecialCharPattern? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SpecialCharPattern)
			{
				return null;
			}
			return new SpecialCharPattern?(SpecialCharPattern.CreateUnsafe(node));
		}

		// Token: 0x06009814 RID: 38932 RVA: 0x00206401 File Offset: 0x00204601
		public SpecialCharPattern(GrammarBuilders g, v value0, pattern value1)
		{
			this._node = g.Rule.SpecialCharPattern.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06009815 RID: 38933 RVA: 0x00206427 File Offset: 0x00204627
		public static implicit operator pred(SpecialCharPattern arg)
		{
			return pred.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x06009816 RID: 38934 RVA: 0x00206435 File Offset: 0x00204635
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x06009817 RID: 38935 RVA: 0x00206449 File Offset: 0x00204649
		public pattern pattern
		{
			get
			{
				return pattern.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06009818 RID: 38936 RVA: 0x0020645D File Offset: 0x0020465D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009819 RID: 38937 RVA: 0x00206470 File Offset: 0x00204670
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600981A RID: 38938 RVA: 0x0020649A File Offset: 0x0020469A
		public bool Equals(SpecialCharPattern other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DBD RID: 15805
		private ProgramNode _node;
	}
}
