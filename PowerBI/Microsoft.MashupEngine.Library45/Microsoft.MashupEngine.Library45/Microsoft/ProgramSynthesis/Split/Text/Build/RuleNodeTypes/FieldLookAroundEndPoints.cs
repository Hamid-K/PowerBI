using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001349 RID: 4937
	public struct FieldLookAroundEndPoints : IProgramNodeBuilder, IEquatable<FieldLookAroundEndPoints>
	{
		// Token: 0x17001A26 RID: 6694
		// (get) Token: 0x06009831 RID: 38961 RVA: 0x002066AA File Offset: 0x002048AA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009832 RID: 38962 RVA: 0x002066B2 File Offset: 0x002048B2
		private FieldLookAroundEndPoints(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009833 RID: 38963 RVA: 0x002066BB File Offset: 0x002048BB
		public static FieldLookAroundEndPoints CreateUnsafe(ProgramNode node)
		{
			return new FieldLookAroundEndPoints(node);
		}

		// Token: 0x06009834 RID: 38964 RVA: 0x002066C4 File Offset: 0x002048C4
		public static FieldLookAroundEndPoints? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FieldLookAroundEndPoints)
			{
				return null;
			}
			return new FieldLookAroundEndPoints?(FieldLookAroundEndPoints.CreateUnsafe(node));
		}

		// Token: 0x06009835 RID: 38965 RVA: 0x002066F9 File Offset: 0x002048F9
		public FieldLookAroundEndPoints(GrammarBuilders g, regexMatch value0, fieldMatch value1, regexMatch value2)
		{
			this._node = g.Rule.FieldLookAroundEndPoints.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06009836 RID: 38966 RVA: 0x00206726 File Offset: 0x00204926
		public static implicit operator d(FieldLookAroundEndPoints arg)
		{
			return d.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A27 RID: 6695
		// (get) Token: 0x06009837 RID: 38967 RVA: 0x00206734 File Offset: 0x00204934
		public regexMatch regexMatch1
		{
			get
			{
				return regexMatch.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A28 RID: 6696
		// (get) Token: 0x06009838 RID: 38968 RVA: 0x00206748 File Offset: 0x00204948
		public fieldMatch fieldMatch
		{
			get
			{
				return fieldMatch.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001A29 RID: 6697
		// (get) Token: 0x06009839 RID: 38969 RVA: 0x0020675C File Offset: 0x0020495C
		public regexMatch regexMatch2
		{
			get
			{
				return regexMatch.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600983A RID: 38970 RVA: 0x00206770 File Offset: 0x00204970
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600983B RID: 38971 RVA: 0x00206784 File Offset: 0x00204984
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600983C RID: 38972 RVA: 0x002067AE File Offset: 0x002049AE
		public bool Equals(FieldLookAroundEndPoints other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DC0 RID: 15808
		private ProgramNode _node;
	}
}
