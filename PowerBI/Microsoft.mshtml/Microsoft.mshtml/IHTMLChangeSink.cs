using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C98 RID: 3224
	[InterfaceType(1)]
	[Guid("3050F64A-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLChangeSink
	{
		// Token: 0x06016209 RID: 90633
		[MethodImpl(4096, MethodCodeType = 3)]
		void Notify();
	}
}
