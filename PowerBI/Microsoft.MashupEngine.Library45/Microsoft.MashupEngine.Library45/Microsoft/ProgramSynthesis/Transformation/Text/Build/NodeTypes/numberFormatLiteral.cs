using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C6C RID: 7276
	public struct numberFormatLiteral : IProgramNodeBuilder, IEquatable<numberFormatLiteral>
	{
		// Token: 0x17002910 RID: 10512
		// (get) Token: 0x0600F670 RID: 63088 RVA: 0x003493FE File Offset: 0x003475FE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F671 RID: 63089 RVA: 0x00349406 File Offset: 0x00347606
		private numberFormatLiteral(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F672 RID: 63090 RVA: 0x0034940F File Offset: 0x0034760F
		public static numberFormatLiteral CreateUnsafe(ProgramNode node)
		{
			return new numberFormatLiteral(node);
		}

		// Token: 0x0600F673 RID: 63091 RVA: 0x00349418 File Offset: 0x00347618
		public static numberFormatLiteral? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.numberFormatLiteral)
			{
				return null;
			}
			return new numberFormatLiteral?(numberFormatLiteral.CreateUnsafe(node));
		}

		// Token: 0x0600F674 RID: 63092 RVA: 0x00349452 File Offset: 0x00347652
		public static numberFormatLiteral CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new numberFormatLiteral(new Hole(g.Symbol.numberFormatLiteral, holeId));
		}

		// Token: 0x0600F675 RID: 63093 RVA: 0x0034946A File Offset: 0x0034766A
		public numberFormatLiteral(GrammarBuilders g, NumberFormat value)
		{
			this = new numberFormatLiteral(new LiteralNode(g.Symbol.numberFormatLiteral, value));
		}

		// Token: 0x17002911 RID: 10513
		// (get) Token: 0x0600F676 RID: 63094 RVA: 0x00349483 File Offset: 0x00347683
		public NumberFormat Value
		{
			get
			{
				return (NumberFormat)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F677 RID: 63095 RVA: 0x0034949A File Offset: 0x0034769A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F678 RID: 63096 RVA: 0x003494B0 File Offset: 0x003476B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F679 RID: 63097 RVA: 0x003494DA File Offset: 0x003476DA
		public bool Equals(numberFormatLiteral other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B5B RID: 23387
		private ProgramNode _node;
	}
}
