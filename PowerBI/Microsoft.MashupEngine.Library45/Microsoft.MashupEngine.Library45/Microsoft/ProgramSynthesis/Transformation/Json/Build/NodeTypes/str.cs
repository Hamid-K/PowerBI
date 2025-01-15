using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A4A RID: 6730
	public struct str : IProgramNodeBuilder, IEquatable<str>
	{
		// Token: 0x1700251A RID: 9498
		// (get) Token: 0x0600DDD0 RID: 56784 RVA: 0x002F2226 File Offset: 0x002F0426
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DDD1 RID: 56785 RVA: 0x002F222E File Offset: 0x002F042E
		private str(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DDD2 RID: 56786 RVA: 0x002F2237 File Offset: 0x002F0437
		public static str CreateUnsafe(ProgramNode node)
		{
			return new str(node);
		}

		// Token: 0x0600DDD3 RID: 56787 RVA: 0x002F2240 File Offset: 0x002F0440
		public static str? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.str)
			{
				return null;
			}
			return new str?(str.CreateUnsafe(node));
		}

		// Token: 0x0600DDD4 RID: 56788 RVA: 0x002F227A File Offset: 0x002F047A
		public static str CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new str(new Hole(g.Symbol.str, holeId));
		}

		// Token: 0x0600DDD5 RID: 56789 RVA: 0x002F2292 File Offset: 0x002F0492
		public str(GrammarBuilders g, string value)
		{
			this = new str(new LiteralNode(g.Symbol.str, value));
		}

		// Token: 0x1700251B RID: 9499
		// (get) Token: 0x0600DDD6 RID: 56790 RVA: 0x002F22AB File Offset: 0x002F04AB
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600DDD7 RID: 56791 RVA: 0x002F22C2 File Offset: 0x002F04C2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DDD8 RID: 56792 RVA: 0x002F22D8 File Offset: 0x002F04D8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DDD9 RID: 56793 RVA: 0x002F2302 File Offset: 0x002F0502
		public bool Equals(str other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400543B RID: 21563
		private ProgramNode _node;
	}
}
