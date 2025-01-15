using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200005B RID: 91
	internal sealed class Bookmarks
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x0001929A File Offset: 0x0001749A
		internal Bookmarks(BookmarksHashtable reportBookmarks)
		{
			Global.Tracer.Assert(reportBookmarks != null, "The bookmark hashtable being wrapped cannot be null.");
			this.m_reportBookmarks = reportBookmarks;
		}

		// Token: 0x170004E5 RID: 1253
		public Bookmark this[string bookmarkId]
		{
			get
			{
				if (bookmarkId == null || this.m_reportBookmarks == null)
				{
					return null;
				}
				BookmarkInformation bookmarkInformation = this.m_reportBookmarks[bookmarkId];
				if (bookmarkInformation != null)
				{
					return new Bookmark(bookmarkId, bookmarkInformation);
				}
				return null;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x000192EF File Offset: 0x000174EF
		public IDictionaryEnumerator BookmarksEnumerator
		{
			get
			{
				if (this.m_reportBookmarks == null)
				{
					return null;
				}
				return this.m_reportBookmarks.GetEnumerator();
			}
		}

		// Token: 0x040001B2 RID: 434
		private BookmarksHashtable m_reportBookmarks;
	}
}
