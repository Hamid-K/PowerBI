using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000295 RID: 661
	internal class FolderIdWrapper : AbstractFolderIdWrapper
	{
		// Token: 0x0600174D RID: 5965 RVA: 0x0003F7A0 File Offset: 0x0003E7A0
		internal FolderIdWrapper(FolderId folderId)
		{
			EwsUtilities.Assert(folderId != null, "FolderIdWrapper.ctor", "folderId is null");
			this.folderId = folderId;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0003F7C5 File Offset: 0x0003E7C5
		internal override void WriteToXml(EwsServiceXmlWriter writer)
		{
			this.folderId.WriteToXml(writer);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0003F7D3 File Offset: 0x0003E7D3
		internal override void Validate(ExchangeVersion version)
		{
			this.folderId.Validate(version);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0003F7E1 File Offset: 0x0003E7E1
		internal override object InternalToJson(ExchangeService service)
		{
			return this.folderId.InternalToJson(service);
		}

		// Token: 0x04001350 RID: 4944
		private FolderId folderId;
	}
}
