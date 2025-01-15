using System;

namespace Microsoft.ReportingServices.Library.Soap2010
{
	// Token: 0x020002EC RID: 748
	public class ItemReferenceData
	{
		// Token: 0x06001AD3 RID: 6867 RVA: 0x000025F4 File Offset: 0x000007F4
		public ItemReferenceData()
		{
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0006C474 File Offset: 0x0006A674
		public ItemReferenceData(string name, string reference, string referenceType)
		{
			this.Name = name;
			this.Reference = reference;
			this.ReferenceType = referenceType;
		}

		// Token: 0x040009A4 RID: 2468
		public string Name;

		// Token: 0x040009A5 RID: 2469
		public string Reference;

		// Token: 0x040009A6 RID: 2470
		public string ReferenceType;
	}
}
