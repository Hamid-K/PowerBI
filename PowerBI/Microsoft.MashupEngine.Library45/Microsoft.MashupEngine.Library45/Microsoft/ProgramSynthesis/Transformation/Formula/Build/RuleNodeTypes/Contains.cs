using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001557 RID: 5463
	public struct Contains : IProgramNodeBuilder, IEquatable<Contains>
	{
		// Token: 0x17001EFB RID: 7931
		// (get) Token: 0x0600B264 RID: 45668 RVA: 0x00271D9A File Offset: 0x0026FF9A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B265 RID: 45669 RVA: 0x00271DA2 File Offset: 0x0026FFA2
		private Contains(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B266 RID: 45670 RVA: 0x00271DAB File Offset: 0x0026FFAB
		public static Contains CreateUnsafe(ProgramNode node)
		{
			return new Contains(node);
		}

		// Token: 0x0600B267 RID: 45671 RVA: 0x00271DB4 File Offset: 0x0026FFB4
		public static Contains? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Contains)
			{
				return null;
			}
			return new Contains?(Contains.CreateUnsafe(node));
		}

		// Token: 0x0600B268 RID: 45672 RVA: 0x00271DEC File Offset: 0x0026FFEC
		public Contains(GrammarBuilders g, row value0, columnName value1, containsFindText value2, containsCount value3)
		{
			this._node = g.Rule.Contains.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node });
		}

		// Token: 0x0600B269 RID: 45673 RVA: 0x00271E3D File Offset: 0x0027003D
		public static implicit operator condition(Contains arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EFC RID: 7932
		// (get) Token: 0x0600B26A RID: 45674 RVA: 0x00271E4B File Offset: 0x0027004B
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001EFD RID: 7933
		// (get) Token: 0x0600B26B RID: 45675 RVA: 0x00271E5F File Offset: 0x0027005F
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001EFE RID: 7934
		// (get) Token: 0x0600B26C RID: 45676 RVA: 0x00271E73 File Offset: 0x00270073
		public containsFindText containsFindText
		{
			get
			{
				return containsFindText.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x17001EFF RID: 7935
		// (get) Token: 0x0600B26D RID: 45677 RVA: 0x00271E87 File Offset: 0x00270087
		public containsCount containsCount
		{
			get
			{
				return containsCount.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x0600B26E RID: 45678 RVA: 0x00271E9B File Offset: 0x0027009B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B26F RID: 45679 RVA: 0x00271EB0 File Offset: 0x002700B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B270 RID: 45680 RVA: 0x00271EDA File Offset: 0x002700DA
		public bool Equals(Contains other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004605 RID: 17925
		private ProgramNode _node;
	}
}
