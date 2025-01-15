using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F09 RID: 7945
	[Guid("0c733a88-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IConvertType
	{
		// Token: 0x0600C2B9 RID: 49849
		[PreserveSig]
		int CanConvert(DBTYPE wFromType, DBTYPE wToType, DBCONVERTFLAGS dwConvertFlags);
	}
}
