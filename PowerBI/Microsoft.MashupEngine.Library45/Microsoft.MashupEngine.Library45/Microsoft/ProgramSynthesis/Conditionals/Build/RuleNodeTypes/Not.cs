using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A38 RID: 2616
	public struct Not : IProgramNodeBuilder, IEquatable<Not>
	{
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x0600401B RID: 16411 RVA: 0x000CA456 File Offset: 0x000C8656
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x000CA45E File Offset: 0x000C865E
		private Not(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x000CA467 File Offset: 0x000C8667
		public static Not CreateUnsafe(ProgramNode node)
		{
			return new Not(node);
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x000CA470 File Offset: 0x000C8670
		public static Not? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Not)
			{
				return null;
			}
			return new Not?(Not.CreateUnsafe(node));
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x000CA4A5 File Offset: 0x000C86A5
		public Not(GrammarBuilders g, match value0)
		{
			this._node = g.Rule.Not.BuildASTNode(value0.Node);
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x000CA4C4 File Offset: 0x000C86C4
		public static implicit operator pred(Not arg)
		{
			return pred.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06004021 RID: 16417 RVA: 0x000CA4D2 File Offset: 0x000C86D2
		public match match
		{
			get
			{
				return match.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x000CA4E6 File Offset: 0x000C86E6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x000CA4FC File Offset: 0x000C86FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x000CA526 File Offset: 0x000C8726
		public bool Equals(Not other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D73 RID: 7539
		private ProgramNode _node;
	}
}
