using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C47 RID: 7239
	public struct rangeDateTime : IProgramNodeBuilder, IEquatable<rangeDateTime>
	{
		// Token: 0x170028DD RID: 10461
		// (get) Token: 0x0600F48A RID: 62602 RVA: 0x00345856 File Offset: 0x00343A56
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F48B RID: 62603 RVA: 0x0034585E File Offset: 0x00343A5E
		private rangeDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F48C RID: 62604 RVA: 0x00345867 File Offset: 0x00343A67
		public static rangeDateTime CreateUnsafe(ProgramNode node)
		{
			return new rangeDateTime(node);
		}

		// Token: 0x0600F48D RID: 62605 RVA: 0x00345870 File Offset: 0x00343A70
		public static rangeDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.rangeDateTime)
			{
				return null;
			}
			return new rangeDateTime?(rangeDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600F48E RID: 62606 RVA: 0x003458AA File Offset: 0x00343AAA
		public static rangeDateTime CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new rangeDateTime(new Hole(g.Symbol.rangeDateTime, holeId));
		}

		// Token: 0x0600F48F RID: 62607 RVA: 0x003458C2 File Offset: 0x00343AC2
		public RangeRoundDateTime Cast_RangeRoundDateTime()
		{
			return RangeRoundDateTime.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F490 RID: 62608 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_RangeRoundDateTime(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F491 RID: 62609 RVA: 0x003458CF File Offset: 0x00343ACF
		public bool Is_RangeRoundDateTime(GrammarBuilders g, out RangeRoundDateTime value)
		{
			value = RangeRoundDateTime.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F492 RID: 62610 RVA: 0x003458E3 File Offset: 0x00343AE3
		public RangeRoundDateTime? As_RangeRoundDateTime(GrammarBuilders g)
		{
			return new RangeRoundDateTime?(RangeRoundDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F493 RID: 62611 RVA: 0x003458F5 File Offset: 0x00343AF5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F494 RID: 62612 RVA: 0x00345908 File Offset: 0x00343B08
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F495 RID: 62613 RVA: 0x00345932 File Offset: 0x00343B32
		public bool Equals(rangeDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B36 RID: 23350
		private ProgramNode _node;
	}
}
