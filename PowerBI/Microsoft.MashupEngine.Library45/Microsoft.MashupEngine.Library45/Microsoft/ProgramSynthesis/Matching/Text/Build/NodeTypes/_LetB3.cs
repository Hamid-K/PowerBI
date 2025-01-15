using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011FA RID: 4602
	public struct _LetB3 : IProgramNodeBuilder, IEquatable<_LetB3>
	{
		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x06008AB0 RID: 35504 RVA: 0x001D1736 File Offset: 0x001CF936
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008AB1 RID: 35505 RVA: 0x001D173E File Offset: 0x001CF93E
		private _LetB3(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008AB2 RID: 35506 RVA: 0x001D1747 File Offset: 0x001CF947
		public static _LetB3 CreateUnsafe(ProgramNode node)
		{
			return new _LetB3(node);
		}

		// Token: 0x06008AB3 RID: 35507 RVA: 0x001D1750 File Offset: 0x001CF950
		public static _LetB3? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB3)
			{
				return null;
			}
			return new _LetB3?(_LetB3.CreateUnsafe(node));
		}

		// Token: 0x06008AB4 RID: 35508 RVA: 0x001D178A File Offset: 0x001CF98A
		public static _LetB3 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB3(new Hole(g.Symbol._LetB3, holeId));
		}

		// Token: 0x06008AB5 RID: 35509 RVA: 0x001D17A2 File Offset: 0x001CF9A2
		public Head Cast_Head()
		{
			return Head.CreateUnsafe(this.Node);
		}

		// Token: 0x06008AB6 RID: 35510 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Head(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06008AB7 RID: 35511 RVA: 0x001D17AF File Offset: 0x001CF9AF
		public bool Is_Head(GrammarBuilders g, out Head value)
		{
			value = Head.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06008AB8 RID: 35512 RVA: 0x001D17C3 File Offset: 0x001CF9C3
		public Head? As_Head(GrammarBuilders g)
		{
			return new Head?(Head.CreateUnsafe(this.Node));
		}

		// Token: 0x06008AB9 RID: 35513 RVA: 0x001D17D5 File Offset: 0x001CF9D5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008ABA RID: 35514 RVA: 0x001D17E8 File Offset: 0x001CF9E8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008ABB RID: 35515 RVA: 0x001D1812 File Offset: 0x001CFA12
		public bool Equals(_LetB3 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038AE RID: 14510
		private ProgramNode _node;
	}
}
