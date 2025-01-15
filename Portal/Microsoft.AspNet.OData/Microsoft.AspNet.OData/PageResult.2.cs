using System;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200004C RID: 76
	[DataContract]
	public abstract class PageResult
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00007D64 File Offset: 0x00005F64
		protected PageResult(Uri nextPageLink, long? count)
		{
			this.NextPageLink = nextPageLink;
			this.Count = count;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00007D7A File Offset: 0x00005F7A
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00007D82 File Offset: 0x00005F82
		[DataMember]
		public Uri NextPageLink { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00007D8B File Offset: 0x00005F8B
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00007D93 File Offset: 0x00005F93
		[DataMember]
		public long? Count
		{
			get
			{
				return this._count;
			}
			private set
			{
				if (value != null && value.Value < 0L)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value.Value, 0);
				}
				this._count = value;
			}
		}

		// Token: 0x0400007A RID: 122
		private long? _count;
	}
}
