using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A3D RID: 2621
	public struct StartsWithString : IProgramNodeBuilder, IEquatable<StartsWithString>
	{
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x0600404C RID: 16460 RVA: 0x000CA8B2 File Offset: 0x000C8AB2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x000CA8BA File Offset: 0x000C8ABA
		private StartsWithString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x000CA8C3 File Offset: 0x000C8AC3
		public static StartsWithString CreateUnsafe(ProgramNode node)
		{
			return new StartsWithString(node);
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x000CA8CC File Offset: 0x000C8ACC
		public static StartsWithString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.StartsWithString)
			{
				return null;
			}
			return new StartsWithString?(StartsWithString.CreateUnsafe(node));
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x000CA901 File Offset: 0x000C8B01
		public StartsWithString(GrammarBuilders g, s value0, str value1)
		{
			this._node = g.Rule.StartsWithString.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x000CA927 File Offset: 0x000C8B27
		public static implicit operator match(StartsWithString arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06004052 RID: 16466 RVA: 0x000CA935 File Offset: 0x000C8B35
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06004053 RID: 16467 RVA: 0x000CA949 File Offset: 0x000C8B49
		public str str
		{
			get
			{
				return str.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x000CA95D File Offset: 0x000C8B5D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x000CA970 File Offset: 0x000C8B70
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x000CA99A File Offset: 0x000C8B9A
		public bool Equals(StartsWithString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D78 RID: 7544
		private ProgramNode _node;
	}
}
