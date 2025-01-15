using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000A2 RID: 162
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F573-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface DispHTCDefaultDispatch
	{
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06000D7B RID: 3451
		[DispId(-2147412969)]
		IHTMLElement element
		{
			[DispId(-2147412969)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06000D7C RID: 3452
		[DispId(-2147412968)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLEventObj CreateEventObject();

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06000D7D RID: 3453
		[DispId(-2147412947)]
		object defaults
		{
			[DispId(-2147412947)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06000D7E RID: 3454
		[DispId(-2147412970)]
		object document
		{
			[DispId(-2147412970)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}
	}
}
