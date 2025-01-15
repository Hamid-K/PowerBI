using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EA2 RID: 7842
	public static class IDBCreateSessionExtensions
	{
		// Token: 0x0600C1DE RID: 49630 RVA: 0x0026FC18 File Offset: 0x0026DE18
		public static object CreateSession(this IDBCreateSession dbCreateSession)
		{
			Guid iunknown = IID.IUnknown;
			IntPtr intPtr;
			Marshal.ThrowExceptionForHR(dbCreateSession.CreateSession(IntPtr.Zero, ref iunknown, out intPtr));
			object objectForIUnknown = Marshal.GetObjectForIUnknown(intPtr);
			Marshal.Release(intPtr);
			return objectForIUnknown;
		}
	}
}
