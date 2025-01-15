using System;
using System.Collections;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	public sealed class CatalogOperationsCollection : CollectionBase
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00002221 File Offset: 0x00000421
		public int Add(CatalogOperation operation)
		{
			return base.InnerList.Add(operation);
		}

		// Token: 0x1700004B RID: 75
		public CatalogOperation this[int index]
		{
			get
			{
				return (CatalogOperation)base.InnerList[index];
			}
		}
	}
}
