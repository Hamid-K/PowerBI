using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001EA5 RID: 7845
	public interface IInteropServices
	{
		// Token: 0x0600C1E7 RID: 49639
		int AggregateDataSource(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv);

		// Token: 0x0600C1E8 RID: 49640
		int AggregateSession(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv);

		// Token: 0x0600C1E9 RID: 49641
		int AggregateCommand(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv);

		// Token: 0x0600C1EA RID: 49642
		int AggregateRowset(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv);

		// Token: 0x0600C1EB RID: 49643
		int AggregateMultipleResults(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv);

		// Token: 0x0600C1EC RID: 49644
		int QueryInterface(object obj, ref Guid iid, out IntPtr ppv);

		// Token: 0x0600C1ED RID: 49645
		int QueryInterface(IntPtr pUnknown, ref Guid iid, out IntPtr ppv);

		// Token: 0x0600C1EE RID: 49646
		int AddRef(IntPtr pUnknown);

		// Token: 0x0600C1EF RID: 49647
		int Release(IntPtr pUnknown);

		// Token: 0x0600C1F0 RID: 49648
		int ReleaseComObject(object obj);

		// Token: 0x0600C1F1 RID: 49649
		int GetErrorInfo(uint dwReserved, out IntPtr errorInfoUnmanaged);

		// Token: 0x0600C1F2 RID: 49650
		int GetErrorInfo(uint dwReserved, out IErrorInfo errorInfoManaged);
	}
}
