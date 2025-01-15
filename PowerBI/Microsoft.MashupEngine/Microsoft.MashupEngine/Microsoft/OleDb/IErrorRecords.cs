using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F10 RID: 7952
	[Guid("0c733a67-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IErrorRecords
	{
		// Token: 0x0600C2C5 RID: 49861
		[PreserveSig]
		int AddErrorRecord(ref ERRORINFO pErrorInfo, uint dwLookupID, IntPtr pdispparams, IntPtr punkCustomError, uint dwDynamicErrorID);

		// Token: 0x0600C2C6 RID: 49862
		[PreserveSig]
		int GetBasicErrorInfo(uint ulRecordNum, ref ERRORINFO pErrorInfo);

		// Token: 0x0600C2C7 RID: 49863
		[PreserveSig]
		int GetCustomErrorObject(uint ulRecordNum, ref Guid riid, out IntPtr ppObject);

		// Token: 0x0600C2C8 RID: 49864
		[PreserveSig]
		int GetErrorInfo(uint ulRecordNum, int lcid, out IErrorInfo ppErrorInfo);

		// Token: 0x0600C2C9 RID: 49865
		[PreserveSig]
		int GetErrorParameters(uint ulRecordNum, IntPtr pdispparams);

		// Token: 0x0600C2CA RID: 49866
		[PreserveSig]
		int GetRecordCount(out uint pcRecords);
	}
}
