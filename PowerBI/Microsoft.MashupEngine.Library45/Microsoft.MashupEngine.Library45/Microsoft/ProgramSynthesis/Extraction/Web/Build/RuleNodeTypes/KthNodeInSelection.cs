using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200104D RID: 4173
	public struct KthNodeInSelection : IProgramNodeBuilder, IEquatable<KthNodeInSelection>
	{
		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x06007BBC RID: 31676 RVA: 0x001A3B46 File Offset: 0x001A1D46
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007BBD RID: 31677 RVA: 0x001A3B4E File Offset: 0x001A1D4E
		private KthNodeInSelection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007BBE RID: 31678 RVA: 0x001A3B57 File Offset: 0x001A1D57
		public static KthNodeInSelection CreateUnsafe(ProgramNode node)
		{
			return new KthNodeInSelection(node);
		}

		// Token: 0x06007BBF RID: 31679 RVA: 0x001A3B60 File Offset: 0x001A1D60
		public static KthNodeInSelection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KthNodeInSelection)
			{
				return null;
			}
			return new KthNodeInSelection?(KthNodeInSelection.CreateUnsafe(node));
		}

		// Token: 0x06007BC0 RID: 31680 RVA: 0x001A3B95 File Offset: 0x001A1D95
		public KthNodeInSelection(GrammarBuilders g, selection value0, k value1)
		{
			this._node = g.Rule.KthNodeInSelection.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007BC1 RID: 31681 RVA: 0x001A3BC7 File Offset: 0x001A1DC7
		public static implicit operator beginNode(KthNodeInSelection arg)
		{
			return beginNode.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06007BC2 RID: 31682 RVA: 0x001A3BD5 File Offset: 0x001A1DD5
		public selection selection
		{
			get
			{
				return selection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06007BC3 RID: 31683 RVA: 0x001A3BE9 File Offset: 0x001A1DE9
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007BC4 RID: 31684 RVA: 0x001A3BFD File Offset: 0x001A1DFD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007BC5 RID: 31685 RVA: 0x001A3C10 File Offset: 0x001A1E10
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007BC6 RID: 31686 RVA: 0x001A3C3A File Offset: 0x001A1E3A
		public bool Equals(KthNodeInSelection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003366 RID: 13158
		private ProgramNode _node;
	}
}
