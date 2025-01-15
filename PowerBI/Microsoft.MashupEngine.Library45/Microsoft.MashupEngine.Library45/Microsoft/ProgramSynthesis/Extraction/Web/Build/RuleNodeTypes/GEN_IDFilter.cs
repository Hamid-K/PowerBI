using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001048 RID: 4168
	public struct GEN_IDFilter : IProgramNodeBuilder, IEquatable<GEN_IDFilter>
	{
		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06007B85 RID: 31621 RVA: 0x001A3632 File Offset: 0x001A1832
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B86 RID: 31622 RVA: 0x001A363A File Offset: 0x001A183A
		private GEN_IDFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B87 RID: 31623 RVA: 0x001A3643 File Offset: 0x001A1843
		public static GEN_IDFilter CreateUnsafe(ProgramNode node)
		{
			return new GEN_IDFilter(node);
		}

		// Token: 0x06007B88 RID: 31624 RVA: 0x001A364C File Offset: 0x001A184C
		public static GEN_IDFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.GEN_IDFilter)
			{
				return null;
			}
			return new GEN_IDFilter?(GEN_IDFilter.CreateUnsafe(node));
		}

		// Token: 0x06007B89 RID: 31625 RVA: 0x001A3681 File Offset: 0x001A1881
		public GEN_IDFilter(GrammarBuilders g, obj value0, obj value1)
		{
			this._node = g.Rule.GEN_IDFilter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007B8A RID: 31626 RVA: 0x001A36A7 File Offset: 0x001A18A7
		public static implicit operator gen_ID(GEN_IDFilter arg)
		{
			return gen_ID.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06007B8B RID: 31627 RVA: 0x001A36B5 File Offset: 0x001A18B5
		public obj obj1
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06007B8C RID: 31628 RVA: 0x001A36C9 File Offset: 0x001A18C9
		public obj obj2
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007B8D RID: 31629 RVA: 0x001A36DD File Offset: 0x001A18DD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B8E RID: 31630 RVA: 0x001A36F0 File Offset: 0x001A18F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B8F RID: 31631 RVA: 0x001A371A File Offset: 0x001A191A
		public bool Equals(GEN_IDFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003361 RID: 13153
		private ProgramNode _node;
	}
}
