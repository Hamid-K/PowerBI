using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200103C RID: 4156
	public struct AsCollection : IProgramNodeBuilder, IEquatable<AsCollection>
	{
		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06007B04 RID: 31492 RVA: 0x001A2AAA File Offset: 0x001A0CAA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B05 RID: 31493 RVA: 0x001A2AB2 File Offset: 0x001A0CB2
		private AsCollection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B06 RID: 31494 RVA: 0x001A2ABB File Offset: 0x001A0CBB
		public static AsCollection CreateUnsafe(ProgramNode node)
		{
			return new AsCollection(node);
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x001A2AC4 File Offset: 0x001A0CC4
		public static AsCollection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.AsCollection)
			{
				return null;
			}
			return new AsCollection?(AsCollection.CreateUnsafe(node));
		}

		// Token: 0x06007B08 RID: 31496 RVA: 0x001A2AF9 File Offset: 0x001A0CF9
		public AsCollection(GrammarBuilders g, allNodes value0)
		{
			this._node = g.Rule.AsCollection.BuildASTNode(value0.Node);
		}

		// Token: 0x06007B09 RID: 31497 RVA: 0x001A2B18 File Offset: 0x001A0D18
		public static implicit operator nodeCollection(AsCollection arg)
		{
			return nodeCollection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06007B0A RID: 31498 RVA: 0x001A2B26 File Offset: 0x001A0D26
		public allNodes allNodes
		{
			get
			{
				return allNodes.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007B0B RID: 31499 RVA: 0x001A2B3A File Offset: 0x001A0D3A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B0C RID: 31500 RVA: 0x001A2B50 File Offset: 0x001A0D50
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B0D RID: 31501 RVA: 0x001A2B7A File Offset: 0x001A0D7A
		public bool Equals(AsCollection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003355 RID: 13141
		private ProgramNode _node;
	}
}
