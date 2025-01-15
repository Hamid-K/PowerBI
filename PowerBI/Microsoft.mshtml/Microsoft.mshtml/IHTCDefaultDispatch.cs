using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200009B RID: 155
	[Guid("3050F4FD-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTCDefaultDispatch
	{
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06000D6E RID: 3438
		[DispId(-2147412969)]
		IHTMLElement element
		{
			[DispId(-2147412969)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06000D6F RID: 3439
		[DispId(-2147412968)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLEventObj CreateEventObject();

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06000D70 RID: 3440
		[DispId(-2147412947)]
		object defaults
		{
			[DispId(-2147412947)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06000D71 RID: 3441
		[DispId(-2147412970)]
		object document
		{
			[DispId(-2147412970)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}
	}
}
