using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CE1 RID: 3297
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class LogicalGlyphOrderingElement
	{
		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x060054AE RID: 21678 RVA: 0x0010A4AF File Offset: 0x001086AF
		// (set) Token: 0x060054AF RID: 21679 RVA: 0x0010A4B7 File Offset: 0x001086B7
		public TextDirection TextDirection { get; set; }

		// Token: 0x060054B0 RID: 21680 RVA: 0x0010A4C0 File Offset: 0x001086C0
		protected LogicalGlyphOrderingElement(TextDirection textDirection)
		{
			this.TextDirection = textDirection;
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x060054B1 RID: 21681
		public abstract IEnumerable<Glyph> Glyphs { get; }

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x060054B2 RID: 21682
		public abstract IEnumerable<IWord> AllWords { get; }

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x060054B3 RID: 21683 RVA: 0x0010A4CF File Offset: 0x001086CF
		public virtual IEnumerable<LogicalGlyphOrderingLine> NestedLines
		{
			get
			{
				return Enumerable.Empty<LogicalGlyphOrderingLine>();
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x060054B4 RID: 21684 RVA: 0x0010A4D6 File Offset: 0x001086D6
		public virtual IEnumerable<LogicalGlyphOrderingLine> AllNestedLines
		{
			get
			{
				return this.NestedLines.SelectMany((LogicalGlyphOrderingLine l) => l.Elements.SelectMany((LogicalGlyphOrderingElement el) => el.AllNestedLines).PrependItem(l));
			}
		}
	}
}
