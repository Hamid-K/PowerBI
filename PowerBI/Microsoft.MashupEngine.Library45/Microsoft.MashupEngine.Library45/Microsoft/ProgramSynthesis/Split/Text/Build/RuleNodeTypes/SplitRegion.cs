using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200133F RID: 4927
	public struct SplitRegion : IProgramNodeBuilder, IEquatable<SplitRegion>
	{
		// Token: 0x17001A05 RID: 6661
		// (get) Token: 0x060097C0 RID: 38848 RVA: 0x00205C42 File Offset: 0x00203E42
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060097C1 RID: 38849 RVA: 0x00205C4A File Offset: 0x00203E4A
		private SplitRegion(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060097C2 RID: 38850 RVA: 0x00205C53 File Offset: 0x00203E53
		public static SplitRegion CreateUnsafe(ProgramNode node)
		{
			return new SplitRegion(node);
		}

		// Token: 0x060097C3 RID: 38851 RVA: 0x00205C5C File Offset: 0x00203E5C
		public static SplitRegion? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitRegion)
			{
				return null;
			}
			return new SplitRegion?(SplitRegion.CreateUnsafe(node));
		}

		// Token: 0x060097C4 RID: 38852 RVA: 0x00205C94 File Offset: 0x00203E94
		public SplitRegion(GrammarBuilders g, v value0, splitMatches value1, ignoreIndexes value2, numSplits value3, delimiterStart value4, delimiterEnd value5, includeDelimiters value6, fillStrategy value7)
		{
			this._node = g.Rule.SplitRegion.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node, value4.Node, value5.Node, value6.Node, value7.Node });
		}

		// Token: 0x060097C5 RID: 38853 RVA: 0x00205D0D File Offset: 0x00203F0D
		public static implicit operator regionSplit(SplitRegion arg)
		{
			return regionSplit.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A06 RID: 6662
		// (get) Token: 0x060097C6 RID: 38854 RVA: 0x00205D1B File Offset: 0x00203F1B
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A07 RID: 6663
		// (get) Token: 0x060097C7 RID: 38855 RVA: 0x00205D2F File Offset: 0x00203F2F
		public splitMatches splitMatches
		{
			get
			{
				return splitMatches.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001A08 RID: 6664
		// (get) Token: 0x060097C8 RID: 38856 RVA: 0x00205D43 File Offset: 0x00203F43
		public ignoreIndexes ignoreIndexes
		{
			get
			{
				return ignoreIndexes.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x17001A09 RID: 6665
		// (get) Token: 0x060097C9 RID: 38857 RVA: 0x00205D57 File Offset: 0x00203F57
		public numSplits numSplits
		{
			get
			{
				return numSplits.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x17001A0A RID: 6666
		// (get) Token: 0x060097CA RID: 38858 RVA: 0x00205D6B File Offset: 0x00203F6B
		public delimiterStart delimiterStart
		{
			get
			{
				return delimiterStart.CreateUnsafe(this.Node.Children[4]);
			}
		}

		// Token: 0x17001A0B RID: 6667
		// (get) Token: 0x060097CB RID: 38859 RVA: 0x00205D7F File Offset: 0x00203F7F
		public delimiterEnd delimiterEnd
		{
			get
			{
				return delimiterEnd.CreateUnsafe(this.Node.Children[5]);
			}
		}

		// Token: 0x17001A0C RID: 6668
		// (get) Token: 0x060097CC RID: 38860 RVA: 0x00205D93 File Offset: 0x00203F93
		public includeDelimiters includeDelimiters
		{
			get
			{
				return includeDelimiters.CreateUnsafe(this.Node.Children[6]);
			}
		}

		// Token: 0x17001A0D RID: 6669
		// (get) Token: 0x060097CD RID: 38861 RVA: 0x00205DA7 File Offset: 0x00203FA7
		public fillStrategy fillStrategy
		{
			get
			{
				return fillStrategy.CreateUnsafe(this.Node.Children[7]);
			}
		}

		// Token: 0x060097CE RID: 38862 RVA: 0x00205DBB File Offset: 0x00203FBB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060097CF RID: 38863 RVA: 0x00205DD0 File Offset: 0x00203FD0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060097D0 RID: 38864 RVA: 0x00205DFA File Offset: 0x00203FFA
		public bool Equals(SplitRegion other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DB6 RID: 15798
		private ProgramNode _node;
	}
}
