using System;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003E8 RID: 1000
	internal class ImageHelper : IVoluntarySerializable
	{
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x0007ED74 File Offset: 0x0007CF74
		// (set) Token: 0x06001FCA RID: 8138 RVA: 0x0007ED7C File Offset: 0x0007CF7C
		public MIMETypeExpr MIMEType
		{
			get
			{
				return this.m_mimeType;
			}
			set
			{
				this.m_mimeType = value;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x0007ED85 File Offset: 0x0007CF85
		// (set) Token: 0x06001FCC RID: 8140 RVA: 0x0007ED8D File Offset: 0x0007CF8D
		public ImageSource Source
		{
			get
			{
				return this.m_source;
			}
			set
			{
				this.m_source = value;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0007ED96 File Offset: 0x0007CF96
		// (set) Token: 0x06001FCE RID: 8142 RVA: 0x0007ED9E File Offset: 0x0007CF9E
		public string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0007ED96 File Offset: 0x0007CF96
		public override string ToString()
		{
			return this.m_value;
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x0007EDA7 File Offset: 0x0007CFA7
		bool IVoluntarySerializable.ShouldBeSerialized()
		{
			return !string.IsNullOrEmpty(this.m_value);
		}

		// Token: 0x04000DDD RID: 3549
		private ImageSource m_source;

		// Token: 0x04000DDE RID: 3550
		private string m_value;

		// Token: 0x04000DDF RID: 3551
		private MIMETypeExpr m_mimeType = new MIMETypeExpr();
	}
}
