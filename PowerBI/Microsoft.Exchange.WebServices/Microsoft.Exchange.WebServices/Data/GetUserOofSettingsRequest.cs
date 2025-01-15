using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000129 RID: 297
	internal sealed class GetUserOofSettingsRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000E7F RID: 3711 RVA: 0x0002C503 File Offset: 0x0002B503
		internal override string GetXmlElementName()
		{
			return "GetUserOofSettingsRequest";
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0002C50A File Offset: 0x0002B50A
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.SmtpAddress, "SmtpAddress");
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0002C522 File Offset: 0x0002B522
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, "Mailbox");
			writer.WriteElementValue(XmlNamespace.Types, "Address", this.SmtpAddress);
			writer.WriteEndElement();
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0002C548 File Offset: 0x0002B548
		internal override string GetResponseXmlElementName()
		{
			return "GetUserOofSettingsResponse";
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0002C550 File Offset: 0x0002B550
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetUserOofSettingsResponse getUserOofSettingsResponse = new GetUserOofSettingsResponse();
			getUserOofSettingsResponse.LoadFromXml(reader, "ResponseMessage");
			if (getUserOofSettingsResponse.ErrorCode == ServiceError.NoError)
			{
				reader.ReadStartElement(XmlNamespace.Types, "OofSettings");
				getUserOofSettingsResponse.OofSettings = new OofSettings();
				getUserOofSettingsResponse.OofSettings.LoadFromXml(reader, reader.LocalName);
				getUserOofSettingsResponse.OofSettings.AllowExternalOof = reader.ReadElementValue<OofExternalAudience>(XmlNamespace.Messages, "AllowExternalOof");
			}
			return getUserOofSettingsResponse;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0002C5B8 File Offset: 0x0002B5B8
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0002C5BB File Offset: 0x0002B5BB
		internal GetUserOofSettingsRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0002C5C4 File Offset: 0x0002B5C4
		internal GetUserOofSettingsResponse Execute()
		{
			GetUserOofSettingsResponse getUserOofSettingsResponse = (GetUserOofSettingsResponse)base.InternalExecute();
			getUserOofSettingsResponse.ThrowIfNecessary();
			return getUserOofSettingsResponse;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0002C5E4 File Offset: 0x0002B5E4
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x0002C5EC File Offset: 0x0002B5EC
		internal string SmtpAddress
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

		// Token: 0x04000931 RID: 2353
		private string smtpAddress;
	}
}
