using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002DF RID: 735
	internal sealed class ServerAdditionalToken : IAdditionalToken
	{
		// Token: 0x06001A35 RID: 6709 RVA: 0x000693B4 File Offset: 0x000675B4
		public ServerAdditionalToken(RSService service, CatalogItemContext item)
		{
			this.m_service = service;
			string text = ((item != null && item.ItemPath != null) ? item.ItemPath.Value : null);
			if (string.IsNullOrEmpty(text))
			{
				text = this.m_service.CatalogToExternal(new CatalogItemPath("/"), true).Value;
			}
			this.m_itemPath = text;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00069413 File Offset: 0x00067613
		public byte[] GetAdditionalToken()
		{
			if (this.m_service.UserContext.AdditionalUserToken == null)
			{
				this.m_service.PopulateAdditionalToken(this.m_itemPath);
			}
			return this.m_service.UserContext.AdditionalUserToken;
		}

		// Token: 0x0400097B RID: 2427
		private readonly RSService m_service;

		// Token: 0x0400097C RID: 2428
		private readonly string m_itemPath;
	}
}
