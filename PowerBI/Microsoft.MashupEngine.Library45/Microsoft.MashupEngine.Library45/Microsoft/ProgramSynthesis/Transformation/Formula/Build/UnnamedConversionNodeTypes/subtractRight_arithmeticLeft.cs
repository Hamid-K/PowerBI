using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001537 RID: 5431
	public struct subtractRight_arithmeticLeft : IProgramNodeBuilder, IEquatable<subtractRight_arithmeticLeft>
	{
		// Token: 0x17001EB4 RID: 7860
		// (get) Token: 0x0600B11D RID: 45341 RVA: 0x00270066 File Offset: 0x0026E266
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B11E RID: 45342 RVA: 0x0027006E File Offset: 0x0026E26E
		private subtractRight_arithmeticLeft(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B11F RID: 45343 RVA: 0x00270077 File Offset: 0x0026E277
		public static subtractRight_arithmeticLeft CreateUnsafe(ProgramNode node)
		{
			return new subtractRight_arithmeticLeft(node);
		}

		// Token: 0x0600B120 RID: 45344 RVA: 0x00270080 File Offset: 0x0026E280
		public static subtractRight_arithmeticLeft? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.subtractRight_arithmeticLeft)
			{
				return null;
			}
			return new subtractRight_arithmeticLeft?(subtractRight_arithmeticLeft.CreateUnsafe(node));
		}

		// Token: 0x0600B121 RID: 45345 RVA: 0x002700B5 File Offset: 0x0026E2B5
		public subtractRight_arithmeticLeft(GrammarBuilders g, arithmeticLeft value0)
		{
			this._node = g.UnnamedConversion.subtractRight_arithmeticLeft.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B122 RID: 45346 RVA: 0x002700D4 File Offset: 0x0026E2D4
		public static implicit operator subtractRight(subtractRight_arithmeticLeft arg)
		{
			return subtractRight.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EB5 RID: 7861
		// (get) Token: 0x0600B123 RID: 45347 RVA: 0x002700E2 File Offset: 0x0026E2E2
		public arithmeticLeft arithmeticLeft
		{
			get
			{
				return arithmeticLeft.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B124 RID: 45348 RVA: 0x002700F6 File Offset: 0x0026E2F6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B125 RID: 45349 RVA: 0x0027010C File Offset: 0x0026E30C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B126 RID: 45350 RVA: 0x00270136 File Offset: 0x0026E336
		public bool Equals(subtractRight_arithmeticLeft other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045E5 RID: 17893
		private ProgramNode _node;
	}
}
