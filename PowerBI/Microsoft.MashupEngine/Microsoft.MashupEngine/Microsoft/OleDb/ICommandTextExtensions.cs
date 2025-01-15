using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001E9E RID: 7838
	public static class ICommandTextExtensions
	{
		// Token: 0x0600C1C3 RID: 49603 RVA: 0x0026F788 File Offset: 0x0026D988
		public unsafe static void SetCommand(this ICommandText command, string value)
		{
			using (ComHeap comHeap = new ComHeap())
			{
				char* ptr = comHeap.AllocString(value);
				Guid @default = DBGUID.Default;
				Marshal.ThrowExceptionForHR(command.SetCommandText(ref @default, ptr));
			}
		}

		// Token: 0x0600C1C4 RID: 49604 RVA: 0x0026F7D4 File Offset: 0x0026D9D4
		public static IRowset Execute(this ICommandText commandText, IOleDbCustomErrorHandler customHandler = null)
		{
			IntPtr zero = IntPtr.Zero;
			IRowset rowset;
			try
			{
				Guid iunknown = IID.IUnknown;
				OleDbException.ThrowExceptionForHR(commandText.Execute(IntPtr.Zero, ref iunknown, null, null, out zero), commandText, typeof(ICommandText), customHandler);
				rowset = (IRowset)Marshal.GetObjectForIUnknown(zero);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.Release(zero);
				}
			}
			return rowset;
		}
	}
}
