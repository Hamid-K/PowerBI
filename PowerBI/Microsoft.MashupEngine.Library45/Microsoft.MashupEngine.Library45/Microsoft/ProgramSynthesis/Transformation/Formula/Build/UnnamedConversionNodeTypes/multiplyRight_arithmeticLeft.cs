using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001538 RID: 5432
	public struct multiplyRight_arithmeticLeft : IProgramNodeBuilder, IEquatable<multiplyRight_arithmeticLeft>
	{
		// Token: 0x17001EB6 RID: 7862
		// (get) Token: 0x0600B127 RID: 45351 RVA: 0x0027014A File Offset: 0x0026E34A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B128 RID: 45352 RVA: 0x00270152 File Offset: 0x0026E352
		private multiplyRight_arithmeticLeft(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B129 RID: 45353 RVA: 0x0027015B File Offset: 0x0026E35B
		public static multiplyRight_arithmeticLeft CreateUnsafe(ProgramNode node)
		{
			return new multiplyRight_arithmeticLeft(node);
		}

		// Token: 0x0600B12A RID: 45354 RVA: 0x00270164 File Offset: 0x0026E364
		public static multiplyRight_arithmeticLeft? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.multiplyRight_arithmeticLeft)
			{
				return null;
			}
			return new multiplyRight_arithmeticLeft?(multiplyRight_arithmeticLeft.CreateUnsafe(node));
		}

		// Token: 0x0600B12B RID: 45355 RVA: 0x00270199 File Offset: 0x0026E399
		public multiplyRight_arithmeticLeft(GrammarBuilders g, arithmeticLeft value0)
		{
			this._node = g.UnnamedConversion.multiplyRight_arithmeticLeft.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B12C RID: 45356 RVA: 0x002701B8 File Offset: 0x0026E3B8
		public static implicit operator multiplyRight(multiplyRight_arithmeticLeft arg)
		{
			return multiplyRight.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EB7 RID: 7863
		// (get) Token: 0x0600B12D RID: 45357 RVA: 0x002701C6 File Offset: 0x0026E3C6
		public arithmeticLeft arithmeticLeft
		{
			get
			{
				return arithmeticLeft.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B12E RID: 45358 RVA: 0x002701DA File Offset: 0x0026E3DA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B12F RID: 45359 RVA: 0x002701F0 File Offset: 0x0026E3F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B130 RID: 45360 RVA: 0x0027021A File Offset: 0x0026E41A
		public bool Equals(multiplyRight_arithmeticLeft other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045E6 RID: 17894
		private ProgramNode _node;
	}
}
