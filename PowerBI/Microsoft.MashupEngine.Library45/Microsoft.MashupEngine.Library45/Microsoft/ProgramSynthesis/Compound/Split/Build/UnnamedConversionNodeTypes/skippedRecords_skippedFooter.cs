using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000939 RID: 2361
	public struct skippedRecords_skippedFooter : IProgramNodeBuilder, IEquatable<skippedRecords_skippedFooter>
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x060036B3 RID: 14003 RVA: 0x000ACC62 File Offset: 0x000AAE62
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x000ACC6A File Offset: 0x000AAE6A
		private skippedRecords_skippedFooter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000ACC73 File Offset: 0x000AAE73
		public static skippedRecords_skippedFooter CreateUnsafe(ProgramNode node)
		{
			return new skippedRecords_skippedFooter(node);
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x000ACC7C File Offset: 0x000AAE7C
		public static skippedRecords_skippedFooter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.skippedRecords_skippedFooter)
			{
				return null;
			}
			return new skippedRecords_skippedFooter?(skippedRecords_skippedFooter.CreateUnsafe(node));
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x000ACCB1 File Offset: 0x000AAEB1
		public skippedRecords_skippedFooter(GrammarBuilders g, skippedFooter value0)
		{
			this._node = g.UnnamedConversion.skippedRecords_skippedFooter.BuildASTNode(value0.Node);
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x000ACCD0 File Offset: 0x000AAED0
		public static implicit operator skippedRecords(skippedRecords_skippedFooter arg)
		{
			return skippedRecords.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x060036B9 RID: 14009 RVA: 0x000ACCDE File Offset: 0x000AAEDE
		public skippedFooter skippedFooter
		{
			get
			{
				return skippedFooter.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x000ACCF2 File Offset: 0x000AAEF2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x000ACD08 File Offset: 0x000AAF08
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x000ACD32 File Offset: 0x000AAF32
		public bool Equals(skippedRecords_skippedFooter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A59 RID: 6745
		private ProgramNode _node;
	}
}
