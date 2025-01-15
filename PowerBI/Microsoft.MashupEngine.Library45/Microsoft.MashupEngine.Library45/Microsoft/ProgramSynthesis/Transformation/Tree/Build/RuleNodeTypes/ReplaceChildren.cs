using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E6D RID: 7789
	public struct ReplaceChildren : IProgramNodeBuilder, IEquatable<ReplaceChildren>
	{
		// Token: 0x17002BB1 RID: 11185
		// (get) Token: 0x060106A6 RID: 67238 RVA: 0x0038A03A File Offset: 0x0038823A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060106A7 RID: 67239 RVA: 0x0038A042 File Offset: 0x00388242
		private ReplaceChildren(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060106A8 RID: 67240 RVA: 0x0038A04B File Offset: 0x0038824B
		public static ReplaceChildren CreateUnsafe(ProgramNode node)
		{
			return new ReplaceChildren(node);
		}

		// Token: 0x060106A9 RID: 67241 RVA: 0x0038A054 File Offset: 0x00388254
		public static ReplaceChildren? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ReplaceChildren)
			{
				return null;
			}
			return new ReplaceChildren?(ReplaceChildren.CreateUnsafe(node));
		}

		// Token: 0x060106AA RID: 67242 RVA: 0x0038A089 File Offset: 0x00388289
		public ReplaceChildren(GrammarBuilders g, select value0, relChildList value1, children value2)
		{
			this._node = g.Rule.ReplaceChildren.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x060106AB RID: 67243 RVA: 0x0038A0B6 File Offset: 0x003882B6
		public static implicit operator sequenceChildren(ReplaceChildren arg)
		{
			return sequenceChildren.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BB2 RID: 11186
		// (get) Token: 0x060106AC RID: 67244 RVA: 0x0038A0C4 File Offset: 0x003882C4
		public select select
		{
			get
			{
				return select.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002BB3 RID: 11187
		// (get) Token: 0x060106AD RID: 67245 RVA: 0x0038A0D8 File Offset: 0x003882D8
		public relChildList relChildList
		{
			get
			{
				return relChildList.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002BB4 RID: 11188
		// (get) Token: 0x060106AE RID: 67246 RVA: 0x0038A0EC File Offset: 0x003882EC
		public children children
		{
			get
			{
				return children.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x060106AF RID: 67247 RVA: 0x0038A100 File Offset: 0x00388300
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060106B0 RID: 67248 RVA: 0x0038A114 File Offset: 0x00388314
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060106B1 RID: 67249 RVA: 0x0038A13E File Offset: 0x0038833E
		public bool Equals(ReplaceChildren other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062AC RID: 25260
		private ProgramNode _node;
	}
}
