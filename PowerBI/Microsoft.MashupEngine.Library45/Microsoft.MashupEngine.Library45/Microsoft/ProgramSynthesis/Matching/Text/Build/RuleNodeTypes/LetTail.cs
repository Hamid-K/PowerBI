using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011EE RID: 4590
	public struct LetTail : IProgramNodeBuilder, IEquatable<LetTail>
	{
		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x06008A00 RID: 35328 RVA: 0x001CFF56 File Offset: 0x001CE156
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008A01 RID: 35329 RVA: 0x001CFF5E File Offset: 0x001CE15E
		private LetTail(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008A02 RID: 35330 RVA: 0x001CFF67 File Offset: 0x001CE167
		public static LetTail CreateUnsafe(ProgramNode node)
		{
			return new LetTail(node);
		}

		// Token: 0x06008A03 RID: 35331 RVA: 0x001CFF70 File Offset: 0x001CE170
		public static LetTail? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetTail)
			{
				return null;
			}
			return new LetTail?(LetTail.CreateUnsafe(node));
		}

		// Token: 0x06008A04 RID: 35332 RVA: 0x001CFFA5 File Offset: 0x001CE1A5
		public LetTail(GrammarBuilders g, _LetB1 value0, _LetB2 value1)
		{
			this._node = new LetNode(g.Rule.LetTail, value0.Node, value1.Node);
		}

		// Token: 0x06008A05 RID: 35333 RVA: 0x001CFFCB File Offset: 0x001CE1CB
		public static implicit operator _LetB4(LetTail arg)
		{
			return _LetB4.CreateUnsafe(arg.Node);
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x06008A06 RID: 35334 RVA: 0x001CFFD9 File Offset: 0x001CE1D9
		public _LetB1 _LetB1
		{
			get
			{
				return _LetB1.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x06008A07 RID: 35335 RVA: 0x001CFFED File Offset: 0x001CE1ED
		public _LetB2 _LetB2
		{
			get
			{
				return _LetB2.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06008A08 RID: 35336 RVA: 0x001D0001 File Offset: 0x001CE201
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008A09 RID: 35337 RVA: 0x001D0014 File Offset: 0x001CE214
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008A0A RID: 35338 RVA: 0x001D003E File Offset: 0x001CE23E
		public bool Equals(LetTail other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038A2 RID: 14498
		private ProgramNode _node;
	}
}
