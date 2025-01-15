using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A3A RID: 2618
	public struct IsNull : IProgramNodeBuilder, IEquatable<IsNull>
	{
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600402F RID: 16431 RVA: 0x000CA61E File Offset: 0x000C881E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x000CA626 File Offset: 0x000C8826
		private IsNull(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x000CA62F File Offset: 0x000C882F
		public static IsNull CreateUnsafe(ProgramNode node)
		{
			return new IsNull(node);
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x000CA638 File Offset: 0x000C8838
		public static IsNull? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IsNull)
			{
				return null;
			}
			return new IsNull?(IsNull.CreateUnsafe(node));
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x000CA66D File Offset: 0x000C886D
		public IsNull(GrammarBuilders g, s value0)
		{
			this._node = g.Rule.IsNull.BuildASTNode(value0.Node);
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x000CA68C File Offset: 0x000C888C
		public static implicit operator match(IsNull arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x000CA69A File Offset: 0x000C889A
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x000CA6AE File Offset: 0x000C88AE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x000CA6C4 File Offset: 0x000C88C4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x000CA6EE File Offset: 0x000C88EE
		public bool Equals(IsNull other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D75 RID: 7541
		private ProgramNode _node;
	}
}
