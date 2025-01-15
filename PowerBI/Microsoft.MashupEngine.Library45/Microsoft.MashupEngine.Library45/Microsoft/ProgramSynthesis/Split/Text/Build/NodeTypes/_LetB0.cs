using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001372 RID: 4978
	public struct _LetB0 : IProgramNodeBuilder, IEquatable<_LetB0>
	{
		// Token: 0x17001A7B RID: 6779
		// (get) Token: 0x06009A5F RID: 39519 RVA: 0x0020ADF2 File Offset: 0x00208FF2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A60 RID: 39520 RVA: 0x0020ADFA File Offset: 0x00208FFA
		private _LetB0(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A61 RID: 39521 RVA: 0x0020AE03 File Offset: 0x00209003
		public static _LetB0 CreateUnsafe(ProgramNode node)
		{
			return new _LetB0(node);
		}

		// Token: 0x06009A62 RID: 39522 RVA: 0x0020AE0C File Offset: 0x0020900C
		public static _LetB0? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB0)
			{
				return null;
			}
			return new _LetB0?(_LetB0.CreateUnsafe(node));
		}

		// Token: 0x06009A63 RID: 39523 RVA: 0x0020AE46 File Offset: 0x00209046
		public static _LetB0 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB0(new Hole(g.Symbol._LetB0, holeId));
		}

		// Token: 0x06009A64 RID: 39524 RVA: 0x0020AE5E File Offset: 0x0020905E
		public Item2 Cast_Item2()
		{
			return Item2.CreateUnsafe(this.Node);
		}

		// Token: 0x06009A65 RID: 39525 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Item2(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06009A66 RID: 39526 RVA: 0x0020AE6B File Offset: 0x0020906B
		public bool Is_Item2(GrammarBuilders g, out Item2 value)
		{
			value = Item2.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06009A67 RID: 39527 RVA: 0x0020AE7F File Offset: 0x0020907F
		public Item2? As_Item2(GrammarBuilders g)
		{
			return new Item2?(Item2.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A68 RID: 39528 RVA: 0x0020AE91 File Offset: 0x00209091
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009A69 RID: 39529 RVA: 0x0020AEA4 File Offset: 0x002090A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009A6A RID: 39530 RVA: 0x0020AECE File Offset: 0x002090CE
		public bool Equals(_LetB0 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DE9 RID: 15849
		private ProgramNode _node;
	}
}
