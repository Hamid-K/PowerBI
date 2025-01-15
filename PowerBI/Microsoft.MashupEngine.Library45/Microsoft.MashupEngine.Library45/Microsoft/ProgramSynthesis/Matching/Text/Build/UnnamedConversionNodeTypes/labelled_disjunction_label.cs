using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x020011DE RID: 4574
	public struct labelled_disjunction_label : IProgramNodeBuilder, IEquatable<labelled_disjunction_label>
	{
		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x06008958 RID: 35160 RVA: 0x001CF053 File Offset: 0x001CD253
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008959 RID: 35161 RVA: 0x001CF05B File Offset: 0x001CD25B
		private labelled_disjunction_label(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600895A RID: 35162 RVA: 0x001CF064 File Offset: 0x001CD264
		public static labelled_disjunction_label CreateUnsafe(ProgramNode node)
		{
			return new labelled_disjunction_label(node);
		}

		// Token: 0x0600895B RID: 35163 RVA: 0x001CF06C File Offset: 0x001CD26C
		public static labelled_disjunction_label? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.labelled_disjunction_label)
			{
				return null;
			}
			return new labelled_disjunction_label?(labelled_disjunction_label.CreateUnsafe(node));
		}

		// Token: 0x0600895C RID: 35164 RVA: 0x001CF0A1 File Offset: 0x001CD2A1
		public labelled_disjunction_label(GrammarBuilders g, label value0)
		{
			this._node = g.UnnamedConversion.labelled_disjunction_label.BuildASTNode(value0.Node);
		}

		// Token: 0x0600895D RID: 35165 RVA: 0x001CF0C0 File Offset: 0x001CD2C0
		public static implicit operator labelled_disjunction(labelled_disjunction_label arg)
		{
			return labelled_disjunction.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x0600895E RID: 35166 RVA: 0x001CF0CE File Offset: 0x001CD2CE
		public label label
		{
			get
			{
				return label.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600895F RID: 35167 RVA: 0x001CF0E2 File Offset: 0x001CD2E2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008960 RID: 35168 RVA: 0x001CF0F8 File Offset: 0x001CD2F8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008961 RID: 35169 RVA: 0x001CF122 File Offset: 0x001CD322
		public bool Equals(labelled_disjunction_label other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003892 RID: 14482
		private ProgramNode _node;
	}
}
