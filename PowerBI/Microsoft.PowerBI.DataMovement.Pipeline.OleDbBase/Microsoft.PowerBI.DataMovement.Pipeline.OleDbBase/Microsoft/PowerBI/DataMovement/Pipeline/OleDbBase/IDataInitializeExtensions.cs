using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000046 RID: 70
	public static class IDataInitializeExtensions
	{
		// Token: 0x0600026A RID: 618 RVA: 0x00007FD0 File Offset: 0x000061D0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public unsafe static T GetDataSource<[global::System.Runtime.CompilerServices.Nullable(2)] T>(this IDataInitialize dataInitialize, CLSCTX clsCtx, string initializationString, Guid riid)
		{
			T t;
			using (ComHeap comHeap = new ComHeap())
			{
				char* ptr = comHeap.AllocString(initializationString);
				Guid guid = riid;
				IntPtr intPtr;
				Marshal.ThrowExceptionForHR(dataInitialize.GetDataSource(IntPtr.Zero, (uint)clsCtx, ptr, ref guid, out intPtr));
				object objectForIUnknown = Marshal.GetObjectForIUnknown(intPtr);
				Marshal.Release(intPtr);
				t = (T)((object)objectForIUnknown);
			}
			return t;
		}
	}
}
