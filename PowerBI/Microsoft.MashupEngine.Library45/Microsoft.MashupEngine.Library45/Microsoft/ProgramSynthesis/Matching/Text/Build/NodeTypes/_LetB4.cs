using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011FB RID: 4603
	public struct _LetB4 : IProgramNodeBuilder, IEquatable<_LetB4>
	{
		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x06008ABC RID: 35516 RVA: 0x001D1826 File Offset: 0x001CFA26
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008ABD RID: 35517 RVA: 0x001D182E File Offset: 0x001CFA2E
		private _LetB4(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008ABE RID: 35518 RVA: 0x001D1837 File Offset: 0x001CFA37
		public static _LetB4 CreateUnsafe(ProgramNode node)
		{
			return new _LetB4(node);
		}

		// Token: 0x06008ABF RID: 35519 RVA: 0x001D1840 File Offset: 0x001CFA40
		public static _LetB4? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB4)
			{
				return null;
			}
			return new _LetB4?(_LetB4.CreateUnsafe(node));
		}

		// Token: 0x06008AC0 RID: 35520 RVA: 0x001D187A File Offset: 0x001CFA7A
		public static _LetB4 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB4(new Hole(g.Symbol._LetB4, holeId));
		}

		// Token: 0x06008AC1 RID: 35521 RVA: 0x001D1892 File Offset: 0x001CFA92
		public LetTail Cast_LetTail()
		{
			return LetTail.CreateUnsafe(this.Node);
		}

		// Token: 0x06008AC2 RID: 35522 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetTail(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06008AC3 RID: 35523 RVA: 0x001D189F File Offset: 0x001CFA9F
		public bool Is_LetTail(GrammarBuilders g, out LetTail value)
		{
			value = LetTail.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06008AC4 RID: 35524 RVA: 0x001D18B3 File Offset: 0x001CFAB3
		public LetTail? As_LetTail(GrammarBuilders g)
		{
			return new LetTail?(LetTail.CreateUnsafe(this.Node));
		}

		// Token: 0x06008AC5 RID: 35525 RVA: 0x001D18C5 File Offset: 0x001CFAC5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008AC6 RID: 35526 RVA: 0x001D18D8 File Offset: 0x001CFAD8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008AC7 RID: 35527 RVA: 0x001D1902 File Offset: 0x001CFB02
		public bool Equals(_LetB4 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038AF RID: 14511
		private ProgramNode _node;
	}
}
