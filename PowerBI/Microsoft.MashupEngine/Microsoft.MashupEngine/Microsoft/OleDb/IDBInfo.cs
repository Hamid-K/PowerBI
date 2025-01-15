using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F05 RID: 7941
	[Guid("0c733a89-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBInfo
	{
		// Token: 0x0600C2AD RID: 49837
		unsafe void GetKeywords(out char* keywords);

		// Token: 0x0600C2AE RID: 49838
		[PreserveSig]
		unsafe int GetLiteralInfo(uint cLiterals, DBLITERAL* nativeLiterals, out uint cLiteralInfo, out DBLITERALINFO* nativeLiteralInfos, out char* strings);
	}
}
