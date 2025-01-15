using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x02001201 RID: 4609
	public struct sRegion : IProgramNodeBuilder, IEquatable<sRegion>
	{
		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x06008AFA RID: 35578 RVA: 0x001D1DBA File Offset: 0x001CFFBA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008AFB RID: 35579 RVA: 0x001D1DC2 File Offset: 0x001CFFC2
		private sRegion(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008AFC RID: 35580 RVA: 0x001D1DCB File Offset: 0x001CFFCB
		public static sRegion CreateUnsafe(ProgramNode node)
		{
			return new sRegion(node);
		}

		// Token: 0x06008AFD RID: 35581 RVA: 0x001D1DD4 File Offset: 0x001CFFD4
		public static sRegion? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sRegion)
			{
				return null;
			}
			return new sRegion?(sRegion.CreateUnsafe(node));
		}

		// Token: 0x06008AFE RID: 35582 RVA: 0x001D1E0E File Offset: 0x001D000E
		public static sRegion CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sRegion(new Hole(g.Symbol.sRegion, holeId));
		}

		// Token: 0x06008AFF RID: 35583 RVA: 0x001D1E26 File Offset: 0x001D0026
		public sRegion(GrammarBuilders g)
		{
			this = new sRegion(new VariableNode(g.Symbol.sRegion));
		}

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x06008B00 RID: 35584 RVA: 0x001D1E3E File Offset: 0x001D003E
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06008B01 RID: 35585 RVA: 0x001D1E4B File Offset: 0x001D004B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008B02 RID: 35586 RVA: 0x001D1E60 File Offset: 0x001D0060
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008B03 RID: 35587 RVA: 0x001D1E8A File Offset: 0x001D008A
		public bool Equals(sRegion other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038B5 RID: 14517
		private ProgramNode _node;
	}
}
