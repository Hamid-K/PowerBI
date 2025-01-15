using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C44 RID: 7236
	public struct rangeNumber : IProgramNodeBuilder, IEquatable<rangeNumber>
	{
		// Token: 0x170028DA RID: 10458
		// (get) Token: 0x0600F45A RID: 62554 RVA: 0x003450EE File Offset: 0x003432EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F45B RID: 62555 RVA: 0x003450F6 File Offset: 0x003432F6
		private rangeNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F45C RID: 62556 RVA: 0x003450FF File Offset: 0x003432FF
		public static rangeNumber CreateUnsafe(ProgramNode node)
		{
			return new rangeNumber(node);
		}

		// Token: 0x0600F45D RID: 62557 RVA: 0x00345108 File Offset: 0x00343308
		public static rangeNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.rangeNumber)
			{
				return null;
			}
			return new rangeNumber?(rangeNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F45E RID: 62558 RVA: 0x00345142 File Offset: 0x00343342
		public static rangeNumber CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new rangeNumber(new Hole(g.Symbol.rangeNumber, holeId));
		}

		// Token: 0x0600F45F RID: 62559 RVA: 0x0034515A File Offset: 0x0034335A
		public RangeRoundNumber Cast_RangeRoundNumber()
		{
			return RangeRoundNumber.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F460 RID: 62560 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_RangeRoundNumber(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F461 RID: 62561 RVA: 0x00345167 File Offset: 0x00343367
		public bool Is_RangeRoundNumber(GrammarBuilders g, out RangeRoundNumber value)
		{
			value = RangeRoundNumber.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F462 RID: 62562 RVA: 0x0034517B File Offset: 0x0034337B
		public RangeRoundNumber? As_RangeRoundNumber(GrammarBuilders g)
		{
			return new RangeRoundNumber?(RangeRoundNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F463 RID: 62563 RVA: 0x0034518D File Offset: 0x0034338D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F464 RID: 62564 RVA: 0x003451A0 File Offset: 0x003433A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F465 RID: 62565 RVA: 0x003451CA File Offset: 0x003433CA
		public bool Equals(rangeNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B33 RID: 23347
		private ProgramNode _node;
	}
}
