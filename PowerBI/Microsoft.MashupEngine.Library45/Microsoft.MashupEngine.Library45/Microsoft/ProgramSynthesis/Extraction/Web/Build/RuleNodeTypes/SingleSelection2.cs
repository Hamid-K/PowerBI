using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001018 RID: 4120
	public struct SingleSelection2 : IProgramNodeBuilder, IEquatable<SingleSelection2>
	{
		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06007983 RID: 31107 RVA: 0x001A0832 File Offset: 0x0019EA32
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007984 RID: 31108 RVA: 0x001A083A File Offset: 0x0019EA3A
		private SingleSelection2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007985 RID: 31109 RVA: 0x001A0843 File Offset: 0x0019EA43
		public static SingleSelection2 CreateUnsafe(ProgramNode node)
		{
			return new SingleSelection2(node);
		}

		// Token: 0x06007986 RID: 31110 RVA: 0x001A084C File Offset: 0x0019EA4C
		public static SingleSelection2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SingleSelection2)
			{
				return null;
			}
			return new SingleSelection2?(SingleSelection2.CreateUnsafe(node));
		}

		// Token: 0x06007987 RID: 31111 RVA: 0x001A0881 File Offset: 0x0019EA81
		public SingleSelection2(GrammarBuilders g, filterSelection2 value0)
		{
			this._node = g.Rule.SingleSelection2.BuildASTNode(value0.Node);
		}

		// Token: 0x06007988 RID: 31112 RVA: 0x001A08A0 File Offset: 0x0019EAA0
		public static implicit operator selection3(SingleSelection2 arg)
		{
			return selection3.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x06007989 RID: 31113 RVA: 0x001A08AE File Offset: 0x0019EAAE
		public filterSelection2 filterSelection2
		{
			get
			{
				return filterSelection2.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600798A RID: 31114 RVA: 0x001A08C2 File Offset: 0x0019EAC2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600798B RID: 31115 RVA: 0x001A08D8 File Offset: 0x0019EAD8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600798C RID: 31116 RVA: 0x001A0902 File Offset: 0x0019EB02
		public bool Equals(SingleSelection2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003331 RID: 13105
		private ProgramNode _node;
	}
}
