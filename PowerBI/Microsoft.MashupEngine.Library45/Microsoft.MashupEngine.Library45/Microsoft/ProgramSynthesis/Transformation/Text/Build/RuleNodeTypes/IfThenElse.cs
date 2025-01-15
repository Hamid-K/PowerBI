using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001BFA RID: 7162
	public struct IfThenElse : IProgramNodeBuilder, IEquatable<IfThenElse>
	{
		// Token: 0x17002816 RID: 10262
		// (get) Token: 0x0600F0C5 RID: 61637 RVA: 0x0033E9C6 File Offset: 0x0033CBC6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F0C6 RID: 61638 RVA: 0x0033E9CE File Offset: 0x0033CBCE
		private IfThenElse(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F0C7 RID: 61639 RVA: 0x0033E9D7 File Offset: 0x0033CBD7
		public static IfThenElse CreateUnsafe(ProgramNode node)
		{
			return new IfThenElse(node);
		}

		// Token: 0x0600F0C8 RID: 61640 RVA: 0x0033E9E0 File Offset: 0x0033CBE0
		public static IfThenElse? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IfThenElse)
			{
				return null;
			}
			return new IfThenElse?(IfThenElse.CreateUnsafe(node));
		}

		// Token: 0x0600F0C9 RID: 61641 RVA: 0x0033EA15 File Offset: 0x0033CC15
		public IfThenElse(GrammarBuilders g, b value0, st value1, @switch value2)
		{
			this._node = g.Rule.IfThenElse.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600F0CA RID: 61642 RVA: 0x0033EA42 File Offset: 0x0033CC42
		public static implicit operator ite(IfThenElse arg)
		{
			return ite.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002817 RID: 10263
		// (get) Token: 0x0600F0CB RID: 61643 RVA: 0x0033EA50 File Offset: 0x0033CC50
		public b b
		{
			get
			{
				return b.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002818 RID: 10264
		// (get) Token: 0x0600F0CC RID: 61644 RVA: 0x0033EA64 File Offset: 0x0033CC64
		public st st
		{
			get
			{
				return st.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002819 RID: 10265
		// (get) Token: 0x0600F0CD RID: 61645 RVA: 0x0033EA78 File Offset: 0x0033CC78
		public @switch @switch
		{
			get
			{
				return @switch.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600F0CE RID: 61646 RVA: 0x0033EA8C File Offset: 0x0033CC8C
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F0CF RID: 61647 RVA: 0x0033EAA0 File Offset: 0x0033CCA0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F0D0 RID: 61648 RVA: 0x0033EACA File Offset: 0x0033CCCA
		public bool Equals(IfThenElse other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AE9 RID: 23273
		private ProgramNode _node;
	}
}
