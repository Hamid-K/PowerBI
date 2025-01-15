using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001098 RID: 4248
	public struct substringFeatureValues : IProgramNodeBuilder, IEquatable<substringFeatureValues>
	{
		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x06007FF7 RID: 32759 RVA: 0x001ACC66 File Offset: 0x001AAE66
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007FF8 RID: 32760 RVA: 0x001ACC6E File Offset: 0x001AAE6E
		private substringFeatureValues(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007FF9 RID: 32761 RVA: 0x001ACC77 File Offset: 0x001AAE77
		public static substringFeatureValues CreateUnsafe(ProgramNode node)
		{
			return new substringFeatureValues(node);
		}

		// Token: 0x06007FFA RID: 32762 RVA: 0x001ACC80 File Offset: 0x001AAE80
		public static substringFeatureValues? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.substringFeatureValues)
			{
				return null;
			}
			return new substringFeatureValues?(substringFeatureValues.CreateUnsafe(node));
		}

		// Token: 0x06007FFB RID: 32763 RVA: 0x001ACCBA File Offset: 0x001AAEBA
		public static substringFeatureValues CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new substringFeatureValues(new Hole(g.Symbol.substringFeatureValues, holeId));
		}

		// Token: 0x06007FFC RID: 32764 RVA: 0x001ACCD2 File Offset: 0x001AAED2
		public substringFeatureValues(GrammarBuilders g, int[] value)
		{
			this = new substringFeatureValues(new LiteralNode(g.Symbol.substringFeatureValues, value));
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06007FFD RID: 32765 RVA: 0x001ACCEB File Offset: 0x001AAEEB
		public int[] Value
		{
			get
			{
				return (int[])((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007FFE RID: 32766 RVA: 0x001ACD02 File Offset: 0x001AAF02
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007FFF RID: 32767 RVA: 0x001ACD18 File Offset: 0x001AAF18
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008000 RID: 32768 RVA: 0x001ACD42 File Offset: 0x001AAF42
		public bool Equals(substringFeatureValues other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033B1 RID: 13233
		private ProgramNode _node;
	}
}
