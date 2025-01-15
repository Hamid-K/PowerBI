using System;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EA7 RID: 3751
	public class Border
	{
		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06006667 RID: 26215 RVA: 0x0014E348 File Offset: 0x0014C548
		public AxisAlignedLine<TableUnit> Line { get; }

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06006668 RID: 26216 RVA: 0x0014E350 File Offset: 0x0014C550
		public BorderInfo BorderInfo { get; }

		// Token: 0x06006669 RID: 26217 RVA: 0x0014E358 File Offset: 0x0014C558
		public Border(AxisAlignedLine<TableUnit> line, BorderInfo borderInfo)
		{
			this.Line = line;
			this.BorderInfo = borderInfo;
		}

		// Token: 0x0600666A RID: 26218 RVA: 0x0014E370 File Offset: 0x0014C570
		public bool Intersects(Border other)
		{
			if (other.Line.Axis == this.Line.Axis)
			{
				return other.Line.Position == this.Line.Position && other.Line.Range.Overlaps(this.Line.Range);
			}
			return other.Line.Range.Contains(this.Line.Position) && this.Line.Range.Contains(other.Line.Position);
		}

		// Token: 0x0600666B RID: 26219 RVA: 0x0014E42C File Offset: 0x0014C62C
		public override string ToString()
		{
			return string.Format("{0} {1}", this.BorderInfo, this.Line);
		}
	}
}
