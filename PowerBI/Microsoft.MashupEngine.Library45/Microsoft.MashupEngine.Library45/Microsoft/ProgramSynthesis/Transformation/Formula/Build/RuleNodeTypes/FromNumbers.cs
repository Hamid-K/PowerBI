using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001591 RID: 5521
	public struct FromNumbers : IProgramNodeBuilder, IEquatable<FromNumbers>
	{
		// Token: 0x17001FA9 RID: 8105
		// (get) Token: 0x0600B4E2 RID: 46306 RVA: 0x00275756 File Offset: 0x00273956
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B4E3 RID: 46307 RVA: 0x0027575E File Offset: 0x0027395E
		private FromNumbers(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B4E4 RID: 46308 RVA: 0x00275767 File Offset: 0x00273967
		public static FromNumbers CreateUnsafe(ProgramNode node)
		{
			return new FromNumbers(node);
		}

		// Token: 0x0600B4E5 RID: 46309 RVA: 0x00275770 File Offset: 0x00273970
		public static FromNumbers? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FromNumbers)
			{
				return null;
			}
			return new FromNumbers?(FromNumbers.CreateUnsafe(node));
		}

		// Token: 0x0600B4E6 RID: 46310 RVA: 0x002757A5 File Offset: 0x002739A5
		public FromNumbers(GrammarBuilders g, row value0, columnNames value1)
		{
			this._node = g.Rule.FromNumbers.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B4E7 RID: 46311 RVA: 0x002757CB File Offset: 0x002739CB
		public static implicit operator fromNumbers(FromNumbers arg)
		{
			return fromNumbers.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001FAA RID: 8106
		// (get) Token: 0x0600B4E8 RID: 46312 RVA: 0x002757D9 File Offset: 0x002739D9
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001FAB RID: 8107
		// (get) Token: 0x0600B4E9 RID: 46313 RVA: 0x002757ED File Offset: 0x002739ED
		public columnNames columnNames
		{
			get
			{
				return columnNames.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B4EA RID: 46314 RVA: 0x00275801 File Offset: 0x00273A01
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B4EB RID: 46315 RVA: 0x00275814 File Offset: 0x00273A14
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B4EC RID: 46316 RVA: 0x0027583E File Offset: 0x00273A3E
		public bool Equals(FromNumbers other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400463F RID: 17983
		private ProgramNode _node;
	}
}
