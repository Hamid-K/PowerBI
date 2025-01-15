using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CF1 RID: 3313
	[TypeLibType(4160)]
	[Guid("3050F5E0-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLDialog2
	{
		// Token: 0x170075CD RID: 30157
		// (get) Token: 0x0601637B RID: 91003
		// (set) Token: 0x0601637A RID: 91002
		[DispId(25014)]
		string status
		{
			[TypeLibFunc(64)]
			[DispId(25014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(64)]
			[DispId(25014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075CE RID: 30158
		// (get) Token: 0x0601637D RID: 91005
		// (set) Token: 0x0601637C RID: 91004
		[DispId(25015)]
		string resizable
		{
			[DispId(25015)]
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(64)]
			[DispId(25015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
