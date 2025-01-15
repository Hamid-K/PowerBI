using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000977 RID: 2423
	public struct r : IProgramNodeBuilder, IEquatable<r>
	{
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060039CC RID: 14796 RVA: 0x000B293A File Offset: 0x000B0B3A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000B2942 File Offset: 0x000B0B42
		private r(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000B294B File Offset: 0x000B0B4B
		public static r CreateUnsafe(ProgramNode node)
		{
			return new r(node);
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000B2954 File Offset: 0x000B0B54
		public static r? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.r)
			{
				return null;
			}
			return new r?(r.CreateUnsafe(node));
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x000B298E File Offset: 0x000B0B8E
		public static r CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new r(new Hole(g.Symbol.r, holeId));
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000B29A6 File Offset: 0x000B0BA6
		public r(GrammarBuilders g, RegularExpression value)
		{
			this = new r(new LiteralNode(g.Symbol.r, value));
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060039D2 RID: 14802 RVA: 0x000B29BF File Offset: 0x000B0BBF
		public RegularExpression Value
		{
			get
			{
				return (RegularExpression)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000B29D6 File Offset: 0x000B0BD6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000B29EC File Offset: 0x000B0BEC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x000B2A16 File Offset: 0x000B0C16
		public bool Equals(r other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A97 RID: 6807
		private ProgramNode _node;
	}
}
