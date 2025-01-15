using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001357 RID: 4951
	public struct GEN_FieldLookAroundEndPoints : IProgramNodeBuilder, IEquatable<GEN_FieldLookAroundEndPoints>
	{
		// Token: 0x17001A52 RID: 6738
		// (get) Token: 0x060098CD RID: 39117 RVA: 0x002074AE File Offset: 0x002056AE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060098CE RID: 39118 RVA: 0x002074B6 File Offset: 0x002056B6
		private GEN_FieldLookAroundEndPoints(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060098CF RID: 39119 RVA: 0x002074BF File Offset: 0x002056BF
		public static GEN_FieldLookAroundEndPoints CreateUnsafe(ProgramNode node)
		{
			return new GEN_FieldLookAroundEndPoints(node);
		}

		// Token: 0x060098D0 RID: 39120 RVA: 0x002074C8 File Offset: 0x002056C8
		public static GEN_FieldLookAroundEndPoints? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.GEN_FieldLookAroundEndPoints)
			{
				return null;
			}
			return new GEN_FieldLookAroundEndPoints?(GEN_FieldLookAroundEndPoints.CreateUnsafe(node));
		}

		// Token: 0x060098D1 RID: 39121 RVA: 0x002074FD File Offset: 0x002056FD
		public GEN_FieldLookAroundEndPoints(GrammarBuilders g, obj value0, obj value1, obj value2)
		{
			this._node = g.Rule.GEN_FieldLookAroundEndPoints.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x060098D2 RID: 39122 RVA: 0x0020752A File Offset: 0x0020572A
		public static implicit operator gen_LookAroundField(GEN_FieldLookAroundEndPoints arg)
		{
			return gen_LookAroundField.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A53 RID: 6739
		// (get) Token: 0x060098D3 RID: 39123 RVA: 0x00207538 File Offset: 0x00205738
		public obj obj1
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A54 RID: 6740
		// (get) Token: 0x060098D4 RID: 39124 RVA: 0x0020754C File Offset: 0x0020574C
		public obj obj2
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001A55 RID: 6741
		// (get) Token: 0x060098D5 RID: 39125 RVA: 0x00207560 File Offset: 0x00205760
		public obj obj3
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x060098D6 RID: 39126 RVA: 0x00207574 File Offset: 0x00205774
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060098D7 RID: 39127 RVA: 0x00207588 File Offset: 0x00205788
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060098D8 RID: 39128 RVA: 0x002075B2 File Offset: 0x002057B2
		public bool Equals(GEN_FieldLookAroundEndPoints other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DCE RID: 15822
		private ProgramNode _node;
	}
}
