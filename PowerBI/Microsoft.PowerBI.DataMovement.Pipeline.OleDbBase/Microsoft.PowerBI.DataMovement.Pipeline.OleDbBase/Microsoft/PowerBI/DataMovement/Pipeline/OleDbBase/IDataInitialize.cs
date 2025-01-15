using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000070 RID: 112
	[Guid("2206ccb1-19c1-11d1-89e0-00c04fd7a829")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDataInitialize
	{
		// Token: 0x060002A6 RID: 678
		[PreserveSig]
		unsafe int GetDataSource(IntPtr punkOuter, uint clsCtx, char* initializationString, ref Guid riid, out IntPtr dataSource);

		// Token: 0x060002A7 RID: 679
		[PreserveSig]
		unsafe int GetInitializationString(IntPtr dataSource, byte includePassword, out char* initString);

		// Token: 0x060002A8 RID: 680
		[PreserveSig]
		unsafe int CreateDBInstance(ref Guid clsidProvider, IntPtr punkOuter, uint clsCtx, char* reserved, ref Guid riid, out IntPtr dataSource);

		// Token: 0x060002A9 RID: 681
		[PreserveSig]
		unsafe int CreateDBInstanceEx(ref Guid clsidProvider, IntPtr punkOuter, uint clsCtx, char* reserved, COSERVERINFO* serverInfo, uint cmq, out MULTI_QI* results);

		// Token: 0x060002AA RID: 682
		[PreserveSig]
		unsafe int LoadStringFromStorage(char* fileName, out char* initializationString);

		// Token: 0x060002AB RID: 683
		unsafe void WriteStringToStorage(char* fileName, char* initializationString, uint creationDisposition);
	}
}
