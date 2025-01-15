using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011EC RID: 4588
	public struct LetSplit : IProgramNodeBuilder, IEquatable<LetSplit>
	{
		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x060089EA RID: 35306 RVA: 0x001CFD5E File Offset: 0x001CDF5E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060089EB RID: 35307 RVA: 0x001CFD66 File Offset: 0x001CDF66
		private LetSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060089EC RID: 35308 RVA: 0x001CFD6F File Offset: 0x001CDF6F
		public static LetSplit CreateUnsafe(ProgramNode node)
		{
			return new LetSplit(node);
		}

		// Token: 0x060089ED RID: 35309 RVA: 0x001CFD78 File Offset: 0x001CDF78
		public static LetSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetSplit)
			{
				return null;
			}
			return new LetSplit?(LetSplit.CreateUnsafe(node));
		}

		// Token: 0x060089EE RID: 35310 RVA: 0x001CFDAD File Offset: 0x001CDFAD
		public LetSplit(GrammarBuilders g, _LetB0 value0, match value1)
		{
			this._node = new LetNode(g.Rule.LetSplit, value0.Node, value1.Node);
		}

		// Token: 0x060089EF RID: 35311 RVA: 0x001CFDD3 File Offset: 0x001CDFD3
		public static implicit operator match(LetSplit arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x060089F0 RID: 35312 RVA: 0x001CFDE1 File Offset: 0x001CDFE1
		public _LetB0 _LetB0
		{
			get
			{
				return _LetB0.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x060089F1 RID: 35313 RVA: 0x001CFDF5 File Offset: 0x001CDFF5
		public match match
		{
			get
			{
				return match.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060089F2 RID: 35314 RVA: 0x001CFE09 File Offset: 0x001CE009
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060089F3 RID: 35315 RVA: 0x001CFE1C File Offset: 0x001CE01C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060089F4 RID: 35316 RVA: 0x001CFE46 File Offset: 0x001CE046
		public bool Equals(LetSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038A0 RID: 14496
		private ProgramNode _node;
	}
}
