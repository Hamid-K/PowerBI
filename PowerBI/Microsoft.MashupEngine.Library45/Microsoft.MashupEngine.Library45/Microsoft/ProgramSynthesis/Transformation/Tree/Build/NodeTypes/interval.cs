using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E8A RID: 7818
	public struct interval : IProgramNodeBuilder, IEquatable<interval>
	{
		// Token: 0x17002BE3 RID: 11235
		// (get) Token: 0x0601083E RID: 67646 RVA: 0x0038D54A File Offset: 0x0038B74A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601083F RID: 67647 RVA: 0x0038D552 File Offset: 0x0038B752
		private interval(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010840 RID: 67648 RVA: 0x0038D55B File Offset: 0x0038B75B
		public static interval CreateUnsafe(ProgramNode node)
		{
			return new interval(node);
		}

		// Token: 0x06010841 RID: 67649 RVA: 0x0038D564 File Offset: 0x0038B764
		public static interval? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.interval)
			{
				return null;
			}
			return new interval?(interval.CreateUnsafe(node));
		}

		// Token: 0x06010842 RID: 67650 RVA: 0x0038D59E File Offset: 0x0038B79E
		public static interval CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new interval(new Hole(g.Symbol.interval, holeId));
		}

		// Token: 0x06010843 RID: 67651 RVA: 0x0038D5B6 File Offset: 0x0038B7B6
		public SingleList Cast_SingleList()
		{
			return SingleList.CreateUnsafe(this.Node);
		}

		// Token: 0x06010844 RID: 67652 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SingleList(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06010845 RID: 67653 RVA: 0x0038D5C3 File Offset: 0x0038B7C3
		public bool Is_SingleList(GrammarBuilders g, out SingleList value)
		{
			value = SingleList.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06010846 RID: 67654 RVA: 0x0038D5D7 File Offset: 0x0038B7D7
		public SingleList? As_SingleList(GrammarBuilders g)
		{
			return new SingleList?(SingleList.CreateUnsafe(this.Node));
		}

		// Token: 0x06010847 RID: 67655 RVA: 0x0038D5E9 File Offset: 0x0038B7E9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010848 RID: 67656 RVA: 0x0038D5FC File Offset: 0x0038B7FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010849 RID: 67657 RVA: 0x0038D626 File Offset: 0x0038B826
		public bool Equals(interval other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062C9 RID: 25289
		private ProgramNode _node;
	}
}
