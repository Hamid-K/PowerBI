using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E5F RID: 7775
	public struct GuardedRule : IProgramNodeBuilder, IEquatable<GuardedRule>
	{
		// Token: 0x17002B7E RID: 11134
		// (get) Token: 0x06010603 RID: 67075 RVA: 0x00389132 File Offset: 0x00387332
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010604 RID: 67076 RVA: 0x0038913A File Offset: 0x0038733A
		private GuardedRule(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010605 RID: 67077 RVA: 0x00389143 File Offset: 0x00387343
		public static GuardedRule CreateUnsafe(ProgramNode node)
		{
			return new GuardedRule(node);
		}

		// Token: 0x06010606 RID: 67078 RVA: 0x0038914C File Offset: 0x0038734C
		public static GuardedRule? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.GuardedRule)
			{
				return null;
			}
			return new GuardedRule?(GuardedRule.CreateUnsafe(node));
		}

		// Token: 0x06010607 RID: 67079 RVA: 0x00389181 File Offset: 0x00387381
		public GuardedRule(GrammarBuilders g, match value0, newDsl value1)
		{
			this._node = g.Rule.GuardedRule.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06010608 RID: 67080 RVA: 0x003891A7 File Offset: 0x003873A7
		public static implicit operator guardedRule(GuardedRule arg)
		{
			return guardedRule.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B7F RID: 11135
		// (get) Token: 0x06010609 RID: 67081 RVA: 0x003891B5 File Offset: 0x003873B5
		public match match
		{
			get
			{
				return match.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002B80 RID: 11136
		// (get) Token: 0x0601060A RID: 67082 RVA: 0x003891C9 File Offset: 0x003873C9
		public newDsl newDsl
		{
			get
			{
				return newDsl.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0601060B RID: 67083 RVA: 0x003891DD File Offset: 0x003873DD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601060C RID: 67084 RVA: 0x003891F0 File Offset: 0x003873F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601060D RID: 67085 RVA: 0x0038921A File Offset: 0x0038741A
		public bool Equals(GuardedRule other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400629E RID: 25246
		private ProgramNode _node;
	}
}
