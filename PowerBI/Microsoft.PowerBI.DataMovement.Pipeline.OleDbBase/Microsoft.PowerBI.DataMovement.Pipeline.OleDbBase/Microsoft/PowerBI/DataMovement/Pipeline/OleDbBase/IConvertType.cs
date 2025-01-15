using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000084 RID: 132
	[Guid("0c733a88-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IConvertType
	{
		// Token: 0x060002DD RID: 733
		[PreserveSig]
		int CanConvert(DBTYPE fromType, DBTYPE toType, DBCONVERTFLAGS convertFlags);
	}
}
