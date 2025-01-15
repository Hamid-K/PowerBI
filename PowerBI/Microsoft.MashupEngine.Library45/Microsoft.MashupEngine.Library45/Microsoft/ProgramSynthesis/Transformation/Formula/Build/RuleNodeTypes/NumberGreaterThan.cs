using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200155E RID: 5470
	public struct NumberGreaterThan : IProgramNodeBuilder, IEquatable<NumberGreaterThan>
	{
		// Token: 0x17001F14 RID: 7956
		// (get) Token: 0x0600B2B5 RID: 45749 RVA: 0x0027250E File Offset: 0x0027070E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B2B6 RID: 45750 RVA: 0x00272516 File Offset: 0x00270716
		private NumberGreaterThan(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B2B7 RID: 45751 RVA: 0x0027251F File Offset: 0x0027071F
		public static NumberGreaterThan CreateUnsafe(ProgramNode node)
		{
			return new NumberGreaterThan(node);
		}

		// Token: 0x0600B2B8 RID: 45752 RVA: 0x00272528 File Offset: 0x00270728
		public static NumberGreaterThan? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NumberGreaterThan)
			{
				return null;
			}
			return new NumberGreaterThan?(NumberGreaterThan.CreateUnsafe(node));
		}

		// Token: 0x0600B2B9 RID: 45753 RVA: 0x0027255D File Offset: 0x0027075D
		public NumberGreaterThan(GrammarBuilders g, row value0, columnName value1, numberGreaterThanValue value2)
		{
			this._node = g.Rule.NumberGreaterThan.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B2BA RID: 45754 RVA: 0x0027258A File Offset: 0x0027078A
		public static implicit operator condition(NumberGreaterThan arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F15 RID: 7957
		// (get) Token: 0x0600B2BB RID: 45755 RVA: 0x00272598 File Offset: 0x00270798
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F16 RID: 7958
		// (get) Token: 0x0600B2BC RID: 45756 RVA: 0x002725AC File Offset: 0x002707AC
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F17 RID: 7959
		// (get) Token: 0x0600B2BD RID: 45757 RVA: 0x002725C0 File Offset: 0x002707C0
		public numberGreaterThanValue numberGreaterThanValue
		{
			get
			{
				return numberGreaterThanValue.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B2BE RID: 45758 RVA: 0x002725D4 File Offset: 0x002707D4
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B2BF RID: 45759 RVA: 0x002725E8 File Offset: 0x002707E8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B2C0 RID: 45760 RVA: 0x00272612 File Offset: 0x00270812
		public bool Equals(NumberGreaterThan other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400460C RID: 17932
		private ProgramNode _node;
	}
}
