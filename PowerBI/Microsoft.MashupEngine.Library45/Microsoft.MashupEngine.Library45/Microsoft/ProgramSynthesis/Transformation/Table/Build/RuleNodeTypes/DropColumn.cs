using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AAD RID: 6829
	public struct DropColumn : IProgramNodeBuilder, IEquatable<DropColumn>
	{
		// Token: 0x170025BB RID: 9659
		// (get) Token: 0x0600E190 RID: 57744 RVA: 0x002FFFCA File Offset: 0x002FE1CA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E191 RID: 57745 RVA: 0x002FFFD2 File Offset: 0x002FE1D2
		private DropColumn(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E192 RID: 57746 RVA: 0x002FFFDB File Offset: 0x002FE1DB
		public static DropColumn CreateUnsafe(ProgramNode node)
		{
			return new DropColumn(node);
		}

		// Token: 0x0600E193 RID: 57747 RVA: 0x002FFFE4 File Offset: 0x002FE1E4
		public static DropColumn? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DropColumn)
			{
				return null;
			}
			return new DropColumn?(DropColumn.CreateUnsafe(node));
		}

		// Token: 0x0600E194 RID: 57748 RVA: 0x00300019 File Offset: 0x002FE219
		public DropColumn(GrammarBuilders g, table value0, sourceColumnName value1, dropCondition value2)
		{
			this._node = g.Rule.DropColumn.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600E195 RID: 57749 RVA: 0x00300046 File Offset: 0x002FE246
		public static implicit operator table(DropColumn arg)
		{
			return table.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025BC RID: 9660
		// (get) Token: 0x0600E196 RID: 57750 RVA: 0x00300054 File Offset: 0x002FE254
		public table table
		{
			get
			{
				return table.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170025BD RID: 9661
		// (get) Token: 0x0600E197 RID: 57751 RVA: 0x00300068 File Offset: 0x002FE268
		public sourceColumnName sourceColumnName
		{
			get
			{
				return sourceColumnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170025BE RID: 9662
		// (get) Token: 0x0600E198 RID: 57752 RVA: 0x0030007C File Offset: 0x002FE27C
		public dropCondition dropCondition
		{
			get
			{
				return dropCondition.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600E199 RID: 57753 RVA: 0x00300090 File Offset: 0x002FE290
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E19A RID: 57754 RVA: 0x003000A4 File Offset: 0x002FE2A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E19B RID: 57755 RVA: 0x003000CE File Offset: 0x002FE2CE
		public bool Equals(DropColumn other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400556C RID: 21868
		private ProgramNode _node;
	}
}
