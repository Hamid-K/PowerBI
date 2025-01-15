using System;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003A5 RID: 933
	internal sealed class Converter
	{
		// Token: 0x0600260F RID: 9743 RVA: 0x000B5C2D File Offset: 0x000B3E2D
		private Converter()
		{
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000B5C35 File Offset: 0x000B3E35
		internal static string ConvertSize(double size)
		{
			return size.ToString("0.###############", CultureInfo.InvariantCulture) + "mm";
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000B5C54 File Offset: 0x000B3E54
		internal static string ConvertSizeFromMM(double sizeValue, RVUnitType unitType)
		{
			string text = "mm";
			if (unitType <= RVUnitType.Inch)
			{
				if (unitType == RVUnitType.Cm)
				{
					sizeValue /= 10.0;
					text = "cm";
					goto IL_0076;
				}
				if (unitType == RVUnitType.Inch)
				{
					sizeValue /= 25.4;
					text = "in";
					goto IL_0076;
				}
			}
			else
			{
				if (unitType == RVUnitType.Pica)
				{
					sizeValue /= 4.2333333333333325;
					text = "pc";
					goto IL_0076;
				}
				if (unitType == RVUnitType.Point)
				{
					sizeValue /= 0.35277777777777775;
					text = "pt";
					goto IL_0076;
				}
			}
			unitType = RVUnitType.Mm;
			IL_0076:
			return Math.Round(sizeValue, 5).ToString(CultureInfo.InvariantCulture) + text;
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000B5CF1 File Offset: 0x000B3EF1
		internal static double ConvertToMM(RVUnit unit)
		{
			if (!Validator.ValidateSizeUnitType(unit))
			{
				Global.Tracer.Assert(false);
			}
			return unit.ToMillimeters();
		}

		// Token: 0x04001628 RID: 5672
		internal const double Inches455 = 11557.0;

		// Token: 0x04001629 RID: 5673
		internal const double Pt1 = 0.35277777777777775;

		// Token: 0x0400162A RID: 5674
		internal const double Pc1 = 4.2333333333333325;

		// Token: 0x0400162B RID: 5675
		internal const double Pt200 = 70.55555555555554;

		// Token: 0x0400162C RID: 5676
		internal const double PtPoint25 = 0.08814;

		// Token: 0x0400162D RID: 5677
		internal const double Pt20 = 7.055555555555555;

		// Token: 0x0400162E RID: 5678
		internal const double Pt1000 = 352.77777777777777;

		// Token: 0x0400162F RID: 5679
		internal const string FullDoubleFormatCode = "0.###############";
	}
}
