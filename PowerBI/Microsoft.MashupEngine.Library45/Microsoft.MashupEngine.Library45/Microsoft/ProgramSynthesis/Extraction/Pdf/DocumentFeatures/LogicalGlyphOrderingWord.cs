using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CE3 RID: 3299
	[NullableContext(1)]
	[Nullable(0)]
	public class LogicalGlyphOrderingWord : LogicalGlyphOrderingElement
	{
		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x060054B9 RID: 21689 RVA: 0x0010A540 File Offset: 0x00108740
		public IWord Word { get; }

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x060054BA RID: 21690 RVA: 0x0010A548 File Offset: 0x00108748
		// (set) Token: 0x060054BB RID: 21691 RVA: 0x0010A550 File Offset: 0x00108750
		public bool IsEuropeanNumber { get; set; }

		// Token: 0x060054BC RID: 21692 RVA: 0x0010A559 File Offset: 0x00108759
		public LogicalGlyphOrderingWord(IWord word, TextDirection textDirection)
			: base(textDirection)
		{
			this.Word = word;
			Glyph glyph = word.Children.FirstOrDefault<Glyph>();
			this.IsEuropeanNumber = glyph != null && glyph.BidiCategory == BidiUnicodeCategory.EuropeanNumber;
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x060054BD RID: 21693 RVA: 0x0010A58C File Offset: 0x0010878C
		public override IEnumerable<Glyph> Glyphs
		{
			get
			{
				if (base.TextDirection != TextDirection.RightToLeft || this.Word.TextDirection == TextDirection.LeftToRight || this.Word.Children[0].BidiCategory.IsNumberCategory())
				{
					return this.Word.Children;
				}
				return this.Word.Children.Reverse<Glyph>();
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x060054BE RID: 21694 RVA: 0x0010A5EB File Offset: 0x001087EB
		public override IEnumerable<IWord> AllWords
		{
			get
			{
				yield return this.Word;
				yield break;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x060054BF RID: 21695 RVA: 0x0010A5FB File Offset: 0x001087FB
		public string Content
		{
			get
			{
				if (base.TextDirection != TextDirection.RightToLeft)
				{
					return this.Word.Content;
				}
				return this.Word.ContentRtl;
			}
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x0010A61D File Offset: 0x0010881D
		public override string ToString()
		{
			return this.Content;
		}
	}
}
