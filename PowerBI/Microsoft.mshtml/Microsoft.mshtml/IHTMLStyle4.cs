using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200004B RID: 75
	[Guid("3050F816-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLStyle4
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000197 RID: 407
		// (set) Token: 0x06000196 RID: 406
		[DispId(-2147412903)]
		string textOverflow
		{
			[DispId(-2147412903)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412903)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000199 RID: 409
		// (set) Token: 0x06000198 RID: 408
		[DispId(-2147412901)]
		object minHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147412901)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412901)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}
	}
}
