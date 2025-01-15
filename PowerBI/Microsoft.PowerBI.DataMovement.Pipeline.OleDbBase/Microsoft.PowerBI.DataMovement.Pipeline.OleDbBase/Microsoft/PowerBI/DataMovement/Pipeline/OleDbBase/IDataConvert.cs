using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200007D RID: 125
	[Guid("0c733a8d-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDataConvert
	{
		// Token: 0x060002C9 RID: 713
		unsafe void DataConvert(DBTYPE srcType, DBTYPE dstType, DBLENGTH srcLength, out DBLENGTH dstLength, void* src, void* dst, DBLENGTH dstMaxLength, DBSTATUS srcStatus, out DBSTATUS status, byte precision, byte scale, DBDATACONVERT flags);

		// Token: 0x060002CA RID: 714
		[PreserveSig]
		int CanConvert(DBTYPE srcType, DBTYPE dstType);

		// Token: 0x060002CB RID: 715
		unsafe void GetConversionSize(DBTYPE srcType, DBTYPE dstType, DBLENGTH* srcLength, DBLENGTH* dstLength, void* src);
	}
}
