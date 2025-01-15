using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009E0 RID: 2528
	[TypeLibType(4160)]
	[Guid("3050F20C-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLCommentElement
	{
		// Token: 0x170050A5 RID: 20645
		// (get) Token: 0x0600F775 RID: 63349
		// (set) Token: 0x0600F774 RID: 63348
		[DispId(1001)]
		string text
		{
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170050A6 RID: 20646
		// (get) Token: 0x0600F777 RID: 63351
		// (set) Token: 0x0600F776 RID: 63350
		[DispId(1002)]
		int atomic
		{
			[TypeLibFunc(20)]
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}
	}
}
