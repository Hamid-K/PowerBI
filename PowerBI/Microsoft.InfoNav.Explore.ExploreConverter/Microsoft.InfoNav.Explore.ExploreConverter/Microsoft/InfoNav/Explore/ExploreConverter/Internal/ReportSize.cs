using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000AF RID: 175
	internal sealed class ReportSize
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x0001386D File Offset: 0x00011A6D
		internal ReportSize()
		{
			this._pixelUnits = 0;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001387C File Offset: 0x00011A7C
		internal ReportSize(int pixelUnits)
		{
			this._pixelUnits = pixelUnits;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0001388B File Offset: 0x00011A8B
		public int PixelUnits
		{
			get
			{
				return this._pixelUnits;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00013893 File Offset: 0x00011A93
		public float PixelUnitsInFloat
		{
			get
			{
				return (float)this._pixelUnits;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001389C File Offset: 0x00011A9C
		public static int ParseInches(string size)
		{
			return (int)(double.Parse(size) * 96.0);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000138B0 File Offset: 0x00011AB0
		public static ReportSize CreateReportSize(string size)
		{
			if (string.IsNullOrEmpty(size))
			{
				return new ReportSize(0);
			}
			int num = size.IndexOf("in", StringComparison.Ordinal);
			if (num != -1)
			{
				return new ReportSize(ReportSize.ParseInches(size.Substring(0, num)));
			}
			int num2 = size.IndexOf("pt", StringComparison.Ordinal);
			if (num2 != -1)
			{
				return new ReportSize((int)(double.Parse(size.Substring(0, num2), CultureInfo.InvariantCulture) * 1.3333333333333333));
			}
			throw new ArgumentException("unit is unsupported", "size");
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00013934 File Offset: 0x00011B34
		public static ReportSize ParseReportSize(XmlReader nodeWithValueReportSizeReader)
		{
			string text = nodeWithValueReportSizeReader.ReadElementContentAsString();
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return ReportSize.CreateReportSize(text);
		}

		// Token: 0x0400023D RID: 573
		private const int PixelUnitsPerInch = 96;

		// Token: 0x0400023E RID: 574
		private const double PixelUnitsPerPt = 1.3333333333333333;

		// Token: 0x0400023F RID: 575
		private readonly int _pixelUnits;
	}
}
