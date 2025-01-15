using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001536 RID: 5430
	public struct addRight_arithmeticLeft : IProgramNodeBuilder, IEquatable<addRight_arithmeticLeft>
	{
		// Token: 0x17001EB2 RID: 7858
		// (get) Token: 0x0600B113 RID: 45331 RVA: 0x0026FF82 File Offset: 0x0026E182
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B114 RID: 45332 RVA: 0x0026FF8A File Offset: 0x0026E18A
		private addRight_arithmeticLeft(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B115 RID: 45333 RVA: 0x0026FF93 File Offset: 0x0026E193
		public static addRight_arithmeticLeft CreateUnsafe(ProgramNode node)
		{
			return new addRight_arithmeticLeft(node);
		}

		// Token: 0x0600B116 RID: 45334 RVA: 0x0026FF9C File Offset: 0x0026E19C
		public static addRight_arithmeticLeft? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.addRight_arithmeticLeft)
			{
				return null;
			}
			return new addRight_arithmeticLeft?(addRight_arithmeticLeft.CreateUnsafe(node));
		}

		// Token: 0x0600B117 RID: 45335 RVA: 0x0026FFD1 File Offset: 0x0026E1D1
		public addRight_arithmeticLeft(GrammarBuilders g, arithmeticLeft value0)
		{
			this._node = g.UnnamedConversion.addRight_arithmeticLeft.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B118 RID: 45336 RVA: 0x0026FFF0 File Offset: 0x0026E1F0
		public static implicit operator addRight(addRight_arithmeticLeft arg)
		{
			return addRight.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EB3 RID: 7859
		// (get) Token: 0x0600B119 RID: 45337 RVA: 0x0026FFFE File Offset: 0x0026E1FE
		public arithmeticLeft arithmeticLeft
		{
			get
			{
				return arithmeticLeft.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B11A RID: 45338 RVA: 0x00270012 File Offset: 0x0026E212
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B11B RID: 45339 RVA: 0x00270028 File Offset: 0x0026E228
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B11C RID: 45340 RVA: 0x00270052 File Offset: 0x0026E252
		public bool Equals(addRight_arithmeticLeft other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045E4 RID: 17892
		private ProgramNode _node;
	}
}
