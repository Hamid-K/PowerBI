using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000047 RID: 71
	public static class IDBCreateCommandExtensions
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00008038 File Offset: 0x00006238
		[global::System.Runtime.CompilerServices.NullableContext(1)]
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
