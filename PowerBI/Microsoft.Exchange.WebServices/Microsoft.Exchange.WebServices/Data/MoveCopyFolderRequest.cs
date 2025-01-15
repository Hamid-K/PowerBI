using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000EE RID: 238
	internal abstract class MoveCopyFolderRequest<TResponse> : MoveCopyRequest<Folder, TResponse> where TResponse : ServiceResponse
	{
		// Token: 0x06000C1B RID: 3099 RVA: 0x000287A5 File Offset: 0x000277A5
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParamCollection(this.FolderIds, "FolderIds");
			this.FolderIds.Validate(base.Service.RequestedServerVersion);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x000287D3 File Offset: 0x000277D3
		internal MoveCopyFolderRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000287E8 File Offset: 0x000277E8
		internal override void WriteIdsToXml(EwsServiceXmlWriter writer)
		{
			this.folderIds.WriteToXml(writer, XmlNamespace.Messages, "FolderIds");
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x000287FC File Offset: 0x000277FC
		internal override void AddIdsToJson(JsonObject jsonObject, ExchangeService service)
		{
			if (this.folderIds.Count > 0)
			{
				jsonObject.Add("FolderIds", this.folderIds.InternalToJson(service));
			}
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00028823 File Offset: 0x00027823
		internal override int GetExpectedResponseMessageCount()
		{
			return this.FolderIds.Count;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00028830 File Offset: 0x00027830
		internal FolderIdWrapperList FolderIds
		{
			get
			{
				return this.folderIds;
			}
		}

		// Token: 0x040008C1 RID: 2241
		private FolderIdWrapperList folderIds = new FolderIdWrapperList();
	}
}
