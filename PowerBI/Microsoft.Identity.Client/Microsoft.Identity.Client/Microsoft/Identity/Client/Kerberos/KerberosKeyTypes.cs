using System;

namespace Microsoft.Identity.Client.Kerberos
{
	// Token: 0x02000227 RID: 551
	public enum KerberosKeyTypes
	{
		// Token: 0x040009AA RID: 2474
		None,
		// Token: 0x040009AB RID: 2475
		DecCbcCrc,
		// Token: 0x040009AC RID: 2476
		DesCbcMd5 = 3,
		// Token: 0x040009AD RID: 2477
		Aes128CtsHmacSha196 = 17,
		// Token: 0x040009AE RID: 2478
		Aes256CtsHmacSha196
	}
}
