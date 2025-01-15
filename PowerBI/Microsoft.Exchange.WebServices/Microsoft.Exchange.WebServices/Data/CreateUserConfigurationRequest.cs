using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000F8 RID: 248
	internal class CreateUserConfigurationRequest : MultiResponseServiceRequest<ServiceResponse>, IJsonSerializable
	{
		// Token: 0x06000C71 RID: 3185 RVA: 0x0002903B File Offset: 0x0002803B
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.userConfiguration, "userConfiguration");
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00029053 File Offset: 0x00028053
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ServiceResponse();
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0002905A File Offset: 0x0002805A
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002905D File Offset: 0x0002805D
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00029060 File Offset: 0x00028060
		internal override string GetXmlElementName()
		{
			return "CreateUserConfiguration";
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00029067 File Offset: 0x00028067
		internal override string GetResponseXmlElementName()
		{
			return "CreateUserConfigurationResponse";
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002906E File Offset: 0x0002806E
		internal override string GetResponseMessageXmlElementName()
		{
			return "CreateUserConfigurationResponseMessage";
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00029075 File Offset: 0x00028075
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.userConfiguration.WriteToXml(writer, XmlNamespace.Messages, "UserConfiguration");
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0002908C File Offset: 0x0002808C
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("UserConfiguration", ((IJsonSerializable)this.UserConfiguration).ToJson(service));
			return jsonObject;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x000290B7 File Offset: 0x000280B7
		internal CreateUserConfigurationRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x000290C1 File Offset: 0x000280C1
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x000290C9 File Offset: 0x000280C9
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

		// Token: 0x040008CA RID: 2250
		protected UserConfiguration userConfiguration;
	}
}
