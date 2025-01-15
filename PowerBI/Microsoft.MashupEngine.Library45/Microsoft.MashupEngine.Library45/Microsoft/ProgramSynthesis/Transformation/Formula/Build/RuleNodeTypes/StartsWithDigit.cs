using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001558 RID: 5464
	public struct StartsWithDigit : IProgramNodeBuilder, IEquatable<StartsWithDigit>
	{
		// Token: 0x17001F00 RID: 7936
		// (get) Token: 0x0600B271 RID: 45681 RVA: 0x00271EEE File Offset: 0x002700EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B272 RID: 45682 RVA: 0x00271EF6 File Offset: 0x002700F6
		private StartsWithDigit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B273 RID: 45683 RVA: 0x00271EFF File Offset: 0x002700FF
		public static StartsWithDigit CreateUnsafe(ProgramNode node)
		{
			return new StartsWithDigit(node);
		}

		// Token: 0x0600B274 RID: 45684 RVA: 0x00271F08 File Offset: 0x00270108
		public static StartsWithDigit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.StartsWithDigit)
			{
				return null;
			}
			return new StartsWithDigit?(StartsWithDigit.CreateUnsafe(node));
		}

		// Token: 0x0600B275 RID: 45685 RVA: 0x00271F3D File Offset: 0x0027013D
		public StartsWithDigit(GrammarBuilders g, row value0, columnName value1)
		{
			this._node = g.Rule.StartsWithDigit.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B276 RID: 45686 RVA: 0x00271F63 File Offset: 0x00270163
		public static implicit operator condition(StartsWithDigit arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F01 RID: 7937
		// (get) Token: 0x0600B277 RID: 45687 RVA: 0x00271F71 File Offset: 0x00270171
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F02 RID: 7938
		// (get) Token: 0x0600B278 RID: 45688 RVA: 0x00271F85 File Offset: 0x00270185
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B279 RID: 45689 RVA: 0x00271F99 File Offset: 0x00270199
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B27A RID: 45690 RVA: 0x00271FAC File Offset: 0x002701AC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B27B RID: 45691 RVA: 0x00271FD6 File Offset: 0x002701D6
		public bool Equals(StartsWithDigit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004606 RID: 17926
		private ProgramNode _node;
	}
}
