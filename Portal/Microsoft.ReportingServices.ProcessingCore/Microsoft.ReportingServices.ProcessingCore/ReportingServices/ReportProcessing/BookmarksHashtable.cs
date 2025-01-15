using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006B8 RID: 1720
	[Serializable]
	internal sealed class BookmarksHashtable : HashtableInstanceInfo
	{
		// Token: 0x06005CCA RID: 23754 RVA: 0x0017A322 File Offset: 0x00178522
		internal BookmarksHashtable()
		{
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x0017A32A File Offset: 0x0017852A
		internal BookmarksHashtable(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002092 RID: 8338
		internal BookmarkInformation this[string key]
		{
			get
			{
				return (BookmarkInformation)this.m_hashtable[key];
			}
			set
			{
				this.m_hashtable[key] = value;
			}
		}

		// Token: 0x06005CCE RID: 23758 RVA: 0x0017A355 File Offset: 0x00178555
		internal void Add(string bookmark, BookmarkInformation bookmarkInfo)
		{
			this.m_hashtable.Add(bookmark, bookmarkInfo);
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x0017A364 File Offset: 0x00178564
		internal void Add(string bookmark, int page, string id)
		{
			if (this.m_hashtable.Contains(bookmark))
			{
				BookmarkInformation bookmarkInformation = this[bookmark];
				if (bookmarkInformation.Page > page)
				{
					bookmarkInformation.Page = page;
					bookmarkInformation.Id = id;
					return;
				}
			}
			else
			{
				BookmarkInformation bookmarkInformation = new BookmarkInformation(id, page);
				this.m_hashtable.Add(bookmark, bookmarkInformation);
			}
		}
	}
}
