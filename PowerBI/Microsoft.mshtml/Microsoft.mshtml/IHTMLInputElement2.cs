using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200057C RID: 1404
	[Guid("3050F821-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLInputElement2
	{
		// Token: 0x17002EC3 RID: 11971
		// (get) Token: 0x06008D2F RID: 36143
		// (set) Token: 0x06008D2E RID: 36142
		[DispId(2022)]
		string accept
		{
			[TypeLibFunc(20)]
			[DispId(2022)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2022)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EC4 RID: 11972
		// (get) Token: 0x06008D31 RID: 36145
		// (set) Token: 0x06008D30 RID: 36144
		[DispId(2023)]
		string useMap
		{
			[DispId(2023)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2023)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
