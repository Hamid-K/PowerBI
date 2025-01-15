using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200156F RID: 5487
	public struct Sum : IProgramNodeBuilder, IEquatable<Sum>
	{
		// Token: 0x17001F49 RID: 8009
		// (get) Token: 0x0600B372 RID: 45938 RVA: 0x0027362E File Offset: 0x0027182E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B373 RID: 45939 RVA: 0x00273636 File Offset: 0x00271836
		private Sum(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B374 RID: 45940 RVA: 0x0027363F File Offset: 0x0027183F
		public static Sum CreateUnsafe(ProgramNode node)
		{
			return new Sum(node);
		}

		// Token: 0x0600B375 RID: 45941 RVA: 0x00273648 File Offset: 0x00271848
		public static Sum? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Sum)
			{
				return null;
			}
			return new Sum?(Sum.CreateUnsafe(node));
		}

		// Token: 0x0600B376 RID: 45942 RVA: 0x0027367D File Offset: 0x0027187D
		public Sum(GrammarBuilders g, fromNumbers value0)
		{
			this._node = g.Rule.Sum.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B377 RID: 45943 RVA: 0x0027369C File Offset: 0x0027189C
		public static implicit operator arithmetic(Sum arg)
		{
			return arithmetic.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F4A RID: 8010
		// (get) Token: 0x0600B378 RID: 45944 RVA: 0x002736AA File Offset: 0x002718AA
		public fromNumbers fromNumbers
		{
			get
			{
				return fromNumbers.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B379 RID: 45945 RVA: 0x002736BE File Offset: 0x002718BE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B37A RID: 45946 RVA: 0x002736D4 File Offset: 0x002718D4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B37B RID: 45947 RVA: 0x002736FE File Offset: 0x002718FE
		public bool Equals(Sum other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400461D RID: 17949
		private ProgramNode _node;
	}
}
