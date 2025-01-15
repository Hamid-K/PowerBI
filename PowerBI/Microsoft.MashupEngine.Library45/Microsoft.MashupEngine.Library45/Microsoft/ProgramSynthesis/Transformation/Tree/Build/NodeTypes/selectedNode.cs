using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E97 RID: 7831
	public struct selectedNode : IProgramNodeBuilder, IEquatable<selectedNode>
	{
		// Token: 0x17002BFB RID: 11259
		// (get) Token: 0x060108C4 RID: 67780 RVA: 0x0038E15E File Offset: 0x0038C35E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060108C5 RID: 67781 RVA: 0x0038E166 File Offset: 0x0038C366
		private selectedNode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060108C6 RID: 67782 RVA: 0x0038E16F File Offset: 0x0038C36F
		public static selectedNode CreateUnsafe(ProgramNode node)
		{
			return new selectedNode(node);
		}

		// Token: 0x060108C7 RID: 67783 RVA: 0x0038E178 File Offset: 0x0038C378
		public static selectedNode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selectedNode)
			{
				return null;
			}
			return new selectedNode?(selectedNode.CreateUnsafe(node));
		}

		// Token: 0x060108C8 RID: 67784 RVA: 0x0038E1B2 File Offset: 0x0038C3B2
		public static selectedNode CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selectedNode(new Hole(g.Symbol.selectedNode, holeId));
		}

		// Token: 0x060108C9 RID: 67785 RVA: 0x0038E1CA File Offset: 0x0038C3CA
		public selectedNode(GrammarBuilders g)
		{
			this = new selectedNode(new VariableNode(g.Symbol.selectedNode));
		}

		// Token: 0x17002BFC RID: 11260
		// (get) Token: 0x060108CA RID: 67786 RVA: 0x0038E1E2 File Offset: 0x0038C3E2
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x060108CB RID: 67787 RVA: 0x0038E1EF File Offset: 0x0038C3EF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060108CC RID: 67788 RVA: 0x0038E204 File Offset: 0x0038C404
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060108CD RID: 67789 RVA: 0x0038E22E File Offset: 0x0038C42E
		public bool Equals(selectedNode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062D6 RID: 25302
		private ProgramNode _node;
	}
}
