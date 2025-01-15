using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AAE RID: 6830
	public struct DropRows : IProgramNodeBuilder, IEquatable<DropRows>
	{
		// Token: 0x170025BF RID: 9663
		// (get) Token: 0x0600E19C RID: 57756 RVA: 0x003000E2 File Offset: 0x002FE2E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E19D RID: 57757 RVA: 0x003000EA File Offset: 0x002FE2EA
		private DropRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E19E RID: 57758 RVA: 0x003000F3 File Offset: 0x002FE2F3
		public static DropRows CreateUnsafe(ProgramNode node)
		{
			return new DropRows(node);
		}

		// Token: 0x0600E19F RID: 57759 RVA: 0x003000FC File Offset: 0x002FE2FC
		public static DropRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DropRows)
			{
				return null;
			}
			return new DropRows?(DropRows.CreateUnsafe(node));
		}

		// Token: 0x0600E1A0 RID: 57760 RVA: 0x00300131 File Offset: 0x002FE331
		public DropRows(GrammarBuilders g, table value0, dropCondition value1)
		{
			this._node = g.Rule.DropRows.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600E1A1 RID: 57761 RVA: 0x00300157 File Offset: 0x002FE357
		public static implicit operator table(DropRows arg)
		{
			return table.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025C0 RID: 9664
		// (get) Token: 0x0600E1A2 RID: 57762 RVA: 0x00300165 File Offset: 0x002FE365
		public table table
		{
			get
			{
				return table.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170025C1 RID: 9665
		// (get) Token: 0x0600E1A3 RID: 57763 RVA: 0x00300179 File Offset: 0x002FE379
		public dropCondition dropCondition
		{
			get
			{
				return dropCondition.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600E1A4 RID: 57764 RVA: 0x0030018D File Offset: 0x002FE38D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E1A5 RID: 57765 RVA: 0x003001A0 File Offset: 0x002FE3A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E1A6 RID: 57766 RVA: 0x003001CA File Offset: 0x002FE3CA
		public bool Equals(DropRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400556D RID: 21869
		private ProgramNode _node;
	}
}
