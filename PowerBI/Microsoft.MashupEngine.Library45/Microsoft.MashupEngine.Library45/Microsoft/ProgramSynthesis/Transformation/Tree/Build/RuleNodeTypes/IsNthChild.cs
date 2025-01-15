using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E63 RID: 7779
	public struct IsNthChild : IProgramNodeBuilder, IEquatable<IsNthChild>
	{
		// Token: 0x17002B8D RID: 11149
		// (get) Token: 0x06010632 RID: 67122 RVA: 0x00389596 File Offset: 0x00387796
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010633 RID: 67123 RVA: 0x0038959E File Offset: 0x0038779E
		private IsNthChild(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010634 RID: 67124 RVA: 0x003895A7 File Offset: 0x003877A7
		public static IsNthChild CreateUnsafe(ProgramNode node)
		{
			return new IsNthChild(node);
		}

		// Token: 0x06010635 RID: 67125 RVA: 0x003895B0 File Offset: 0x003877B0
		public static IsNthChild? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IsNthChild)
			{
				return null;
			}
			return new IsNthChild?(IsNthChild.CreateUnsafe(node));
		}

		// Token: 0x06010636 RID: 67126 RVA: 0x003895E5 File Offset: 0x003877E5
		public IsNthChild(GrammarBuilders g, x value0, k value1)
		{
			this._node = g.Rule.IsNthChild.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06010637 RID: 67127 RVA: 0x0038960B File Offset: 0x0038780B
		public static implicit operator pred(IsNthChild arg)
		{
			return pred.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B8E RID: 11150
		// (get) Token: 0x06010638 RID: 67128 RVA: 0x00389619 File Offset: 0x00387819
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002B8F RID: 11151
		// (get) Token: 0x06010639 RID: 67129 RVA: 0x0038962D File Offset: 0x0038782D
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0601063A RID: 67130 RVA: 0x00389641 File Offset: 0x00387841
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601063B RID: 67131 RVA: 0x00389654 File Offset: 0x00387854
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601063C RID: 67132 RVA: 0x0038967E File Offset: 0x0038787E
		public bool Equals(IsNthChild other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062A2 RID: 25250
		private ProgramNode _node;
	}
}
