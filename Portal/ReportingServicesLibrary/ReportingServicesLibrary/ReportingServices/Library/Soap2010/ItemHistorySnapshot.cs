using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library.Soap2010
{
	// Token: 0x020002E8 RID: 744
	public class ItemHistorySnapshot
	{
		// Token: 0x06001AC9 RID: 6857 RVA: 0x0006C09D File Offset: 0x0006A29D
		public ItemHistorySnapshot()
		{
			this.HistoryID = null;
			this.CreationDate = DateTime.MinValue;
			this.Size = 0;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0006C0BE File Offset: 0x0006A2BE
		public ItemHistorySnapshot(ReportHistorySnapshot rhs)
		{
			this.HistoryID = rhs.HistoryID;
			this.CreationDate = rhs.CreationDate;
			this.Size = rhs.Size;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0006C0EC File Offset: 0x0006A2EC
		public static ItemHistorySnapshot[] FromReportHistorySnapshorArray(ReportHistorySnapshot[] ReportHistory)
		{
			ItemHistorySnapshot[] array = new ItemHistorySnapshot[ReportHistory.Length];
			for (int i = 0; i < ReportHistory.Length; i++)
			{
				array[i] = new ItemHistorySnapshot(ReportHistory[i]);
			}
			return array;
		}

		// Token: 0x04000982 RID: 2434
		public string HistoryID;

		// Token: 0x04000983 RID: 2435
		public DateTime CreationDate;

		// Token: 0x04000984 RID: 2436
		public int Size;
	}
}
