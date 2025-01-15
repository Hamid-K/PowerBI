using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000139 RID: 313
	internal sealed class SetEncryptionConfigurationRequest : SimpleServiceRequestBase
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0002D868 File Offset: 0x0002C868
		public string ImageBase64
		{
			get
			{
				return this.imageBase64;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0002D870 File Offset: 0x0002C870
		public string EmailText
		{
			get
			{
				return this.emailText;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0002D878 File Offset: 0x0002C878
		public string PortalText
		{
			get
			{
				return this.portalText;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0002D880 File Offset: 0x0002C880
		public string DisclaimerText
		{
			get
			{
				return this.disclaimerText;
			}
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0002D888 File Offset: 0x0002C888
		internal SetEncryptionConfigurationRequest(ExchangeService service, string imageBase64, string emailText, string portalText, string disclaimerText)
			: base(service)
		{
			this.emailText = emailText;
			this.portalText = portalText;
			this.imageBase64 = imageBase64;
			this.disclaimerText = disclaimerText;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0002D8AF File Offset: 0x0002C8AF
		internal override string GetXmlElementName()
		{
			return "SetEncryptionConfiguration";
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0002D8B8 File Offset: 0x0002C8B8
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Messages, "ImageBase64", this.ImageBase64);
			writer.WriteElementValue(XmlNamespace.Messages, "EmailText", this.EmailText);
			writer.WriteElementValue(XmlNamespace.Messages, "PortalText", this.PortalText);
			writer.WriteElementValue(XmlNamespace.Messages, "DisclaimerText", this.disclaimerText);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0002D90D File Offset: 0x0002C90D
		internal override string GetResponseXmlElementName()
		{
			return "SetEncryptionConfigurationResponse";
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0002D914 File Offset: 0x0002C914
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			SetEncryptionConfigurationResponse setEncryptionConfigurationResponse = new SetEncryptionConfigurationResponse();
			setEncryptionConfigurationResponse.LoadFromXml(reader, this.GetResponseXmlElementName());
			return setEncryptionConfigurationResponse;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0002D935 File Offset: 0x0002C935
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0002D938 File Offset: 0x0002C938
		internal ServiceResponse Execute()
		{
			SetEncryptionConfigurationResponse setEncryptionConfigurationResponse = (SetEncryptionConfigurationResponse)base.InternalExecute();
			setEncryptionConfigurationResponse.ThrowIfNecessary();
			return setEncryptionConfigurationResponse;
		}

		// Token: 0x04000958 RID: 2392
		private readonly string imageBase64;

		// Token: 0x04000959 RID: 2393
		private readonly string emailText;

		// Token: 0x0400095A RID: 2394
		private readonly string portalText;

		// Token: 0x0400095B RID: 2395
		private readonly string disclaimerText;
	}
}
