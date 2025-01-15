using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A45 RID: 2629
	public struct StartsWith : IProgramNodeBuilder, IEquatable<StartsWith>
	{
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060040A1 RID: 16545 RVA: 0x000CB04E File Offset: 0x000C924E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x000CB056 File Offset: 0x000C9256
		private StartsWith(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x000CB05F File Offset: 0x000C925F
		public static StartsWith CreateUnsafe(ProgramNode node)
		{
			return new StartsWith(node);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x000CB068 File Offset: 0x000C9268
		public static StartsWith? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.StartsWith)
			{
				return null;
			}
			return new StartsWith?(StartsWith.CreateUnsafe(node));
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x000CB09D File Offset: 0x000C929D
		public StartsWith(GrammarBuilders g, s value0, r value1)
		{
			this._node = g.Rule.StartsWith.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x000CB0C3 File Offset: 0x000C92C3
		public static implicit operator match(StartsWith arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060040A7 RID: 16551 RVA: 0x000CB0D1 File Offset: 0x000C92D1
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x000CB0E5 File Offset: 0x000C92E5
		public r r
		{
			get
			{
				return r.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x000CB0F9 File Offset: 0x000C92F9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x000CB10C File Offset: 0x000C930C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x000CB136 File Offset: 0x000C9336
		public bool Equals(StartsWith other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D80 RID: 7552
		private ProgramNode _node;
	}
}
