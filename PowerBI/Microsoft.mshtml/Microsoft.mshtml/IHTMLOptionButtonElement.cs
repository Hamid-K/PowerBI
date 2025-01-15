using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000581 RID: 1409
	[TypeLibType(4160)]
	[Guid("3050F2BC-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLOptionButtonElement
	{
		// Token: 0x17002EE7 RID: 12007
		// (get) Token: 0x06008D74 RID: 36212
		// (set) Token: 0x06008D73 RID: 36211
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

		// Token: 0x17002EE8 RID: 12008
		// (get) Token: 0x06008D75 RID: 36213
		[DispId(2000)]
		string type
		{
			[DispId(2000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002EE9 RID: 12009
		// (get) Token: 0x06008D77 RID: 36215
		// (set) Token: 0x06008D76 RID: 36214
		[DispId(-2147418112)]
		string name
		{
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EEA RID: 12010
		// (get) Token: 0x06008D79 RID: 36217
		// (set) Token: 0x06008D78 RID: 36216
		[DispId(2009)]
		bool @checked
		{
			[TypeLibFunc(4)]
			[DispId(2009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2009)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EEB RID: 12011
		// (get) Token: 0x06008D7B RID: 36219
		// (set) Token: 0x06008D7A RID: 36218
		[DispId(2008)]
		bool defaultChecked
		{
			[TypeLibFunc(4)]
			[DispId(2008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2008)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EEC RID: 12012
		// (get) Token: 0x06008D7D RID: 36221
		// (set) Token: 0x06008D7C RID: 36220
		[DispId(-2147412082)]
		object onchange
		{
			[DispId(-2147412082)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412082)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EED RID: 12013
		// (get) Token: 0x06008D7F RID: 36223
		// (set) Token: 0x06008D7E RID: 36222
		[DispId(-2147418036)]
		bool disabled
		{
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EEE RID: 12014
		// (get) Token: 0x06008D81 RID: 36225
		// (set) Token: 0x06008D80 RID: 36224
		[DispId(2001)]
		bool status
		{
			[DispId(2001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EEF RID: 12015
		// (get) Token: 0x06008D83 RID: 36227
		// (set) Token: 0x06008D82 RID: 36226
		[DispId(2007)]
		bool indeterminate
		{
			[TypeLibFunc(4)]
			[DispId(2007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2007)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EF0 RID: 12016
		// (get) Token: 0x06008D84 RID: 36228
		[DispId(-2147416108)]
		IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}
	}
}
