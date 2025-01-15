using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007AB RID: 1963
	[TypeLibType(4160)]
	[Guid("3050F4B3-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLDataTransfer
	{
		// Token: 0x0600D4F9 RID: 54521
		[DispId(1001)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool setData([MarshalAs(19)] [In] string format, [MarshalAs(27)] [In] ref object data);

		// Token: 0x0600D4FA RID: 54522
		[DispId(1002)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getData([MarshalAs(19)] [In] string format);

		// Token: 0x0600D4FB RID: 54523
		[DispId(1003)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool clearData([MarshalAs(19)] [In] string format);

		// Token: 0x1700466A RID: 18026
		// (get) Token: 0x0600D4FD RID: 54525
		// (set) Token: 0x0600D4FC RID: 54524
		[DispId(1004)]
		string dropEffect
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700466B RID: 18027
		// (get) Token: 0x0600D4FF RID: 54527
		// (set) Token: 0x0600D4FE RID: 54526
		[DispId(1005)]
		string effectAllowed
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
