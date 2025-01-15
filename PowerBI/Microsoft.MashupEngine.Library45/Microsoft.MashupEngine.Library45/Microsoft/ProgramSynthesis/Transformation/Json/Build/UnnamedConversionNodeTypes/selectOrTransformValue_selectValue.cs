using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001A1F RID: 6687
	public struct selectOrTransformValue_selectValue : IProgramNodeBuilder, IEquatable<selectOrTransformValue_selectValue>
	{
		// Token: 0x170024C3 RID: 9411
		// (get) Token: 0x0600DBA0 RID: 56224 RVA: 0x002EDC2E File Offset: 0x002EBE2E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DBA1 RID: 56225 RVA: 0x002EDC36 File Offset: 0x002EBE36
		private selectOrTransformValue_selectValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DBA2 RID: 56226 RVA: 0x002EDC3F File Offset: 0x002EBE3F
		public static selectOrTransformValue_selectValue CreateUnsafe(ProgramNode node)
		{
			return new selectOrTransformValue_selectValue(node);
		}

		// Token: 0x0600DBA3 RID: 56227 RVA: 0x002EDC48 File Offset: 0x002EBE48
		public static selectOrTransformValue_selectValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.selectOrTransformValue_selectValue)
			{
				return null;
			}
			return new selectOrTransformValue_selectValue?(selectOrTransformValue_selectValue.CreateUnsafe(node));
		}

		// Token: 0x0600DBA4 RID: 56228 RVA: 0x002EDC7D File Offset: 0x002EBE7D
		public selectOrTransformValue_selectValue(GrammarBuilders g, selectValue value0)
		{
			this._node = g.UnnamedConversion.selectOrTransformValue_selectValue.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DBA5 RID: 56229 RVA: 0x002EDC9C File Offset: 0x002EBE9C
		public static implicit operator selectOrTransformValue(selectOrTransformValue_selectValue arg)
		{
			return selectOrTransformValue.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024C4 RID: 9412
		// (get) Token: 0x0600DBA6 RID: 56230 RVA: 0x002EDCAA File Offset: 0x002EBEAA
		public selectValue selectValue
		{
			get
			{
				return selectValue.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DBA7 RID: 56231 RVA: 0x002EDCBE File Offset: 0x002EBEBE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DBA8 RID: 56232 RVA: 0x002EDCD4 File Offset: 0x002EBED4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DBA9 RID: 56233 RVA: 0x002EDCFE File Offset: 0x002EBEFE
		public bool Equals(selectOrTransformValue_selectValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005410 RID: 21520
		private ProgramNode _node;
	}
}
