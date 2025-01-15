using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200104E RID: 4174
	public struct KthNode : IProgramNodeBuilder, IEquatable<KthNode>
	{
		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06007BC7 RID: 31687 RVA: 0x001A3C4E File Offset: 0x001A1E4E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007BC8 RID: 31688 RVA: 0x001A3C56 File Offset: 0x001A1E56
		private KthNode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007BC9 RID: 31689 RVA: 0x001A3C5F File Offset: 0x001A1E5F
		public static KthNode CreateUnsafe(ProgramNode node)
		{
			return new KthNode(node);
		}

		// Token: 0x06007BCA RID: 31690 RVA: 0x001A3C68 File Offset: 0x001A1E68
		public static KthNode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KthNode)
			{
				return null;
			}
			return new KthNode?(KthNode.CreateUnsafe(node));
		}

		// Token: 0x06007BCB RID: 31691 RVA: 0x001A3C9D File Offset: 0x001A1E9D
		public KthNode(GrammarBuilders g, selectionEnd value0, k value1)
		{
			this._node = g.Rule.KthNode.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007BCC RID: 31692 RVA: 0x001A3CCF File Offset: 0x001A1ECF
		public static implicit operator endNode(KthNode arg)
		{
			return endNode.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06007BCD RID: 31693 RVA: 0x001A3CDD File Offset: 0x001A1EDD
		public selectionEnd selectionEnd
		{
			get
			{
				return selectionEnd.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06007BCE RID: 31694 RVA: 0x001A3CF1 File Offset: 0x001A1EF1
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007BCF RID: 31695 RVA: 0x001A3D05 File Offset: 0x001A1F05
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007BD0 RID: 31696 RVA: 0x001A3D18 File Offset: 0x001A1F18
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007BD1 RID: 31697 RVA: 0x001A3D42 File Offset: 0x001A1F42
		public bool Equals(KthNode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003367 RID: 13159
		private ProgramNode _node;
	}
}
