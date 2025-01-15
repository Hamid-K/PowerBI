using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001046 RID: 4166
	public struct GEN_NthLastChildFilter : IProgramNodeBuilder, IEquatable<GEN_NthLastChildFilter>
	{
		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06007B6F RID: 31599 RVA: 0x001A343A File Offset: 0x001A163A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B70 RID: 31600 RVA: 0x001A3442 File Offset: 0x001A1642
		private GEN_NthLastChildFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B71 RID: 31601 RVA: 0x001A344B File Offset: 0x001A164B
		public static GEN_NthLastChildFilter CreateUnsafe(ProgramNode node)
		{
			return new GEN_NthLastChildFilter(node);
		}

		// Token: 0x06007B72 RID: 31602 RVA: 0x001A3454 File Offset: 0x001A1654
		public static GEN_NthLastChildFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.GEN_NthLastChildFilter)
			{
				return null;
			}
			return new GEN_NthLastChildFilter?(GEN_NthLastChildFilter.CreateUnsafe(node));
		}

		// Token: 0x06007B73 RID: 31603 RVA: 0x001A3489 File Offset: 0x001A1689
		public GEN_NthLastChildFilter(GrammarBuilders g, obj value0, obj value1)
		{
			this._node = g.Rule.GEN_NthLastChildFilter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007B74 RID: 31604 RVA: 0x001A34AF File Offset: 0x001A16AF
		public static implicit operator gen_NthLastChild(GEN_NthLastChildFilter arg)
		{
			return gen_NthLastChild.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06007B75 RID: 31605 RVA: 0x001A34BD File Offset: 0x001A16BD
		public obj obj1
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06007B76 RID: 31606 RVA: 0x001A34D1 File Offset: 0x001A16D1
		public obj obj2
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007B77 RID: 31607 RVA: 0x001A34E5 File Offset: 0x001A16E5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B78 RID: 31608 RVA: 0x001A34F8 File Offset: 0x001A16F8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B79 RID: 31609 RVA: 0x001A3522 File Offset: 0x001A1722
		public bool Equals(GEN_NthLastChildFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400335F RID: 13151
		private ProgramNode _node;
	}
}
