using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000278 RID: 632
	[DataContract]
	internal class NetworkWriteBackItem
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x000412AF File Offset: 0x0003F4AF
		// (set) Token: 0x06001575 RID: 5493 RVA: 0x000412B7 File Offset: 0x0003F4B7
		public int WriteFailCount
		{
			get
			{
				return this._writeFailCount;
			}
			internal set
			{
				this._writeFailCount = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x000412C0 File Offset: 0x0003F4C0
		// (set) Token: 0x06001577 RID: 5495 RVA: 0x000412C8 File Offset: 0x0003F4C8
		public long FirstWrite
		{
			get
			{
				return this._firstWrite;
			}
			internal set
			{
				this._firstWrite = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x000412D1 File Offset: 0x0003F4D1
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x000412D9 File Offset: 0x0003F4D9
		public OMCacheItem OmItem
		{
			get
			{
				return this._omItem;
			}
			internal set
			{
				this._omItem = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x000412E2 File Offset: 0x0003F4E2
		// (set) Token: 0x0600157B RID: 5499 RVA: 0x000412EA File Offset: 0x0003F4EA
		public StoreOperation Operation
		{
			get
			{
				return this._operation;
			}
			internal set
			{
				this._operation = value;
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x000412F3 File Offset: 0x0003F4F3
		internal NetworkWriteBackItem(WriteBackItem item)
		{
			this.WriteFailCount = item.WriteFailCount;
			this.Operation = item.Operation;
			this.FirstWrite = item.FirstWrite;
			this.OmItem = new OMCacheItem(item.OmItem);
		}

		// Token: 0x04000C6D RID: 3181
		internal static int SizeOfReferences = 6 * IntPtr.Size;

		// Token: 0x04000C6E RID: 3182
		[DataMember]
		private int _writeFailCount;

		// Token: 0x04000C6F RID: 3183
		[DataMember]
		private long _firstWrite;

		// Token: 0x04000C70 RID: 3184
		[DataMember]
		private OMCacheItem _omItem;

		// Token: 0x04000C71 RID: 3185
		[DataMember]
		private StoreOperation _operation;
	}
}
