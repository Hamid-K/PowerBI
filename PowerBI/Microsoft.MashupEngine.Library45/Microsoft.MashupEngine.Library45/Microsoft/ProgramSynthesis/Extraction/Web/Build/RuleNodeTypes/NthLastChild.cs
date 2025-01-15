using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200102B RID: 4139
	public struct NthLastChild : IProgramNodeBuilder, IEquatable<NthLastChild>
	{
		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06007A4B RID: 31307 RVA: 0x001A1A0E File Offset: 0x0019FC0E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A4C RID: 31308 RVA: 0x001A1A16 File Offset: 0x0019FC16
		private NthLastChild(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A4D RID: 31309 RVA: 0x001A1A1F File Offset: 0x0019FC1F
		public static NthLastChild CreateUnsafe(ProgramNode node)
		{
			return new NthLastChild(node);
		}

		// Token: 0x06007A4E RID: 31310 RVA: 0x001A1A28 File Offset: 0x0019FC28
		public static NthLastChild? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NthLastChild)
			{
				return null;
			}
			return new NthLastChild?(NthLastChild.CreateUnsafe(node));
		}

		// Token: 0x06007A4F RID: 31311 RVA: 0x001A1A5D File Offset: 0x0019FC5D
		public NthLastChild(GrammarBuilders g, idx2 value0, node value1)
		{
			this._node = g.Rule.NthLastChild.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007A50 RID: 31312 RVA: 0x001A1A83 File Offset: 0x0019FC83
		public static implicit operator atomExpr(NthLastChild arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06007A51 RID: 31313 RVA: 0x001A1A91 File Offset: 0x0019FC91
		public idx2 idx2
		{
			get
			{
				return idx2.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x06007A52 RID: 31314 RVA: 0x001A1AA5 File Offset: 0x0019FCA5
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007A53 RID: 31315 RVA: 0x001A1AB9 File Offset: 0x0019FCB9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A54 RID: 31316 RVA: 0x001A1ACC File Offset: 0x0019FCCC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A55 RID: 31317 RVA: 0x001A1AF6 File Offset: 0x0019FCF6
		public bool Equals(NthLastChild other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003344 RID: 13124
		private ProgramNode _node;
	}
}
