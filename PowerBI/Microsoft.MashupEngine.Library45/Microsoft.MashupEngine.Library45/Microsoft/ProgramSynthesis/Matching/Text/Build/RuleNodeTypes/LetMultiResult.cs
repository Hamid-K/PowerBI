using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011ED RID: 4589
	public struct LetMultiResult : IProgramNodeBuilder, IEquatable<LetMultiResult>
	{
		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x060089F5 RID: 35317 RVA: 0x001CFE5A File Offset: 0x001CE05A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060089F6 RID: 35318 RVA: 0x001CFE62 File Offset: 0x001CE062
		private LetMultiResult(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060089F7 RID: 35319 RVA: 0x001CFE6B File Offset: 0x001CE06B
		public static LetMultiResult CreateUnsafe(ProgramNode node)
		{
			return new LetMultiResult(node);
		}

		// Token: 0x060089F8 RID: 35320 RVA: 0x001CFE74 File Offset: 0x001CE074
		public static LetMultiResult? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetMultiResult)
			{
				return null;
			}
			return new LetMultiResult?(LetMultiResult.CreateUnsafe(node));
		}

		// Token: 0x060089F9 RID: 35321 RVA: 0x001CFEA9 File Offset: 0x001CE0A9
		public LetMultiResult(GrammarBuilders g, inputSRegions value0, multi_result_matches value1)
		{
			this._node = new LetNode(g.Rule.LetMultiResult, value0.Node, value1.Node);
		}

		// Token: 0x060089FA RID: 35322 RVA: 0x001CFECF File Offset: 0x001CE0CF
		public static implicit operator multi_result(LetMultiResult arg)
		{
			return multi_result.CreateUnsafe(arg.Node);
		}

		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x060089FB RID: 35323 RVA: 0x001CFEDD File Offset: 0x001CE0DD
		public inputSRegions inputSRegions
		{
			get
			{
				return inputSRegions.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x060089FC RID: 35324 RVA: 0x001CFEF1 File Offset: 0x001CE0F1
		public multi_result_matches multi_result_matches
		{
			get
			{
				return multi_result_matches.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060089FD RID: 35325 RVA: 0x001CFF05 File Offset: 0x001CE105
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060089FE RID: 35326 RVA: 0x001CFF18 File Offset: 0x001CE118
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060089FF RID: 35327 RVA: 0x001CFF42 File Offset: 0x001CE142
		public bool Equals(LetMultiResult other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038A1 RID: 14497
		private ProgramNode _node;
	}
}
