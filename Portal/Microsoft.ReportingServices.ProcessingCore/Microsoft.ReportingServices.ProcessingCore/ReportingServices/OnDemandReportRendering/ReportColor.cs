using System;
using System.Drawing;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002C5 RID: 709
	public sealed class ReportColor
	{
		// Token: 0x06001AD5 RID: 6869 RVA: 0x0006B9CC File Offset: 0x00069BCC
		public ReportColor(string color)
			: this(color, false)
		{
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0006B9D6 File Offset: 0x00069BD6
		public ReportColor(string color, bool allowTransparency)
		{
			this.m_GDIColor = Color.Empty;
			base..ctor();
			this.m_color = color;
			this.Validate(allowTransparency);
			this.m_parsed = true;
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0006B9FE File Offset: 0x00069BFE
		internal ReportColor(string color, Color gdiColor, bool parsed)
		{
			this.m_GDIColor = Color.Empty;
			base..ctor();
			this.m_color = color;
			this.m_parsed = parsed;
			this.m_GDIColor = gdiColor;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0006BA26 File Offset: 0x00069C26
		internal ReportColor(ReportColor oldColor)
		{
			this.m_GDIColor = Color.Empty;
			base..ctor();
			this.m_color = oldColor.ToString();
			this.m_parsed = oldColor.Parsed;
			if (this.m_parsed)
			{
				this.m_GDIColor = oldColor.ToColor();
			}
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0006BA65 File Offset: 0x00069C65
		public override string ToString()
		{
			return this.m_color;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0006BA6D File Offset: 0x00069C6D
		public Color ToColor()
		{
			if (!this.m_parsed)
			{
				Validator.ParseColor(this.m_color, out this.m_GDIColor, false);
				this.m_parsed = true;
			}
			return this.m_GDIColor;
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0006BA96 File Offset: 0x00069C96
		internal void Validate(bool allowTransparency)
		{
			if (!Validator.ValidateColor(this.m_color, out this.m_GDIColor, allowTransparency))
			{
				throw new RenderingObjectModelException(ErrorCode.rrInvalidColor, new object[] { this.m_color });
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0006BAC6 File Offset: 0x00069CC6
		public static bool TryParse(string value, out ReportColor reportColor)
		{
			return ReportColor.TryParse(value, false, out reportColor);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0006BAD0 File Offset: 0x00069CD0
		public static bool TryParse(string value, bool allowTransparency, out ReportColor reportColor)
		{
			Color color;
			if (Validator.ValidateColor(value, out color, allowTransparency))
			{
				reportColor = new ReportColor(value, color, true);
				return true;
			}
			reportColor = null;
			return false;
		}

		// Token: 0x04000D5A RID: 3418
		private string m_color;

		// Token: 0x04000D5B RID: 3419
		private Color m_GDIColor;

		// Token: 0x04000D5C RID: 3420
		private bool m_parsed;
	}
}
