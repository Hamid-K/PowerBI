using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000FB RID: 251
	internal sealed class DeleteFolderRequest : DeleteRequest<ServiceResponse>
	{
		// Token: 0x06000C8E RID: 3214 RVA: 0x00029317 File Offset: 0x00028317
		internal DeleteFolderRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002932C File Offset: 0x0002832C
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.FolderIds, "FolderIds");
			this.FolderIds.Validate(base.Service.RequestedServerVersion);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002935A File Offset: 0x0002835A
		internal override int GetExpectedResponseMessageCount()
		{
			return this.FolderIds.Count;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00029367 File Offset: 0x00028367
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ServiceResponse();
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002936E File Offset: 0x0002836E
		internal override string GetXmlElementName()
		{
			return "DeleteFolder";
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00029375 File Offset: 0x00028375
		internal override string GetResponseXmlElementName()
		{
			return "DeleteFolderResponse";
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002937C File Offset: 0x0002837C
		internal override string GetResponseMessageXmlElementName()
		{
			return "DeleteFolderResponseMessage";
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00029383 File Offset: 0x00028383
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.FolderIds.WriteToXml(writer, XmlNamespace.Messages, "FolderIds");
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00029397 File Offset: 0x00028397
		protected override void InternalToJson(JsonObject body)
		{
			body.Add("FolderIds", this.FolderIds.InternalToJson(base.Service));
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x000293B5 File Offset: 0x000283B5
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x000293B8 File Offset: 0x000283B8
		internal FolderIdWrapperList FolderIds
		{
			get
			{
				return this.folderIds;
			}
		}

		// Token: 0x040008CD RID: 2253
		private FolderIdWrapperList folderIds = new FolderIdWrapperList();
	}
}
