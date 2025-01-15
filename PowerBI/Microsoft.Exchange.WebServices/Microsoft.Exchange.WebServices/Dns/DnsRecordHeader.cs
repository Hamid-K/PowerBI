using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.WebServices.Dns
{
	// Token: 0x020001DD RID: 477
	[StructLayout(0, CharSet = 3)]
	internal struct DnsRecordHeader
	{
		// Token: 0x04000D06 RID: 3334
		internal IntPtr NextRecord;

		// Token: 0x04000D07 RID: 3335
		internal string Name;

		// Token: 0x04000D08 RID: 3336
		internal DnsRecordType RecordType;

		// Token: 0x04000D09 RID: 3337
		internal ushort DataLength;

		// Token: 0x04000D0A RID: 3338
		internal uint Flags;

		// Token: 0x04000D0B RID: 3339
		internal uint Ttl;

		// Token: 0x04000D0C RID: 3340
		internal uint Reserved;
	}
}
