using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities
{
	// Token: 0x02000C35 RID: 3125
	public static class ColorUtils
	{
		// Token: 0x060050A1 RID: 20641 RVA: 0x000FD4B1 File Offset: 0x000FB6B1
		public static bool ColorEquals(this Color x, Color y)
		{
			return x.ToArgb() == y.ToArgb();
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x000FD4C4 File Offset: 0x000FB6C4
		public static bool ColorEquals(this Color x, Color y, int precision)
		{
			int num = ColorUtils.Masks[precision];
			return (x.ToArgb() & num) == (y.ToArgb() & num);
		}

		// Token: 0x0400238D RID: 9101
		[Nullable(1)]
		private static readonly IReadOnlyList<int> Masks = Enumerable.Range(0, 9).Select(delegate(int bits)
		{
			int num = 8 - bits;
			int num2 = 255 >> num << num;
			return num2 | (num2 << 8) | (num2 << 16) | (num2 << 24);
		}).ToArray<int>();
	}
}
