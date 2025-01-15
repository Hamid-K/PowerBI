using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A3F RID: 2623
	public struct StartsWithLetter : IProgramNodeBuilder, IEquatable<StartsWithLetter>
	{
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x000CAA92 File Offset: 0x000C8C92
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x000CAA9A File Offset: 0x000C8C9A
		private StartsWithLetter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x000CAAA3 File Offset: 0x000C8CA3
		public static StartsWithLetter CreateUnsafe(ProgramNode node)
		{
			return new StartsWithLetter(node);
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x000CAAAC File Offset: 0x000C8CAC
		public static StartsWithLetter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.StartsWithLetter)
			{
				return null;
			}
			return new StartsWithLetter?(StartsWithLetter.CreateUnsafe(node));
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x000CAAE1 File Offset: 0x000C8CE1
		public StartsWithLetter(GrammarBuilders g, s value0)
		{
			this._node = g.Rule.StartsWithLetter.BuildASTNode(value0.Node);
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000CAB00 File Offset: 0x000C8D00
		public static implicit operator match(StartsWithLetter arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06004067 RID: 16487 RVA: 0x000CAB0E File Offset: 0x000C8D0E
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x000CAB22 File Offset: 0x000C8D22
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x000CAB38 File Offset: 0x000C8D38
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x000CAB62 File Offset: 0x000C8D62
		public bool Equals(StartsWithLetter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D7A RID: 7546
		private ProgramNode _node;
	}
}
