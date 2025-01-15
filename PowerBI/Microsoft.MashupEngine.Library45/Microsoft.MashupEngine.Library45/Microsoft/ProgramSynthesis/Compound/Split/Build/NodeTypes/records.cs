using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000980 RID: 2432
	public struct records : IProgramNodeBuilder, IEquatable<records>
	{
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x000B31BA File Offset: 0x000B13BA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x000B31C2 File Offset: 0x000B13C2
		private records(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x000B31CB File Offset: 0x000B13CB
		public static records CreateUnsafe(ProgramNode node)
		{
			return new records(node);
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x000B31D4 File Offset: 0x000B13D4
		public static records? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.records)
			{
				return null;
			}
			return new records?(records.CreateUnsafe(node));
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x000B320E File Offset: 0x000B140E
		public static records CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new records(new Hole(g.Symbol.records, holeId));
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x000B3226 File Offset: 0x000B1426
		public records(GrammarBuilders g)
		{
			this = new records(new VariableNode(g.Symbol.records));
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x000B323E File Offset: 0x000B143E
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x000B324B File Offset: 0x000B144B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000B3260 File Offset: 0x000B1460
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x000B328A File Offset: 0x000B148A
		public bool Equals(records other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001AA0 RID: 6816
		private ProgramNode _node;
	}
}
