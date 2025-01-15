using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200005C RID: 92
	public sealed class Bookmark
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x00019306 File Offset: 0x00017506
		internal Bookmark(string bookmarkId, BookmarkInformation underlyingNode)
		{
			Global.Tracer.Assert(underlyingNode != null, "The bookmark node being wrapped cannot be null.");
			this.m_bookmarkId = bookmarkId;
			this.m_underlyingNode = underlyingNode;
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001932F File Offset: 0x0001752F
		public string BookmarkId
		{
			get
			{
				return this.m_bookmarkId;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00019337 File Offset: 0x00017537
		public string UniqueName
		{
			get
			{
				return this.m_underlyingNode.Id;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00019344 File Offset: 0x00017544
		public int Page
		{
			get
			{
				return this.m_underlyingNode.Page;
			}
		}

		// Token: 0x040001B3 RID: 435
		private BookmarkInformation m_underlyingNode;

		// Token: 0x040001B4 RID: 436
		private string m_bookmarkId;
	}
}
