using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001BF8 RID: 7160
	public struct inputNumber_parsedNumber : IProgramNodeBuilder, IEquatable<inputNumber_parsedNumber>
	{
		// Token: 0x17002812 RID: 10258
		// (get) Token: 0x0600F0B1 RID: 61617 RVA: 0x0033E7FE File Offset: 0x0033C9FE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F0B2 RID: 61618 RVA: 0x0033E806 File Offset: 0x0033CA06
		private inputNumber_parsedNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F0B3 RID: 61619 RVA: 0x0033E80F File Offset: 0x0033CA0F
		public static inputNumber_parsedNumber CreateUnsafe(ProgramNode node)
		{
			return new inputNumber_parsedNumber(node);
		}

		// Token: 0x0600F0B4 RID: 61620 RVA: 0x0033E818 File Offset: 0x0033CA18
		public static inputNumber_parsedNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.inputNumber_parsedNumber)
			{
				return null;
			}
			return new inputNumber_parsedNumber?(inputNumber_parsedNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F0B5 RID: 61621 RVA: 0x0033E84D File Offset: 0x0033CA4D
		public inputNumber_parsedNumber(GrammarBuilders g, parsedNumber value0)
		{
			this._node = g.UnnamedConversion.inputNumber_parsedNumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F0B6 RID: 61622 RVA: 0x0033E86C File Offset: 0x0033CA6C
		public static implicit operator inputNumber(inputNumber_parsedNumber arg)
		{
			return inputNumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002813 RID: 10259
		// (get) Token: 0x0600F0B7 RID: 61623 RVA: 0x0033E87A File Offset: 0x0033CA7A
		public parsedNumber parsedNumber
		{
			get
			{
				return parsedNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F0B8 RID: 61624 RVA: 0x0033E88E File Offset: 0x0033CA8E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F0B9 RID: 61625 RVA: 0x0033E8A4 File Offset: 0x0033CAA4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F0BA RID: 61626 RVA: 0x0033E8CE File Offset: 0x0033CACE
		public bool Equals(inputNumber_parsedNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AE7 RID: 23271
		private ProgramNode _node;
	}
}
