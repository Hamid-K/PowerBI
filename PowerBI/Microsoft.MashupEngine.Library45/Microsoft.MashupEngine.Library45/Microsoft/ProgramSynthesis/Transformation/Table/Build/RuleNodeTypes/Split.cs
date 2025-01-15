using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AB4 RID: 6836
	public struct Split : IProgramNodeBuilder, IEquatable<Split>
	{
		// Token: 0x170025D2 RID: 9682
		// (get) Token: 0x0600E1DF RID: 57823 RVA: 0x003006FE File Offset: 0x002FE8FE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E1E0 RID: 57824 RVA: 0x00300706 File Offset: 0x002FE906
		private Split(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E1E1 RID: 57825 RVA: 0x0030070F File Offset: 0x002FE90F
		public static Split CreateUnsafe(ProgramNode node)
		{
			return new Split(node);
		}

		// Token: 0x0600E1E2 RID: 57826 RVA: 0x00300718 File Offset: 0x002FE918
		public static Split? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Split)
			{
				return null;
			}
			return new Split?(Split.CreateUnsafe(node));
		}

		// Token: 0x0600E1E3 RID: 57827 RVA: 0x0030074D File Offset: 0x002FE94D
		public Split(GrammarBuilders g, regionSplit value0)
		{
			this._node = g.Rule.Split.BuildASTNode(value0.Node);
		}

		// Token: 0x0600E1E4 RID: 57828 RVA: 0x0030076C File Offset: 0x002FE96C
		public static implicit operator splitCell(Split arg)
		{
			return splitCell.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025D3 RID: 9683
		// (get) Token: 0x0600E1E5 RID: 57829 RVA: 0x0030077A File Offset: 0x002FE97A
		public regionSplit regionSplit
		{
			get
			{
				return regionSplit.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600E1E6 RID: 57830 RVA: 0x0030078E File Offset: 0x002FE98E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E1E7 RID: 57831 RVA: 0x003007A4 File Offset: 0x002FE9A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E1E8 RID: 57832 RVA: 0x003007CE File Offset: 0x002FE9CE
		public bool Equals(Split other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005573 RID: 21875
		private ProgramNode _node;
	}
}
