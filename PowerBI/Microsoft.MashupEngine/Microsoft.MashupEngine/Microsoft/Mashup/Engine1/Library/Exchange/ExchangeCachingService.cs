using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BCF RID: 3023
	internal class ExchangeCachingService : IExchangeService
	{
		// Token: 0x06005269 RID: 21097 RVA: 0x001167E3 File Offset: 0x001149E3
		public ExchangeCachingService(IEngineHost host, IExchangeService service, string credentialHash, string mailbox)
		{
			this.host = host;
			this.service = service;
			this.cache = this.host.GetPersistentCache();
			this.credentialHash = credentialHash;
			this.mailbox = mailbox;
		}

		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x0600526A RID: 21098 RVA: 0x00116819 File Offset: 0x00114A19
		public ExchangeService InnerService
		{
			get
			{
				return this.service.InnerService;
			}
		}

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x0600526B RID: 21099 RVA: 0x00116826 File Offset: 0x00114A26
		public IEngineHost EngineHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x00116830 File Offset: 0x00114A30
		public int FindItemCount(FolderId folderId, SearchFilter searchFilter)
		{
			string key = this.GetKey(folderId, 0, 1, searchFilter, EmptyArray<PropertyDefinitionBase>.Instance, EmptyArray<SortOption>.Instance, this.mailbox, true);
			Stream stream;
			if (this.cache.TryGetValue(key, out stream))
			{
				using (stream)
				{
					return ExchangeSearchResultSerializer.DeserializeCount(stream);
				}
			}
			int num = this.service.FindItemCount(folderId, searchFilter);
			ExchangeSearchResultSerializer.SerializeCount(this.cache, key, num);
			return num;
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x001168B4 File Offset: 0x00114AB4
		public IEnumerable<ExchangeSearchResult> FindItems(FolderId folderId, ItemView itemView, SearchFilter searchFilter, string folderPath, ExchangeColumnInfo[] columnInfos, HashSet<PropertyDefinitionBase> additionalProperties, out bool moreAvailable, out int? nextPageOffset)
		{
			PropertyDefinitionBase[] propertiesToFetch = columnInfos.GetPropertiesToFetch(additionalProperties);
			string key = this.GetKey(folderId, itemView.Offset, itemView.PageSize, searchFilter, propertiesToFetch, columnInfos.GetTopLevelSortCollection(), this.mailbox, false);
			Stream stream;
			if (this.cache.TryGetValue(key, out stream))
			{
				using (stream)
				{
					return ExchangeSearchResultSerializer.Deserialize(stream, out moreAvailable, out nextPageOffset).ToArray<ExchangeSearchResult>();
				}
			}
			IEnumerable<ExchangeSearchResult> enumerable = this.service.FindItems(folderId, itemView, searchFilter, folderPath, columnInfos, additionalProperties, out moreAvailable, out nextPageOffset);
			ExchangeSearchResultSerializer.Serialize(this.cache, key, enumerable, moreAvailable, nextPageOffset, columnInfos, propertiesToFetch);
			return enumerable;
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x00116970 File Offset: 0x00114B70
		public IEnumerable<ExchangeSearchResult> FindFolders(FolderId folderId, FolderView folderView, SearchFilter searchFilter, out bool moreAvailable, out int? nextPageOffset)
		{
			string key = this.GetKey(folderId, folderView.Offset, folderView.PageSize, searchFilter, null, null, this.mailbox, false);
			Stream stream;
			if (this.cache.TryGetValue(key, out stream))
			{
				using (stream)
				{
					return ExchangeSearchResultSerializer.Deserialize(stream, out moreAvailable, out nextPageOffset).ToArray<ExchangeSearchResult>();
				}
			}
			IEnumerable<ExchangeSearchResult> enumerable = this.service.FindFolders(folderId, folderView, searchFilter, out moreAvailable, out nextPageOffset);
			ExchangeSearchResultSerializer.Serialize(this.cache, key, enumerable, moreAvailable, nextPageOffset, null, null);
			return enumerable;
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x00116A10 File Offset: 0x00114C10
		public ExchangeSearchResult GetItem(string itemIdString, string folderPath, PropertyDefinitionBase[] properties, params ExchangeColumnInfo[] columnInfos)
		{
			if (columnInfos.Length == 1 && columnInfos[0].ColumnCategory == ColumnCategory.AttachmentTableColumn)
			{
				return this.service.GetItem(itemIdString, folderPath, properties, columnInfos);
			}
			string key = this.GetKey(itemIdString, properties);
			Stream stream;
			if (this.cache.TryGetValue(key, out stream))
			{
				using (stream)
				{
					return ExchangeSearchResultSerializer.Deserialize(stream);
				}
			}
			ExchangeSearchResult item = this.service.GetItem(itemIdString, folderPath, properties, columnInfos);
			ExchangeSearchResultSerializer.Serialize(this.cache, key, item, columnInfos, properties);
			return item;
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x00116AAC File Offset: 0x00114CAC
		public static string GetStringCacheKey(string credentialPath, string key)
		{
			return PersistentCacheKey.ExchangeContents.Qualify(credentialPath, key);
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x00116AC8 File Offset: 0x00114CC8
		private string GetKey(string itemId, PropertyDefinitionBase[] properties)
		{
			string[] sortedPropertyNames = properties.GetSortedPropertyNames();
			return PersistentCacheKey.ExchangeContents.Qualify(this.credentialHash, itemId, string.Join("/", sortedPropertyNames));
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x00116AFC File Offset: 0x00114CFC
		private string GetKey(FolderId folderId, int offset, int pageSize, SearchFilter searchFilter, PropertyDefinitionBase[] properties, SortOption[] sortOptions, string mailbox, bool isCount = false)
		{
			string[] sortedPropertyNames = properties.GetSortedPropertyNames();
			string[] sortedSortOptionNames = sortOptions.GetSortedSortOptionNames();
			return PersistentCacheKey.ExchangeContents.Qualify(ExchangeCachingService.BuildKey(new string[]
			{
				this.credentialHash,
				ExchangeCachingService.GetFolderIdString(folderId),
				offset.ToString(CultureInfo.InvariantCulture),
				pageSize.ToString(CultureInfo.InvariantCulture),
				searchFilter.GetSearchFilterString(),
				string.Join("/", sortedPropertyNames),
				string.Join("/", sortedSortOptionNames),
				isCount.ToString(),
				mailbox
			}));
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x00116B98 File Offset: 0x00114D98
		private static string GetFolderIdString(FolderId folderId)
		{
			if (folderId == null)
			{
				return string.Empty;
			}
			string text = folderId.UniqueId;
			if (folderId.FolderName != null)
			{
				text = text + "/" + folderId.FolderName.Value.ToString();
			}
			return text;
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x00116BF0 File Offset: 0x00114DF0
		private static string BuildKey(params string[] keys)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			for (int i = 0; i < keys.Length; i++)
			{
				stringBuilder.Append(keys[i]);
				if (i != keys.Length - 1)
				{
					stringBuilder.Append("/");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002D6B RID: 11627
		private readonly IExchangeService service;

		// Token: 0x04002D6C RID: 11628
		private readonly IEngineHost host;

		// Token: 0x04002D6D RID: 11629
		private readonly IPersistentCache cache;

		// Token: 0x04002D6E RID: 11630
		private readonly string credentialHash;

		// Token: 0x04002D6F RID: 11631
		private readonly string mailbox;
	}
}
