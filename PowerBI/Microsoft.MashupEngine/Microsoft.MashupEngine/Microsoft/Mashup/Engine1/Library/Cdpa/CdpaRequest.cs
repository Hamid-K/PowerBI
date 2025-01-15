using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E06 RID: 3590
	[DataContract]
	internal abstract class CdpaRequest
	{
		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x060060BF RID: 24767 RVA: 0x0014A25D File Offset: 0x0014845D
		// (set) Token: 0x060060C0 RID: 24768 RVA: 0x0014A265 File Offset: 0x00148465
		[DataMember(Name = "responseFormat", IsRequired = false)]
		public string ResponseFormat { get; set; }

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x060060C1 RID: 24769 RVA: 0x0014A26E File Offset: 0x0014846E
		// (set) Token: 0x060060C2 RID: 24770 RVA: 0x0014A276 File Offset: 0x00148476
		[DataMember(Name = "extraParameters", IsRequired = false)]
		public CdpaExtraParameters ExtraParameters { get; set; }
	}
}
