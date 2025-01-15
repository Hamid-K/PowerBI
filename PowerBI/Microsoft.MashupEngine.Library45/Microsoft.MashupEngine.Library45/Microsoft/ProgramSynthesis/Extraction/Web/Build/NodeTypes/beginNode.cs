using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001063 RID: 4195
	public struct beginNode : IProgramNodeBuilder, IEquatable<beginNode>
	{
		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x06007CCD RID: 31949 RVA: 0x001A58EE File Offset: 0x001A3AEE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007CCE RID: 31950 RVA: 0x001A58F6 File Offset: 0x001A3AF6
		private beginNode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007CCF RID: 31951 RVA: 0x001A58FF File Offset: 0x001A3AFF
		public static beginNode CreateUnsafe(ProgramNode node)
		{
			return new beginNode(node);
		}

		// Token: 0x06007CD0 RID: 31952 RVA: 0x001A5908 File Offset: 0x001A3B08
		public static beginNode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.beginNode)
			{
				return null;
			}
			return new beginNode?(beginNode.CreateUnsafe(node));
		}

		// Token: 0x06007CD1 RID: 31953 RVA: 0x001A5942 File Offset: 0x001A3B42
		public static beginNode CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new beginNode(new Hole(g.Symbol.beginNode, holeId));
		}

		// Token: 0x06007CD2 RID: 31954 RVA: 0x001A595A File Offset: 0x001A3B5A
		public KthNodeInSelection Cast_KthNodeInSelection()
		{
			return KthNodeInSelection.CreateUnsafe(this.Node);
		}

		// Token: 0x06007CD3 RID: 31955 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_KthNodeInSelection(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007CD4 RID: 31956 RVA: 0x001A5967 File Offset: 0x001A3B67
		public bool Is_KthNodeInSelection(GrammarBuilders g, out KthNodeInSelection value)
		{
			value = KthNodeInSelection.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007CD5 RID: 31957 RVA: 0x001A597B File Offset: 0x001A3B7B
		public KthNodeInSelection? As_KthNodeInSelection(GrammarBuilders g)
		{
			return new KthNodeInSelection?(KthNodeInSelection.CreateUnsafe(this.Node));
		}

		// Token: 0x06007CD6 RID: 31958 RVA: 0x001A598D File Offset: 0x001A3B8D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007CD7 RID: 31959 RVA: 0x001A59A0 File Offset: 0x001A3BA0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007CD8 RID: 31960 RVA: 0x001A59CA File Offset: 0x001A3BCA
		public bool Equals(beginNode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400337C RID: 13180
		private ProgramNode _node;
	}
}
