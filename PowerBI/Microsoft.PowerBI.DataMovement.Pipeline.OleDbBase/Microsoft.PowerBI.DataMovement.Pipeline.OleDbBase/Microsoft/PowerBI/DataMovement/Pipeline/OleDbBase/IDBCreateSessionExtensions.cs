using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000048 RID: 72
	public static class IDBCreateSessionExtensions
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000806C File Offset: 0x0000626C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
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
