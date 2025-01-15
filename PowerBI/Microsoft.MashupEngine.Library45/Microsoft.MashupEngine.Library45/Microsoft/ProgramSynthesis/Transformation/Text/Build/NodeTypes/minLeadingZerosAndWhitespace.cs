using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C69 RID: 7273
	public struct minLeadingZerosAndWhitespace : IProgramNodeBuilder, IEquatable<minLeadingZerosAndWhitespace>
	{
		// Token: 0x1700290A RID: 10506
		// (get) Token: 0x0600F652 RID: 63058 RVA: 0x00349126 File Offset: 0x00347326
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F653 RID: 63059 RVA: 0x0034912E File Offset: 0x0034732E
		private minLeadingZerosAndWhitespace(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F654 RID: 63060 RVA: 0x00349137 File Offset: 0x00347337
		public static minLeadingZerosAndWhitespace CreateUnsafe(ProgramNode node)
		{
			return new minLeadingZerosAndWhitespace(node);
		}

		// Token: 0x0600F655 RID: 63061 RVA: 0x00349140 File Offset: 0x00347340
		public static minLeadingZerosAndWhitespace? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.minLeadingZerosAndWhitespace)
			{
				return null;
			}
			return new minLeadingZerosAndWhitespace?(minLeadingZerosAndWhitespace.CreateUnsafe(node));
		}

		// Token: 0x0600F656 RID: 63062 RVA: 0x0034917A File Offset: 0x0034737A
		public static minLeadingZerosAndWhitespace CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new minLeadingZerosAndWhitespace(new Hole(g.Symbol.minLeadingZerosAndWhitespace, holeId));
		}

		// Token: 0x0600F657 RID: 63063 RVA: 0x00349192 File Offset: 0x00347392
		public minLeadingZerosAndWhitespace(GrammarBuilders g, uint? value)
		{
			this = new minLeadingZerosAndWhitespace(new LiteralNode(g.Symbol.minLeadingZerosAndWhitespace, value));
		}

		// Token: 0x1700290B RID: 10507
		// (get) Token: 0x0600F658 RID: 63064 RVA: 0x003491B0 File Offset: 0x003473B0
		public uint? Value
		{
			get
			{
				return (uint?)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F659 RID: 63065 RVA: 0x003491C7 File Offset: 0x003473C7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F65A RID: 63066 RVA: 0x003491DC File Offset: 0x003473DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F65B RID: 63067 RVA: 0x00349206 File Offset: 0x00347406
		public bool Equals(minLeadingZerosAndWhitespace other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B58 RID: 23384
		private ProgramNode _node;
	}
}
