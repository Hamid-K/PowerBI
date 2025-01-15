using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CDF RID: 3295
	[NullableContext(1)]
	[Nullable(0)]
	public class LogicalGlyphOrderingLine
	{
		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x060054A2 RID: 21666 RVA: 0x0010A384 File Offset: 0x00108584
		public TextDirection TextDirection { get; }

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x060054A3 RID: 21667 RVA: 0x0010A38C File Offset: 0x0010858C
		public IReadOnlyList<LogicalGlyphOrderingElement> Elements { get; }

		// Token: 0x060054A4 RID: 21668 RVA: 0x0010A394 File Offset: 0x00108594
		public LogicalGlyphOrderingLine(TextDirection textDirection, IReadOnlyList<LogicalGlyphOrderingElement> elements)
		{
			this.TextDirection = textDirection;
			this.Elements = elements;
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x060054A5 RID: 21669 RVA: 0x0010A3AA File Offset: 0x001085AA
		public IEnumerable<Glyph> Glyphs
		{
			get
			{
				return this.Elements.SelectMany((LogicalGlyphOrderingElement el) => el.Glyphs);
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x060054A6 RID: 21670 RVA: 0x0010A3D6 File Offset: 0x001085D6
		public IEnumerable<IWord> AllWords
		{
			get
			{
				return this.Elements.SelectMany((LogicalGlyphOrderingElement el) => el.AllWords);
			}
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x0010A402 File Offset: 0x00108602
		public LogicalGlyphOrderingLine Join(LogicalGlyphOrderingLine after)
		{
			return new LogicalGlyphOrderingLine(this.TextDirection, this.Elements.Concat(after.Elements).ToList<LogicalGlyphOrderingElement>());
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x0010A428 File Offset: 0x00108628
		public override string ToString()
		{
			return ((this.TextDirection == TextDirection.LeftToRight) ? '\u2066' : '\u2067').ToString() + string.Concat(this.Glyphs.Select((Glyph g) => g.Text)) + "\u2069";
		}
	}
}
