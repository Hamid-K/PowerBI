using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200094F RID: 2383
	public struct SkipFooter : IProgramNodeBuilder, IEquatable<SkipFooter>
	{
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060037AB RID: 14251 RVA: 0x000AE376 File Offset: 0x000AC576
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000AE37E File Offset: 0x000AC57E
		private SkipFooter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000AE387 File Offset: 0x000AC587
		public static SkipFooter CreateUnsafe(ProgramNode node)
		{
			return new SkipFooter(node);
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000AE390 File Offset: 0x000AC590
		public static SkipFooter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SkipFooter)
			{
				return null;
			}
			return new SkipFooter?(SkipFooter.CreateUnsafe(node));
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000AE3C5 File Offset: 0x000AC5C5
		public SkipFooter(GrammarBuilders g, k value0, allRecords value1)
		{
			this._node = g.Rule.SkipFooter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000AE3EB File Offset: 0x000AC5EB
		public static implicit operator skippedFooter(SkipFooter arg)
		{
			return skippedFooter.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060037B1 RID: 14257 RVA: 0x000AE3F9 File Offset: 0x000AC5F9
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x000AE40D File Offset: 0x000AC60D
		public allRecords allRecords
		{
			get
			{
				return allRecords.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000AE421 File Offset: 0x000AC621
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000AE434 File Offset: 0x000AC634
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000AE45E File Offset: 0x000AC65E
		public bool Equals(SkipFooter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A6F RID: 6767
		private ProgramNode _node;
	}
}
