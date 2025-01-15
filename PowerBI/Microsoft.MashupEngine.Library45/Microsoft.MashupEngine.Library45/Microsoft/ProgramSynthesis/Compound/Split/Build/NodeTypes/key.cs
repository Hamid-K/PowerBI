using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000973 RID: 2419
	public struct key : IProgramNodeBuilder, IEquatable<key>
	{
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x000B2576 File Offset: 0x000B0776
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x000B257E File Offset: 0x000B077E
		private key(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x000B2587 File Offset: 0x000B0787
		public static key CreateUnsafe(ProgramNode node)
		{
			return new key(node);
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000B2590 File Offset: 0x000B0790
		public static key? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.key)
			{
				return null;
			}
			return new key?(key.CreateUnsafe(node));
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x000B25CA File Offset: 0x000B07CA
		public static key CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new key(new Hole(g.Symbol.key, holeId));
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000B25E2 File Offset: 0x000B07E2
		public key(GrammarBuilders g, string value)
		{
			this = new key(new LiteralNode(g.Symbol.key, value));
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060039AA RID: 14762 RVA: 0x000B25FB File Offset: 0x000B07FB
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x000B2612 File Offset: 0x000B0812
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x000B2628 File Offset: 0x000B0828
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x000B2652 File Offset: 0x000B0852
		public bool Equals(key other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A93 RID: 6803
		private ProgramNode _node;
	}
}
