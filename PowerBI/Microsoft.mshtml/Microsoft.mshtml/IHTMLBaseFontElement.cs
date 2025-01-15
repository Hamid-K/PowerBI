using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200078D RID: 1933
	[Guid("3050F202-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLBaseFontElement
	{
		// Token: 0x1700431F RID: 17183
		// (get) Token: 0x0600CC4D RID: 52301
		// (set) Token: 0x0600CC4C RID: 52300
		[DispId(-2147413110)]
		object color
		{
			[DispId(-2147413110)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004320 RID: 17184
		// (get) Token: 0x0600CC4F RID: 52303
		// (set) Token: 0x0600CC4E RID: 52302
		[DispId(-2147413094)]
		string face
		{
			[DispId(-2147413094)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413094)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004321 RID: 17185
		// (get) Token: 0x0600CC51 RID: 52305
		// (set) Token: 0x0600CC50 RID: 52304
		[DispId(-2147413086)]
		int size
		{
			[DispId(-2147413086)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413086)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}
	}
}
