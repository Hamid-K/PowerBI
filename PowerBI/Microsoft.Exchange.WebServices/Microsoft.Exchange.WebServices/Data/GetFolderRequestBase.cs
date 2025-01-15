using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000113 RID: 275
	internal abstract class GetFolderRequestBase<TResponse> : GetRequest<Folder, TResponse> where TResponse : ServiceResponse
	{
		// Token: 0x06000DAF RID: 3503 RVA: 0x0002B22F File Offset: 0x0002A22F
		protected GetFolderRequestBase(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0002B244 File Offset: 0x0002A244
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParamCollection(this.FolderIds, "FolderIds");
			this.FolderIds.Validate(base.Service.RequestedServerVersion);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002B272 File Offset: 0x0002A272
		internal override int GetExpectedResponseMessageCount()
		{
			return this.FolderIds.Count;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0002B27F File Offset: 0x0002A27F
		internal override ServiceObjectType GetServiceObjectType()
		{
			return ServiceObjectType.Folder;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0002B282 File Offset: 0x0002A282
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			this.FolderIds.WriteToXml(writer, XmlNamespace.Messages, "FolderIds");
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002B29D File Offset: 0x0002A29D
		internal override void AddIdsToRequest(JsonObject jsonRequest, ExchangeService service)
		{
			jsonRequest.Add("FolderIds", this.FolderIds.InternalToJson(service));
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0002B2B6 File Offset: 0x0002A2B6
		internal override string GetXmlElementName()
		{
			return "GetFolder";
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002B2BD File Offset: 0x0002A2BD
		internal override string GetResponseXmlElementName()
		{
			return "GetFolderResponse";
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002B2C4 File Offset: 0x0002A2C4
		internal override string GetResponseMessageXmlElementName()
		{
			return "GetFolderResponseMessage";
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002B2CB File Offset: 0x0002A2CB
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0002B2CE File Offset: 0x0002A2CE
		public FolderIdWrapperList FolderIds
		{
			get
			{
				return this.folderIds;
			}
		}

		// Token: 0x0400090A RID: 2314
		private FolderIdWrapperList folderIds = new FolderIdWrapperList();
	}
}
