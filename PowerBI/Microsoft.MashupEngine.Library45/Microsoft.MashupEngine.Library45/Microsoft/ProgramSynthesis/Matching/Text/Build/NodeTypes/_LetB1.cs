using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011F8 RID: 4600
	public struct _LetB1 : IProgramNodeBuilder, IEquatable<_LetB1>
	{
		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x06008A98 RID: 35480 RVA: 0x001D1556 File Offset: 0x001CF756
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008A99 RID: 35481 RVA: 0x001D155E File Offset: 0x001CF75E
		private _LetB1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008A9A RID: 35482 RVA: 0x001D1567 File Offset: 0x001CF767
		public static _LetB1 CreateUnsafe(ProgramNode node)
		{
			return new _LetB1(node);
		}

		// Token: 0x06008A9B RID: 35483 RVA: 0x001D1570 File Offset: 0x001CF770
		public static _LetB1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB1)
			{
				return null;
			}
			return new _LetB1?(_LetB1.CreateUnsafe(node));
		}

		// Token: 0x06008A9C RID: 35484 RVA: 0x001D15AA File Offset: 0x001CF7AA
		public static _LetB1 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB1(new Hole(g.Symbol._LetB1, holeId));
		}

		// Token: 0x06008A9D RID: 35485 RVA: 0x001D15C2 File Offset: 0x001CF7C2
		public Tail Cast_Tail()
		{
			return Tail.CreateUnsafe(this.Node);
		}

		// Token: 0x06008A9E RID: 35486 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Tail(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06008A9F RID: 35487 RVA: 0x001D15CF File Offset: 0x001CF7CF
		public bool Is_Tail(GrammarBuilders g, out Tail value)
		{
			value = Tail.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06008AA0 RID: 35488 RVA: 0x001D15E3 File Offset: 0x001CF7E3
		public Tail? As_Tail(GrammarBuilders g)
		{
			return new Tail?(Tail.CreateUnsafe(this.Node));
		}

		// Token: 0x06008AA1 RID: 35489 RVA: 0x001D15F5 File Offset: 0x001CF7F5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008AA2 RID: 35490 RVA: 0x001D1608 File Offset: 0x001CF808
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008AA3 RID: 35491 RVA: 0x001D1632 File Offset: 0x001CF832
		public bool Equals(_LetB1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038AC RID: 14508
		private ProgramNode _node;
	}
}
