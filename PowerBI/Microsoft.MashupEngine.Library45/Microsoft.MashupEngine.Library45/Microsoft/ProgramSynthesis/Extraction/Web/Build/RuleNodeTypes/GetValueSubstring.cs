using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001034 RID: 4148
	public struct GetValueSubstring : IProgramNodeBuilder, IEquatable<GetValueSubstring>
	{
		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06007AAF RID: 31407 RVA: 0x001A230E File Offset: 0x001A050E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007AB0 RID: 31408 RVA: 0x001A2316 File Offset: 0x001A0516
		private GetValueSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007AB1 RID: 31409 RVA: 0x001A231F File Offset: 0x001A051F
		public static GetValueSubstring CreateUnsafe(ProgramNode node)
		{
			return new GetValueSubstring(node);
		}

		// Token: 0x06007AB2 RID: 31410 RVA: 0x001A2328 File Offset: 0x001A0528
		public static GetValueSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.GetValueSubstring)
			{
				return null;
			}
			return new GetValueSubstring?(GetValueSubstring.CreateUnsafe(node));
		}

		// Token: 0x06007AB3 RID: 31411 RVA: 0x001A235D File Offset: 0x001A055D
		public GetValueSubstring(GrammarBuilders g, resultRegion value0)
		{
			this._node = g.Rule.GetValueSubstring.BuildASTNode(value0.Node);
		}

		// Token: 0x06007AB4 RID: 31412 RVA: 0x001A237C File Offset: 0x001A057C
		public static implicit operator y(GetValueSubstring arg)
		{
			return y.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06007AB5 RID: 31413 RVA: 0x001A238A File Offset: 0x001A058A
		public resultRegion resultRegion
		{
			get
			{
				return resultRegion.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007AB6 RID: 31414 RVA: 0x001A239E File Offset: 0x001A059E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007AB7 RID: 31415 RVA: 0x001A23B4 File Offset: 0x001A05B4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007AB8 RID: 31416 RVA: 0x001A23DE File Offset: 0x001A05DE
		public bool Equals(GetValueSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400334D RID: 13133
		private ProgramNode _node;
	}
}
