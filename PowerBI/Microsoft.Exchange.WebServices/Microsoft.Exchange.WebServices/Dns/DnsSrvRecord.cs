using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.WebServices.Dns
{
	// Token: 0x020001DE RID: 478
	internal class DnsSrvRecord : DnsRecord
	{
		// Token: 0x06001576 RID: 5494 RVA: 0x0003C888 File Offset: 0x0003B888
		internal override void Load(DnsRecordHeader header, IntPtr dataPointer)
		{
			base.Load(header, dataPointer);
			DnsSrvRecord.Win32DnsSrvRecord win32DnsSrvRecord = (DnsSrvRecord.Win32DnsSrvRecord)Marshal.PtrToStructure(dataPointer, typeof(DnsSrvRecord.Win32DnsSrvRecord));
			this.target = win32DnsSrvRecord.NameTarget;
			this.priority = (int)win32DnsSrvRecord.Priority;
			this.weight = (int)win32DnsSrvRecord.Weight;
			this.port = (int)win32DnsSrvRecord.Port;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0003C8E7 File Offset: 0x0003B8E7
		internal override DnsRecordType RecordType
		{
			get
			{
				return DnsRecordType.SRV;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0003C8EB File Offset: 0x0003B8EB
		internal string NameTarget
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x0003C8F3 File Offset: 0x0003B8F3
		internal int Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0003C8FB File Offset: 0x0003B8FB
		internal int Weight
		{
			get
			{
				return this.weight;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x0003C903 File Offset: 0x0003B903
		internal int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x04000D0D RID: 3341
		private string target;

		// Token: 0x04000D0E RID: 3342
		private int priority;

		// Token: 0x04000D0F RID: 3343
		private int weight;

		// Token: 0x04000D10 RID: 3344
		private int port;

		// Token: 0x020001DF RID: 479
		[StructLayout(0, CharSet = 3)]
		private struct Win32DnsSrvRecord
		{
			// Token: 0x04000D11 RID: 3345
			public DnsRecordHeader Header;

			// Token: 0x04000D12 RID: 3346
			public string NameTarget;

			// Token: 0x04000D13 RID: 3347
			public ushort Priority;

			// Token: 0x04000D14 RID: 3348
			public ushort Weight;

			// Token: 0x04000D15 RID: 3349
			public ushort Port;

			// Token: 0x04000D16 RID: 3350
			public ushort Pad;
		}
	}
}
