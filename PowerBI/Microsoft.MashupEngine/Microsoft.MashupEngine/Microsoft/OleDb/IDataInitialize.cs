using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EF5 RID: 7925
	[Guid("2206CCB1-19C1-11D1-89E0-00C04FD7A829")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDataInitialize
	{
		// Token: 0x0600C285 RID: 49797
		unsafe void GetDataSource(IntPtr pUnkOuter, uint dwClsCtx, char* pwszInitializationString, ref Guid riid, ref IntPtr ppDataSource);

		// Token: 0x0600C286 RID: 49798
		unsafe void GetInitializationString(IntPtr pDataSource, bool fIncludePassword, out char* ppwszInitString);

		// Token: 0x0600C287 RID: 49799
		unsafe void CreateDBInstance(ref Guid clsidProvider, IntPtr punkOuter, uint dwClsCtx, char* pwszReserved, ref Guid riid, out IntPtr ppDataSource);

		// Token: 0x0600C288 RID: 49800
		unsafe void CreateDBInstanceEx(ref Guid clsidProvider, IntPtr punkOuter, uint dwClsCtx, char* pwszReserved, IntPtr pServerInfo, uint cmq, IntPtr rgmqResults);

		// Token: 0x0600C289 RID: 49801
		unsafe void LoadStringFromStorage(char* ppwszFileName, out char* ppwszInitializationString);

		// Token: 0x0600C28A RID: 49802
		unsafe void WriteStringToStorage(char* pwszFileName, char* pwszInitializationString, int dwCreationDisposition);
	}
}
