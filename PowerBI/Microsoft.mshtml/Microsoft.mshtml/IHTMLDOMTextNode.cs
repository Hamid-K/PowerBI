using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200006C RID: 108
	[Guid("3050F4B1-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLDOMTextNode
	{
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06000B1A RID: 2842
		// (set) Token: 0x06000B19 RID: 2841
		[DispId(1000)]
		string data
		{
			[DispId(1000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x06000B1B RID: 2843
		[DispId(1001)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06000B1C RID: 2844
		[DispId(1002)]
		int length
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06000B1D RID: 2845
		[DispId(1003)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode splitText([In] int offset);
	}
}
