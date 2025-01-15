using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C79 RID: 7289
	public struct pl1 : IProgramNodeBuilder, IEquatable<pl1>
	{
		// Token: 0x1700292A RID: 10538
		// (get) Token: 0x0600F6F2 RID: 63218 RVA: 0x00349FDE File Offset: 0x003481DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F6F3 RID: 63219 RVA: 0x00349FE6 File Offset: 0x003481E6
		private pl1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F6F4 RID: 63220 RVA: 0x00349FEF File Offset: 0x003481EF
		public static pl1 CreateUnsafe(ProgramNode node)
		{
			return new pl1(node);
		}

		// Token: 0x0600F6F5 RID: 63221 RVA: 0x00349FF8 File Offset: 0x003481F8
		public static pl1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.pl1)
			{
				return null;
			}
			return new pl1?(pl1.CreateUnsafe(node));
		}

		// Token: 0x0600F6F6 RID: 63222 RVA: 0x0034A032 File Offset: 0x00348232
		public static pl1 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new pl1(new Hole(g.Symbol.pl1, holeId));
		}

		// Token: 0x0600F6F7 RID: 63223 RVA: 0x0034A04A File Offset: 0x0034824A
		public pl1(GrammarBuilders g)
		{
			this = new pl1(new VariableNode(g.Symbol.pl1));
		}

		// Token: 0x1700292B RID: 10539
		// (get) Token: 0x0600F6F8 RID: 63224 RVA: 0x0034A062 File Offset: 0x00348262
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600F6F9 RID: 63225 RVA: 0x0034A06F File Offset: 0x0034826F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F6FA RID: 63226 RVA: 0x0034A084 File Offset: 0x00348284
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F6FB RID: 63227 RVA: 0x0034A0AE File Offset: 0x003482AE
		public bool Equals(pl1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B68 RID: 23400
		private ProgramNode _node;
	}
}
