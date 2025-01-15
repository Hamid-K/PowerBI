using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D77 RID: 3447
	[NullableContext(1)]
	[Nullable(0)]
	public class TextDecoration
	{
		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x060057DB RID: 22491 RVA: 0x001172F1 File Offset: 0x001154F1
		public TextDecorationKind Kind { get; }

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x060057DC RID: 22492 RVA: 0x001172F9 File Offset: 0x001154F9
		public Separator Separator { get; }

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x060057DD RID: 22493 RVA: 0x00117301 File Offset: 0x00115501
		public IReadOnlyList<Glyph> DecoratedGlyphs { get; }

		// Token: 0x060057DE RID: 22494 RVA: 0x00117309 File Offset: 0x00115509
		public TextDecoration(TextDecorationKind kind, Separator separator, IReadOnlyList<Glyph> decoratedGlyphs)
		{
			this.Kind = kind;
			this.Separator = separator;
			this.DecoratedGlyphs = decoratedGlyphs;
		}
	}
}
