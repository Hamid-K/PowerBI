using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs
{
	// Token: 0x02000194 RID: 404
	internal static class User32
	{
		// Token: 0x060012FA RID: 4858
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern IntPtr GetProcessWindowStation();

		// Token: 0x060012FB RID: 4859
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public unsafe static extern bool GetUserObjectInformation(IntPtr hObj, int nIndex, void* pvBuffer, uint nLength, ref uint lpnLengthNeeded);

		// Token: 0x04000737 RID: 1847
		private const string LibraryName = "user32.dll";

		// Token: 0x04000738 RID: 1848
		public const int UOI_FLAGS = 1;

		// Token: 0x04000739 RID: 1849
		public const int WSF_VISIBLE = 1;
	}
}
