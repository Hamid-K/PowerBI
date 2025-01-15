using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000045 RID: 69
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public static class ICommandTextExtensions
	{
		// Token: 0x06000266 RID: 614 RVA: 0x00007F04 File Offset: 0x00006104
		public unsafe static void SetCommand(this ICommandText command, Guid dialect, string value)
		{
			using (ComHeap comHeap = new ComHeap())
			{
				char* ptr = comHeap.AllocString(value);
				Guid guid = dialect;
				Marshal.ThrowExceptionForHR(command.SetCommandText(ref guid, ptr));
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00007F4C File Offset: 0x0000614C
		public static T Execute<[global::System.Runtime.CompilerServices.Nullable(2)] T>(this ICommandText commandText)
		{
			return commandText.Execute(null);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00007F58 File Offset: 0x00006158
		public unsafe static T Execute<[global::System.Runtime.CompilerServices.Nullable(2)] T>(this ICommandText commandText, [global::System.Runtime.CompilerServices.Nullable(0)] DBPARAMS* parameters)
		{
			Guid iunknown = IID.IUnknown;
			IntPtr intPtr;
			Marshal.ThrowExceptionForHR(commandText.Execute(IntPtr.Zero, ref iunknown, parameters, null, out intPtr));
			object objectForIUnknown = Marshal.GetObjectForIUnknown(intPtr);
			Marshal.Release(intPtr);
			return (T)((object)objectForIUnknown);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00007F94 File Offset: 0x00006194
		public unsafe static IMultipleResults ExecuteMultipleResults(this ICommandText commandText, [global::System.Runtime.CompilerServices.Nullable(0)] DBPARAMS* parameters)
		{
			Guid imultipleResults = IID.IMultipleResults;
			IntPtr intPtr;
			Marshal.ThrowExceptionForHR(commandText.Execute(IntPtr.Zero, ref imultipleResults, parameters, null, out intPtr));
			object objectForIUnknown = Marshal.GetObjectForIUnknown(intPtr);
			Marshal.Release(intPtr);
			return (IMultipleResults)objectForIUnknown;
		}
	}
}
