using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C99 RID: 3225
	[Guid("3050F649-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHTMLChangeLog
	{
		// Token: 0x0601620A RID: 90634
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetNextChange([In] ref byte pbBuffer, [In] int nBufferSize, out int pnRecordLength);
	}
}
