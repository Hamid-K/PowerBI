using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200153B RID: 5435
	public struct rowNumberTransform_fromRowNumber : IProgramNodeBuilder, IEquatable<rowNumberTransform_fromRowNumber>
	{
		// Token: 0x17001EBC RID: 7868
		// (get) Token: 0x0600B145 RID: 45381 RVA: 0x002703F6 File Offset: 0x0026E5F6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B146 RID: 45382 RVA: 0x002703FE File Offset: 0x0026E5FE
		private rowNumberTransform_fromRowNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B147 RID: 45383 RVA: 0x00270407 File Offset: 0x0026E607
		public static rowNumberTransform_fromRowNumber CreateUnsafe(ProgramNode node)
		{
			return new rowNumberTransform_fromRowNumber(node);
		}

		// Token: 0x0600B148 RID: 45384 RVA: 0x00270410 File Offset: 0x0026E610
		public static rowNumberTransform_fromRowNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.rowNumberTransform_fromRowNumber)
			{
				return null;
			}
			return new rowNumberTransform_fromRowNumber?(rowNumberTransform_fromRowNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B149 RID: 45385 RVA: 0x00270445 File Offset: 0x0026E645
		public rowNumberTransform_fromRowNumber(GrammarBuilders g, fromRowNumber value0)
		{
			this._node = g.UnnamedConversion.rowNumberTransform_fromRowNumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B14A RID: 45386 RVA: 0x00270464 File Offset: 0x0026E664
		public static implicit operator rowNumberTransform(rowNumberTransform_fromRowNumber arg)
		{
			return rowNumberTransform.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EBD RID: 7869
		// (get) Token: 0x0600B14B RID: 45387 RVA: 0x00270472 File Offset: 0x0026E672
		public fromRowNumber fromRowNumber
		{
			get
			{
				return fromRowNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B14C RID: 45388 RVA: 0x00270486 File Offset: 0x0026E686
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B14D RID: 45389 RVA: 0x0027049C File Offset: 0x0026E69C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B14E RID: 45390 RVA: 0x002704C6 File Offset: 0x0026E6C6
		public bool Equals(rowNumberTransform_fromRowNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045E9 RID: 17897
		private ProgramNode _node;
	}
}
