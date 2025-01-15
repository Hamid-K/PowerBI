using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DBB RID: 3515
	[DataContract]
	internal class CdpaStringComparisonOptions
	{
		// Token: 0x17001C38 RID: 7224
		// (get) Token: 0x06005F8F RID: 24463 RVA: 0x0014893A File Offset: 0x00146B3A
		// (set) Token: 0x06005F90 RID: 24464 RVA: 0x00148942 File Offset: 0x00146B42
		[DataMember(Name = "ignoreCase", IsRequired = true)]
		public bool IgnoreCase { get; set; }
	}
}
