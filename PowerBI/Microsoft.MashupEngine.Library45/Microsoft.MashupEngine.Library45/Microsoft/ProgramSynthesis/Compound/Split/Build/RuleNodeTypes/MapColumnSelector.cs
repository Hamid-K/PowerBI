using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000954 RID: 2388
	public struct MapColumnSelector : IProgramNodeBuilder, IEquatable<MapColumnSelector>
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060037E2 RID: 14306 RVA: 0x000AE866 File Offset: 0x000ACA66
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000AE86E File Offset: 0x000ACA6E
		private MapColumnSelector(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000AE877 File Offset: 0x000ACA77
		public static MapColumnSelector CreateUnsafe(ProgramNode node)
		{
			return new MapColumnSelector(node);
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000AE880 File Offset: 0x000ACA80
		public static MapColumnSelector? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MapColumnSelector)
			{
				return null;
			}
			return new MapColumnSelector?(MapColumnSelector.CreateUnsafe(node));
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000AE8B5 File Offset: 0x000ACAB5
		public MapColumnSelector(GrammarBuilders g, columnSelectorList value0, rowRecords value1)
		{
			this._node = g.Rule.MapColumnSelector.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000AE8E7 File Offset: 0x000ACAE7
		public static implicit operator mapColumnSelectors(MapColumnSelector arg)
		{
			return mapColumnSelectors.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060037E8 RID: 14312 RVA: 0x000AE8F5 File Offset: 0x000ACAF5
		public columnSelectorList columnSelectorList
		{
			get
			{
				return columnSelectorList.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060037E9 RID: 14313 RVA: 0x000AE910 File Offset: 0x000ACB10
		public rowRecords rowRecords
		{
			get
			{
				return rowRecords.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000AE924 File Offset: 0x000ACB24
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x000AE938 File Offset: 0x000ACB38
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x000AE962 File Offset: 0x000ACB62
		public bool Equals(MapColumnSelector other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A74 RID: 6772
		private ProgramNode _node;
	}
}
