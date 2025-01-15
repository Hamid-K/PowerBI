using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001585 RID: 5509
	public struct Slice : IProgramNodeBuilder, IEquatable<Slice>
	{
		// Token: 0x17001F83 RID: 8067
		// (get) Token: 0x0600B45C RID: 46172 RVA: 0x00274B22 File Offset: 0x00272D22
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B45D RID: 46173 RVA: 0x00274B2A File Offset: 0x00272D2A
		private Slice(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B45E RID: 46174 RVA: 0x00274B33 File Offset: 0x00272D33
		public static Slice CreateUnsafe(ProgramNode node)
		{
			return new Slice(node);
		}

		// Token: 0x0600B45F RID: 46175 RVA: 0x00274B3C File Offset: 0x00272D3C
		public static Slice? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Slice)
			{
				return null;
			}
			return new Slice?(Slice.CreateUnsafe(node));
		}

		// Token: 0x0600B460 RID: 46176 RVA: 0x00274B71 File Offset: 0x00272D71
		public Slice(GrammarBuilders g, x value0, pos value1, pos value2)
		{
			this._node = g.Rule.Slice.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B461 RID: 46177 RVA: 0x00274B9E File Offset: 0x00272D9E
		public static implicit operator slice(Slice arg)
		{
			return slice.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F84 RID: 8068
		// (get) Token: 0x0600B462 RID: 46178 RVA: 0x00274BAC File Offset: 0x00272DAC
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F85 RID: 8069
		// (get) Token: 0x0600B463 RID: 46179 RVA: 0x00274BC0 File Offset: 0x00272DC0
		public pos pos1
		{
			get
			{
				return pos.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F86 RID: 8070
		// (get) Token: 0x0600B464 RID: 46180 RVA: 0x00274BD4 File Offset: 0x00272DD4
		public pos pos2
		{
			get
			{
				return pos.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B465 RID: 46181 RVA: 0x00274BE8 File Offset: 0x00272DE8
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B466 RID: 46182 RVA: 0x00274BFC File Offset: 0x00272DFC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B467 RID: 46183 RVA: 0x00274C26 File Offset: 0x00272E26
		public bool Equals(Slice other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004633 RID: 17971
		private ProgramNode _node;
	}
}
