using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001045 RID: 4165
	public struct GEN_NthChildFilter : IProgramNodeBuilder, IEquatable<GEN_NthChildFilter>
	{
		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06007B64 RID: 31588 RVA: 0x001A333E File Offset: 0x001A153E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B65 RID: 31589 RVA: 0x001A3346 File Offset: 0x001A1546
		private GEN_NthChildFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B66 RID: 31590 RVA: 0x001A334F File Offset: 0x001A154F
		public static GEN_NthChildFilter CreateUnsafe(ProgramNode node)
		{
			return new GEN_NthChildFilter(node);
		}

		// Token: 0x06007B67 RID: 31591 RVA: 0x001A3358 File Offset: 0x001A1558
		public static GEN_NthChildFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.GEN_NthChildFilter)
			{
				return null;
			}
			return new GEN_NthChildFilter?(GEN_NthChildFilter.CreateUnsafe(node));
		}

		// Token: 0x06007B68 RID: 31592 RVA: 0x001A338D File Offset: 0x001A158D
		public GEN_NthChildFilter(GrammarBuilders g, obj value0, obj value1)
		{
			this._node = g.Rule.GEN_NthChildFilter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007B69 RID: 31593 RVA: 0x001A33B3 File Offset: 0x001A15B3
		public static implicit operator gen_NthChild(GEN_NthChildFilter arg)
		{
			return gen_NthChild.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06007B6A RID: 31594 RVA: 0x001A33C1 File Offset: 0x001A15C1
		public obj obj1
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06007B6B RID: 31595 RVA: 0x001A33D5 File Offset: 0x001A15D5
		public obj obj2
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007B6C RID: 31596 RVA: 0x001A33E9 File Offset: 0x001A15E9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x001A33FC File Offset: 0x001A15FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B6E RID: 31598 RVA: 0x001A3426 File Offset: 0x001A1626
		public bool Equals(GEN_NthChildFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400335E RID: 13150
		private ProgramNode _node;
	}
}
