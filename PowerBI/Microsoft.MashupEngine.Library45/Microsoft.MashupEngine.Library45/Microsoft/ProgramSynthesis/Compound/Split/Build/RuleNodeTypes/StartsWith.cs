using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000951 RID: 2385
	public struct StartsWith : IProgramNodeBuilder, IEquatable<StartsWith>
	{
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x000AE58A File Offset: 0x000AC78A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000AE592 File Offset: 0x000AC792
		private StartsWith(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000AE59B File Offset: 0x000AC79B
		public static StartsWith CreateUnsafe(ProgramNode node)
		{
			return new StartsWith(node);
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x000AE5A4 File Offset: 0x000AC7A4
		public static StartsWith? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.StartsWith)
			{
				return null;
			}
			return new StartsWith?(StartsWith.CreateUnsafe(node));
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x000AE5D9 File Offset: 0x000AC7D9
		public StartsWith(GrammarBuilders g, s value0, r value1)
		{
			this._node = g.Rule.StartsWith.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000AE5FF File Offset: 0x000AC7FF
		public static implicit operator basicLinePredicate(StartsWith arg)
		{
			return basicLinePredicate.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060037C8 RID: 14280 RVA: 0x000AE60D File Offset: 0x000AC80D
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060037C9 RID: 14281 RVA: 0x000AE621 File Offset: 0x000AC821
		public r r
		{
			get
			{
				return r.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000AE635 File Offset: 0x000AC835
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000AE648 File Offset: 0x000AC848
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000AE672 File Offset: 0x000AC872
		public bool Equals(StartsWith other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A71 RID: 6769
		private ProgramNode _node;
	}
}
