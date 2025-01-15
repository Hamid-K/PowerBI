using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x02001202 RID: 4610
	public struct sRegions : IProgramNodeBuilder, IEquatable<sRegions>
	{
		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x06008B04 RID: 35588 RVA: 0x001D1E9E File Offset: 0x001D009E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008B05 RID: 35589 RVA: 0x001D1EA6 File Offset: 0x001D00A6
		private sRegions(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008B06 RID: 35590 RVA: 0x001D1EAF File Offset: 0x001D00AF
		public static sRegions CreateUnsafe(ProgramNode node)
		{
			return new sRegions(node);
		}

		// Token: 0x06008B07 RID: 35591 RVA: 0x001D1EB8 File Offset: 0x001D00B8
		public static sRegions? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sRegions)
			{
				return null;
			}
			return new sRegions?(sRegions.CreateUnsafe(node));
		}

		// Token: 0x06008B08 RID: 35592 RVA: 0x001D1EF2 File Offset: 0x001D00F2
		public static sRegions CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sRegions(new Hole(g.Symbol.sRegions, holeId));
		}

		// Token: 0x06008B09 RID: 35593 RVA: 0x001D1F0A File Offset: 0x001D010A
		public sRegions(GrammarBuilders g)
		{
			this = new sRegions(new VariableNode(g.Symbol.sRegions));
		}

		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x06008B0A RID: 35594 RVA: 0x001D1F22 File Offset: 0x001D0122
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06008B0B RID: 35595 RVA: 0x001D1F2F File Offset: 0x001D012F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008B0C RID: 35596 RVA: 0x001D1F44 File Offset: 0x001D0144
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008B0D RID: 35597 RVA: 0x001D1F6E File Offset: 0x001D016E
		public bool Equals(sRegions other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038B6 RID: 14518
		private ProgramNode _node;
	}
}
