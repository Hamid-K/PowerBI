using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EA8 RID: 3752
	public class BorderGroup
	{
		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x0600666C RID: 26220 RVA: 0x0014E44E File Offset: 0x0014C64E
		public IReadOnlyList<Border> Borders { get; }

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x0600666D RID: 26221 RVA: 0x0014E456 File Offset: 0x0014C656
		public Bounds<TableUnit> Span { get; }

		// Token: 0x0600666E RID: 26222 RVA: 0x0014E45E File Offset: 0x0014C65E
		public override string ToString()
		{
			return string.Format("BorderGround(Span={0}, count={1})", this.Span, this.Borders.Count);
		}

		// Token: 0x0600666F RID: 26223 RVA: 0x0014E488 File Offset: 0x0014C688
		public BorderGroup(IReadOnlyList<Border> borders)
		{
			this.Borders = borders;
			Bounds<TableUnit> bounds = Bounds<TableUnit>.Join(borders.Select((Border b) => b.Line.Bounds));
			this.Span = new Bounds<TableUnit>(bounds.Corner(Ordinal.TopLeft), bounds.Corner(Ordinal.BottomRight) - new Vector<TableUnit>(1, 1));
		}
	}
}
