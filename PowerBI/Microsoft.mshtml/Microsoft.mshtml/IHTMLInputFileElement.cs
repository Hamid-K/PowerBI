using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000580 RID: 1408
	[TypeLibType(4160)]
	[Guid("3050F2AD-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLInputFileElement
	{
		// Token: 0x17002EDD RID: 11997
		// (get) Token: 0x06008D60 RID: 36192
		[DispId(2000)]
		string type
		{
			[DispId(2000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002EDE RID: 11998
		// (get) Token: 0x06008D62 RID: 36194
		// (set) Token: 0x06008D61 RID: 36193
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

		// Token: 0x17002EDF RID: 11999
		// (get) Token: 0x06008D64 RID: 36196
		// (set) Token: 0x06008D63 RID: 36195
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

		// Token: 0x17002EE0 RID: 12000
		// (get) Token: 0x06008D66 RID: 36198
		// (set) Token: 0x06008D65 RID: 36197
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

		// Token: 0x17002EE1 RID: 12001
		// (get) Token: 0x06008D67 RID: 36199
		[DispId(-2147416108)]
		IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002EE2 RID: 12002
		// (get) Token: 0x06008D69 RID: 36201
		// (set) Token: 0x06008D68 RID: 36200
		[DispId(2002)]
		int size
		{
			[DispId(2002)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EE3 RID: 12003
		// (get) Token: 0x06008D6B RID: 36203
		// (set) Token: 0x06008D6A RID: 36202
		[DispId(2003)]
		int maxLength
		{
			[TypeLibFunc(20)]
			[DispId(2003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06008D6C RID: 36204
		[DispId(2004)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void select();

		// Token: 0x17002EE4 RID: 12004
		// (get) Token: 0x06008D6E RID: 36206
		// (set) Token: 0x06008D6D RID: 36205
		[DispId(-2147412082)]
		object onchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412082)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412082)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EE5 RID: 12005
		// (get) Token: 0x06008D70 RID: 36208
		// (set) Token: 0x06008D6F RID: 36207
		[DispId(-2147412102)]
		object onselect
		{
			[DispId(-2147412102)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412102)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EE6 RID: 12006
		// (get) Token: 0x06008D72 RID: 36210
		// (set) Token: 0x06008D71 RID: 36209
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
	}
}
