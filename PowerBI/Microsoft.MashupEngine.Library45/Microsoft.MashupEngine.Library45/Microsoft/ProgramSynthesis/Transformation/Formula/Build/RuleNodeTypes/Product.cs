using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001570 RID: 5488
	public struct Product : IProgramNodeBuilder, IEquatable<Product>
	{
		// Token: 0x17001F4B RID: 8011
		// (get) Token: 0x0600B37C RID: 45948 RVA: 0x00273712 File Offset: 0x00271912
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B37D RID: 45949 RVA: 0x0027371A File Offset: 0x0027191A
		private Product(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B37E RID: 45950 RVA: 0x00273723 File Offset: 0x00271923
		public static Product CreateUnsafe(ProgramNode node)
		{
			return new Product(node);
		}

		// Token: 0x0600B37F RID: 45951 RVA: 0x0027372C File Offset: 0x0027192C
		public static Product? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Product)
			{
				return null;
			}
			return new Product?(Product.CreateUnsafe(node));
		}

		// Token: 0x0600B380 RID: 45952 RVA: 0x00273761 File Offset: 0x00271961
		public Product(GrammarBuilders g, fromNumbers value0)
		{
			this._node = g.Rule.Product.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B381 RID: 45953 RVA: 0x00273780 File Offset: 0x00271980
		public static implicit operator arithmetic(Product arg)
		{
			return arithmetic.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F4C RID: 8012
		// (get) Token: 0x0600B382 RID: 45954 RVA: 0x0027378E File Offset: 0x0027198E
		public fromNumbers fromNumbers
		{
			get
			{
				return fromNumbers.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B383 RID: 45955 RVA: 0x002737A2 File Offset: 0x002719A2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B384 RID: 45956 RVA: 0x002737B8 File Offset: 0x002719B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B385 RID: 45957 RVA: 0x002737E2 File Offset: 0x002719E2
		public bool Equals(Product other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400461E RID: 17950
		private ProgramNode _node;
	}
}
