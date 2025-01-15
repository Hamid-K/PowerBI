using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000C0A RID: 3082
	internal interface IExchangeService
	{
		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x060053D3 RID: 21459
		ExchangeService InnerService { get; }

		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x060053D4 RID: 21460
		IEngineHost EngineHost { get; }

		// Token: 0x060053D5 RID: 21461
		IEnumerable<ExchangeSearchResult> FindItems(FolderId folderId, ItemView itemView, SearchFilter searchFilter, string folderPath, ExchangeColumnInfo[] columnInfos, HashSet<PropertyDefinitionBase> additionalPropertie, out bool moreAvailable, out int? nextPageOffset);

		// Token: 0x060053D6 RID: 21462
		int FindItemCount(FolderId folderId, SearchFilter searchFilter);

		// Token: 0x060053D7 RID: 21463
		IEnumerable<ExchangeSearchResult> FindFolders(FolderId folderId, FolderView folderView, SearchFilter searchFilter, out bool moreAvailable, out int? nextPageOffset);

		// Token: 0x060053D8 RID: 21464
		ExchangeSearchResult GetItem(string itemIdString, string folderPath, PropertyDefinitionBase[] properties, params ExchangeColumnInfo[] columns);
	}
}
