using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E74 RID: 7796
	public struct SingleList : IProgramNodeBuilder, IEquatable<SingleList>
	{
		// Token: 0x17002BC3 RID: 11203
		// (get) Token: 0x060106F0 RID: 67312 RVA: 0x0038A6DA File Offset: 0x003888DA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060106F1 RID: 67313 RVA: 0x0038A6E2 File Offset: 0x003888E2
		private SingleList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060106F2 RID: 67314 RVA: 0x0038A6EB File Offset: 0x003888EB
		public static SingleList CreateUnsafe(ProgramNode node)
		{
			return new SingleList(node);
		}

		// Token: 0x060106F3 RID: 67315 RVA: 0x0038A6F4 File Offset: 0x003888F4
		public static SingleList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SingleList)
			{
				return null;
			}
			return new SingleList?(SingleList.CreateUnsafe(node));
		}

		// Token: 0x060106F4 RID: 67316 RVA: 0x0038A729 File Offset: 0x00388929
		public SingleList(GrammarBuilders g, newDsl value0)
		{
			this._node = g.Rule.SingleList.BuildASTNode(value0.Node);
		}

		// Token: 0x060106F5 RID: 67317 RVA: 0x0038A748 File Offset: 0x00388948
		public static implicit operator interval(SingleList arg)
		{
			return interval.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BC4 RID: 11204
		// (get) Token: 0x060106F6 RID: 67318 RVA: 0x0038A756 File Offset: 0x00388956
		public newDsl newDsl
		{
			get
			{
				return newDsl.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060106F7 RID: 67319 RVA: 0x0038A76A File Offset: 0x0038896A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060106F8 RID: 67320 RVA: 0x0038A780 File Offset: 0x00388980
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060106F9 RID: 67321 RVA: 0x0038A7AA File Offset: 0x003889AA
		public bool Equals(SingleList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062B3 RID: 25267
		private ProgramNode _node;
	}
}
