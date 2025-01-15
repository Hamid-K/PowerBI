using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E7A RID: 7802
	public struct guardedRule : IProgramNodeBuilder, IEquatable<guardedRule>
	{
		// Token: 0x17002BD3 RID: 11219
		// (get) Token: 0x06010730 RID: 67376 RVA: 0x0038ACC6 File Offset: 0x00388EC6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010731 RID: 67377 RVA: 0x0038ACCE File Offset: 0x00388ECE
		private guardedRule(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010732 RID: 67378 RVA: 0x0038ACD7 File Offset: 0x00388ED7
		public static guardedRule CreateUnsafe(ProgramNode node)
		{
			return new guardedRule(node);
		}

		// Token: 0x06010733 RID: 67379 RVA: 0x0038ACE0 File Offset: 0x00388EE0
		public static guardedRule? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.guardedRule)
			{
				return null;
			}
			return new guardedRule?(guardedRule.CreateUnsafe(node));
		}

		// Token: 0x06010734 RID: 67380 RVA: 0x0038AD1A File Offset: 0x00388F1A
		public static guardedRule CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new guardedRule(new Hole(g.Symbol.guardedRule, holeId));
		}

		// Token: 0x06010735 RID: 67381 RVA: 0x0038AD32 File Offset: 0x00388F32
		public GuardedRule Cast_GuardedRule()
		{
			return GuardedRule.CreateUnsafe(this.Node);
		}

		// Token: 0x06010736 RID: 67382 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_GuardedRule(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06010737 RID: 67383 RVA: 0x0038AD3F File Offset: 0x00388F3F
		public bool Is_GuardedRule(GrammarBuilders g, out GuardedRule value)
		{
			value = GuardedRule.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06010738 RID: 67384 RVA: 0x0038AD53 File Offset: 0x00388F53
		public GuardedRule? As_GuardedRule(GrammarBuilders g)
		{
			return new GuardedRule?(GuardedRule.CreateUnsafe(this.Node));
		}

		// Token: 0x06010739 RID: 67385 RVA: 0x0038AD65 File Offset: 0x00388F65
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601073A RID: 67386 RVA: 0x0038AD78 File Offset: 0x00388F78
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601073B RID: 67387 RVA: 0x0038ADA2 File Offset: 0x00388FA2
		public bool Equals(guardedRule other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062B9 RID: 25273
		private ProgramNode _node;
	}
}
