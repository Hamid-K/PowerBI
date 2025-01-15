using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x020011DF RID: 4575
	public struct labelled_multi_result_nil_label : IProgramNodeBuilder, IEquatable<labelled_multi_result_nil_label>
	{
		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x06008962 RID: 35170 RVA: 0x001CF136 File Offset: 0x001CD336
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008963 RID: 35171 RVA: 0x001CF13E File Offset: 0x001CD33E
		private labelled_multi_result_nil_label(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008964 RID: 35172 RVA: 0x001CF147 File Offset: 0x001CD347
		public static labelled_multi_result_nil_label CreateUnsafe(ProgramNode node)
		{
			return new labelled_multi_result_nil_label(node);
		}

		// Token: 0x06008965 RID: 35173 RVA: 0x001CF150 File Offset: 0x001CD350
		public static labelled_multi_result_nil_label? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.labelled_multi_result_nil_label)
			{
				return null;
			}
			return new labelled_multi_result_nil_label?(labelled_multi_result_nil_label.CreateUnsafe(node));
		}

		// Token: 0x06008966 RID: 35174 RVA: 0x001CF185 File Offset: 0x001CD385
		public labelled_multi_result_nil_label(GrammarBuilders g, nil_label value0)
		{
			this._node = g.UnnamedConversion.labelled_multi_result_nil_label.BuildASTNode(value0.Node);
		}

		// Token: 0x06008967 RID: 35175 RVA: 0x001CF1A4 File Offset: 0x001CD3A4
		public static implicit operator labelled_multi_result(labelled_multi_result_nil_label arg)
		{
			return labelled_multi_result.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x06008968 RID: 35176 RVA: 0x001CF1B2 File Offset: 0x001CD3B2
		public nil_label nil_label
		{
			get
			{
				return nil_label.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06008969 RID: 35177 RVA: 0x001CF1C6 File Offset: 0x001CD3C6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600896A RID: 35178 RVA: 0x001CF1DC File Offset: 0x001CD3DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600896B RID: 35179 RVA: 0x001CF206 File Offset: 0x001CD406
		public bool Equals(labelled_multi_result_nil_label other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003893 RID: 14483
		private ProgramNode _node;
	}
}
