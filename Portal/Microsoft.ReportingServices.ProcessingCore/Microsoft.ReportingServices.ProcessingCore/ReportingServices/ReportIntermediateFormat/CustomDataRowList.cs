using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004AA RID: 1194
	[Serializable]
	internal sealed class CustomDataRowList : RowList
	{
		// Token: 0x06003AB3 RID: 15027 RVA: 0x000FE8E5 File Offset: 0x000FCAE5
		public CustomDataRowList()
		{
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x000FE8ED File Offset: 0x000FCAED
		internal CustomDataRowList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001950 RID: 6480
		internal CustomDataRow this[int index]
		{
			get
			{
				return (CustomDataRow)base[index];
			}
		}
	}
}
