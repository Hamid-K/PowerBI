using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC7 RID: 3271
	[Guid("3050F6C1-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface ISequenceNumber
	{
		// Token: 0x06016306 RID: 90886
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetSequenceNumber([In] int nCurrent, out int pnNew);
	}
}
