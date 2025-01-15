using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200105F RID: 4191
	public struct mapNodeInSequence : IProgramNodeBuilder, IEquatable<mapNodeInSequence>
	{
		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06007C9D RID: 31901 RVA: 0x001A552E File Offset: 0x001A372E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007C9E RID: 31902 RVA: 0x001A5536 File Offset: 0x001A3736
		private mapNodeInSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C9F RID: 31903 RVA: 0x001A553F File Offset: 0x001A373F
		public static mapNodeInSequence CreateUnsafe(ProgramNode node)
		{
			return new mapNodeInSequence(node);
		}

		// Token: 0x06007CA0 RID: 31904 RVA: 0x001A5548 File Offset: 0x001A3748
		public static mapNodeInSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.mapNodeInSequence)
			{
				return null;
			}
			return new mapNodeInSequence?(mapNodeInSequence.CreateUnsafe(node));
		}

		// Token: 0x06007CA1 RID: 31905 RVA: 0x001A5582 File Offset: 0x001A3782
		public static mapNodeInSequence CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new mapNodeInSequence(new Hole(g.Symbol.mapNodeInSequence, holeId));
		}

		// Token: 0x06007CA2 RID: 31906 RVA: 0x001A559A File Offset: 0x001A379A
		public NodeToWebRegionInSequence Cast_NodeToWebRegionInSequence()
		{
			return NodeToWebRegionInSequence.CreateUnsafe(this.Node);
		}

		// Token: 0x06007CA3 RID: 31907 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_NodeToWebRegionInSequence(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007CA4 RID: 31908 RVA: 0x001A55A7 File Offset: 0x001A37A7
		public bool Is_NodeToWebRegionInSequence(GrammarBuilders g, out NodeToWebRegionInSequence value)
		{
			value = NodeToWebRegionInSequence.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007CA5 RID: 31909 RVA: 0x001A55BB File Offset: 0x001A37BB
		public NodeToWebRegionInSequence? As_NodeToWebRegionInSequence(GrammarBuilders g)
		{
			return new NodeToWebRegionInSequence?(NodeToWebRegionInSequence.CreateUnsafe(this.Node));
		}

		// Token: 0x06007CA6 RID: 31910 RVA: 0x001A55CD File Offset: 0x001A37CD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007CA7 RID: 31911 RVA: 0x001A55E0 File Offset: 0x001A37E0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007CA8 RID: 31912 RVA: 0x001A560A File Offset: 0x001A380A
		public bool Equals(mapNodeInSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003378 RID: 13176
		private ProgramNode _node;
	}
}
