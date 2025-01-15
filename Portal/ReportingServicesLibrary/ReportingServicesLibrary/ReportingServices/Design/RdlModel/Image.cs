using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003E5 RID: 997
	public sealed class Image : ReportItem
	{
		// Token: 0x06001FB5 RID: 8117 RVA: 0x0007EC46 File Offset: 0x0007CE46
		public Image()
		{
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0007EC59 File Offset: 0x0007CE59
		public Image(string name)
		{
			base.Name = name;
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x0007EC73 File Offset: 0x0007CE73
		// (set) Token: 0x06001FB8 RID: 8120 RVA: 0x0007EC80 File Offset: 0x0007CE80
		public MIMETypeExpr MIMEType
		{
			get
			{
				return this.m_imageHelper.MIMEType;
			}
			set
			{
				this.m_imageHelper.MIMEType = value;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x0007EC8E File Offset: 0x0007CE8E
		// (set) Token: 0x06001FBA RID: 8122 RVA: 0x0007EC9B File Offset: 0x0007CE9B
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

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x0007ECA9 File Offset: 0x0007CEA9
		// (set) Token: 0x06001FBC RID: 8124 RVA: 0x0007ECB6 File Offset: 0x0007CEB6
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

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x0007ECC4 File Offset: 0x0007CEC4
		// (set) Token: 0x06001FBE RID: 8126 RVA: 0x0007ECCC File Offset: 0x0007CECC
		[DefaultValue(ImageSizing.AutoSize)]
		public ImageSizing Sizing
		{
			get
			{
				return this.m_sizing;
			}
			set
			{
				this.m_sizing = value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x0007ECD5 File Offset: 0x0007CED5
		// (set) Token: 0x06001FC0 RID: 8128 RVA: 0x0007ECDD File Offset: 0x0007CEDD
		public Action Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x04000DD3 RID: 3539
		private Action m_action;

		// Token: 0x04000DD4 RID: 3540
		private ImageSizing m_sizing;

		// Token: 0x04000DD5 RID: 3541
		private ImageHelper m_imageHelper = new ImageHelper();
	}
}
