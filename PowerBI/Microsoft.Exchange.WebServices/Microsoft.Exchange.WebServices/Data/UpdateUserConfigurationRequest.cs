using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200014A RID: 330
	internal class UpdateUserConfigurationRequest : MultiResponseServiceRequest<ServiceResponse>, IJsonSerializable
	{
		// Token: 0x0600100F RID: 4111 RVA: 0x0002F154 File Offset: 0x0002E154
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.userConfiguration, "userConfiguration");
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0002F16C File Offset: 0x0002E16C
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ServiceResponse();
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0002F173 File Offset: 0x0002E173
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0002F176 File Offset: 0x0002E176
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0002F179 File Offset: 0x0002E179
		internal override string GetXmlElementName()
		{
			return "UpdateUserConfiguration";
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0002F180 File Offset: 0x0002E180
		internal override string GetResponseXmlElementName()
		{
			return "UpdateUserConfigurationResponse";
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0002F187 File Offset: 0x0002E187
		internal override string GetResponseMessageXmlElementName()
		{
			return "UpdateUserConfigurationResponseMessage";
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0002F18E File Offset: 0x0002E18E
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.userConfiguration.WriteToXml(writer, XmlNamespace.Messages, "UserConfiguration");
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0002F1A2 File Offset: 0x0002E1A2
		internal UpdateUserConfigurationRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0002F1AC File Offset: 0x0002E1AC
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x0002F1B4 File Offset: 0x0002E1B4
		public UserConfiguration UserConfiguration
		{
			get
			{
				return this.userConfiguration;
			}
			set
			{
				this.userConfiguration = value;
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0002F1C0 File Offset: 0x0002E1C0
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("UserConfiguration", ((IJsonSerializable)this.UserConfiguration).ToJson(service));
			return jsonObject;
		}

		// Token: 0x04000988 RID: 2440
		protected UserConfiguration userConfiguration;
	}
}
