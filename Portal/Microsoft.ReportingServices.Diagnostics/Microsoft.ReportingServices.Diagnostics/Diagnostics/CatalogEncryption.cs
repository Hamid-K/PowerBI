using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000046 RID: 70
	internal static class CatalogEncryption
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000ADA4 File Offset: 0x00008FA4
		public static Encryption Instance
		{
			get
			{
				return CatalogEncryption._catalogInstance;
			}
		}

		// Token: 0x04000217 RID: 535
		private static readonly Encryption _catalogInstance = SymmetricKeyEncryption.Instance;
	}
}
