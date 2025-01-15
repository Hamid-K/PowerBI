using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000101 RID: 257
	internal sealed class EmptyFolderRequest : DeleteRequest<ServiceResponse>
	{
		// Token: 0x06000CCD RID: 3277 RVA: 0x000297F1 File Offset: 0x000287F1
		internal EmptyFolderRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00029806 File Offset: 0x00028806
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.FolderIds, "FolderIds");
			this.FolderIds.Validate(base.Service.RequestedServerVersion);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00029834 File Offset: 0x00028834
		internal override int GetExpectedResponseMessageCount()
		{
			return this.FolderIds.Count;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00029841 File Offset: 0x00028841
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ServiceResponse();
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00029848 File Offset: 0x00028848
		internal override string GetXmlElementName()
		{
			return "EmptyFolder";
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002984F File Offset: 0x0002884F
		internal override string GetResponseXmlElementName()
		{
			return "EmptyFolderResponse";
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00029856 File Offset: 0x00028856
		internal override string GetResponseMessageXmlElementName()
		{
			return "EmptyFolderResponseMessage";
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0002985D File Offset: 0x0002885D
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.FolderIds.WriteToXml(writer, XmlNamespace.Messages, "FolderIds");
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00029871 File Offset: 0x00028871
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			writer.WriteAttributeValue("DeleteSubFolders", this.DeleteSubFolders);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00029890 File Offset: 0x00028890
		protected override void InternalToJson(JsonObject body)
		{
			body.Add("DeleteSubFolders", this.DeleteSubFolders);
			body.Add("FolderIds", this.FolderIds.InternalToJson(base.Service));
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x000298BF File Offset: 0x000288BF
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010_SP1;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x000298C2 File Offset: 0x000288C2
		internal FolderIdWrapperList FolderIds
		{
			get
			{
				return this.folderIds;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x000298CA File Offset: 0x000288CA
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x000298D2 File Offset: 0x000288D2
		internal bool DeleteSubFolders { get; set; }

		// Token: 0x040008DC RID: 2268
		private FolderIdWrapperList folderIds = new FolderIdWrapperList();
	}
}
