using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C0D RID: 7181
	public struct DtRangeConstStr : IProgramNodeBuilder, IEquatable<DtRangeConstStr>
	{
		// Token: 0x17002851 RID: 10321
		// (get) Token: 0x0600F198 RID: 61848 RVA: 0x0033FD06 File Offset: 0x0033DF06
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F199 RID: 61849 RVA: 0x0033FD0E File Offset: 0x0033DF0E
		private DtRangeConstStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F19A RID: 61850 RVA: 0x0033FD17 File Offset: 0x0033DF17
		public static DtRangeConstStr CreateUnsafe(ProgramNode node)
		{
			return new DtRangeConstStr(node);
		}

		// Token: 0x0600F19B RID: 61851 RVA: 0x0033FD20 File Offset: 0x0033DF20
		public static DtRangeConstStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DtRangeConstStr)
			{
				return null;
			}
			return new DtRangeConstStr?(DtRangeConstStr.CreateUnsafe(node));
		}

		// Token: 0x0600F19C RID: 61852 RVA: 0x0033FD55 File Offset: 0x0033DF55
		public DtRangeConstStr(GrammarBuilders g, s value0)
		{
			this._node = g.Rule.DtRangeConstStr.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F19D RID: 61853 RVA: 0x0033FD74 File Offset: 0x0033DF74
		public static implicit operator dtRangeSubstring(DtRangeConstStr arg)
		{
			return dtRangeSubstring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002852 RID: 10322
		// (get) Token: 0x0600F19E RID: 61854 RVA: 0x0033FD82 File Offset: 0x0033DF82
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F19F RID: 61855 RVA: 0x0033FD96 File Offset: 0x0033DF96
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F1A0 RID: 61856 RVA: 0x0033FDAC File Offset: 0x0033DFAC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F1A1 RID: 61857 RVA: 0x0033FDD6 File Offset: 0x0033DFD6
		public bool Equals(DtRangeConstStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AFC RID: 23292
		private ProgramNode _node;
	}
}
