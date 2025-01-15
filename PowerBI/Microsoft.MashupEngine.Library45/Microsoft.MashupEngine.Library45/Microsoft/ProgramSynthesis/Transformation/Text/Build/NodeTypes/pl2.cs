using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C7A RID: 7290
	public struct pl2 : IProgramNodeBuilder, IEquatable<pl2>
	{
		// Token: 0x1700292C RID: 10540
		// (get) Token: 0x0600F6FC RID: 63228 RVA: 0x0034A0C2 File Offset: 0x003482C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F6FD RID: 63229 RVA: 0x0034A0CA File Offset: 0x003482CA
		private pl2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F6FE RID: 63230 RVA: 0x0034A0D3 File Offset: 0x003482D3
		public static pl2 CreateUnsafe(ProgramNode node)
		{
			return new pl2(node);
		}

		// Token: 0x0600F6FF RID: 63231 RVA: 0x0034A0DC File Offset: 0x003482DC
		public static pl2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.pl2)
			{
				return null;
			}
			return new pl2?(pl2.CreateUnsafe(node));
		}

		// Token: 0x0600F700 RID: 63232 RVA: 0x0034A116 File Offset: 0x00348316
		public static pl2 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new pl2(new Hole(g.Symbol.pl2, holeId));
		}

		// Token: 0x0600F701 RID: 63233 RVA: 0x0034A12E File Offset: 0x0034832E
		public pl2(GrammarBuilders g)
		{
			this = new pl2(new VariableNode(g.Symbol.pl2));
		}

		// Token: 0x1700292D RID: 10541
		// (get) Token: 0x0600F702 RID: 63234 RVA: 0x0034A146 File Offset: 0x00348346
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600F703 RID: 63235 RVA: 0x0034A153 File Offset: 0x00348353
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F704 RID: 63236 RVA: 0x0034A168 File Offset: 0x00348368
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F705 RID: 63237 RVA: 0x0034A192 File Offset: 0x00348392
		public bool Equals(pl2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B69 RID: 23401
		private ProgramNode _node;
	}
}
