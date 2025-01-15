using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001060 RID: 4192
	public struct regionSequence : IProgramNodeBuilder, IEquatable<regionSequence>
	{
		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06007CA9 RID: 31913 RVA: 0x001A561E File Offset: 0x001A381E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007CAA RID: 31914 RVA: 0x001A5626 File Offset: 0x001A3826
		private regionSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007CAB RID: 31915 RVA: 0x001A562F File Offset: 0x001A382F
		public static regionSequence CreateUnsafe(ProgramNode node)
		{
			return new regionSequence(node);
		}

		// Token: 0x06007CAC RID: 31916 RVA: 0x001A5638 File Offset: 0x001A3838
		public static regionSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.regionSequence)
			{
				return null;
			}
			return new regionSequence?(regionSequence.CreateUnsafe(node));
		}

		// Token: 0x06007CAD RID: 31917 RVA: 0x001A5672 File Offset: 0x001A3872
		public static regionSequence CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new regionSequence(new Hole(g.Symbol.regionSequence, holeId));
		}

		// Token: 0x06007CAE RID: 31918 RVA: 0x001A568A File Offset: 0x001A388A
		public FindEndNode Cast_FindEndNode()
		{
			return FindEndNode.CreateUnsafe(this.Node);
		}

		// Token: 0x06007CAF RID: 31919 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FindEndNode(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007CB0 RID: 31920 RVA: 0x001A5697 File Offset: 0x001A3897
		public bool Is_FindEndNode(GrammarBuilders g, out FindEndNode value)
		{
			value = FindEndNode.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007CB1 RID: 31921 RVA: 0x001A56AB File Offset: 0x001A38AB
		public FindEndNode? As_FindEndNode(GrammarBuilders g)
		{
			return new FindEndNode?(FindEndNode.CreateUnsafe(this.Node));
		}

		// Token: 0x06007CB2 RID: 31922 RVA: 0x001A56BD File Offset: 0x001A38BD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007CB3 RID: 31923 RVA: 0x001A56D0 File Offset: 0x001A38D0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007CB4 RID: 31924 RVA: 0x001A56FA File Offset: 0x001A38FA
		public bool Equals(regionSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003379 RID: 13177
		private ProgramNode _node;
	}
}
