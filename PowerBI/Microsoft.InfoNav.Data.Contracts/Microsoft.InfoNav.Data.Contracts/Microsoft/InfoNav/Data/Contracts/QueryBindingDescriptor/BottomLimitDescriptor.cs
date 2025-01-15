using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000CA RID: 202
	[DataContract]
	public sealed class BottomLimitDescriptor
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000C040 File Offset: 0x0000A240
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0000C048 File Offset: 0x0000A248
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 1)]
		public int Count { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0000C051 File Offset: 0x0000A251
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x0000C059 File Offset: 0x0000A259
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string Calc { get; set; }
	}
}
