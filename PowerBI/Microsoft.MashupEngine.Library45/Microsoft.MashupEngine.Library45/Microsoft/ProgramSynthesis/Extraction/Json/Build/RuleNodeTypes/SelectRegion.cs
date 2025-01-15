using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes
{
	// Token: 0x02000B62 RID: 2914
	public struct SelectRegion : IProgramNodeBuilder, IEquatable<SelectRegion>
	{
		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x0600499A RID: 18842 RVA: 0x000E81BA File Offset: 0x000E63BA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x000E81C2 File Offset: 0x000E63C2
		private SelectRegion(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x000E81CB File Offset: 0x000E63CB
		public static SelectRegion CreateUnsafe(ProgramNode node)
		{
			return new SelectRegion(node);
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x000E81D4 File Offset: 0x000E63D4
		public static SelectRegion? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectRegion)
			{
				return null;
			}
			return new SelectRegion?(SelectRegion.CreateUnsafe(node));
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x000E8209 File Offset: 0x000E6409
		public SelectRegion(GrammarBuilders g, v value0, path value1)
		{
			this._node = g.Rule.SelectRegion.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x000E822F File Offset: 0x000E642F
		public static implicit operator selectRegion(SelectRegion arg)
		{
			return selectRegion.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060049A0 RID: 18848 RVA: 0x000E823D File Offset: 0x000E643D
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060049A1 RID: 18849 RVA: 0x000E8251 File Offset: 0x000E6451
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x000E8265 File Offset: 0x000E6465
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x000E8278 File Offset: 0x000E6478
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x000E82A2 File Offset: 0x000E64A2
		public bool Equals(SelectRegion other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400215D RID: 8541
		private ProgramNode _node;
	}
}
