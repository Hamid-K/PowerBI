using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001373 RID: 4979
	public struct _LetB1 : IProgramNodeBuilder, IEquatable<_LetB1>
	{
		// Token: 0x17001A7C RID: 6780
		// (get) Token: 0x06009A6B RID: 39531 RVA: 0x0020AEE2 File Offset: 0x002090E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A6C RID: 39532 RVA: 0x0020AEEA File Offset: 0x002090EA
		private _LetB1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A6D RID: 39533 RVA: 0x0020AEF3 File Offset: 0x002090F3
		public static _LetB1 CreateUnsafe(ProgramNode node)
		{
			return new _LetB1(node);
		}

		// Token: 0x06009A6E RID: 39534 RVA: 0x0020AEFC File Offset: 0x002090FC
		public static _LetB1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB1)
			{
				return null;
			}
			return new _LetB1?(_LetB1.CreateUnsafe(node));
		}

		// Token: 0x06009A6F RID: 39535 RVA: 0x0020AF36 File Offset: 0x00209136
		public static _LetB1 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB1(new Hole(g.Symbol._LetB1, holeId));
		}

		// Token: 0x06009A70 RID: 39536 RVA: 0x0020AF4E File Offset: 0x0020914E
		public Append Cast_Append()
		{
			return Append.CreateUnsafe(this.Node);
		}

		// Token: 0x06009A71 RID: 39537 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Append(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06009A72 RID: 39538 RVA: 0x0020AF5B File Offset: 0x0020915B
		public bool Is_Append(GrammarBuilders g, out Append value)
		{
			value = Append.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06009A73 RID: 39539 RVA: 0x0020AF6F File Offset: 0x0020916F
		public Append? As_Append(GrammarBuilders g)
		{
			return new Append?(Append.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A74 RID: 39540 RVA: 0x0020AF81 File Offset: 0x00209181
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009A75 RID: 39541 RVA: 0x0020AF94 File Offset: 0x00209194
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009A76 RID: 39542 RVA: 0x0020AFBE File Offset: 0x002091BE
		public bool Equals(_LetB1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DEA RID: 15850
		private ProgramNode _node;
	}
}
