using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200153A RID: 5434
	public struct inumber_fromNumber : IProgramNodeBuilder, IEquatable<inumber_fromNumber>
	{
		// Token: 0x17001EBA RID: 7866
		// (get) Token: 0x0600B13B RID: 45371 RVA: 0x00270312 File Offset: 0x0026E512
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B13C RID: 45372 RVA: 0x0027031A File Offset: 0x0026E51A
		private inumber_fromNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B13D RID: 45373 RVA: 0x00270323 File Offset: 0x0026E523
		public static inumber_fromNumber CreateUnsafe(ProgramNode node)
		{
			return new inumber_fromNumber(node);
		}

		// Token: 0x0600B13E RID: 45374 RVA: 0x0027032C File Offset: 0x0026E52C
		public static inumber_fromNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.inumber_fromNumber)
			{
				return null;
			}
			return new inumber_fromNumber?(inumber_fromNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B13F RID: 45375 RVA: 0x00270361 File Offset: 0x0026E561
		public inumber_fromNumber(GrammarBuilders g, fromNumber value0)
		{
			this._node = g.UnnamedConversion.inumber_fromNumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B140 RID: 45376 RVA: 0x00270380 File Offset: 0x0026E580
		public static implicit operator inumber(inumber_fromNumber arg)
		{
			return inumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EBB RID: 7867
		// (get) Token: 0x0600B141 RID: 45377 RVA: 0x0027038E File Offset: 0x0026E58E
		public fromNumber fromNumber
		{
			get
			{
				return fromNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B142 RID: 45378 RVA: 0x002703A2 File Offset: 0x0026E5A2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B143 RID: 45379 RVA: 0x002703B8 File Offset: 0x0026E5B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B144 RID: 45380 RVA: 0x002703E2 File Offset: 0x0026E5E2
		public bool Equals(inumber_fromNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045E8 RID: 17896
		private ProgramNode _node;
	}
}
