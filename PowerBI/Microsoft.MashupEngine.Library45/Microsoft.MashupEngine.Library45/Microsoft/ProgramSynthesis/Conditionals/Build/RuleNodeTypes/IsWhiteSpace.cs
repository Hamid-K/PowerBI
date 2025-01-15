using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A3B RID: 2619
	public struct IsWhiteSpace : IProgramNodeBuilder, IEquatable<IsWhiteSpace>
	{
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06004039 RID: 16441 RVA: 0x000CA702 File Offset: 0x000C8902
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000CA70A File Offset: 0x000C890A
		private IsWhiteSpace(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x000CA713 File Offset: 0x000C8913
		public static IsWhiteSpace CreateUnsafe(ProgramNode node)
		{
			return new IsWhiteSpace(node);
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x000CA71C File Offset: 0x000C891C
		public static IsWhiteSpace? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IsWhiteSpace)
			{
				return null;
			}
			return new IsWhiteSpace?(IsWhiteSpace.CreateUnsafe(node));
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x000CA751 File Offset: 0x000C8951
		public IsWhiteSpace(GrammarBuilders g, s value0)
		{
			this._node = g.Rule.IsWhiteSpace.BuildASTNode(value0.Node);
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x000CA770 File Offset: 0x000C8970
		public static implicit operator match(IsWhiteSpace arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x0600403F RID: 16447 RVA: 0x000CA77E File Offset: 0x000C897E
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x000CA792 File Offset: 0x000C8992
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x000CA7A8 File Offset: 0x000C89A8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x000CA7D2 File Offset: 0x000C89D2
		public bool Equals(IsWhiteSpace other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D76 RID: 7542
		private ProgramNode _node;
	}
}
