using System;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008E0 RID: 2272
	internal struct ScopeElement
	{
		// Token: 0x06003106 RID: 12550 RVA: 0x00090413 File Offset: 0x0008E613
		private ScopeElement(Symbol symbol, Symbol replacement)
		{
			this.Symbol = symbol;
			this.Replacement = replacement;
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x00090423 File Offset: 0x0008E623
		internal static ScopeElement Define(Symbol symbol)
		{
			return new ScopeElement(symbol, null);
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x0009042C File Offset: 0x0008E62C
		internal static ScopeElement Substitute(Symbol symbol, Symbol replacement)
		{
			return new ScopeElement(symbol, replacement);
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x00090435 File Offset: 0x0008E635
		internal bool IsDefinition
		{
			get
			{
				return this.Replacement == null;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x00090443 File Offset: 0x0008E643
		internal bool IsSubstitution
		{
			get
			{
				return this.Replacement != null;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x00090451 File Offset: 0x0008E651
		internal readonly Symbol Symbol { get; }

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x00090459 File Offset: 0x0008E659
		internal readonly Symbol Replacement { get; }
	}
}
