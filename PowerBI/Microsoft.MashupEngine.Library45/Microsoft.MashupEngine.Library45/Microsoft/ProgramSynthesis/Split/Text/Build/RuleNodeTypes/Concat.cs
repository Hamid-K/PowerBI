using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001350 RID: 4944
	public struct Concat : IProgramNodeBuilder, IEquatable<Concat>
	{
		// Token: 0x17001A3C RID: 6716
		// (get) Token: 0x0600987F RID: 39039 RVA: 0x00206DAE File Offset: 0x00204FAE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009880 RID: 39040 RVA: 0x00206DB6 File Offset: 0x00204FB6
		private Concat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009881 RID: 39041 RVA: 0x00206DBF File Offset: 0x00204FBF
		public static Concat CreateUnsafe(ProgramNode node)
		{
			return new Concat(node);
		}

		// Token: 0x06009882 RID: 39042 RVA: 0x00206DC8 File Offset: 0x00204FC8
		public static Concat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Concat)
			{
				return null;
			}
			return new Concat?(Concat.CreateUnsafe(node));
		}

		// Token: 0x06009883 RID: 39043 RVA: 0x00206DFD File Offset: 0x00204FFD
		public Concat(GrammarBuilders g, r value0, regexMatch value1)
		{
			this._node = g.Rule.Concat.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06009884 RID: 39044 RVA: 0x00206E23 File Offset: 0x00205023
		public static implicit operator r(Concat arg)
		{
			return r.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A3D RID: 6717
		// (get) Token: 0x06009885 RID: 39045 RVA: 0x00206E31 File Offset: 0x00205031
		public r r
		{
			get
			{
				return r.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A3E RID: 6718
		// (get) Token: 0x06009886 RID: 39046 RVA: 0x00206E45 File Offset: 0x00205045
		public regexMatch regexMatch
		{
			get
			{
				return regexMatch.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06009887 RID: 39047 RVA: 0x00206E59 File Offset: 0x00205059
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009888 RID: 39048 RVA: 0x00206E6C File Offset: 0x0020506C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009889 RID: 39049 RVA: 0x00206E96 File Offset: 0x00205096
		public bool Equals(Concat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DC7 RID: 15815
		private ProgramNode _node;
	}
}
