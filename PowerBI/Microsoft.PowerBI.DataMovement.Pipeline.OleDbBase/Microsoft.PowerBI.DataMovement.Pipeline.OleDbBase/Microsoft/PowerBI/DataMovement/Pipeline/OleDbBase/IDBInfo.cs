using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200007F RID: 127
	[Guid("0c733a89-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBInfo
	{
		// Token: 0x060002CE RID: 718
		unsafe void GetKeywords(out char* keywords);

		// Token: 0x060002CF RID: 719
		[PreserveSig]
		unsafe int GetLiteralInfo(uint literals, DBLITERAL* nativeLiterals, out uint literalInfo, out DBLITERALINFO* nativeLiteralInfos, out char* strings);
	}
}
