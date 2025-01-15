using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200107D RID: 4221
	public struct fieldSubstring : IProgramNodeBuilder, IEquatable<fieldSubstring>
	{
		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06007E97 RID: 32407 RVA: 0x001AA27E File Offset: 0x001A847E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007E98 RID: 32408 RVA: 0x001AA286 File Offset: 0x001A8486
		private fieldSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007E99 RID: 32409 RVA: 0x001AA28F File Offset: 0x001A848F
		public static fieldSubstring CreateUnsafe(ProgramNode node)
		{
			return new fieldSubstring(node);
		}

		// Token: 0x06007E9A RID: 32410 RVA: 0x001AA298 File Offset: 0x001A8498
		public static fieldSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fieldSubstring)
			{
				return null;
			}
			return new fieldSubstring?(fieldSubstring.CreateUnsafe(node));
		}

		// Token: 0x06007E9B RID: 32411 RVA: 0x001AA2D2 File Offset: 0x001A84D2
		public static fieldSubstring CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fieldSubstring(new Hole(g.Symbol.fieldSubstring, holeId));
		}

		// Token: 0x06007E9C RID: 32412 RVA: 0x001AA2EA File Offset: 0x001A84EA
		public LetSubstring Cast_LetSubstring()
		{
			return LetSubstring.CreateUnsafe(this.Node);
		}

		// Token: 0x06007E9D RID: 32413 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetSubstring(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007E9E RID: 32414 RVA: 0x001AA2F7 File Offset: 0x001A84F7
		public bool Is_LetSubstring(GrammarBuilders g, out LetSubstring value)
		{
			value = LetSubstring.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007E9F RID: 32415 RVA: 0x001AA30B File Offset: 0x001A850B
		public LetSubstring? As_LetSubstring(GrammarBuilders g)
		{
			return new LetSubstring?(LetSubstring.CreateUnsafe(this.Node));
		}

		// Token: 0x06007EA0 RID: 32416 RVA: 0x001AA31D File Offset: 0x001A851D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007EA1 RID: 32417 RVA: 0x001AA330 File Offset: 0x001A8530
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007EA2 RID: 32418 RVA: 0x001AA35A File Offset: 0x001A855A
		public bool Equals(fieldSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003396 RID: 13206
		private ProgramNode _node;
	}
}
