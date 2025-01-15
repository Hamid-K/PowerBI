using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000FD RID: 253
	internal class DeleteUserConfigurationRequest : MultiResponseServiceRequest<ServiceResponse>, IJsonSerializable
	{
		// Token: 0x06000CAB RID: 3243 RVA: 0x000295E1 File Offset: 0x000285E1
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.name, "name");
			EwsUtilities.ValidateParam(this.parentFolderId, "parentFolderId");
			this.ParentFolderId.Validate(base.Service.RequestedServerVersion);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002961F File Offset: 0x0002861F
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ServiceResponse();
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00029626 File Offset: 0x00028626
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00029629 File Offset: 0x00028629
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002962C File Offset: 0x0002862C
		internal override string GetXmlElementName()
		{
			return "DeleteUserConfiguration";
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00029633 File Offset: 0x00028633
		internal override string GetResponseXmlElementName()
		{
			return "DeleteUserConfigurationResponse";
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002963A File Offset: 0x0002863A
		internal override string GetResponseMessageXmlElementName()
		{
			return "DeleteUserConfigurationResponseMessage";
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00029641 File Offset: 0x00028641
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			UserConfiguration.WriteUserConfigurationNameToXml(writer, XmlNamespace.Messages, this.name, this.parentFolderId);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00029658 File Offset: 0x00028658
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("UserConfigurationName", UserConfiguration.GetJsonUserConfigName(service, this.parentFolderId, this.name));
			return jsonObject;
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00029689 File Offset: 0x00028689
		internal DeleteUserConfigurationRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00029693 File Offset: 0x00028693
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x0002969B File Offset: 0x0002869B
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

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x000296A4 File Offset: 0x000286A4
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x000296AC File Offset: 0x000286AC
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

		// Token: 0x040008D2 RID: 2258
		private string name;

		// Token: 0x040008D3 RID: 2259
		private FolderId parentFolderId;
	}
}
