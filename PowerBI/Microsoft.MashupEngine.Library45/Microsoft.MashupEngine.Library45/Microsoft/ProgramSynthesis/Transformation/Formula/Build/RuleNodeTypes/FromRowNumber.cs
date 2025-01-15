using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001590 RID: 5520
	public struct FromRowNumber : IProgramNodeBuilder, IEquatable<FromRowNumber>
	{
		// Token: 0x17001FA7 RID: 8103
		// (get) Token: 0x0600B4D8 RID: 46296 RVA: 0x00275672 File Offset: 0x00273872
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B4D9 RID: 46297 RVA: 0x0027567A File Offset: 0x0027387A
		private FromRowNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B4DA RID: 46298 RVA: 0x00275683 File Offset: 0x00273883
		public static FromRowNumber CreateUnsafe(ProgramNode node)
		{
			return new FromRowNumber(node);
		}

		// Token: 0x0600B4DB RID: 46299 RVA: 0x0027568C File Offset: 0x0027388C
		public static FromRowNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FromRowNumber)
			{
				return null;
			}
			return new FromRowNumber?(FromRowNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B4DC RID: 46300 RVA: 0x002756C1 File Offset: 0x002738C1
		public FromRowNumber(GrammarBuilders g, row value0)
		{
			this._node = g.Rule.FromRowNumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B4DD RID: 46301 RVA: 0x002756E0 File Offset: 0x002738E0
		public static implicit operator fromRowNumber(FromRowNumber arg)
		{
			return fromRowNumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001FA8 RID: 8104
		// (get) Token: 0x0600B4DE RID: 46302 RVA: 0x002756EE File Offset: 0x002738EE
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B4DF RID: 46303 RVA: 0x00275702 File Offset: 0x00273902
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B4E0 RID: 46304 RVA: 0x00275718 File Offset: 0x00273918
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B4E1 RID: 46305 RVA: 0x00275742 File Offset: 0x00273942
		public bool Equals(FromRowNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400463E RID: 17982
		private ProgramNode _node;
	}
}
