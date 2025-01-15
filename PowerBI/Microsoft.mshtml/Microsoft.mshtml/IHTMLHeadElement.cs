using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000770 RID: 1904
	[TypeLibType(4160)]
	[Guid("3050F81D-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLHeadElement
	{
		// Token: 0x1700384F RID: 14415
		// (get) Token: 0x0600AF79 RID: 44921
		// (set) Token: 0x0600AF78 RID: 44920
		[DispId(1001)]
		string profile
		{
			[TypeLibFunc(20)]
			[DispId(1001)]
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
	}
}
