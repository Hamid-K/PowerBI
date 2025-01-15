using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200101C RID: 4124
	public struct DisjSelection3 : IProgramNodeBuilder, IEquatable<DisjSelection3>
	{
		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x060079AC RID: 31148 RVA: 0x001A0BDA File Offset: 0x0019EDDA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060079AD RID: 31149 RVA: 0x001A0BE2 File Offset: 0x0019EDE2
		private DisjSelection3(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060079AE RID: 31150 RVA: 0x001A0BEB File Offset: 0x0019EDEB
		public static DisjSelection3 CreateUnsafe(ProgramNode node)
		{
			return new DisjSelection3(node);
		}

		// Token: 0x060079AF RID: 31151 RVA: 0x001A0BF4 File Offset: 0x0019EDF4
		public static DisjSelection3? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DisjSelection3)
			{
				return null;
			}
			return new DisjSelection3?(DisjSelection3.CreateUnsafe(node));
		}

		// Token: 0x060079B0 RID: 31152 RVA: 0x001A0C29 File Offset: 0x0019EE29
		public DisjSelection3(GrammarBuilders g, selection5 value0, filterSelection3 value1)
		{
			this._node = g.Rule.DisjSelection3.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060079B1 RID: 31153 RVA: 0x001A0C4F File Offset: 0x0019EE4F
		public static implicit operator selection5(DisjSelection3 arg)
		{
			return selection5.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x060079B2 RID: 31154 RVA: 0x001A0C5D File Offset: 0x0019EE5D
		public selection5 selection5
		{
			get
			{
				return selection5.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x060079B3 RID: 31155 RVA: 0x001A0C71 File Offset: 0x0019EE71
		public filterSelection3 filterSelection3
		{
			get
			{
				return filterSelection3.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060079B4 RID: 31156 RVA: 0x001A0C85 File Offset: 0x0019EE85
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060079B5 RID: 31157 RVA: 0x001A0C98 File Offset: 0x0019EE98
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060079B6 RID: 31158 RVA: 0x001A0CC2 File Offset: 0x0019EEC2
		public bool Equals(DisjSelection3 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003335 RID: 13109
		private ProgramNode _node;
	}
}
