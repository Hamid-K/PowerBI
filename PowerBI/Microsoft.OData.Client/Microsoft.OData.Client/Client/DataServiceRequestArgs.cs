using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client
{
	// Token: 0x020000C4 RID: 196
	public class DataServiceRequestArgs
	{
		// Token: 0x0600065F RID: 1631 RVA: 0x0001BEC1 File Offset: 0x0001A0C1
		public DataServiceRequestArgs()
		{
			this.HeaderCollection = new HeaderCollection();
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001BED4 File Offset: 0x0001A0D4
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0001BEE6 File Offset: 0x0001A0E6
		public string AcceptContentType
		{
			get
			{
				return this.HeaderCollection.GetHeader("Accept");
			}
			set
			{
				this.HeaderCollection.SetHeader("Accept", value);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001BEF9 File Offset: 0x0001A0F9
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x0001BF0B File Offset: 0x0001A10B
		public string ContentType
		{
			get
			{
				return this.HeaderCollection.GetHeader("Content-Type");
			}
			set
			{
				this.HeaderCollection.SetHeader("Content-Type", value);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001BF1E File Offset: 0x0001A11E
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0001BF30 File Offset: 0x0001A130
		public string Slug
		{
			get
			{
				return this.HeaderCollection.GetHeader("Slug");
			}
			set
			{
				this.HeaderCollection.SetHeader("Slug", value);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001BF43 File Offset: 0x0001A143
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x0001BF55 File Offset: 0x0001A155
		public Dictionary<string, string> Headers
		{
			get
			{
				return (Dictionary<string, string>)this.HeaderCollection.UnderlyingDictionary;
			}
			internal set
			{
				this.HeaderCollection = new HeaderCollection(value);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001BF63 File Offset: 0x0001A163
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x0001BF6B File Offset: 0x0001A16B
		internal HeaderCollection HeaderCollection { get; private set; }
	}
}
