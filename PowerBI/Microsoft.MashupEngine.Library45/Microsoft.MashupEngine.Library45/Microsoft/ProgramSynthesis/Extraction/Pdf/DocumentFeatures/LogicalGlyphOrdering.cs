using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CDD RID: 3293
	[NullableContext(1)]
	[Nullable(0)]
	public class LogicalGlyphOrdering
	{
		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06005496 RID: 21654 RVA: 0x0010A297 File Offset: 0x00108497
		public static LogicalGlyphOrdering Empty { get; } = new LogicalGlyphOrdering(new LogicalGlyphOrderingLine[0]);

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06005497 RID: 21655 RVA: 0x0010A29E File Offset: 0x0010849E
		public IReadOnlyList<LogicalGlyphOrderingLine> DirectLines { get; }

		// Token: 0x06005498 RID: 21656 RVA: 0x0010A2A6 File Offset: 0x001084A6
		public LogicalGlyphOrdering(IEnumerable<LogicalGlyphOrderingLine> directLines)
		{
			this.DirectLines = directLines.ToList<LogicalGlyphOrderingLine>();
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06005499 RID: 21657 RVA: 0x0010A2BA File Offset: 0x001084BA
		public IEnumerable<LogicalGlyphOrderingLine> AllNestedLines
		{
			get
			{
				return this.DirectLines.SelectMany((LogicalGlyphOrderingLine l) => l.Elements.SelectMany((LogicalGlyphOrderingElement el) => el.AllNestedLines).PrependItem(l));
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x0600549A RID: 21658 RVA: 0x0010A2E6 File Offset: 0x001084E6
		public IEnumerable<IWord> AllWords
		{
			get
			{
				return this.DirectLines.SelectMany((LogicalGlyphOrderingLine l) => l.AllWords);
			}
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x0010A312 File Offset: 0x00108512
		public override string ToString()
		{
			return string.Join<LogicalGlyphOrderingLine>("\n", this.DirectLines);
		}
	}
}
