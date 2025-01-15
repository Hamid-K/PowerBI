using System;
using System.Collections;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public sealed class ReportOperationsCollection : CollectionBase
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000227D File Offset: 0x0000047D
		public int Add(ReportOperation operation)
		{
			return base.InnerList.Add(operation);
		}

		// Token: 0x1700004D RID: 77
		public ReportOperation this[int index]
		{
			get
			{
				return (ReportOperation)base.InnerList[index];
			}
		}
	}
}
