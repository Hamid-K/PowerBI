using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200155F RID: 5471
	public struct NumberLessThan : IProgramNodeBuilder, IEquatable<NumberLessThan>
	{
		// Token: 0x17001F18 RID: 7960
		// (get) Token: 0x0600B2C1 RID: 45761 RVA: 0x00272626 File Offset: 0x00270826
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B2C2 RID: 45762 RVA: 0x0027262E File Offset: 0x0027082E
		private NumberLessThan(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B2C3 RID: 45763 RVA: 0x00272637 File Offset: 0x00270837
		public static NumberLessThan CreateUnsafe(ProgramNode node)
		{
			return new NumberLessThan(node);
		}

		// Token: 0x0600B2C4 RID: 45764 RVA: 0x00272640 File Offset: 0x00270840
		public static NumberLessThan? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NumberLessThan)
			{
				return null;
			}
			return new NumberLessThan?(NumberLessThan.CreateUnsafe(node));
		}

		// Token: 0x0600B2C5 RID: 45765 RVA: 0x00272675 File Offset: 0x00270875
		public NumberLessThan(GrammarBuilders g, row value0, columnName value1, numberLessThanValue value2)
		{
			this._node = g.Rule.NumberLessThan.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B2C6 RID: 45766 RVA: 0x002726A2 File Offset: 0x002708A2
		public static implicit operator condition(NumberLessThan arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F19 RID: 7961
		// (get) Token: 0x0600B2C7 RID: 45767 RVA: 0x002726B0 File Offset: 0x002708B0
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F1A RID: 7962
		// (get) Token: 0x0600B2C8 RID: 45768 RVA: 0x002726C4 File Offset: 0x002708C4
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F1B RID: 7963
		// (get) Token: 0x0600B2C9 RID: 45769 RVA: 0x002726D8 File Offset: 0x002708D8
		public numberLessThanValue numberLessThanValue
		{
			get
			{
				return numberLessThanValue.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B2CA RID: 45770 RVA: 0x002726EC File Offset: 0x002708EC
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B2CB RID: 45771 RVA: 0x00272700 File Offset: 0x00270900
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B2CC RID: 45772 RVA: 0x0027272A File Offset: 0x0027092A
		public bool Equals(NumberLessThan other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400460D RID: 17933
		private ProgramNode _node;
	}
}
