using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CE5 RID: 3301
	[NullableContext(1)]
	[Nullable(0)]
	public class LogicalGlyphOrderingScript : LogicalGlyphOrderingElement
	{
		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x060054C9 RID: 21705 RVA: 0x0010A6DB File Offset: 0x001088DB
		[Nullable(2)]
		public LogicalGlyphOrderingLine Subscript
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x060054CA RID: 21706 RVA: 0x0010A6E3 File Offset: 0x001088E3
		[Nullable(2)]
		public LogicalGlyphOrderingLine Superscript
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x0010A6EB File Offset: 0x001088EB
		[NullableContext(2)]
		public LogicalGlyphOrderingScript(TextDirection textDirection, LogicalGlyphOrderingLine subscript, LogicalGlyphOrderingLine superscript, [Nullable(1)] Glyph pseudoGlyph)
			: base(textDirection)
		{
			this.Subscript = subscript;
			this.Superscript = superscript;
			this._pseudoGlyph = pseudoGlyph;
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x060054CC RID: 21708 RVA: 0x0010A70A File Offset: 0x0010890A
		public override IEnumerable<LogicalGlyphOrderingLine> NestedLines
		{
			get
			{
				return new LogicalGlyphOrderingLine[] { this.Subscript, this.Superscript }.Collect<LogicalGlyphOrderingLine>();
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x060054CD RID: 21709 RVA: 0x0010A729 File Offset: 0x00108929
		public override IEnumerable<Glyph> Glyphs
		{
			get
			{
				return new Glyph[] { this._pseudoGlyph };
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x060054CE RID: 21710 RVA: 0x0010A73A File Offset: 0x0010893A
		public override IEnumerable<IWord> AllWords
		{
			get
			{
				return this.NestedLines.SelectMany((LogicalGlyphOrderingLine l) => l.AllWords);
			}
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x0010A766 File Offset: 0x00108966
		public override string ToString()
		{
			return this._pseudoGlyph.Text;
		}

		// Token: 0x04002653 RID: 9811
		private readonly Glyph _pseudoGlyph;
	}
}
