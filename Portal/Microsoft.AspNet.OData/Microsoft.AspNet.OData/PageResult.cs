using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Newtonsoft.Json;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200004B RID: 75
	[DataContract]
	[JsonObject]
	public class PageResult<T> : PageResult, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00007D27 File Offset: 0x00005F27
		public PageResult(IEnumerable<T> items, Uri nextPageLink, long? count)
			: base(nextPageLink, count)
		{
			if (items == null)
			{
				throw Error.ArgumentNull("data");
			}
			this.Items = items;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007D46 File Offset: 0x00005F46
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00007D4E File Offset: 0x00005F4E
		[DataMember]
		public IEnumerable<T> Items { get; private set; }

		// Token: 0x060001BF RID: 447 RVA: 0x00007D57 File Offset: 0x00005F57
		public IEnumerator<T> GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007D57 File Offset: 0x00005F57
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}
	}
}
