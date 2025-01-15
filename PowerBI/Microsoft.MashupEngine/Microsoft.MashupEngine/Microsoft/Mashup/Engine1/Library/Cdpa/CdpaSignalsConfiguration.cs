using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E04 RID: 3588
	[DataContract]
	internal class CdpaSignalsConfiguration
	{
		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x060060B7 RID: 24759 RVA: 0x0014A165 File Offset: 0x00148365
		// (set) Token: 0x060060B8 RID: 24760 RVA: 0x0014A16D File Offset: 0x0014836D
		[DataMember(Name = "table", IsRequired = true)]
		public CdpaTableConfiguration Table { get; set; }
	}
}
