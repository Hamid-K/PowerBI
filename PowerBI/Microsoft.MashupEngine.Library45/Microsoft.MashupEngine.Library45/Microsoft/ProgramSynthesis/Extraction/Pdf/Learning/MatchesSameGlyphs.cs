using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics;
using Microsoft.ProgramSynthesis.Utils.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Learning
{
	// Token: 0x02000C25 RID: 3109
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1 })]
	internal class MatchesSameGlyphs : ValueToValueConstraint<PdfRegion, PdfRegion>
	{
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x0600504B RID: 20555 RVA: 0x000FC6AF File Offset: 0x000FA8AF
		public new BoundsOnPdfPage Output
		{
			get
			{
				return (BoundsOnPdfPage)base.Output;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x0600504C RID: 20556 RVA: 0x000FC6BC File Offset: 0x000FA8BC
		public HashSet<Glyph> CoveredGlyphs { get; }

		// Token: 0x0600504D RID: 20557 RVA: 0x000FC6C4 File Offset: 0x000FA8C4
		public MatchesSameGlyphs(BoundsOnPdfPage input, BoundsOnPdfPage output, bool isSoft = false)
			: base(input, Semantics.SnapToGlyphs(output), isSoft)
		{
			this.CoveredGlyphs = new DisjunctiveBoundsExampleSpec.SnapToGlyphsBounds(output).CoveredGlyphs;
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x000FC6E8 File Offset: 0x000FA8E8
		public override bool ConflictsWith(Constraint<PdfRegion, PdfRegion> other)
		{
			if (other is Example<PdfRegion, PdfRegion>)
			{
				return true;
			}
			MatchesSameGlyphs matchesSameGlyphs = other as MatchesSameGlyphs;
			return matchesSameGlyphs != null && matchesSameGlyphs.Input.Equals(base.Input) && !matchesSameGlyphs.Output.Equals(this.Output);
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x000FC734 File Offset: 0x000FA934
		public override bool Valid(Program<PdfRegion, PdfRegion> program)
		{
			BoundsOnPdfPage boundsOnPdfPage = program.Run(base.Input) as BoundsOnPdfPage;
			if (boundsOnPdfPage == null)
			{
				return false;
			}
			BoundsOnPdfPage boundsOnPdfPage2 = Semantics.SnapToGlyphs(boundsOnPdfPage);
			if (!boundsOnPdfPage2.Equals(this.Output))
			{
				HashSet<Glyph> coveredGlyphs = new DisjunctiveBoundsExampleSpec.SnapToGlyphsBounds(boundsOnPdfPage2).CoveredGlyphs;
				List<Glyph> list = coveredGlyphs.Except(this.CoveredGlyphs).ToList<Glyph>();
				List<Glyph> list2 = this.CoveredGlyphs.Except(coveredGlyphs).ToList<Glyph>();
				Logger.Instance.Trace("Extra glyphs", list, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\Learning\\MatchesSameGlyphs.cs", 38);
				Logger.Instance.Trace("Missing glyphs", list2, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\Learning\\MatchesSameGlyphs.cs", 39);
				return false;
			}
			return true;
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x000FC7D0 File Offset: 0x000FA9D0
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("'{0}' -> same glyphs as {1}", new object[]
			{
				base.Input,
				(this.Output == null) ? "(null)" : FormattableString.Invariant(FormattableStringFactory.Create("'{0}'", new object[] { this.Output }))
			}));
		}
	}
}
