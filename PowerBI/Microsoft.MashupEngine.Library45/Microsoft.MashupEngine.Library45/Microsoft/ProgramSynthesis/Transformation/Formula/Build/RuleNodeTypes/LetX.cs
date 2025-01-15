using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001598 RID: 5528
	public struct LetX : IProgramNodeBuilder, IEquatable<LetX>
	{
		// Token: 0x17001FBC RID: 8124
		// (get) Token: 0x0600B52D RID: 46381 RVA: 0x00275E0E File Offset: 0x0027400E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B52E RID: 46382 RVA: 0x00275E16 File Offset: 0x00274016
		private LetX(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B52F RID: 46383 RVA: 0x00275E1F File Offset: 0x0027401F
		public static LetX CreateUnsafe(ProgramNode node)
		{
			return new LetX(node);
		}

		// Token: 0x0600B530 RID: 46384 RVA: 0x00275E28 File Offset: 0x00274028
		public static LetX? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetX)
			{
				return null;
			}
			return new LetX?(LetX.CreateUnsafe(node));
		}

		// Token: 0x0600B531 RID: 46385 RVA: 0x00275E5D File Offset: 0x0027405D
		public LetX(GrammarBuilders g, fromStrTrim value0, substring value1)
		{
			this._node = new LetNode(g.Rule.LetX, value0.Node, value1.Node);
		}

		// Token: 0x0600B532 RID: 46386 RVA: 0x00275E83 File Offset: 0x00274083
		public static implicit operator letSubstring(LetX arg)
		{
			return letSubstring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001FBD RID: 8125
		// (get) Token: 0x0600B533 RID: 46387 RVA: 0x00275E91 File Offset: 0x00274091
		public fromStrTrim fromStrTrim
		{
			get
			{
				return fromStrTrim.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001FBE RID: 8126
		// (get) Token: 0x0600B534 RID: 46388 RVA: 0x00275EA5 File Offset: 0x002740A5
		public substring substring
		{
			get
			{
				return substring.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B535 RID: 46389 RVA: 0x00275EB9 File Offset: 0x002740B9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B536 RID: 46390 RVA: 0x00275ECC File Offset: 0x002740CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B537 RID: 46391 RVA: 0x00275EF6 File Offset: 0x002740F6
		public bool Equals(LetX other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004646 RID: 17990
		private ProgramNode _node;
	}
}
