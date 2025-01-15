using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001BF9 RID: 7161
	public struct numberFormat_numberFormatLiteral : IProgramNodeBuilder, IEquatable<numberFormat_numberFormatLiteral>
	{
		// Token: 0x17002814 RID: 10260
		// (get) Token: 0x0600F0BB RID: 61627 RVA: 0x0033E8E2 File Offset: 0x0033CAE2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F0BC RID: 61628 RVA: 0x0033E8EA File Offset: 0x0033CAEA
		private numberFormat_numberFormatLiteral(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F0BD RID: 61629 RVA: 0x0033E8F3 File Offset: 0x0033CAF3
		public static numberFormat_numberFormatLiteral CreateUnsafe(ProgramNode node)
		{
			return new numberFormat_numberFormatLiteral(node);
		}

		// Token: 0x0600F0BE RID: 61630 RVA: 0x0033E8FC File Offset: 0x0033CAFC
		public static numberFormat_numberFormatLiteral? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.numberFormat_numberFormatLiteral)
			{
				return null;
			}
			return new numberFormat_numberFormatLiteral?(numberFormat_numberFormatLiteral.CreateUnsafe(node));
		}

		// Token: 0x0600F0BF RID: 61631 RVA: 0x0033E931 File Offset: 0x0033CB31
		public numberFormat_numberFormatLiteral(GrammarBuilders g, numberFormatLiteral value0)
		{
			this._node = g.UnnamedConversion.numberFormat_numberFormatLiteral.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F0C0 RID: 61632 RVA: 0x0033E950 File Offset: 0x0033CB50
		public static implicit operator numberFormat(numberFormat_numberFormatLiteral arg)
		{
			return numberFormat.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002815 RID: 10261
		// (get) Token: 0x0600F0C1 RID: 61633 RVA: 0x0033E95E File Offset: 0x0033CB5E
		public numberFormatLiteral numberFormatLiteral
		{
			get
			{
				return numberFormatLiteral.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F0C2 RID: 61634 RVA: 0x0033E972 File Offset: 0x0033CB72
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F0C3 RID: 61635 RVA: 0x0033E988 File Offset: 0x0033CB88
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F0C4 RID: 61636 RVA: 0x0033E9B2 File Offset: 0x0033CBB2
		public bool Equals(numberFormat_numberFormatLiteral other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AE8 RID: 23272
		private ProgramNode _node;
	}
}
