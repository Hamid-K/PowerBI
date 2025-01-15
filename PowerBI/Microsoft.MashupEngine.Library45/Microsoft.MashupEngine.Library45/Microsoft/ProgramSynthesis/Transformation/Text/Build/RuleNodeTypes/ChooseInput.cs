using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001BFD RID: 7165
	public struct ChooseInput : IProgramNodeBuilder, IEquatable<ChooseInput>
	{
		// Token: 0x1700281F RID: 10271
		// (get) Token: 0x0600F0E6 RID: 61670 RVA: 0x0033ECBE File Offset: 0x0033CEBE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F0E7 RID: 61671 RVA: 0x0033ECC6 File Offset: 0x0033CEC6
		private ChooseInput(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F0E8 RID: 61672 RVA: 0x0033ECCF File Offset: 0x0033CECF
		public static ChooseInput CreateUnsafe(ProgramNode node)
		{
			return new ChooseInput(node);
		}

		// Token: 0x0600F0E9 RID: 61673 RVA: 0x0033ECD8 File Offset: 0x0033CED8
		public static ChooseInput? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ChooseInput)
			{
				return null;
			}
			return new ChooseInput?(ChooseInput.CreateUnsafe(node));
		}

		// Token: 0x0600F0EA RID: 61674 RVA: 0x0033ED0D File Offset: 0x0033CF0D
		public ChooseInput(GrammarBuilders g, vs value0, columnName value1)
		{
			this._node = g.Rule.ChooseInput.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F0EB RID: 61675 RVA: 0x0033ED33 File Offset: 0x0033CF33
		public static implicit operator v(ChooseInput arg)
		{
			return v.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002820 RID: 10272
		// (get) Token: 0x0600F0EC RID: 61676 RVA: 0x0033ED41 File Offset: 0x0033CF41
		public vs vs
		{
			get
			{
				return vs.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002821 RID: 10273
		// (get) Token: 0x0600F0ED RID: 61677 RVA: 0x0033ED55 File Offset: 0x0033CF55
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F0EE RID: 61678 RVA: 0x0033ED69 File Offset: 0x0033CF69
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F0EF RID: 61679 RVA: 0x0033ED7C File Offset: 0x0033CF7C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F0F0 RID: 61680 RVA: 0x0033EDA6 File Offset: 0x0033CFA6
		public bool Equals(ChooseInput other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AEC RID: 23276
		private ProgramNode _node;
	}
}
