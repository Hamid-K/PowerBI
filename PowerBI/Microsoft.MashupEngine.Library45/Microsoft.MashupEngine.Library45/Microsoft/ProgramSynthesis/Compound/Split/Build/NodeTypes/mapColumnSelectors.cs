using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000961 RID: 2401
	public struct mapColumnSelectors : IProgramNodeBuilder, IEquatable<mapColumnSelectors>
	{
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06003884 RID: 14468 RVA: 0x000AFADE File Offset: 0x000ADCDE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000AFAE6 File Offset: 0x000ADCE6
		private mapColumnSelectors(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000AFAEF File Offset: 0x000ADCEF
		public static mapColumnSelectors CreateUnsafe(ProgramNode node)
		{
			return new mapColumnSelectors(node);
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000AFAF8 File Offset: 0x000ADCF8
		public static mapColumnSelectors? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.mapColumnSelectors)
			{
				return null;
			}
			return new mapColumnSelectors?(mapColumnSelectors.CreateUnsafe(node));
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000AFB32 File Offset: 0x000ADD32
		public static mapColumnSelectors CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new mapColumnSelectors(new Hole(g.Symbol.mapColumnSelectors, holeId));
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000AFB4A File Offset: 0x000ADD4A
		public MapColumnSelector Cast_MapColumnSelector()
		{
			return MapColumnSelector.CreateUnsafe(this.Node);
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_MapColumnSelector(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000AFB57 File Offset: 0x000ADD57
		public bool Is_MapColumnSelector(GrammarBuilders g, out MapColumnSelector value)
		{
			value = MapColumnSelector.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000AFB6B File Offset: 0x000ADD6B
		public MapColumnSelector? As_MapColumnSelector(GrammarBuilders g)
		{
			return new MapColumnSelector?(MapColumnSelector.CreateUnsafe(this.Node));
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000AFB7D File Offset: 0x000ADD7D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000AFB90 File Offset: 0x000ADD90
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000AFBBA File Offset: 0x000ADDBA
		public bool Equals(mapColumnSelectors other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A81 RID: 6785
		private ProgramNode _node;
	}
}
