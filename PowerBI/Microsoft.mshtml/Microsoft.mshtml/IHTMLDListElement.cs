using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004C7 RID: 1223
	[TypeLibType(4160)]
	[Guid("3050F1F1-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLDListElement
	{
		// Token: 0x17002503 RID: 9475
		// (get) Token: 0x06006E46 RID: 28230
		// (set) Token: 0x06006E45 RID: 28229
		[DispId(1001)]
		bool compact
		{
			[TypeLibFunc(4)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}
	}
}
