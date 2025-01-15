using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000398 RID: 920
	[Guid("3050F825-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLAnchorElement2
	{
		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06003AD1 RID: 15057
		// (set) Token: 0x06003AD0 RID: 15056
		[DispId(1023)]
		string charset
		{
			[DispId(1023)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1023)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06003AD3 RID: 15059
		// (set) Token: 0x06003AD2 RID: 15058
		[DispId(1024)]
		string coords
		{
			[TypeLibFunc(20)]
			[DispId(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06003AD5 RID: 15061
		// (set) Token: 0x06003AD4 RID: 15060
		[DispId(1025)]
		string hreflang
		{
			[TypeLibFunc(20)]
			[DispId(1025)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1025)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06003AD7 RID: 15063
		// (set) Token: 0x06003AD6 RID: 15062
		[DispId(1026)]
		string shape
		{
			[DispId(1026)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1026)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06003AD9 RID: 15065
		// (set) Token: 0x06003AD8 RID: 15064
		[DispId(1027)]
		string type
		{
			[DispId(1027)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1027)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
