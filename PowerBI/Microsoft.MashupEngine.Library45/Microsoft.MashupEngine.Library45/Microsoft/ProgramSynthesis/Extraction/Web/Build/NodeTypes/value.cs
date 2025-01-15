using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200108D RID: 4237
	public struct value : IProgramNodeBuilder, IEquatable<value>
	{
		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x06007F89 RID: 32649 RVA: 0x001AC20A File Offset: 0x001AA40A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007F8A RID: 32650 RVA: 0x001AC212 File Offset: 0x001AA412
		private value(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007F8B RID: 32651 RVA: 0x001AC21B File Offset: 0x001AA41B
		public static value CreateUnsafe(ProgramNode node)
		{
			return new value(node);
		}

		// Token: 0x06007F8C RID: 32652 RVA: 0x001AC224 File Offset: 0x001AA424
		public static value? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.value)
			{
				return null;
			}
			return new value?(value.CreateUnsafe(node));
		}

		// Token: 0x06007F8D RID: 32653 RVA: 0x001AC25E File Offset: 0x001AA45E
		public static value CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new value(new Hole(g.Symbol.value, holeId));
		}

		// Token: 0x06007F8E RID: 32654 RVA: 0x001AC276 File Offset: 0x001AA476
		public value(GrammarBuilders g, string value)
		{
			this = new value(new LiteralNode(g.Symbol.value, value));
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06007F8F RID: 32655 RVA: 0x001AC28F File Offset: 0x001AA48F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007F90 RID: 32656 RVA: 0x001AC2A6 File Offset: 0x001AA4A6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007F91 RID: 32657 RVA: 0x001AC2BC File Offset: 0x001AA4BC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007F92 RID: 32658 RVA: 0x001AC2E6 File Offset: 0x001AA4E6
		public bool Equals(value other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033A6 RID: 13222
		private ProgramNode _node;
	}
}
