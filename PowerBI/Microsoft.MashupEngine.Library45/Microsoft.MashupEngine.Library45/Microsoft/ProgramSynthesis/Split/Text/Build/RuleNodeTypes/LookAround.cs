using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001347 RID: 4935
	public struct LookAround : IProgramNodeBuilder, IEquatable<LookAround>
	{
		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x0600981B RID: 38939 RVA: 0x002064AE File Offset: 0x002046AE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600981C RID: 38940 RVA: 0x002064B6 File Offset: 0x002046B6
		private LookAround(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600981D RID: 38941 RVA: 0x002064BF File Offset: 0x002046BF
		public static LookAround CreateUnsafe(ProgramNode node)
		{
			return new LookAround(node);
		}

		// Token: 0x0600981E RID: 38942 RVA: 0x002064C8 File Offset: 0x002046C8
		public static LookAround? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LookAround)
			{
				return null;
			}
			return new LookAround?(LookAround.CreateUnsafe(node));
		}

		// Token: 0x0600981F RID: 38943 RVA: 0x002064FD File Offset: 0x002046FD
		public LookAround(GrammarBuilders g, r value0, c value1, r value2)
		{
			this._node = g.Rule.LookAround.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06009820 RID: 38944 RVA: 0x0020652A File Offset: 0x0020472A
		public static implicit operator d(LookAround arg)
		{
			return d.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x06009821 RID: 38945 RVA: 0x00206538 File Offset: 0x00204738
		public r r1
		{
			get
			{
				return r.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x06009822 RID: 38946 RVA: 0x0020654C File Offset: 0x0020474C
		public c c
		{
			get
			{
				return c.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x06009823 RID: 38947 RVA: 0x00206560 File Offset: 0x00204760
		public r r2
		{
			get
			{
				return r.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06009824 RID: 38948 RVA: 0x00206574 File Offset: 0x00204774
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009825 RID: 38949 RVA: 0x00206588 File Offset: 0x00204788
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009826 RID: 38950 RVA: 0x002065B2 File Offset: 0x002047B2
		public bool Equals(LookAround other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DBE RID: 15806
		private ProgramNode _node;
	}
}
