using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F03 RID: 7939
	[Guid("0c733a8d-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDataConvert
	{
		// Token: 0x0600C2A8 RID: 49832
		unsafe void DataConvert(DBTYPE wSrcType, DBTYPE wDstType, DBLENGTH cbSrcLength, out DBLENGTH cbDstLength, void* pSrc, void* pDst, DBLENGTH cbDstMaxLength, DBSTATUS dbsSrcStatus, out DBSTATUS dbsStatus, byte bPrecision, byte bScale, DBDATACONVERT dwFlags);

		// Token: 0x0600C2A9 RID: 49833
		[PreserveSig]
		int CanConvert(DBTYPE wSrcType, DBTYPE wDstType);

		// Token: 0x0600C2AA RID: 49834
		unsafe void GetConversionSize(DBTYPE wSrcType, DBTYPE wDstType, DBLENGTH* pcbSrcLength, DBLENGTH* pcbDstLength, void* pSrc);
	}
}
