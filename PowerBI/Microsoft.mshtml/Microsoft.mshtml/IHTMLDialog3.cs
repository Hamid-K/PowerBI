using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CF2 RID: 3314
	[Guid("3050F388-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLDialog3
	{
		// Token: 0x170075CF RID: 30159
		// (get) Token: 0x0601637F RID: 91007
		// (set) Token: 0x0601637E RID: 91006
		[DispId(25016)]
		string unadorned
		{
			[TypeLibFunc(64)]
			[DispId(25016)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(25016)]
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075D0 RID: 30160
		// (get) Token: 0x06016381 RID: 91009
		// (set) Token: 0x06016380 RID: 91008
		[DispId(25007)]
		string dialogHide
		{
			[DispId(25007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(25007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
