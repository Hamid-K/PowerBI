using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C67 RID: 7271
	public struct minTrailingZerosAndWhitespace : IProgramNodeBuilder, IEquatable<minTrailingZerosAndWhitespace>
	{
		// Token: 0x17002906 RID: 10502
		// (get) Token: 0x0600F63E RID: 63038 RVA: 0x00348F3E File Offset: 0x0034713E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F63F RID: 63039 RVA: 0x00348F46 File Offset: 0x00347146
		private minTrailingZerosAndWhitespace(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F640 RID: 63040 RVA: 0x00348F4F File Offset: 0x0034714F
		public static minTrailingZerosAndWhitespace CreateUnsafe(ProgramNode node)
		{
			return new minTrailingZerosAndWhitespace(node);
		}

		// Token: 0x0600F641 RID: 63041 RVA: 0x00348F58 File Offset: 0x00347158
		public static minTrailingZerosAndWhitespace? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.minTrailingZerosAndWhitespace)
			{
				return null;
			}
			return new minTrailingZerosAndWhitespace?(minTrailingZerosAndWhitespace.CreateUnsafe(node));
		}

		// Token: 0x0600F642 RID: 63042 RVA: 0x00348F92 File Offset: 0x00347192
		public static minTrailingZerosAndWhitespace CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new minTrailingZerosAndWhitespace(new Hole(g.Symbol.minTrailingZerosAndWhitespace, holeId));
		}

		// Token: 0x0600F643 RID: 63043 RVA: 0x00348FAA File Offset: 0x003471AA
		public minTrailingZerosAndWhitespace(GrammarBuilders g, uint? value)
		{
			this = new minTrailingZerosAndWhitespace(new LiteralNode(g.Symbol.minTrailingZerosAndWhitespace, value));
		}

		// Token: 0x17002907 RID: 10503
		// (get) Token: 0x0600F644 RID: 63044 RVA: 0x00348FC8 File Offset: 0x003471C8
		public uint? Value
		{
			get
			{
				return (uint?)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F645 RID: 63045 RVA: 0x00348FDF File Offset: 0x003471DF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F646 RID: 63046 RVA: 0x00348FF4 File Offset: 0x003471F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F647 RID: 63047 RVA: 0x0034901E File Offset: 0x0034721E
		public bool Equals(minTrailingZerosAndWhitespace other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B56 RID: 23382
		private ProgramNode _node;
	}
}
