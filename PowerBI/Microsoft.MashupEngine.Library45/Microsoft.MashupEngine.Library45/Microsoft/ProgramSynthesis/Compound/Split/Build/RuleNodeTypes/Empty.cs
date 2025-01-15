using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000940 RID: 2368
	public struct Empty : IProgramNodeBuilder, IEquatable<Empty>
	{
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x000AD2E6 File Offset: 0x000AB4E6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x000AD2EE File Offset: 0x000AB4EE
		private Empty(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x000AD2F7 File Offset: 0x000AB4F7
		public static Empty CreateUnsafe(ProgramNode node)
		{
			return new Empty(node);
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x000AD300 File Offset: 0x000AB500
		public static Empty? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Empty)
			{
				return null;
			}
			return new Empty?(Empty.CreateUnsafe(node));
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000AD335 File Offset: 0x000AB535
		public Empty(GrammarBuilders g)
		{
			this._node = g.Rule.Empty.BuildASTNode(Array.Empty<ProgramNode>());
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000AD352 File Offset: 0x000AB552
		public static implicit operator columnSelectorList(Empty arg)
		{
			return columnSelectorList.CreateUnsafe(arg.Node);
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000AD360 File Offset: 0x000AB560
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000AD374 File Offset: 0x000AB574
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x000AD39E File Offset: 0x000AB59E
		public bool Equals(Empty other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A60 RID: 6752
		private ProgramNode _node;
	}
}
