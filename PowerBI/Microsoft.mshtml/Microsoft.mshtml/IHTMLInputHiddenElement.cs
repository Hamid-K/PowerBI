using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200057E RID: 1406
	[TypeLibType(4160)]
	[Guid("3050F2A4-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLInputHiddenElement
	{
		// Token: 0x17002ECB RID: 11979
		// (get) Token: 0x06008D3D RID: 36157
		[DispId(2000)]
		string type
		{
			[DispId(2000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002ECC RID: 11980
		// (get) Token: 0x06008D3F RID: 36159
		// (set) Token: 0x06008D3E RID: 36158
		[DispId(-2147413011)]
		string value
		{
			[DispId(-2147413011)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002ECD RID: 11981
		// (get) Token: 0x06008D41 RID: 36161
		// (set) Token: 0x06008D40 RID: 36160
		[DispId(-2147418112)]
		string name
		{
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
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

		// Token: 0x17002ECE RID: 11982
		// (get) Token: 0x06008D43 RID: 36163
		// (set) Token: 0x06008D42 RID: 36162
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

		// Token: 0x17002ECF RID: 11983
		// (get) Token: 0x06008D45 RID: 36165
		// (set) Token: 0x06008D44 RID: 36164
		[DispId(-2147418036)]
		bool disabled
		{
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002ED0 RID: 11984
		// (get) Token: 0x06008D46 RID: 36166
		[DispId(-2147416108)]
		IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06008D47 RID: 36167
		[DispId(2006)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLTxtRange createTextRange();
	}
}
