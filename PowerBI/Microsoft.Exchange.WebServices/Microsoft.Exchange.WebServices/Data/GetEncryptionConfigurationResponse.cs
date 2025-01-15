using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000161 RID: 353
	public sealed class GetEncryptionConfigurationResponse : ServiceResponse
	{
		// Token: 0x0600108D RID: 4237 RVA: 0x00030C72 File Offset: 0x0002FC72
		internal GetEncryptionConfigurationResponse()
		{
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x00030C7A File Offset: 0x0002FC7A
		public string ImageBase64
		{
			get
			{
				return this.imageBase64;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00030C82 File Offset: 0x0002FC82
		public string EmailText
		{
			get
			{
				return this.emailText;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00030C8A File Offset: 0x0002FC8A
		public string PortalText
		{
			get
			{
				return this.portalText;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x00030C92 File Offset: 0x0002FC92
		public string DisclaimerText
		{
			get
			{
				return this.disclaimerText;
			}
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00030C9C File Offset: 0x0002FC9C
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.imageBase64 = reader.ReadElementValue<string>(XmlNamespace.Messages, "ImageBase64");
			this.emailText = reader.ReadElementValue<string>(XmlNamespace.Messages, "EmailText");
			this.portalText = reader.ReadElementValue<string>(XmlNamespace.Messages, "PortalText");
			this.disclaimerText = reader.ReadElementValue<string>(XmlNamespace.Messages, "DisclaimerText");
		}

		// Token: 0x040009AF RID: 2479
		private string imageBase64;

		// Token: 0x040009B0 RID: 2480
		private string emailText;

		// Token: 0x040009B1 RID: 2481
		private string portalText;

		// Token: 0x040009B2 RID: 2482
		private string disclaimerText;
	}
}
