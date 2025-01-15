using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001341 RID: 4929
	public struct DelimitersList : IProgramNodeBuilder, IEquatable<DelimitersList>
	{
		// Token: 0x17001A11 RID: 6673
		// (get) Token: 0x060097DC RID: 38876 RVA: 0x00205F0A File Offset: 0x0020410A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060097DD RID: 38877 RVA: 0x00205F12 File Offset: 0x00204112
		private DelimitersList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060097DE RID: 38878 RVA: 0x00205F1B File Offset: 0x0020411B
		public static DelimitersList CreateUnsafe(ProgramNode node)
		{
			return new DelimitersList(node);
		}

		// Token: 0x060097DF RID: 38879 RVA: 0x00205F24 File Offset: 0x00204124
		public static DelimitersList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DelimitersList)
			{
				return null;
			}
			return new DelimitersList?(DelimitersList.CreateUnsafe(node));
		}

		// Token: 0x060097E0 RID: 38880 RVA: 0x00205F59 File Offset: 0x00204159
		public DelimitersList(GrammarBuilders g, delimiterList value0, d value1)
		{
			this._node = g.Rule.DelimitersList.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060097E1 RID: 38881 RVA: 0x00205F7F File Offset: 0x0020417F
		public static implicit operator delimiterList(DelimitersList arg)
		{
			return delimiterList.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A12 RID: 6674
		// (get) Token: 0x060097E2 RID: 38882 RVA: 0x00205F8D File Offset: 0x0020418D
		public delimiterList delimiterList
		{
			get
			{
				return delimiterList.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A13 RID: 6675
		// (get) Token: 0x060097E3 RID: 38883 RVA: 0x00205FA1 File Offset: 0x002041A1
		public d d
		{
			get
			{
				return d.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060097E4 RID: 38884 RVA: 0x00205FB5 File Offset: 0x002041B5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060097E5 RID: 38885 RVA: 0x00205FC8 File Offset: 0x002041C8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060097E6 RID: 38886 RVA: 0x00205FF2 File Offset: 0x002041F2
		public bool Equals(DelimitersList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DB8 RID: 15800
		private ProgramNode _node;
	}
}
