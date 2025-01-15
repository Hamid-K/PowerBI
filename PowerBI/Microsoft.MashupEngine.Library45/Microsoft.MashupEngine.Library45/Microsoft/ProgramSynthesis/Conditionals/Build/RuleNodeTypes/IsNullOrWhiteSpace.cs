using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A39 RID: 2617
	public struct IsNullOrWhiteSpace : IProgramNodeBuilder, IEquatable<IsNullOrWhiteSpace>
	{
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06004025 RID: 16421 RVA: 0x000CA53A File Offset: 0x000C873A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x000CA542 File Offset: 0x000C8742
		private IsNullOrWhiteSpace(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x000CA54B File Offset: 0x000C874B
		public static IsNullOrWhiteSpace CreateUnsafe(ProgramNode node)
		{
			return new IsNullOrWhiteSpace(node);
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x000CA554 File Offset: 0x000C8754
		public static IsNullOrWhiteSpace? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IsNullOrWhiteSpace)
			{
				return null;
			}
			return new IsNullOrWhiteSpace?(IsNullOrWhiteSpace.CreateUnsafe(node));
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x000CA589 File Offset: 0x000C8789
		public IsNullOrWhiteSpace(GrammarBuilders g, s value0)
		{
			this._node = g.Rule.IsNullOrWhiteSpace.BuildASTNode(value0.Node);
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x000CA5A8 File Offset: 0x000C87A8
		public static implicit operator match(IsNullOrWhiteSpace arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x0600402B RID: 16427 RVA: 0x000CA5B6 File Offset: 0x000C87B6
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x000CA5CA File Offset: 0x000C87CA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x000CA5E0 File Offset: 0x000C87E0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x000CA60A File Offset: 0x000C880A
		public bool Equals(IsNullOrWhiteSpace other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D74 RID: 7540
		private ProgramNode _node;
	}
}
