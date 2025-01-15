using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C77 RID: 7287
	public struct sharedParsedDt : IProgramNodeBuilder, IEquatable<sharedParsedDt>
	{
		// Token: 0x17002926 RID: 10534
		// (get) Token: 0x0600F6DE RID: 63198 RVA: 0x00349E16 File Offset: 0x00348016
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F6DF RID: 63199 RVA: 0x00349E1E File Offset: 0x0034801E
		private sharedParsedDt(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F6E0 RID: 63200 RVA: 0x00349E27 File Offset: 0x00348027
		public static sharedParsedDt CreateUnsafe(ProgramNode node)
		{
			return new sharedParsedDt(node);
		}

		// Token: 0x0600F6E1 RID: 63201 RVA: 0x00349E30 File Offset: 0x00348030
		public static sharedParsedDt? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sharedParsedDt)
			{
				return null;
			}
			return new sharedParsedDt?(sharedParsedDt.CreateUnsafe(node));
		}

		// Token: 0x0600F6E2 RID: 63202 RVA: 0x00349E6A File Offset: 0x0034806A
		public static sharedParsedDt CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sharedParsedDt(new Hole(g.Symbol.sharedParsedDt, holeId));
		}

		// Token: 0x0600F6E3 RID: 63203 RVA: 0x00349E82 File Offset: 0x00348082
		public sharedParsedDt(GrammarBuilders g)
		{
			this = new sharedParsedDt(new VariableNode(g.Symbol.sharedParsedDt));
		}

		// Token: 0x17002927 RID: 10535
		// (get) Token: 0x0600F6E4 RID: 63204 RVA: 0x00349E9A File Offset: 0x0034809A
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600F6E5 RID: 63205 RVA: 0x00349EA7 File Offset: 0x003480A7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F6E6 RID: 63206 RVA: 0x00349EBC File Offset: 0x003480BC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F6E7 RID: 63207 RVA: 0x00349EE6 File Offset: 0x003480E6
		public bool Equals(sharedParsedDt other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B66 RID: 23398
		private ProgramNode _node;
	}
}
