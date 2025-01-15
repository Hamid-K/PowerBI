using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C24 RID: 7204
	public struct SingleBranch : IProgramNodeBuilder, IEquatable<SingleBranch>
	{
		// Token: 0x1700289B RID: 10395
		// (get) Token: 0x0600F29A RID: 62106 RVA: 0x0034148A File Offset: 0x0033F68A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F29B RID: 62107 RVA: 0x00341492 File Offset: 0x0033F692
		private SingleBranch(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F29C RID: 62108 RVA: 0x0034149B File Offset: 0x0033F69B
		public static SingleBranch CreateUnsafe(ProgramNode node)
		{
			return new SingleBranch(node);
		}

		// Token: 0x0600F29D RID: 62109 RVA: 0x003414A4 File Offset: 0x0033F6A4
		public static SingleBranch? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SingleBranch)
			{
				return null;
			}
			return new SingleBranch?(SingleBranch.CreateUnsafe(node));
		}

		// Token: 0x0600F29E RID: 62110 RVA: 0x003414D9 File Offset: 0x0033F6D9
		public SingleBranch(GrammarBuilders g, st value0)
		{
			this._node = g.Rule.SingleBranch.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F29F RID: 62111 RVA: 0x003414F8 File Offset: 0x0033F6F8
		public static implicit operator @switch(SingleBranch arg)
		{
			return @switch.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700289C RID: 10396
		// (get) Token: 0x0600F2A0 RID: 62112 RVA: 0x00341506 File Offset: 0x0033F706
		public st st
		{
			get
			{
				return st.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F2A1 RID: 62113 RVA: 0x0034151A File Offset: 0x0033F71A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F2A2 RID: 62114 RVA: 0x00341530 File Offset: 0x0033F730
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F2A3 RID: 62115 RVA: 0x0034155A File Offset: 0x0033F75A
		public bool Equals(SingleBranch other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B13 RID: 23315
		private ProgramNode _node;
	}
}
