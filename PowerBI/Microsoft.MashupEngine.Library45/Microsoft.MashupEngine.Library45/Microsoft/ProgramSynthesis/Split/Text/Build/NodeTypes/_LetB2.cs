using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001374 RID: 4980
	public struct _LetB2 : IProgramNodeBuilder, IEquatable<_LetB2>
	{
		// Token: 0x17001A7D RID: 6781
		// (get) Token: 0x06009A77 RID: 39543 RVA: 0x0020AFD2 File Offset: 0x002091D2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A78 RID: 39544 RVA: 0x0020AFDA File Offset: 0x002091DA
		private _LetB2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A79 RID: 39545 RVA: 0x0020AFE3 File Offset: 0x002091E3
		public static _LetB2 CreateUnsafe(ProgramNode node)
		{
			return new _LetB2(node);
		}

		// Token: 0x06009A7A RID: 39546 RVA: 0x0020AFEC File Offset: 0x002091EC
		public static _LetB2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB2)
			{
				return null;
			}
			return new _LetB2?(_LetB2.CreateUnsafe(node));
		}

		// Token: 0x06009A7B RID: 39547 RVA: 0x0020B026 File Offset: 0x00209226
		public static _LetB2 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB2(new Hole(g.Symbol._LetB2, holeId));
		}

		// Token: 0x06009A7C RID: 39548 RVA: 0x0020B03E File Offset: 0x0020923E
		public Split Cast_Split()
		{
			return Split.CreateUnsafe(this.Node);
		}

		// Token: 0x06009A7D RID: 39549 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Split(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06009A7E RID: 39550 RVA: 0x0020B04B File Offset: 0x0020924B
		public bool Is_Split(GrammarBuilders g, out Split value)
		{
			value = Split.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06009A7F RID: 39551 RVA: 0x0020B05F File Offset: 0x0020925F
		public Split? As_Split(GrammarBuilders g)
		{
			return new Split?(Split.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A80 RID: 39552 RVA: 0x0020B071 File Offset: 0x00209271
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009A81 RID: 39553 RVA: 0x0020B084 File Offset: 0x00209284
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009A82 RID: 39554 RVA: 0x0020B0AE File Offset: 0x002092AE
		public bool Equals(_LetB2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DEB RID: 15851
		private ProgramNode _node;
	}
}
