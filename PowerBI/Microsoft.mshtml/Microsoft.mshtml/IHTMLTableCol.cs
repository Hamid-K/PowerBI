using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009F8 RID: 2552
	[Guid("3050F23A-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLTableCol
	{
		// Token: 0x17005574 RID: 21876
		// (get) Token: 0x060103C8 RID: 66504
		// (set) Token: 0x060103C7 RID: 66503
		[DispId(1001)]
		int span
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17005575 RID: 21877
		// (get) Token: 0x060103CA RID: 66506
		// (set) Token: 0x060103C9 RID: 66505
		[DispId(-2147418107)]
		object width
		{
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005576 RID: 21878
		// (get) Token: 0x060103CC RID: 66508
		// (set) Token: 0x060103CB RID: 66507
		[DispId(-2147418040)]
		string align
		{
			[DispId(-2147418040)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418040)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005577 RID: 21879
		// (get) Token: 0x060103CE RID: 66510
		// (set) Token: 0x060103CD RID: 66509
		[DispId(-2147413081)]
		string vAlign
		{
			[DispId(-2147413081)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413081)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
