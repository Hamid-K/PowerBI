using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000017 RID: 23
	internal static class NativeShared
	{
		// Token: 0x060000E8 RID: 232
		[DllImport("ReportingServicesWMIProvider.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int __GenerateDatabaseAnyUpgradeScript(string dbCurrentVersion, string dbDesiredVersion, string dbName, out string upgradeScript);

		// Token: 0x060000E9 RID: 233
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "LoadLibrary", SetLastError = true)]
		private static extern IntPtr NativeLoadLibrary(string libname);

		// Token: 0x060000EA RID: 234 RVA: 0x000066FC File Offset: 0x000048FC
		internal static uint GenerateDatabaseUpgradeScript(string dbCurrentVersion, string dbDesiredVersion, string dbName, out string upgradeScript)
		{
			if (IntPtr.Zero == NativeShared.libraryHandle)
			{
				NativeShared.libraryHandle = NativeShared.NativeLoadLibrary(Path.Combine(Globals.SqlSharedCodeDirectory, "ReportingServicesWMIProvider.DLL"));
			}
			return (uint)NativeShared.__GenerateDatabaseAnyUpgradeScript(dbCurrentVersion, dbDesiredVersion, dbName, out upgradeScript);
		}

		// Token: 0x040000D0 RID: 208
		private static IntPtr libraryHandle = IntPtr.Zero;
	}
}
