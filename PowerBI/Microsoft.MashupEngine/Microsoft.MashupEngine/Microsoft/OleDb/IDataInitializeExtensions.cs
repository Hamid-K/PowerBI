using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EA0 RID: 7840
	public static class IDataInitializeExtensions
	{
		// Token: 0x0600C1DC RID: 49628 RVA: 0x0026FB5C File Offset: 0x0026DD5C
		public unsafe static object GetDataSource(this IDataInitialize dataInitialize, string connString)
		{
			Guid idbinitialize = IID.IDBInitialize;
			IntPtr zero = IntPtr.Zero;
			object obj;
			try
			{
				using (ComHeap comHeap = new ComHeap())
				{
					char* ptr = comHeap.AllocString(connString);
					dataInitialize.GetDataSource(IntPtr.Zero, 23U, ptr, ref idbinitialize, ref zero);
					obj = (IDBInitialize)Marshal.GetObjectForIUnknown(zero);
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.Release(zero);
				}
			}
			return obj;
		}

		// Token: 0x040061A9 RID: 25001
		private const uint CLSCTX_ALL = 23U;
	}
}
