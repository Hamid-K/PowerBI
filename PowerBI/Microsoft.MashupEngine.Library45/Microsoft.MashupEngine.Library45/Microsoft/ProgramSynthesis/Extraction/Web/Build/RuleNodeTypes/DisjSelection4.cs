using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200101F RID: 4127
	public struct DisjSelection4 : IProgramNodeBuilder, IEquatable<DisjSelection4>
	{
		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x060079CB RID: 31179 RVA: 0x001A0E9E File Offset: 0x0019F09E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060079CC RID: 31180 RVA: 0x001A0EA6 File Offset: 0x0019F0A6
		private DisjSelection4(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060079CD RID: 31181 RVA: 0x001A0EAF File Offset: 0x0019F0AF
		public static DisjSelection4 CreateUnsafe(ProgramNode node)
		{
			return new DisjSelection4(node);
		}

		// Token: 0x060079CE RID: 31182 RVA: 0x001A0EB8 File Offset: 0x0019F0B8
		public static DisjSelection4? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DisjSelection4)
			{
				return null;
			}
			return new DisjSelection4?(DisjSelection4.CreateUnsafe(node));
		}

		// Token: 0x060079CF RID: 31183 RVA: 0x001A0EED File Offset: 0x0019F0ED
		public DisjSelection4(GrammarBuilders g, selection7 value0, filterSelection4 value1)
		{
			this._node = g.Rule.DisjSelection4.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060079D0 RID: 31184 RVA: 0x001A0F13 File Offset: 0x0019F113
		public static implicit operator selection7(DisjSelection4 arg)
		{
			return selection7.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x060079D1 RID: 31185 RVA: 0x001A0F21 File Offset: 0x0019F121
		public selection7 selection7
		{
			get
			{
				return selection7.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x060079D2 RID: 31186 RVA: 0x001A0F35 File Offset: 0x0019F135
		public filterSelection4 filterSelection4
		{
			get
			{
				return filterSelection4.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060079D3 RID: 31187 RVA: 0x001A0F49 File Offset: 0x0019F149
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060079D4 RID: 31188 RVA: 0x001A0F5C File Offset: 0x0019F15C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060079D5 RID: 31189 RVA: 0x001A0F86 File Offset: 0x0019F186
		public bool Equals(DisjSelection4 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003338 RID: 13112
		private ProgramNode _node;
	}
}
