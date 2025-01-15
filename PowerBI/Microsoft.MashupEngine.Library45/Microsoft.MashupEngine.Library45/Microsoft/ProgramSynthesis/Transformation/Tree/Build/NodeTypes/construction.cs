using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E7E RID: 7806
	public struct construction : IProgramNodeBuilder, IEquatable<construction>
	{
		// Token: 0x17002BD7 RID: 11223
		// (get) Token: 0x0601077E RID: 67454 RVA: 0x0038BA8E File Offset: 0x00389C8E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601077F RID: 67455 RVA: 0x0038BA96 File Offset: 0x00389C96
		private construction(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010780 RID: 67456 RVA: 0x0038BA9F File Offset: 0x00389C9F
		public static construction CreateUnsafe(ProgramNode node)
		{
			return new construction(node);
		}

		// Token: 0x06010781 RID: 67457 RVA: 0x0038BAA8 File Offset: 0x00389CA8
		public static construction? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.construction)
			{
				return null;
			}
			return new construction?(construction.CreateUnsafe(node));
		}

		// Token: 0x06010782 RID: 67458 RVA: 0x0038BAE2 File Offset: 0x00389CE2
		public static construction CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new construction(new Hole(g.Symbol.construction, holeId));
		}

		// Token: 0x06010783 RID: 67459 RVA: 0x0038BAFA File Offset: 0x00389CFA
		public bool Is_LeafConstLabelNode(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LeafConstLabelNode;
		}

		// Token: 0x06010784 RID: 67460 RVA: 0x0038BB14 File Offset: 0x00389D14
		public bool Is_LeafConstLabelNode(GrammarBuilders g, out LeafConstLabelNode value)
		{
			if (this.Node.GrammarRule == g.Rule.LeafConstLabelNode)
			{
				value = LeafConstLabelNode.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LeafConstLabelNode);
			return false;
		}

		// Token: 0x06010785 RID: 67461 RVA: 0x0038BB4C File Offset: 0x00389D4C
		public LeafConstLabelNode? As_LeafConstLabelNode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LeafConstLabelNode)
			{
				return null;
			}
			return new LeafConstLabelNode?(LeafConstLabelNode.CreateUnsafe(this.Node));
		}

		// Token: 0x06010786 RID: 67462 RVA: 0x0038BB8C File Offset: 0x00389D8C
		public LeafConstLabelNode Cast_LeafConstLabelNode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LeafConstLabelNode)
			{
				return LeafConstLabelNode.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LeafConstLabelNode is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06010787 RID: 67463 RVA: 0x0038BBE1 File Offset: 0x00389DE1
		public bool Is_ConstLabelNode(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConstLabelNode;
		}

		// Token: 0x06010788 RID: 67464 RVA: 0x0038BBFB File Offset: 0x00389DFB
		public bool Is_ConstLabelNode(GrammarBuilders g, out ConstLabelNode value)
		{
			if (this.Node.GrammarRule == g.Rule.ConstLabelNode)
			{
				value = ConstLabelNode.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConstLabelNode);
			return false;
		}

		// Token: 0x06010789 RID: 67465 RVA: 0x0038BC30 File Offset: 0x00389E30
		public ConstLabelNode? As_ConstLabelNode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConstLabelNode)
			{
				return null;
			}
			return new ConstLabelNode?(ConstLabelNode.CreateUnsafe(this.Node));
		}

		// Token: 0x0601078A RID: 67466 RVA: 0x0038BC70 File Offset: 0x00389E70
		public ConstLabelNode Cast_ConstLabelNode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConstLabelNode)
			{
				return ConstLabelNode.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConstLabelNode is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0601078B RID: 67467 RVA: 0x0038BCC5 File Offset: 0x00389EC5
		public bool Is_ConstSequenceLabelNode(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConstSequenceLabelNode;
		}

		// Token: 0x0601078C RID: 67468 RVA: 0x0038BCDF File Offset: 0x00389EDF
		public bool Is_ConstSequenceLabelNode(GrammarBuilders g, out ConstSequenceLabelNode value)
		{
			if (this.Node.GrammarRule == g.Rule.ConstSequenceLabelNode)
			{
				value = ConstSequenceLabelNode.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConstSequenceLabelNode);
			return false;
		}

		// Token: 0x0601078D RID: 67469 RVA: 0x0038BD14 File Offset: 0x00389F14
		public ConstSequenceLabelNode? As_ConstSequenceLabelNode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConstSequenceLabelNode)
			{
				return null;
			}
			return new ConstSequenceLabelNode?(ConstSequenceLabelNode.CreateUnsafe(this.Node));
		}

		// Token: 0x0601078E RID: 67470 RVA: 0x0038BD54 File Offset: 0x00389F54
		public ConstSequenceLabelNode Cast_ConstSequenceLabelNode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConstSequenceLabelNode)
			{
				return ConstSequenceLabelNode.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConstSequenceLabelNode is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0601078F RID: 67471 RVA: 0x0038BDA9 File Offset: 0x00389FA9
		public bool Is_LeafConstSequenceLabelNode(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LeafConstSequenceLabelNode;
		}

		// Token: 0x06010790 RID: 67472 RVA: 0x0038BDC3 File Offset: 0x00389FC3
		public bool Is_LeafConstSequenceLabelNode(GrammarBuilders g, out LeafConstSequenceLabelNode value)
		{
			if (this.Node.GrammarRule == g.Rule.LeafConstSequenceLabelNode)
			{
				value = LeafConstSequenceLabelNode.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LeafConstSequenceLabelNode);
			return false;
		}

		// Token: 0x06010791 RID: 67473 RVA: 0x0038BDF8 File Offset: 0x00389FF8
		public LeafConstSequenceLabelNode? As_LeafConstSequenceLabelNode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LeafConstSequenceLabelNode)
			{
				return null;
			}
			return new LeafConstSequenceLabelNode?(LeafConstSequenceLabelNode.CreateUnsafe(this.Node));
		}

		// Token: 0x06010792 RID: 67474 RVA: 0x0038BE38 File Offset: 0x0038A038
		public LeafConstSequenceLabelNode Cast_LeafConstSequenceLabelNode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LeafConstSequenceLabelNode)
			{
				return LeafConstSequenceLabelNode.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LeafConstSequenceLabelNode is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06010793 RID: 67475 RVA: 0x0038BE90 File Offset: 0x0038A090
		public T Switch<T>(GrammarBuilders g, Func<LeafConstLabelNode, T> func0, Func<ConstLabelNode, T> func1, Func<ConstSequenceLabelNode, T> func2, Func<LeafConstSequenceLabelNode, T> func3)
		{
			LeafConstLabelNode leafConstLabelNode;
			if (this.Is_LeafConstLabelNode(g, out leafConstLabelNode))
			{
				return func0(leafConstLabelNode);
			}
			ConstLabelNode constLabelNode;
			if (this.Is_ConstLabelNode(g, out constLabelNode))
			{
				return func1(constLabelNode);
			}
			ConstSequenceLabelNode constSequenceLabelNode;
			if (this.Is_ConstSequenceLabelNode(g, out constSequenceLabelNode))
			{
				return func2(constSequenceLabelNode);
			}
			LeafConstSequenceLabelNode leafConstSequenceLabelNode;
			if (this.Is_LeafConstSequenceLabelNode(g, out leafConstSequenceLabelNode))
			{
				return func3(leafConstSequenceLabelNode);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol construction");
		}

		// Token: 0x06010794 RID: 67476 RVA: 0x0038BF10 File Offset: 0x0038A110
		public void Switch(GrammarBuilders g, Action<LeafConstLabelNode> func0, Action<ConstLabelNode> func1, Action<ConstSequenceLabelNode> func2, Action<LeafConstSequenceLabelNode> func3)
		{
			LeafConstLabelNode leafConstLabelNode;
			if (this.Is_LeafConstLabelNode(g, out leafConstLabelNode))
			{
				func0(leafConstLabelNode);
				return;
			}
			ConstLabelNode constLabelNode;
			if (this.Is_ConstLabelNode(g, out constLabelNode))
			{
				func1(constLabelNode);
				return;
			}
			ConstSequenceLabelNode constSequenceLabelNode;
			if (this.Is_ConstSequenceLabelNode(g, out constSequenceLabelNode))
			{
				func2(constSequenceLabelNode);
				return;
			}
			LeafConstSequenceLabelNode leafConstSequenceLabelNode;
			if (this.Is_LeafConstSequenceLabelNode(g, out leafConstSequenceLabelNode))
			{
				func3(leafConstSequenceLabelNode);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol construction");
		}

		// Token: 0x06010795 RID: 67477 RVA: 0x0038BF8F File Offset: 0x0038A18F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010796 RID: 67478 RVA: 0x0038BFA4 File Offset: 0x0038A1A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010797 RID: 67479 RVA: 0x0038BFCE File Offset: 0x0038A1CE
		public bool Equals(construction other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062BD RID: 25277
		private ProgramNode _node;
	}
}
