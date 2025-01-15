using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011F6 RID: 4598
	public struct labelled_multi_result : IProgramNodeBuilder, IEquatable<labelled_multi_result>
	{
		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x06008A7A RID: 35450 RVA: 0x001D112A File Offset: 0x001CF32A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008A7B RID: 35451 RVA: 0x001D1132 File Offset: 0x001CF332
		private labelled_multi_result(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008A7C RID: 35452 RVA: 0x001D113B File Offset: 0x001CF33B
		public static labelled_multi_result CreateUnsafe(ProgramNode node)
		{
			return new labelled_multi_result(node);
		}

		// Token: 0x06008A7D RID: 35453 RVA: 0x001D1144 File Offset: 0x001CF344
		public static labelled_multi_result? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.labelled_multi_result)
			{
				return null;
			}
			return new labelled_multi_result?(labelled_multi_result.CreateUnsafe(node));
		}

		// Token: 0x06008A7E RID: 35454 RVA: 0x001D117E File Offset: 0x001CF37E
		public static labelled_multi_result CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new labelled_multi_result(new Hole(g.Symbol.labelled_multi_result, holeId));
		}

		// Token: 0x06008A7F RID: 35455 RVA: 0x001D1196 File Offset: 0x001CF396
		public bool Is_labelled_multi_result_nil_label(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.labelled_multi_result_nil_label;
		}

		// Token: 0x06008A80 RID: 35456 RVA: 0x001D11B0 File Offset: 0x001CF3B0
		public bool Is_labelled_multi_result_nil_label(GrammarBuilders g, out labelled_multi_result_nil_label value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.labelled_multi_result_nil_label)
			{
				value = labelled_multi_result_nil_label.CreateUnsafe(this.Node);
				return true;
			}
			value = default(labelled_multi_result_nil_label);
			return false;
		}

		// Token: 0x06008A81 RID: 35457 RVA: 0x001D11E8 File Offset: 0x001CF3E8
		public labelled_multi_result_nil_label? As_labelled_multi_result_nil_label(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.labelled_multi_result_nil_label)
			{
				return null;
			}
			return new labelled_multi_result_nil_label?(labelled_multi_result_nil_label.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A82 RID: 35458 RVA: 0x001D1228 File Offset: 0x001CF428
		public labelled_multi_result_nil_label Cast_labelled_multi_result_nil_label(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.labelled_multi_result_nil_label)
			{
				return labelled_multi_result_nil_label.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_labelled_multi_result_nil_label is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008A83 RID: 35459 RVA: 0x001D127D File Offset: 0x001CF47D
		public bool Is_LabelledMatchColumns(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LabelledMatchColumns;
		}

		// Token: 0x06008A84 RID: 35460 RVA: 0x001D1297 File Offset: 0x001CF497
		public bool Is_LabelledMatchColumns(GrammarBuilders g, out LabelledMatchColumns value)
		{
			if (this.Node.GrammarRule == g.Rule.LabelledMatchColumns)
			{
				value = LabelledMatchColumns.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LabelledMatchColumns);
			return false;
		}

		// Token: 0x06008A85 RID: 35461 RVA: 0x001D12CC File Offset: 0x001CF4CC
		public LabelledMatchColumns? As_LabelledMatchColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LabelledMatchColumns)
			{
				return null;
			}
			return new LabelledMatchColumns?(LabelledMatchColumns.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A86 RID: 35462 RVA: 0x001D130C File Offset: 0x001CF50C
		public LabelledMatchColumns Cast_LabelledMatchColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LabelledMatchColumns)
			{
				return LabelledMatchColumns.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LabelledMatchColumns is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008A87 RID: 35463 RVA: 0x001D1364 File Offset: 0x001CF564
		public T Switch<T>(GrammarBuilders g, Func<labelled_multi_result_nil_label, T> func0, Func<LabelledMatchColumns, T> func1)
		{
			labelled_multi_result_nil_label labelled_multi_result_nil_label;
			if (this.Is_labelled_multi_result_nil_label(g, out labelled_multi_result_nil_label))
			{
				return func0(labelled_multi_result_nil_label);
			}
			LabelledMatchColumns labelledMatchColumns;
			if (this.Is_LabelledMatchColumns(g, out labelledMatchColumns))
			{
				return func1(labelledMatchColumns);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol labelled_multi_result");
		}

		// Token: 0x06008A88 RID: 35464 RVA: 0x001D13BC File Offset: 0x001CF5BC
		public void Switch(GrammarBuilders g, Action<labelled_multi_result_nil_label> func0, Action<LabelledMatchColumns> func1)
		{
			labelled_multi_result_nil_label labelled_multi_result_nil_label;
			if (this.Is_labelled_multi_result_nil_label(g, out labelled_multi_result_nil_label))
			{
				func0(labelled_multi_result_nil_label);
				return;
			}
			LabelledMatchColumns labelledMatchColumns;
			if (this.Is_LabelledMatchColumns(g, out labelledMatchColumns))
			{
				func1(labelledMatchColumns);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol labelled_multi_result");
		}

		// Token: 0x06008A89 RID: 35465 RVA: 0x001D1413 File Offset: 0x001CF613
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008A8A RID: 35466 RVA: 0x001D1428 File Offset: 0x001CF628
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008A8B RID: 35467 RVA: 0x001D1452 File Offset: 0x001CF652
		public bool Equals(labelled_multi_result other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038AA RID: 14506
		private ProgramNode _node;
	}
}
