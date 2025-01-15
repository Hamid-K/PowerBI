using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000694 RID: 1684
	[Guid("3050F2AA-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLTextAreaElement
	{
		// Token: 0x17003141 RID: 12609
		// (get) Token: 0x06009863 RID: 39011
		[DispId(2000)]
		string type
		{
			[DispId(2000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003142 RID: 12610
		// (get) Token: 0x06009865 RID: 39013
		// (set) Token: 0x06009864 RID: 39012
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

		// Token: 0x17003143 RID: 12611
		// (get) Token: 0x06009867 RID: 39015
		// (set) Token: 0x06009866 RID: 39014
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

		// Token: 0x17003144 RID: 12612
		// (get) Token: 0x06009869 RID: 39017
		// (set) Token: 0x06009868 RID: 39016
		[DispId(2001)]
		object status
		{
			[DispId(2001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003145 RID: 12613
		// (get) Token: 0x0600986B RID: 39019
		// (set) Token: 0x0600986A RID: 39018
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

		// Token: 0x17003146 RID: 12614
		// (get) Token: 0x0600986C RID: 39020
		[DispId(-2147416108)]
		IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003147 RID: 12615
		// (get) Token: 0x0600986E RID: 39022
		// (set) Token: 0x0600986D RID: 39021
		[DispId(-2147413029)]
		string defaultValue
		{
			[TypeLibFunc(84)]
			[DispId(-2147413029)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413029)]
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x0600986F RID: 39023
		[DispId(7005)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void select();

		// Token: 0x17003148 RID: 12616
		// (get) Token: 0x06009871 RID: 39025
		// (set) Token: 0x06009870 RID: 39024
		[DispId(-2147412082)]
		object onchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412082)]
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

		// Token: 0x17003149 RID: 12617
		// (get) Token: 0x06009873 RID: 39027
		// (set) Token: 0x06009872 RID: 39026
		[DispId(-2147412102)]
		object onselect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412102)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412102)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700314A RID: 12618
		// (get) Token: 0x06009875 RID: 39029
		// (set) Token: 0x06009874 RID: 39028
		[DispId(7004)]
		bool readOnly
		{
			[DispId(7004)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(7004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700314B RID: 12619
		// (get) Token: 0x06009877 RID: 39031
		// (set) Token: 0x06009876 RID: 39030
		[DispId(7001)]
		int rows
		{
			[DispId(7001)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(7001)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700314C RID: 12620
		// (get) Token: 0x06009879 RID: 39033
		// (set) Token: 0x06009878 RID: 39032
		[DispId(7002)]
		int cols
		{
			[DispId(7002)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(7002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700314D RID: 12621
		// (get) Token: 0x0600987B RID: 39035
		// (set) Token: 0x0600987A RID: 39034
		[DispId(7003)]
		string wrap
		{
			[TypeLibFunc(20)]
			[DispId(7003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(7003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x0600987C RID: 39036
		[DispId(7006)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLTxtRange createTextRange();
	}
}
