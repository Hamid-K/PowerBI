using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003E9 RID: 1001
	public class BackgroundImage
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x0007EDCA File Offset: 0x0007CFCA
		public MIMETypeExpr MIMEType
		{
			get
			{
				return this.m_imageHelper.MIMEType;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x0007EDD7 File Offset: 0x0007CFD7
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x0007EDE4 File Offset: 0x0007CFE4
		public ImageSource Source
		{
			get
			{
				return this.m_imageHelper.Source;
			}
			set
			{
				this.m_imageHelper.Source = value;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x0007EDF2 File Offset: 0x0007CFF2
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x0007EDFF File Offset: 0x0007CFFF
		public string Value
		{
			get
			{
				return this.m_imageHelper.Value;
			}
			set
			{
				this.m_imageHelper.Value = value;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x0007EE0D File Offset: 0x0007D00D
		// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x0007EE15 File Offset: 0x0007D015
		[DefaultValue("Repeat")]
		public string BackgroundRepeat
		{
			get
			{
				return this.m_backgroundRepeat;
			}
			set
			{
				this.m_backgroundRepeat = value;
			}
		}

		// Token: 0x04000DE0 RID: 3552
		private ImageHelper m_imageHelper = new ImageHelper();

		// Token: 0x04000DE1 RID: 3553
		private string m_backgroundRepeat = "Repeat";
	}
}
