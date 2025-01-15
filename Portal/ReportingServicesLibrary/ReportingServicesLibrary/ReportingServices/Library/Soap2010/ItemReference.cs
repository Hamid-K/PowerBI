using System;

namespace Microsoft.ReportingServices.Library.Soap2010
{
	// Token: 0x020002EB RID: 747
	public class ItemReference
	{
		// Token: 0x06001AD1 RID: 6865 RVA: 0x000025F4 File Offset: 0x000007F4
		public ItemReference()
		{
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0006C45E File Offset: 0x0006A65E
		public ItemReference(string name, string reference)
		{
			this.Name = name;
			this.Reference = reference;
		}

		// Token: 0x040009A2 RID: 2466
		public string Name;

		// Token: 0x040009A3 RID: 2467
		public string Reference;
	}
}
