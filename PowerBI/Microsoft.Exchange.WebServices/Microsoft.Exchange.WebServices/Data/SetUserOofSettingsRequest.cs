using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200013C RID: 316
	internal sealed class SetUserOofSettingsRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000F58 RID: 3928 RVA: 0x0002DC94 File Offset: 0x0002CC94
		internal override string GetXmlElementName()
		{
			return "SetUserOofSettingsRequest";
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002DC9B File Offset: 0x0002CC9B
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.SmtpAddress, "SmtpAddress");
			EwsUtilities.ValidateParam(this.OofSettings, "OofSettings");
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0002DCC3 File Offset: 0x0002CCC3
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, "Mailbox");
			writer.WriteElementValue(XmlNamespace.Types, "Address", this.SmtpAddress);
			writer.WriteEndElement();
			this.OofSettings.WriteToXml(writer, "UserOofSettings");
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002DCFA File Offset: 0x0002CCFA
		internal override string GetResponseXmlElementName()
		{
			return "SetUserOofSettingsResponse";
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002DD04 File Offset: 0x0002CD04
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			ServiceResponse serviceResponse = new ServiceResponse();
			serviceResponse.LoadFromXml(reader, "ResponseMessage");
			return serviceResponse;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0002DD24 File Offset: 0x0002CD24
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002DD27 File Offset: 0x0002CD27
		internal SetUserOofSettingsRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002DD30 File Offset: 0x0002CD30
		internal ServiceResponse Execute()
		{
			ServiceResponse serviceResponse = (ServiceResponse)base.InternalExecute();
			serviceResponse.ThrowIfNecessary();
			return serviceResponse;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0002DD50 File Offset: 0x0002CD50
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x0002DD58 File Offset: 0x0002CD58
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
			set
			{
				this.smtpAddress = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0002DD61 File Offset: 0x0002CD61
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x0002DD69 File Offset: 0x0002CD69
		public OofSettings OofSettings
		{
			get
			{
				return this.oofSettings;
			}
			set
			{
				this.oofSettings = value;
			}
		}

		// Token: 0x04000966 RID: 2406
		private string smtpAddress;

		// Token: 0x04000967 RID: 2407
		private OofSettings oofSettings;
	}
}
