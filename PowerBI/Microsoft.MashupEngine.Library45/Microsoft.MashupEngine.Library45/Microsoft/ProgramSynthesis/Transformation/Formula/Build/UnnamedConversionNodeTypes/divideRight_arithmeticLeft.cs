using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001539 RID: 5433
	public struct divideRight_arithmeticLeft : IProgramNodeBuilder, IEquatable<divideRight_arithmeticLeft>
	{
		// Token: 0x17001EB8 RID: 7864
		// (get) Token: 0x0600B131 RID: 45361 RVA: 0x0027022E File Offset: 0x0026E42E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B132 RID: 45362 RVA: 0x00270236 File Offset: 0x0026E436
		private divideRight_arithmeticLeft(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B133 RID: 45363 RVA: 0x0027023F File Offset: 0x0026E43F
		public static divideRight_arithmeticLeft CreateUnsafe(ProgramNode node)
		{
			return new divideRight_arithmeticLeft(node);
		}

		// Token: 0x0600B134 RID: 45364 RVA: 0x00270248 File Offset: 0x0026E448
		public static divideRight_arithmeticLeft? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.divideRight_arithmeticLeft)
			{
				return null;
			}
			return new divideRight_arithmeticLeft?(divideRight_arithmeticLeft.CreateUnsafe(node));
		}

		// Token: 0x0600B135 RID: 45365 RVA: 0x0027027D File Offset: 0x0026E47D
		public divideRight_arithmeticLeft(GrammarBuilders g, arithmeticLeft value0)
		{
			this._node = g.UnnamedConversion.divideRight_arithmeticLeft.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B136 RID: 45366 RVA: 0x0027029C File Offset: 0x0026E49C
		public static implicit operator divideRight(divideRight_arithmeticLeft arg)
		{
			return divideRight.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EB9 RID: 7865
		// (get) Token: 0x0600B137 RID: 45367 RVA: 0x002702AA File Offset: 0x0026E4AA
		public arithmeticLeft arithmeticLeft
		{
			get
			{
				return arithmeticLeft.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B138 RID: 45368 RVA: 0x002702BE File Offset: 0x0026E4BE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B139 RID: 45369 RVA: 0x002702D4 File Offset: 0x0026E4D4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B13A RID: 45370 RVA: 0x002702FE File Offset: 0x0026E4FE
		public bool Equals(divideRight_arithmeticLeft other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045E7 RID: 17895
		private ProgramNode _node;
	}
}
