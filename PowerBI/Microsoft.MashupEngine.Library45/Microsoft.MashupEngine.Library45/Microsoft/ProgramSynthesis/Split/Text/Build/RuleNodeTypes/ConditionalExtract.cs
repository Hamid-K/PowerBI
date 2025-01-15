using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001345 RID: 4933
	public struct ConditionalExtract : IProgramNodeBuilder, IEquatable<ConditionalExtract>
	{
		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x06009804 RID: 38916 RVA: 0x0020629A File Offset: 0x0020449A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009805 RID: 38917 RVA: 0x002062A2 File Offset: 0x002044A2
		private ConditionalExtract(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009806 RID: 38918 RVA: 0x002062AB File Offset: 0x002044AB
		public static ConditionalExtract CreateUnsafe(ProgramNode node)
		{
			return new ConditionalExtract(node);
		}

		// Token: 0x06009807 RID: 38919 RVA: 0x002062B4 File Offset: 0x002044B4
		public static ConditionalExtract? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConditionalExtract)
			{
				return null;
			}
			return new ConditionalExtract?(ConditionalExtract.CreateUnsafe(node));
		}

		// Token: 0x06009808 RID: 38920 RVA: 0x002062E9 File Offset: 0x002044E9
		public ConditionalExtract(GrammarBuilders g, pred value0, extPoint value1, cndExtPoint value2)
		{
			this._node = g.Rule.ConditionalExtract.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06009809 RID: 38921 RVA: 0x00206316 File Offset: 0x00204516
		public static implicit operator cndExtPoint(ConditionalExtract arg)
		{
			return cndExtPoint.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x0600980A RID: 38922 RVA: 0x00206324 File Offset: 0x00204524
		public pred pred
		{
			get
			{
				return pred.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x0600980B RID: 38923 RVA: 0x00206338 File Offset: 0x00204538
		public extPoint extPoint
		{
			get
			{
				return extPoint.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001A1C RID: 6684
		// (get) Token: 0x0600980C RID: 38924 RVA: 0x0020634C File Offset: 0x0020454C
		public cndExtPoint cndExtPoint
		{
			get
			{
				return cndExtPoint.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600980D RID: 38925 RVA: 0x00206360 File Offset: 0x00204560
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600980E RID: 38926 RVA: 0x00206374 File Offset: 0x00204574
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600980F RID: 38927 RVA: 0x0020639E File Offset: 0x0020459E
		public bool Equals(ConditionalExtract other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DBC RID: 15804
		private ProgramNode _node;
	}
}
