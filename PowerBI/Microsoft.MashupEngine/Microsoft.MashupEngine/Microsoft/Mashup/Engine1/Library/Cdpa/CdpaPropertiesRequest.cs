using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E07 RID: 3591
	[DataContract]
	internal class CdpaPropertiesRequest : CdpaRequest
	{
		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x060060C4 RID: 24772 RVA: 0x0014A27F File Offset: 0x0014847F
		// (set) Token: 0x060060C5 RID: 24773 RVA: 0x0014A287 File Offset: 0x00148487
		[DataMember(Name = "configuration", IsRequired = true)]
		public CdpaSignalsConfiguration Configuration { get; set; }

		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x060060C6 RID: 24774 RVA: 0x0014A290 File Offset: 0x00148490
		// (set) Token: 0x060060C7 RID: 24775 RVA: 0x0014A298 File Offset: 0x00148498
		[DataMember(Name = "searchString", IsRequired = false)]
		public string SearchString { get; set; }

		// Token: 0x060060C8 RID: 24776 RVA: 0x0014A2A1 File Offset: 0x001484A1
		public CdpaPropertiesRequest ShallowCopy()
		{
			return new CdpaPropertiesRequest
			{
				Configuration = this.Configuration,
				SearchString = this.SearchString
			};
		}
	}
}
