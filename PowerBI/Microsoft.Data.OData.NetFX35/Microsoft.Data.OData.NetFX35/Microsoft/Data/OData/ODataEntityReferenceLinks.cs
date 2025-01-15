using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x0200025A RID: 602
	public sealed class ODataEntityReferenceLinks : ODataAnnotatable
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0004738E File Offset: 0x0004558E
		// (set) Token: 0x060012CE RID: 4814 RVA: 0x00047396 File Offset: 0x00045596
		public long? Count { get; set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0004739F File Offset: 0x0004559F
		// (set) Token: 0x060012D0 RID: 4816 RVA: 0x000473A7 File Offset: 0x000455A7
		public Uri NextPageLink { get; set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x000473B0 File Offset: 0x000455B0
		// (set) Token: 0x060012D2 RID: 4818 RVA: 0x000473B8 File Offset: 0x000455B8
		public IEnumerable<ODataEntityReferenceLink> Links { get; set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x000473C1 File Offset: 0x000455C1
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x000473C9 File Offset: 0x000455C9
		internal ODataEntityReferenceLinksSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataEntityReferenceLinksSerializationInfo.Validate(value);
			}
		}

		// Token: 0x0400070D RID: 1805
		private ODataEntityReferenceLinksSerializationInfo serializationInfo;
	}
}
