using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200152E RID: 5422
	public struct concatSuffix_concat : IProgramNodeBuilder, IEquatable<concatSuffix_concat>
	{
		// Token: 0x17001EA2 RID: 7842
		// (get) Token: 0x0600B0C3 RID: 45251 RVA: 0x0026F862 File Offset: 0x0026DA62
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B0C4 RID: 45252 RVA: 0x0026F86A File Offset: 0x0026DA6A
		private concatSuffix_concat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B0C5 RID: 45253 RVA: 0x0026F873 File Offset: 0x0026DA73
		public static concatSuffix_concat CreateUnsafe(ProgramNode node)
		{
			return new concatSuffix_concat(node);
		}

		// Token: 0x0600B0C6 RID: 45254 RVA: 0x0026F87C File Offset: 0x0026DA7C
		public static concatSuffix_concat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.concatSuffix_concat)
			{
				return null;
			}
			return new concatSuffix_concat?(concatSuffix_concat.CreateUnsafe(node));
		}

		// Token: 0x0600B0C7 RID: 45255 RVA: 0x0026F8B1 File Offset: 0x0026DAB1
		public concatSuffix_concat(GrammarBuilders g, concat value0)
		{
			this._node = g.UnnamedConversion.concatSuffix_concat.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B0C8 RID: 45256 RVA: 0x0026F8D0 File Offset: 0x0026DAD0
		public static implicit operator concatSuffix(concatSuffix_concat arg)
		{
			return concatSuffix.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EA3 RID: 7843
		// (get) Token: 0x0600B0C9 RID: 45257 RVA: 0x0026F8DE File Offset: 0x0026DADE
		public concat concat
		{
			get
			{
				return concat.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B0CA RID: 45258 RVA: 0x0026F8F2 File Offset: 0x0026DAF2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B0CB RID: 45259 RVA: 0x0026F908 File Offset: 0x0026DB08
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B0CC RID: 45260 RVA: 0x0026F932 File Offset: 0x0026DB32
		public bool Equals(concatSuffix_concat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045DC RID: 17884
		private ProgramNode _node;
	}
}
