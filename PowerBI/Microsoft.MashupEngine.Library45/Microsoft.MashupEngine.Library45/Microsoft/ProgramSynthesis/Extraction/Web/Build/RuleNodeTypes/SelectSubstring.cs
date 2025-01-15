using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001035 RID: 4149
	public struct SelectSubstring : IProgramNodeBuilder, IEquatable<SelectSubstring>
	{
		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06007AB9 RID: 31417 RVA: 0x001A23F2 File Offset: 0x001A05F2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007ABA RID: 31418 RVA: 0x001A23FA File Offset: 0x001A05FA
		private SelectSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007ABB RID: 31419 RVA: 0x001A2403 File Offset: 0x001A0603
		public static SelectSubstring CreateUnsafe(ProgramNode node)
		{
			return new SelectSubstring(node);
		}

		// Token: 0x06007ABC RID: 31420 RVA: 0x001A240C File Offset: 0x001A060C
		public static SelectSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectSubstring)
			{
				return null;
			}
			return new SelectSubstring?(SelectSubstring.CreateUnsafe(node));
		}

		// Token: 0x06007ABD RID: 31421 RVA: 0x001A2441 File Offset: 0x001A0641
		public SelectSubstring(GrammarBuilders g, substringDisj value0, substringFeatureNames value1, substringFeatureValues value2)
		{
			this._node = g.Rule.SelectSubstring.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06007ABE RID: 31422 RVA: 0x001A246E File Offset: 0x001A066E
		public static implicit operator selectSubstring(SelectSubstring arg)
		{
			return selectSubstring.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x06007ABF RID: 31423 RVA: 0x001A247C File Offset: 0x001A067C
		public substringDisj substringDisj
		{
			get
			{
				return substringDisj.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06007AC0 RID: 31424 RVA: 0x001A2490 File Offset: 0x001A0690
		public substringFeatureNames substringFeatureNames
		{
			get
			{
				return substringFeatureNames.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x06007AC1 RID: 31425 RVA: 0x001A24A4 File Offset: 0x001A06A4
		public substringFeatureValues substringFeatureValues
		{
			get
			{
				return substringFeatureValues.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06007AC2 RID: 31426 RVA: 0x001A24B8 File Offset: 0x001A06B8
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007AC3 RID: 31427 RVA: 0x001A24CC File Offset: 0x001A06CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007AC4 RID: 31428 RVA: 0x001A24F6 File Offset: 0x001A06F6
		public bool Equals(SelectSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400334E RID: 13134
		private ProgramNode _node;
	}
}
