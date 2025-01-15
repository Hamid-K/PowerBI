using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CF0 RID: 3312
	[TypeLibType(4160)]
	[Guid("3050F216-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLDialog
	{
		// Token: 0x170075C6 RID: 30150
		// (get) Token: 0x0601636D RID: 90989
		// (set) Token: 0x0601636C RID: 90988
		[DispId(-2147418108)]
		object dialogTop
		{
			[DispId(-2147418108)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418108)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170075C7 RID: 30151
		// (get) Token: 0x0601636F RID: 90991
		// (set) Token: 0x0601636E RID: 90990
		[DispId(-2147418109)]
		object dialogLeft
		{
			[TypeLibFunc(4)]
			[DispId(-2147418109)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418109)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170075C8 RID: 30152
		// (get) Token: 0x06016371 RID: 90993
		// (set) Token: 0x06016370 RID: 90992
		[DispId(-2147418107)]
		object dialogWidth
		{
			[TypeLibFunc(4)]
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170075C9 RID: 30153
		// (get) Token: 0x06016373 RID: 90995
		// (set) Token: 0x06016372 RID: 90994
		[DispId(-2147418106)]
		object dialogHeight
		{
			[TypeLibFunc(4)]
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170075CA RID: 30154
		// (get) Token: 0x06016374 RID: 90996
		[DispId(25000)]
		object dialogArguments
		{
			[DispId(25000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170075CB RID: 30155
		// (get) Token: 0x06016375 RID: 90997
		[DispId(25013)]
		object menuArguments
		{
			[DispId(25013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170075CC RID: 30156
		// (get) Token: 0x06016377 RID: 90999
		// (set) Token: 0x06016376 RID: 90998
		[DispId(25001)]
		object returnValue
		{
			[DispId(25001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(25001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06016378 RID: 91000
		[DispId(25011)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void close();

		// Token: 0x06016379 RID: 91001
		[DispId(25012)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();
	}
}
