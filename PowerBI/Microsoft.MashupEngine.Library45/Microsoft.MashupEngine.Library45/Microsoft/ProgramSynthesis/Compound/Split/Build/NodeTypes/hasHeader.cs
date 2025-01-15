using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000971 RID: 2417
	public struct hasHeader : IProgramNodeBuilder, IEquatable<hasHeader>
	{
		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06003990 RID: 14736 RVA: 0x000B2392 File Offset: 0x000B0592
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x000B239A File Offset: 0x000B059A
		private hasHeader(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x000B23A3 File Offset: 0x000B05A3
		public static hasHeader CreateUnsafe(ProgramNode node)
		{
			return new hasHeader(node);
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x000B23AC File Offset: 0x000B05AC
		public static hasHeader? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.hasHeader)
			{
				return null;
			}
			return new hasHeader?(hasHeader.CreateUnsafe(node));
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000B23E6 File Offset: 0x000B05E6
		public static hasHeader CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new hasHeader(new Hole(g.Symbol.hasHeader, holeId));
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000B23FE File Offset: 0x000B05FE
		public hasHeader(GrammarBuilders g, bool value)
		{
			this = new hasHeader(new LiteralNode(g.Symbol.hasHeader, value));
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06003996 RID: 14742 RVA: 0x000B241C File Offset: 0x000B061C
		public bool Value
		{
			get
			{
				return (bool)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000B2433 File Offset: 0x000B0633
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x000B2448 File Offset: 0x000B0648
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000B2472 File Offset: 0x000B0672
		public bool Equals(hasHeader other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A91 RID: 6801
		private ProgramNode _node;
	}
}
