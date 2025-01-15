using System;

namespace Microsoft.Exchange.WebServices.Dns
{
	// Token: 0x020001DC RID: 476
	internal abstract class DnsRecord
	{
		// Token: 0x06001571 RID: 5489 RVA: 0x0003C845 File Offset: 0x0003B845
		internal virtual void Load(DnsRecordHeader header, IntPtr dataPointer)
		{
			this.name = header.Name;
			this.timeToLive = Math.Max(1U, header.Ttl);
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001572 RID: 5490
		internal abstract DnsRecordType RecordType { get; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0003C867 File Offset: 0x0003B867
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0003C86F File Offset: 0x0003B86F
		public TimeSpan TimeToLive
		{
			get
			{
				return TimeSpan.FromSeconds(this.timeToLive);
			}
		}

		// Token: 0x04000D04 RID: 3332
		private string name;

		// Token: 0x04000D05 RID: 3333
		private uint timeToLive;
	}
}
