using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020008CF RID: 2255
	[Guid("3050F266-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLMapElement
	{
		// Token: 0x17004BC1 RID: 19393
		// (get) Token: 0x0600E612 RID: 58898
		[DispId(1002)]
		IHTMLAreasCollection areas
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004BC2 RID: 19394
		// (get) Token: 0x0600E614 RID: 58900
		// (set) Token: 0x0600E613 RID: 58899
		[DispId(-2147418112)]
		string name
		{
			[DispId(-2147418112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
