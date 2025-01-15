using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A35 RID: 6709
	public struct TransformFlatten : IProgramNodeBuilder, IEquatable<TransformFlatten>
	{
		// Token: 0x170024FC RID: 9468
		// (get) Token: 0x0600DC89 RID: 56457 RVA: 0x002EF116 File Offset: 0x002ED316
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC8A RID: 56458 RVA: 0x002EF11E File Offset: 0x002ED31E
		private TransformFlatten(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC8B RID: 56459 RVA: 0x002EF127 File Offset: 0x002ED327
		public static TransformFlatten CreateUnsafe(ProgramNode node)
		{
			return new TransformFlatten(node);
		}

		// Token: 0x0600DC8C RID: 56460 RVA: 0x002EF130 File Offset: 0x002ED330
		public static TransformFlatten? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TransformFlatten)
			{
				return null;
			}
			return new TransformFlatten?(TransformFlatten.CreateUnsafe(node));
		}

		// Token: 0x0600DC8D RID: 56461 RVA: 0x002EF165 File Offset: 0x002ED365
		public TransformFlatten(GrammarBuilders g, array value0, selectArray value1)
		{
			this._node = g.Rule.TransformFlatten.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x0600DC8E RID: 56462 RVA: 0x002EF197 File Offset: 0x002ED397
		public static implicit operator elements(TransformFlatten arg)
		{
			return elements.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024FD RID: 9469
		// (get) Token: 0x0600DC8F RID: 56463 RVA: 0x002EF1A5 File Offset: 0x002ED3A5
		public array array
		{
			get
			{
				return array.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x170024FE RID: 9470
		// (get) Token: 0x0600DC90 RID: 56464 RVA: 0x002EF1C0 File Offset: 0x002ED3C0
		public selectArray selectArray
		{
			get
			{
				return selectArray.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DC91 RID: 56465 RVA: 0x002EF1D4 File Offset: 0x002ED3D4
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC92 RID: 56466 RVA: 0x002EF1E8 File Offset: 0x002ED3E8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC93 RID: 56467 RVA: 0x002EF212 File Offset: 0x002ED412
		public bool Equals(TransformFlatten other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005426 RID: 21542
		private ProgramNode _node;
	}
}
