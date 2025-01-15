using System;
using System.Drawing;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200001E RID: 30
	public sealed class ReportColor
	{
		// Token: 0x060003CA RID: 970 RVA: 0x00009B79 File Offset: 0x00007D79
		public ReportColor(Color color)
		{
			this.m_GDIColor = color;
			this.m_color = color.ToString();
			this.m_parsed = true;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00009BAD File Offset: 0x00007DAD
		public ReportColor(string color)
		{
			this.m_color = color;
			this.Validate();
			this.m_parsed = true;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00009BD4 File Offset: 0x00007DD4
		internal ReportColor(string color, bool parsed)
		{
			this.m_color = color;
			this.m_parsed = parsed;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00009BF5 File Offset: 0x00007DF5
		public override string ToString()
		{
			return this.m_color;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00009BFD File Offset: 0x00007DFD
		public Color ToColor()
		{
			if (!this.m_parsed)
			{
				Validator.ParseColor(this.m_color, out this.m_GDIColor);
				this.m_parsed = true;
			}
			return this.m_GDIColor;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00009C25 File Offset: 0x00007E25
		internal void Validate()
		{
			if (!Validator.ValidateColor(this.m_color, out this.m_GDIColor))
			{
				throw new ReportRenderingException(ErrorCode.rrInvalidColor, new object[] { this.m_color });
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00009C54 File Offset: 0x00007E54
		internal bool Parsed
		{
			get
			{
				return this.m_parsed;
			}
		}

		// Token: 0x04000078 RID: 120
		private string m_color;

		// Token: 0x04000079 RID: 121
		private Color m_GDIColor = Color.Empty;

		// Token: 0x0400007A RID: 122
		private bool m_parsed;
	}
}
