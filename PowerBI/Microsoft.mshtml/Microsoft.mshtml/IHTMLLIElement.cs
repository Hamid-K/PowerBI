using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004AE RID: 1198
	[Guid("3050F1E0-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLLIElement
	{
		// Token: 0x17001BB2 RID: 7090
		// (get) Token: 0x06005473 RID: 21619
		// (set) Token: 0x06005472 RID: 21618
		[DispId(-2147413095)]
		string type
		{
			[DispId(-2147413095)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413095)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001BB3 RID: 7091
		// (get) Token: 0x06005475 RID: 21621
		// (set) Token: 0x06005474 RID: 21620
		[DispId(1001)]
		int value
		{
			[TypeLibFunc(20)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}
	}
}
