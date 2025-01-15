using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BCE RID: 3022
	internal class ExchangeCachingSearchResult : ExchangeSearchResult
	{
		// Token: 0x06005267 RID: 21095 RVA: 0x00116778 File Offset: 0x00114978
		public ExchangeCachingSearchResult(Dictionary<string, object> cachingDictionary, string id, string folderPath)
			: base(id, folderPath)
		{
			this.cachingDictionary = cachingDictionary;
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x0011678C File Offset: 0x0011498C
		public override object GetColumnValue(ExchangeColumnInfo columnInfo)
		{
			if (columnInfo.ColumnCategory == ColumnCategory.FolderPath)
			{
				return base.FolderPath;
			}
			if (columnInfo.UniqueName == "Id")
			{
				return base.Id;
			}
			object obj;
			if (this.cachingDictionary.TryGetValue(columnInfo.UniqueName, out obj))
			{
				return obj;
			}
			throw new InvalidOperationException("Caching key is missing.");
		}

		// Token: 0x04002D6A RID: 11626
		private Dictionary<string, object> cachingDictionary;
	}
}
