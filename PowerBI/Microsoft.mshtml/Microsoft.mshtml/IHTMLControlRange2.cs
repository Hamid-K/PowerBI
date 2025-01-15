using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000278 RID: 632
	[Guid("3050F65E-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLControlRange2
	{
		// Token: 0x0600279C RID: 10140
		[DispId(1016)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void addElement([MarshalAs(28)] [In] IHTMLElement item);
	}
}
