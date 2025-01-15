using System;
using Microsoft.ProgramSynthesis.Rules;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008E7 RID: 2279
	public abstract class TerminalNode : ProgramNode
	{
		// Token: 0x06003140 RID: 12608 RVA: 0x00091589 File Offset: 0x0008F789
		protected TerminalNode(Symbol symbol)
		{
			this.Symbol = symbol;
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x00091598 File Offset: 0x0008F798
		public override ProgramNode[] Children
		{
			get
			{
				return new ProgramNode[0];
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x000915A0 File Offset: 0x0008F7A0
		public override Symbol Symbol { get; }

		// Token: 0x06003143 RID: 12611 RVA: 0x000915A8 File Offset: 0x0008F7A8
		public override bool Equals(ProgramNode other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && object.Equals(this.Symbol, other.Symbol)));
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000915DB File Offset: 0x0008F7DB
		public override int GetHashCode()
		{
			return this.Symbol.GetHashCode();
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x000915E8 File Offset: 0x0008F7E8
		public override GrammarRule GrammarRule
		{
			get
			{
				return this.Symbol.TerminalRule;
			}
		}
	}
}
