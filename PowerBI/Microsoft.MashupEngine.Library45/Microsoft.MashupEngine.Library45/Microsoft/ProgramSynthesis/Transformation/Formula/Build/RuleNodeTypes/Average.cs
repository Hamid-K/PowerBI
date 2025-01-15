using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001571 RID: 5489
	public struct Average : IProgramNodeBuilder, IEquatable<Average>
	{
		// Token: 0x17001F4D RID: 8013
		// (get) Token: 0x0600B386 RID: 45958 RVA: 0x002737F6 File Offset: 0x002719F6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B387 RID: 45959 RVA: 0x002737FE File Offset: 0x002719FE
		private Average(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B388 RID: 45960 RVA: 0x00273807 File Offset: 0x00271A07
		public static Average CreateUnsafe(ProgramNode node)
		{
			return new Average(node);
		}

		// Token: 0x0600B389 RID: 45961 RVA: 0x00273810 File Offset: 0x00271A10
		public static Average? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Average)
			{
				return null;
			}
			return new Average?(Average.CreateUnsafe(node));
		}

		// Token: 0x0600B38A RID: 45962 RVA: 0x00273845 File Offset: 0x00271A45
		public Average(GrammarBuilders g, fromNumbers value0)
		{
			this._node = g.Rule.Average.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B38B RID: 45963 RVA: 0x00273864 File Offset: 0x00271A64
		public static implicit operator arithmetic(Average arg)
		{
			return arithmetic.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F4E RID: 8014
		// (get) Token: 0x0600B38C RID: 45964 RVA: 0x00273872 File Offset: 0x00271A72
		public fromNumbers fromNumbers
		{
			get
			{
				return fromNumbers.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B38D RID: 45965 RVA: 0x00273886 File Offset: 0x00271A86
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B38E RID: 45966 RVA: 0x0027389C File Offset: 0x00271A9C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B38F RID: 45967 RVA: 0x002738C6 File Offset: 0x00271AC6
		public bool Equals(Average other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400461F RID: 17951
		private ProgramNode _node;
	}
}
