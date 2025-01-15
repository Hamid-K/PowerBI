using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200107F RID: 4223
	public struct selectSubstring : IProgramNodeBuilder, IEquatable<selectSubstring>
	{
		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06007EAF RID: 32431 RVA: 0x001AA45E File Offset: 0x001A865E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007EB0 RID: 32432 RVA: 0x001AA466 File Offset: 0x001A8666
		private selectSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007EB1 RID: 32433 RVA: 0x001AA46F File Offset: 0x001A866F
		public static selectSubstring CreateUnsafe(ProgramNode node)
		{
			return new selectSubstring(node);
		}

		// Token: 0x06007EB2 RID: 32434 RVA: 0x001AA478 File Offset: 0x001A8678
		public static selectSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selectSubstring)
			{
				return null;
			}
			return new selectSubstring?(selectSubstring.CreateUnsafe(node));
		}

		// Token: 0x06007EB3 RID: 32435 RVA: 0x001AA4B2 File Offset: 0x001A86B2
		public static selectSubstring CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selectSubstring(new Hole(g.Symbol.selectSubstring, holeId));
		}

		// Token: 0x06007EB4 RID: 32436 RVA: 0x001AA4CA File Offset: 0x001A86CA
		public SelectSubstring Cast_SelectSubstring()
		{
			return SelectSubstring.CreateUnsafe(this.Node);
		}

		// Token: 0x06007EB5 RID: 32437 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SelectSubstring(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007EB6 RID: 32438 RVA: 0x001AA4D7 File Offset: 0x001A86D7
		public bool Is_SelectSubstring(GrammarBuilders g, out SelectSubstring value)
		{
			value = SelectSubstring.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007EB7 RID: 32439 RVA: 0x001AA4EB File Offset: 0x001A86EB
		public SelectSubstring? As_SelectSubstring(GrammarBuilders g)
		{
			return new SelectSubstring?(SelectSubstring.CreateUnsafe(this.Node));
		}

		// Token: 0x06007EB8 RID: 32440 RVA: 0x001AA4FD File Offset: 0x001A86FD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007EB9 RID: 32441 RVA: 0x001AA510 File Offset: 0x001A8710
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x001AA53A File Offset: 0x001A873A
		public bool Equals(selectSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003398 RID: 13208
		private ProgramNode _node;
	}
}
