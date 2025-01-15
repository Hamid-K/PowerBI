using System;
using System.Globalization;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200077E RID: 1918
	internal sealed class Converter
	{
		// Token: 0x06006AFD RID: 27389 RVA: 0x001AFE06 File Offset: 0x001AE006
		private Converter()
		{
		}

		// Token: 0x06006AFE RID: 27390 RVA: 0x001AFE0E File Offset: 0x001AE00E
		internal static string ConvertSize(double size)
		{
			return size.ToString(CultureInfo.InvariantCulture) + "mm";
		}

		// Token: 0x06006AFF RID: 27391 RVA: 0x001AFE28 File Offset: 0x001AE028
		internal static double ConvertToMM(RVUnit unit)
		{
			double value = unit.Value;
			switch (unit.Type)
			{
			case RVUnitType.Cm:
				return value * 10.0;
			case RVUnitType.Inch:
				return value * 25.4;
			case RVUnitType.Mm:
				return value;
			case RVUnitType.Pica:
				return value * 4.2333;
			case RVUnitType.Point:
				return value * 0.3528;
			}
			Global.Tracer.Assert(false);
			return value;
		}

		// Token: 0x040035F8 RID: 13816
		internal static double Inches160 = 4064.0;

		// Token: 0x040035F9 RID: 13817
		internal static double Pt1 = 0.3528;

		// Token: 0x040035FA RID: 13818
		internal static double Pt200 = 70.56;

		// Token: 0x040035FB RID: 13819
		internal static double PtPoint25 = 0.08814;

		// Token: 0x040035FC RID: 13820
		internal static double Pt20 = 7.056;

		// Token: 0x040035FD RID: 13821
		internal static double Pt1000 = 352.8;
	}
}
