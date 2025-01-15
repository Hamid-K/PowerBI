using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200107E RID: 4222
	public struct y : IProgramNodeBuilder, IEquatable<y>
	{
		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x06007EA3 RID: 32419 RVA: 0x001AA36E File Offset: 0x001A856E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007EA4 RID: 32420 RVA: 0x001AA376 File Offset: 0x001A8576
		private y(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007EA5 RID: 32421 RVA: 0x001AA37F File Offset: 0x001A857F
		public static y CreateUnsafe(ProgramNode node)
		{
			return new y(node);
		}

		// Token: 0x06007EA6 RID: 32422 RVA: 0x001AA388 File Offset: 0x001A8588
		public static y? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.y)
			{
				return null;
			}
			return new y?(y.CreateUnsafe(node));
		}

		// Token: 0x06007EA7 RID: 32423 RVA: 0x001AA3C2 File Offset: 0x001A85C2
		public static y CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new y(new Hole(g.Symbol.y, holeId));
		}

		// Token: 0x06007EA8 RID: 32424 RVA: 0x001AA3DA File Offset: 0x001A85DA
		public GetValueSubstring Cast_GetValueSubstring()
		{
			return GetValueSubstring.CreateUnsafe(this.Node);
		}

		// Token: 0x06007EA9 RID: 32425 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_GetValueSubstring(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007EAA RID: 32426 RVA: 0x001AA3E7 File Offset: 0x001A85E7
		public bool Is_GetValueSubstring(GrammarBuilders g, out GetValueSubstring value)
		{
			value = GetValueSubstring.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007EAB RID: 32427 RVA: 0x001AA3FB File Offset: 0x001A85FB
		public GetValueSubstring? As_GetValueSubstring(GrammarBuilders g)
		{
			return new GetValueSubstring?(GetValueSubstring.CreateUnsafe(this.Node));
		}

		// Token: 0x06007EAC RID: 32428 RVA: 0x001AA40D File Offset: 0x001A860D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007EAD RID: 32429 RVA: 0x001AA420 File Offset: 0x001A8620
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007EAE RID: 32430 RVA: 0x001AA44A File Offset: 0x001A864A
		public bool Equals(y other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003397 RID: 13207
		private ProgramNode _node;
	}
}
