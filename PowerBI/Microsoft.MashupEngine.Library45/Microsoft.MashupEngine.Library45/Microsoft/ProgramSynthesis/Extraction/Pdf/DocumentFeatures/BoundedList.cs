using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000C68 RID: 3176
	[NullableContext(1)]
	[Nullable(0)]
	internal class BoundedList<TCell> : IBoundedList<TCell> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x060051D2 RID: 20946 RVA: 0x001019F6 File Offset: 0x000FFBF6
		public Axis Axis { get; }

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x060051D3 RID: 20947 RVA: 0x001019FE File Offset: 0x000FFBFE
		public IReadOnlyList<TCell> Cells { get; }

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x060051D4 RID: 20948 RVA: 0x00101A06 File Offset: 0x000FFC06
		[Nullable(new byte[] { 0, 1 })]
		public Range<PixelUnit> Range
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x00101A0E File Offset: 0x000FFC0E
		public BoundedList(Axis axis, IReadOnlyList<TCell> cells, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> range)
		{
			this.Axis = axis;
			this.Cells = cells;
			this.Range = range;
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x00101A2C File Offset: 0x000FFC2C
		public override string ToString()
		{
			return string.Format("{{ BoundedList Range: {0}, Cells: {1} }}", this.Range, StringUtils.BuildLimitedListLiteral<string>(this.Cells.Select((TCell cell) => cell.MinimalToString()), 5));
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x00101A7E File Offset: 0x000FFC7E
		public string MinimalToString()
		{
			return string.Format("{{CellCount:{0}}}", this.Cells.Count);
		}
	}
}
