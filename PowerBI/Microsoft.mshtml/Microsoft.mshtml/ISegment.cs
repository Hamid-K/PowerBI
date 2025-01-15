using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CAE RID: 3246
	[InterfaceType(1)]
	[Guid("3050F683-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface ISegment
	{
		// Token: 0x06016262 RID: 90722
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetPointers([MarshalAs(28)] [In] IMarkupPointer pIStart, [MarshalAs(28)] [In] IMarkupPointer pIEnd);
	}
}
