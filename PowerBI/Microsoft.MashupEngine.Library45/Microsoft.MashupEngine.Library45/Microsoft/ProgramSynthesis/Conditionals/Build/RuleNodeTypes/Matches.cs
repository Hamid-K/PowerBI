using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A44 RID: 2628
	public struct Matches : IProgramNodeBuilder, IEquatable<Matches>
	{
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06004096 RID: 16534 RVA: 0x000CAF52 File Offset: 0x000C9152
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x000CAF5A File Offset: 0x000C915A
		private Matches(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x000CAF63 File Offset: 0x000C9163
		public static Matches CreateUnsafe(ProgramNode node)
		{
			return new Matches(node);
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x000CAF6C File Offset: 0x000C916C
		public static Matches? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Matches)
			{
				return null;
			}
			return new Matches?(Matches.CreateUnsafe(node));
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x000CAFA1 File Offset: 0x000C91A1
		public Matches(GrammarBuilders g, s value0, r value1)
		{
			this._node = g.Rule.Matches.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x000CAFC7 File Offset: 0x000C91C7
		public static implicit operator match(Matches arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x0600409C RID: 16540 RVA: 0x000CAFD5 File Offset: 0x000C91D5
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600409D RID: 16541 RVA: 0x000CAFE9 File Offset: 0x000C91E9
		public r r
		{
			get
			{
				return r.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x000CAFFD File Offset: 0x000C91FD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x000CB010 File Offset: 0x000C9210
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x000CB03A File Offset: 0x000C923A
		public bool Equals(Matches other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D7F RID: 7551
		private ProgramNode _node;
	}
}
