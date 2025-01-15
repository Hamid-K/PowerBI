using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001340 RID: 4928
	public struct SplitMultiple : IProgramNodeBuilder, IEquatable<SplitMultiple>
	{
		// Token: 0x17001A0E RID: 6670
		// (get) Token: 0x060097D1 RID: 38865 RVA: 0x00205E0E File Offset: 0x0020400E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060097D2 RID: 38866 RVA: 0x00205E16 File Offset: 0x00204016
		private SplitMultiple(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060097D3 RID: 38867 RVA: 0x00205E1F File Offset: 0x0020401F
		public static SplitMultiple CreateUnsafe(ProgramNode node)
		{
			return new SplitMultiple(node);
		}

		// Token: 0x060097D4 RID: 38868 RVA: 0x00205E28 File Offset: 0x00204028
		public static SplitMultiple? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitMultiple)
			{
				return null;
			}
			return new SplitMultiple?(SplitMultiple.CreateUnsafe(node));
		}

		// Token: 0x060097D5 RID: 38869 RVA: 0x00205E5D File Offset: 0x0020405D
		public SplitMultiple(GrammarBuilders g, multipleMatches value0, d value1)
		{
			this._node = g.Rule.SplitMultiple.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060097D6 RID: 38870 RVA: 0x00205E83 File Offset: 0x00204083
		public static implicit operator multipleMatches(SplitMultiple arg)
		{
			return multipleMatches.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A0F RID: 6671
		// (get) Token: 0x060097D7 RID: 38871 RVA: 0x00205E91 File Offset: 0x00204091
		public multipleMatches multipleMatches
		{
			get
			{
				return multipleMatches.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A10 RID: 6672
		// (get) Token: 0x060097D8 RID: 38872 RVA: 0x00205EA5 File Offset: 0x002040A5
		public d d
		{
			get
			{
				return d.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060097D9 RID: 38873 RVA: 0x00205EB9 File Offset: 0x002040B9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060097DA RID: 38874 RVA: 0x00205ECC File Offset: 0x002040CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060097DB RID: 38875 RVA: 0x00205EF6 File Offset: 0x002040F6
		public bool Equals(SplitMultiple other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DB7 RID: 15799
		private ProgramNode _node;
	}
}
