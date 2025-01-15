using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200108C RID: 4236
	public struct name : IProgramNodeBuilder, IEquatable<name>
	{
		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06007F7F RID: 32639 RVA: 0x001AC11A File Offset: 0x001AA31A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007F80 RID: 32640 RVA: 0x001AC122 File Offset: 0x001AA322
		private name(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007F81 RID: 32641 RVA: 0x001AC12B File Offset: 0x001AA32B
		public static name CreateUnsafe(ProgramNode node)
		{
			return new name(node);
		}

		// Token: 0x06007F82 RID: 32642 RVA: 0x001AC134 File Offset: 0x001AA334
		public static name? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.name)
			{
				return null;
			}
			return new name?(name.CreateUnsafe(node));
		}

		// Token: 0x06007F83 RID: 32643 RVA: 0x001AC16E File Offset: 0x001AA36E
		public static name CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new name(new Hole(g.Symbol.name, holeId));
		}

		// Token: 0x06007F84 RID: 32644 RVA: 0x001AC186 File Offset: 0x001AA386
		public name(GrammarBuilders g, string value)
		{
			this = new name(new LiteralNode(g.Symbol.name, value));
		}

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x06007F85 RID: 32645 RVA: 0x001AC19F File Offset: 0x001AA39F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007F86 RID: 32646 RVA: 0x001AC1B6 File Offset: 0x001AA3B6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007F87 RID: 32647 RVA: 0x001AC1CC File Offset: 0x001AA3CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007F88 RID: 32648 RVA: 0x001AC1F6 File Offset: 0x001AA3F6
		public bool Equals(name other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033A5 RID: 13221
		private ProgramNode _node;
	}
}
