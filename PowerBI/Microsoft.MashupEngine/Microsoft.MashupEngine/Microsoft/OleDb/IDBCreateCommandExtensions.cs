using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EA1 RID: 7841
	public static class IDBCreateCommandExtensions
	{
		// Token: 0x0600C1DD RID: 49629 RVA: 0x0026FBE4 File Offset: 0x0026DDE4
		public static object CreateCommand(this IDBCreateCommand dbCreateCommand)
		{
			Guid iunknown = IID.IUnknown;
			IntPtr intPtr;
			Marshal.ThrowExceptionForHR(dbCreateCommand.CreateCommand(IntPtr.Zero, ref iunknown, out intPtr));
			object objectForIUnknown = Marshal.GetObjectForIUnknown(intPtr);
			Marshal.Release(intPtr);
			return objectForIUnknown;
		}
	}
}
