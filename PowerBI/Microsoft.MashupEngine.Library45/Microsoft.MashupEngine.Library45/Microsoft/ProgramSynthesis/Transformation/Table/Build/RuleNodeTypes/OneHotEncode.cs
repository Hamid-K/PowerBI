using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AA9 RID: 6825
	public struct OneHotEncode : IProgramNodeBuilder, IEquatable<OneHotEncode>
	{
		// Token: 0x170025A9 RID: 9641
		// (get) Token: 0x0600E15E RID: 57694 RVA: 0x002FFAF2 File Offset: 0x002FDCF2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E15F RID: 57695 RVA: 0x002FFAFA File Offset: 0x002FDCFA
		private OneHotEncode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E160 RID: 57696 RVA: 0x002FFB03 File Offset: 0x002FDD03
		public static OneHotEncode CreateUnsafe(ProgramNode node)
		{
			return new OneHotEncode(node);
		}

		// Token: 0x0600E161 RID: 57697 RVA: 0x002FFB0C File Offset: 0x002FDD0C
		public static OneHotEncode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.OneHotEncode)
			{
				return null;
			}
			return new OneHotEncode?(OneHotEncode.CreateUnsafe(node));
		}

		// Token: 0x0600E162 RID: 57698 RVA: 0x002FFB41 File Offset: 0x002FDD41
		public OneHotEncode(GrammarBuilders g, table value0, sourceColumnName value1)
		{
			this._node = g.Rule.OneHotEncode.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600E163 RID: 57699 RVA: 0x002FFB67 File Offset: 0x002FDD67
		public static implicit operator table(OneHotEncode arg)
		{
			return table.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025AA RID: 9642
		// (get) Token: 0x0600E164 RID: 57700 RVA: 0x002FFB75 File Offset: 0x002FDD75
		public table table
		{
			get
			{
				return table.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170025AB RID: 9643
		// (get) Token: 0x0600E165 RID: 57701 RVA: 0x002FFB89 File Offset: 0x002FDD89
		public sourceColumnName sourceColumnName
		{
			get
			{
				return sourceColumnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600E166 RID: 57702 RVA: 0x002FFB9D File Offset: 0x002FDD9D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E167 RID: 57703 RVA: 0x002FFBB0 File Offset: 0x002FDDB0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E168 RID: 57704 RVA: 0x002FFBDA File Offset: 0x002FDDDA
		public bool Equals(OneHotEncode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005568 RID: 21864
		private ProgramNode _node;
	}
}
