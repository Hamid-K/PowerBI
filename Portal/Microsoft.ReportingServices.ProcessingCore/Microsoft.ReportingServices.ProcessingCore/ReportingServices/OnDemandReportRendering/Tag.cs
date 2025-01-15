using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200033E RID: 830
	internal sealed class Tag
	{
		// Token: 0x06001F1A RID: 7962 RVA: 0x00077AA0 File Offset: 0x00075CA0
		internal Tag(Image image, ExpressionInfo expression)
		{
			this.m_image = image;
			this.m_expression = expression;
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x00077AB6 File Offset: 0x00075CB6
		public ReportVariantProperty Value
		{
			get
			{
				if (this.m_value == null)
				{
					this.m_value = new ReportVariantProperty(this.m_expression);
				}
				return this.m_value;
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x00077AD7 File Offset: 0x00075CD7
		public TagInstance Instance
		{
			get
			{
				if (this.m_instance == null)
				{
					this.m_instance = new TagInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x00077AF3 File Offset: 0x00075CF3
		internal Image Image
		{
			get
			{
				return this.m_image;
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00077AFB File Offset: 0x00075CFB
		internal ExpressionInfo Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x00077B03 File Offset: 0x00075D03
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.ResetInstanceCache();
			}
		}

		// Token: 0x04000FC2 RID: 4034
		private readonly Image m_image;

		// Token: 0x04000FC3 RID: 4035
		private readonly ExpressionInfo m_expression;

		// Token: 0x04000FC4 RID: 4036
		private ReportVariantProperty m_value;

		// Token: 0x04000FC5 RID: 4037
		private TagInstance m_instance;
	}
}
