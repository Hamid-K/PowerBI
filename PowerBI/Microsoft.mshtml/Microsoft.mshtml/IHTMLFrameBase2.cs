using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B47 RID: 2887
	[TypeLibType(4160)]
	[Guid("3050F6DB-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLFrameBase2
	{
		// Token: 0x170064CF RID: 25807
		// (get) Token: 0x06013078 RID: 77944
		[DispId(-2147415103)]
		IHTMLWindow2 contentWindow
		{
			[DispId(-2147415103)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170064D0 RID: 25808
		// (get) Token: 0x0601307A RID: 77946
		// (set) Token: 0x06013079 RID: 77945
		[DispId(-2147412080)]
		object onload
		{
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170064D1 RID: 25809
		// (get) Token: 0x0601307C RID: 77948
		// (set) Token: 0x0601307B RID: 77947
		[DispId(-2147412087)]
		object onreadystatechange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170064D2 RID: 25810
		// (get) Token: 0x0601307D RID: 77949
		[DispId(-2147412996)]
		string readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170064D3 RID: 25811
		// (get) Token: 0x0601307F RID: 77951
		// (set) Token: 0x0601307E RID: 77950
		[DispId(-2147412906)]
		bool allowTransparency
		{
			[DispId(-2147412906)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147412906)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}
	}
}
