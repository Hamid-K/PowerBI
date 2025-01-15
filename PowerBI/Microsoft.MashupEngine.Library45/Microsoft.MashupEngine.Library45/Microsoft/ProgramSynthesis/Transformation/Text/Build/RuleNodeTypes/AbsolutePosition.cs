using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C1A RID: 7194
	public struct AbsolutePosition : IProgramNodeBuilder, IEquatable<AbsolutePosition>
	{
		// Token: 0x17002879 RID: 10361
		// (get) Token: 0x0600F228 RID: 61992 RVA: 0x003409F6 File Offset: 0x0033EBF6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F229 RID: 61993 RVA: 0x003409FE File Offset: 0x0033EBFE
		private AbsolutePosition(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F22A RID: 61994 RVA: 0x00340A07 File Offset: 0x0033EC07
		[Obsolete("The AbsolutePosition rule is marked as @deprecated in the DSL grammar.")]
		public static AbsolutePosition CreateUnsafe(ProgramNode node)
		{
			return new AbsolutePosition(node);
		}

		// Token: 0x0600F22B RID: 61995 RVA: 0x00340A10 File Offset: 0x0033EC10
		[Obsolete("The AbsolutePosition rule is marked as @deprecated in the DSL grammar.")]
		public static AbsolutePosition? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.AbsolutePosition)
			{
				return null;
			}
			return new AbsolutePosition?(AbsolutePosition.CreateUnsafe(node));
		}

		// Token: 0x0600F22C RID: 61996 RVA: 0x00340A45 File Offset: 0x0033EC45
		[Obsolete("The AbsolutePosition rule is marked as @deprecated in the DSL grammar.")]
		public AbsolutePosition(GrammarBuilders g, x value0, k value1)
		{
			this._node = g.Rule.AbsolutePosition.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F22D RID: 61997 RVA: 0x00340A6B File Offset: 0x0033EC6B
		public static implicit operator pos(AbsolutePosition arg)
		{
			return pos.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700287A RID: 10362
		// (get) Token: 0x0600F22E RID: 61998 RVA: 0x00340A79 File Offset: 0x0033EC79
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700287B RID: 10363
		// (get) Token: 0x0600F22F RID: 61999 RVA: 0x00340A8D File Offset: 0x0033EC8D
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F230 RID: 62000 RVA: 0x00340AA1 File Offset: 0x0033ECA1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F231 RID: 62001 RVA: 0x00340AB4 File Offset: 0x0033ECB4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F232 RID: 62002 RVA: 0x00340ADE File Offset: 0x0033ECDE
		public bool Equals(AbsolutePosition other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B09 RID: 23305
		private ProgramNode _node;
	}
}
