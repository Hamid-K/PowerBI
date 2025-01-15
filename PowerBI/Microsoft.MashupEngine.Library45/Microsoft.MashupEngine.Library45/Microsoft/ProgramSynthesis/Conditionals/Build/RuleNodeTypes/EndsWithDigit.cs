using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A41 RID: 2625
	public struct EndsWithDigit : IProgramNodeBuilder, IEquatable<EndsWithDigit>
	{
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x000CAC72 File Offset: 0x000C8E72
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x000CAC7A File Offset: 0x000C8E7A
		private EndsWithDigit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x000CAC83 File Offset: 0x000C8E83
		public static EndsWithDigit CreateUnsafe(ProgramNode node)
		{
			return new EndsWithDigit(node);
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x000CAC8C File Offset: 0x000C8E8C
		public static EndsWithDigit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.EndsWithDigit)
			{
				return null;
			}
			return new EndsWithDigit?(EndsWithDigit.CreateUnsafe(node));
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x000CACC1 File Offset: 0x000C8EC1
		public EndsWithDigit(GrammarBuilders g, s value0)
		{
			this._node = g.Rule.EndsWithDigit.BuildASTNode(value0.Node);
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x000CACE0 File Offset: 0x000C8EE0
		public static implicit operator match(EndsWithDigit arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x000CACEE File Offset: 0x000C8EEE
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x000CAD02 File Offset: 0x000C8F02
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x000CAD18 File Offset: 0x000C8F18
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x000CAD42 File Offset: 0x000C8F42
		public bool Equals(EndsWithDigit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D7C RID: 7548
		private ProgramNode _node;
	}
}
