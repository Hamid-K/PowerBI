using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Internal
{
	// Token: 0x02000190 RID: 400
	internal static class DynamicLinkLibrary
	{
		// Token: 0x060007CD RID: 1997 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		public static Delegate GetProcAddress(SafeHandle libraryHandle, string functionName, Type type)
		{
			IntPtr procAddress = DynamicLinkLibrary.NativeMethods.GetProcAddress(libraryHandle, functionName);
			if (procAddress != IntPtr.Zero)
			{
				return Marshal.GetDelegateForFunctionPointer(procAddress, type);
			}
			return null;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0000EDCB File Offset: 0x0000CFCB
		public static SafeHandle LoadLibrary(string fileName)
		{
			return DynamicLinkLibrary.NativeMethods.LoadLibraryProc(fileName);
		}

		// Token: 0x02000191 RID: 401
		private sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x060007CF RID: 1999 RVA: 0x0000EDD3 File Offset: 0x0000CFD3
			private SafeLibraryHandle()
				: base(true)
			{
			}

			// Token: 0x060007D0 RID: 2000 RVA: 0x0000EDDC File Offset: 0x0000CFDC
			protected override bool ReleaseHandle()
			{
				return DynamicLinkLibrary.NativeMethods.FreeLibrary(this.handle);
			}
		}

		// Token: 0x02000192 RID: 402
		private static class NativeMethods
		{
			// Token: 0x060007D1 RID: 2001
			[DllImport("kernel32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool FreeLibrary(IntPtr libraryHandle);

			// Token: 0x060007D2 RID: 2002
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
			public static extern IntPtr GetProcAddress(SafeHandle libraryHandle, string functionName);

			// Token: 0x060007D3 RID: 2003
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "LoadLibrary")]
			public static extern DynamicLinkLibrary.SafeLibraryHandle LoadLibraryProc(string fileName);

			// Token: 0x040004A0 RID: 1184
			private const string kernel32 = "kernel32.dll";
		}
	}
}
