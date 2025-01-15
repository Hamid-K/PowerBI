using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001A20 RID: 6688
	public struct selectOrTransformValue_transformValue : IProgramNodeBuilder, IEquatable<selectOrTransformValue_transformValue>
	{
		// Token: 0x170024C5 RID: 9413
		// (get) Token: 0x0600DBAA RID: 56234 RVA: 0x002EDD12 File Offset: 0x002EBF12
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DBAB RID: 56235 RVA: 0x002EDD1A File Offset: 0x002EBF1A
		private selectOrTransformValue_transformValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DBAC RID: 56236 RVA: 0x002EDD23 File Offset: 0x002EBF23
		public static selectOrTransformValue_transformValue CreateUnsafe(ProgramNode node)
		{
			return new selectOrTransformValue_transformValue(node);
		}

		// Token: 0x0600DBAD RID: 56237 RVA: 0x002EDD2C File Offset: 0x002EBF2C
		public static selectOrTransformValue_transformValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.selectOrTransformValue_transformValue)
			{
				return null;
			}
			return new selectOrTransformValue_transformValue?(selectOrTransformValue_transformValue.CreateUnsafe(node));
		}

		// Token: 0x0600DBAE RID: 56238 RVA: 0x002EDD61 File Offset: 0x002EBF61
		public selectOrTransformValue_transformValue(GrammarBuilders g, transformValue value0)
		{
			this._node = g.UnnamedConversion.selectOrTransformValue_transformValue.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DBAF RID: 56239 RVA: 0x002EDD80 File Offset: 0x002EBF80
		public static implicit operator selectOrTransformValue(selectOrTransformValue_transformValue arg)
		{
			return selectOrTransformValue.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024C6 RID: 9414
		// (get) Token: 0x0600DBB0 RID: 56240 RVA: 0x002EDD8E File Offset: 0x002EBF8E
		public transformValue transformValue
		{
			get
			{
				return transformValue.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DBB1 RID: 56241 RVA: 0x002EDDA2 File Offset: 0x002EBFA2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DBB2 RID: 56242 RVA: 0x002EDDB8 File Offset: 0x002EBFB8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DBB3 RID: 56243 RVA: 0x002EDDE2 File Offset: 0x002EBFE2
		public bool Equals(selectOrTransformValue_transformValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005411 RID: 21521
		private ProgramNode _node;
	}
}
