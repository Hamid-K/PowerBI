using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001062 RID: 4194
	public struct mapRegionInSequence : IProgramNodeBuilder, IEquatable<mapRegionInSequence>
	{
		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06007CC1 RID: 31937 RVA: 0x001A57FE File Offset: 0x001A39FE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007CC2 RID: 31938 RVA: 0x001A5806 File Offset: 0x001A3A06
		private mapRegionInSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007CC3 RID: 31939 RVA: 0x001A580F File Offset: 0x001A3A0F
		public static mapRegionInSequence CreateUnsafe(ProgramNode node)
		{
			return new mapRegionInSequence(node);
		}

		// Token: 0x06007CC4 RID: 31940 RVA: 0x001A5818 File Offset: 0x001A3A18
		public static mapRegionInSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.mapRegionInSequence)
			{
				return null;
			}
			return new mapRegionInSequence?(mapRegionInSequence.CreateUnsafe(node));
		}

		// Token: 0x06007CC5 RID: 31941 RVA: 0x001A5852 File Offset: 0x001A3A52
		public static mapRegionInSequence CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new mapRegionInSequence(new Hole(g.Symbol.mapRegionInSequence, holeId));
		}

		// Token: 0x06007CC6 RID: 31942 RVA: 0x001A586A File Offset: 0x001A3A6A
		public NodeRegionToWebRegionInSequence Cast_NodeRegionToWebRegionInSequence()
		{
			return NodeRegionToWebRegionInSequence.CreateUnsafe(this.Node);
		}

		// Token: 0x06007CC7 RID: 31943 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_NodeRegionToWebRegionInSequence(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007CC8 RID: 31944 RVA: 0x001A5877 File Offset: 0x001A3A77
		public bool Is_NodeRegionToWebRegionInSequence(GrammarBuilders g, out NodeRegionToWebRegionInSequence value)
		{
			value = NodeRegionToWebRegionInSequence.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007CC9 RID: 31945 RVA: 0x001A588B File Offset: 0x001A3A8B
		public NodeRegionToWebRegionInSequence? As_NodeRegionToWebRegionInSequence(GrammarBuilders g)
		{
			return new NodeRegionToWebRegionInSequence?(NodeRegionToWebRegionInSequence.CreateUnsafe(this.Node));
		}

		// Token: 0x06007CCA RID: 31946 RVA: 0x001A589D File Offset: 0x001A3A9D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007CCB RID: 31947 RVA: 0x001A58B0 File Offset: 0x001A3AB0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007CCC RID: 31948 RVA: 0x001A58DA File Offset: 0x001A3ADA
		public bool Equals(mapRegionInSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400337B RID: 13179
		private ProgramNode _node;
	}
}
