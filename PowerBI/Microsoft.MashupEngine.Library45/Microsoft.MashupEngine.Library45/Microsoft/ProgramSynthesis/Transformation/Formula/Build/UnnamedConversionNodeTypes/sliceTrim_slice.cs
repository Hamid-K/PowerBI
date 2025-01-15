using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001545 RID: 5445
	public struct sliceTrim_slice : IProgramNodeBuilder, IEquatable<sliceTrim_slice>
	{
		// Token: 0x17001ED0 RID: 7888
		// (get) Token: 0x0600B1A9 RID: 45481 RVA: 0x00270CDE File Offset: 0x0026EEDE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B1AA RID: 45482 RVA: 0x00270CE6 File Offset: 0x0026EEE6
		private sliceTrim_slice(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B1AB RID: 45483 RVA: 0x00270CEF File Offset: 0x0026EEEF
		public static sliceTrim_slice CreateUnsafe(ProgramNode node)
		{
			return new sliceTrim_slice(node);
		}

		// Token: 0x0600B1AC RID: 45484 RVA: 0x00270CF8 File Offset: 0x0026EEF8
		public static sliceTrim_slice? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.sliceTrim_slice)
			{
				return null;
			}
			return new sliceTrim_slice?(sliceTrim_slice.CreateUnsafe(node));
		}

		// Token: 0x0600B1AD RID: 45485 RVA: 0x00270D2D File Offset: 0x0026EF2D
		public sliceTrim_slice(GrammarBuilders g, slice value0)
		{
			this._node = g.UnnamedConversion.sliceTrim_slice.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B1AE RID: 45486 RVA: 0x00270D4C File Offset: 0x0026EF4C
		public static implicit operator sliceTrim(sliceTrim_slice arg)
		{
			return sliceTrim.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001ED1 RID: 7889
		// (get) Token: 0x0600B1AF RID: 45487 RVA: 0x00270D5A File Offset: 0x0026EF5A
		public slice slice
		{
			get
			{
				return slice.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B1B0 RID: 45488 RVA: 0x00270D6E File Offset: 0x0026EF6E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B1B1 RID: 45489 RVA: 0x00270D84 File Offset: 0x0026EF84
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B1B2 RID: 45490 RVA: 0x00270DAE File Offset: 0x0026EFAE
		public bool Equals(sliceTrim_slice other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045F3 RID: 17907
		private ProgramNode _node;
	}
}
