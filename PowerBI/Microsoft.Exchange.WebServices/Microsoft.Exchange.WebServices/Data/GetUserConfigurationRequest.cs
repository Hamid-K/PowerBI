using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000128 RID: 296
	internal class GetUserConfigurationRequest : MultiResponseServiceRequest<GetUserConfigurationResponse>, IJsonSerializable
	{
		// Token: 0x06000E6D RID: 3693 RVA: 0x0002C34A File Offset: 0x0002B34A
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.name, "name");
			EwsUtilities.ValidateParam(this.parentFolderId, "parentFolderId");
			this.ParentFolderId.Validate(base.Service.RequestedServerVersion);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0002C388 File Offset: 0x0002B388
		internal override GetUserConfigurationResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			if (this.userConfiguration == null)
			{
				this.userConfiguration = new UserConfiguration(service, this.properties);
				this.userConfiguration.Name = this.name;
				this.userConfiguration.ParentFolderId = this.parentFolderId;
			}
			return new GetUserConfigurationResponse(this.userConfiguration);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0002C3DC File Offset: 0x0002B3DC
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0002C3DF File Offset: 0x0002B3DF
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0002C3E2 File Offset: 0x0002B3E2
		internal override string GetXmlElementName()
		{
			return "GetUserConfiguration";
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0002C3E9 File Offset: 0x0002B3E9
		internal override string GetResponseXmlElementName()
		{
			return "GetUserConfigurationResponse";
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0002C3F0 File Offset: 0x0002B3F0
		internal override string GetResponseMessageXmlElementName()
		{
			return "GetUserConfigurationResponseMessage";
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0002C3F7 File Offset: 0x0002B3F7
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			UserConfiguration.WriteUserConfigurationNameToXml(writer, XmlNamespace.Messages, this.name, this.parentFolderId);
			writer.WriteElementValue(XmlNamespace.Messages, "UserConfigurationProperties", this.properties.ToString().Replace(",", string.Empty));
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0002C438 File Offset: 0x0002B438
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("UserConfigurationName", UserConfiguration.GetJsonUserConfigName(service, this.parentFolderId, this.name));
			jsonObject.Add("UserConfigurationProperties", this.properties.ToString().Replace(",", string.Empty));
			return jsonObject;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0002C493 File Offset: 0x0002B493
		internal GetUserConfigurationRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0002C49D File Offset: 0x0002B49D
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x0002C4A5 File Offset: 0x0002B4A5
		internal string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0002C4AE File Offset: 0x0002B4AE
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x0002C4B6 File Offset: 0x0002B4B6
		internal FolderId ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
			set
			{
				this.parentFolderId = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0002C4BF File Offset: 0x0002B4BF
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x0002C4C7 File Offset: 0x0002B4C7
		internal UserConfiguration UserConfiguration
		{
			get
			{
				return this.userConfiguration;
			}
			set
			{
				this.userConfiguration = value;
				this.name = this.userConfiguration.Name;
				this.parentFolderId = this.userConfiguration.ParentFolderId;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0002C4F2 File Offset: 0x0002B4F2
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x0002C4FA File Offset: 0x0002B4FA
		internal UserConfigurationProperties Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x0400092C RID: 2348
		private const string EnumDelimiter = ",";

		// Token: 0x0400092D RID: 2349
		private string name;

		// Token: 0x0400092E RID: 2350
		private FolderId parentFolderId;

		// Token: 0x0400092F RID: 2351
		private UserConfigurationProperties properties;

		// Token: 0x04000930 RID: 2352
		private UserConfiguration userConfiguration;
	}
}
