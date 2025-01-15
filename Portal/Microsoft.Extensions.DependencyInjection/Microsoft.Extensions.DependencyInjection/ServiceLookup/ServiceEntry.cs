using System;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x0200001B RID: 27
	internal class ServiceEntry
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000039BB File Offset: 0x00001BBB
		public ServiceEntry(IService service)
		{
			this.First = service;
			this.Last = service;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000039DC File Offset: 0x00001BDC
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000039E4 File Offset: 0x00001BE4
		public IService First { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000039ED File Offset: 0x00001BED
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000039F5 File Offset: 0x00001BF5
		public IService Last { get; private set; }

		// Token: 0x060000B6 RID: 182 RVA: 0x00003A00 File Offset: 0x00001C00
		public void Add(IService service)
		{
			object sync = this._sync;
			lock (sync)
			{
				this.Last.Next = service;
				this.Last = service;
			}
		}

		// Token: 0x0400002E RID: 46
		private object _sync = new object();
	}
}
