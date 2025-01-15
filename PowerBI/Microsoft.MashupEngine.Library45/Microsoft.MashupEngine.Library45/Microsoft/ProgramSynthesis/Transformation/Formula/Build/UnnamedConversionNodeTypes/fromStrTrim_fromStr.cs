using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001546 RID: 5446
	public struct fromStrTrim_fromStr : IProgramNodeBuilder, IEquatable<fromStrTrim_fromStr>
	{
		// Token: 0x17001ED2 RID: 7890
		// (get) Token: 0x0600B1B3 RID: 45491 RVA: 0x00270DC2 File Offset: 0x0026EFC2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B1B4 RID: 45492 RVA: 0x00270DCA File Offset: 0x0026EFCA
		private fromStrTrim_fromStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B1B5 RID: 45493 RVA: 0x00270DD3 File Offset: 0x0026EFD3
		public static fromStrTrim_fromStr CreateUnsafe(ProgramNode node)
		{
			return new fromStrTrim_fromStr(node);
		}

		// Token: 0x0600B1B6 RID: 45494 RVA: 0x00270DDC File Offset: 0x0026EFDC
		public static fromStrTrim_fromStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.fromStrTrim_fromStr)
			{
				return null;
			}
			return new fromStrTrim_fromStr?(fromStrTrim_fromStr.CreateUnsafe(node));
		}

		// Token: 0x0600B1B7 RID: 45495 RVA: 0x00270E11 File Offset: 0x0026F011
		public fromStrTrim_fromStr(GrammarBuilders g, fromStr value0)
		{
			this._node = g.UnnamedConversion.fromStrTrim_fromStr.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B1B8 RID: 45496 RVA: 0x00270E30 File Offset: 0x0026F030
		public static implicit operator fromStrTrim(fromStrTrim_fromStr arg)
		{
			return fromStrTrim.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001ED3 RID: 7891
		// (get) Token: 0x0600B1B9 RID: 45497 RVA: 0x00270E3E File Offset: 0x0026F03E
		public fromStr fromStr
		{
			get
			{
				return fromStr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B1BA RID: 45498 RVA: 0x00270E52 File Offset: 0x0026F052
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B1BB RID: 45499 RVA: 0x00270E68 File Offset: 0x0026F068
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B1BC RID: 45500 RVA: 0x00270E92 File Offset: 0x0026F092
		public bool Equals(fromStrTrim_fromStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045F4 RID: 17908
		private ProgramNode _node;
	}
}
