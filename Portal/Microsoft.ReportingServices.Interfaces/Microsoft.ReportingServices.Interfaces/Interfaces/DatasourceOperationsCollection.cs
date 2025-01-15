using System;
using System.Collections;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	public sealed class DatasourceOperationsCollection : CollectionBase
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000022D9 File Offset: 0x000004D9
		public int Add(DatasourceOperation operation)
		{
			return base.InnerList.Add(operation);
		}

		// Token: 0x1700004F RID: 79
		public DatasourceOperation this[int index]
		{
			get
			{
				return (DatasourceOperation)base.InnerList[index];
			}
		}
	}
}
