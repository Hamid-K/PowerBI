using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x0200000A RID: 10
	public class ProductDetails : Attribute
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000280B File Offset: 0x00000A0B
		public ProductDetails()
		{
			this.DefaultInstanceName = string.Empty;
		}

		// Token: 0x04000036 RID: 54
		public string DefaultInstanceName;
	}
}
