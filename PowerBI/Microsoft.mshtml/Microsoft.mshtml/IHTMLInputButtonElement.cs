using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200057D RID: 1405
	[TypeLibType(4160)]
	[Guid("3050F2B2-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLInputButtonElement
	{
		// Token: 0x17002EC5 RID: 11973
		// (get) Token: 0x06008D32 RID: 36146
		[DispId(2000)]
		string type
		{
			[DispId(2000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002EC6 RID: 11974
		// (get) Token: 0x06008D34 RID: 36148
		// (set) Token: 0x06008D33 RID: 36147
		[DispId(-2147413011)]
		string value
		{
			[DispId(-2147413011)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413011)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EC7 RID: 11975
		// (get) Token: 0x06008D36 RID: 36150
		// (set) Token: 0x06008D35 RID: 36149
		[DispId(-2147418112)]
		string name
		{
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EC8 RID: 11976
		// (get) Token: 0x06008D38 RID: 36152
		// (set) Token: 0x06008D37 RID: 36151
		[DispId(2021)]
		object status
		{
			[DispId(2021)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2021)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EC9 RID: 11977
		// (get) Token: 0x06008D3A RID: 36154
		// (set) Token: 0x06008D39 RID: 36153
		[DispId(-2147418036)]
		bool disabled
		{
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002ECA RID: 11978
		// (get) Token: 0x06008D3B RID: 36155
		[DispId(-2147416108)]
		IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06008D3C RID: 36156
		[DispId(2006)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLTxtRange createTextRange();
	}
}
