using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200057F RID: 1407
	[TypeLibType(4160)]
	[Guid("3050F2A6-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLInputTextElement
	{
		// Token: 0x17002ED1 RID: 11985
		// (get) Token: 0x06008D48 RID: 36168
		[DispId(2000)]
		string type
		{
			[DispId(2000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002ED2 RID: 11986
		// (get) Token: 0x06008D4A RID: 36170
		// (set) Token: 0x06008D49 RID: 36169
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

		// Token: 0x17002ED3 RID: 11987
		// (get) Token: 0x06008D4C RID: 36172
		// (set) Token: 0x06008D4B RID: 36171
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

		// Token: 0x17002ED4 RID: 11988
		// (get) Token: 0x06008D4E RID: 36174
		// (set) Token: 0x06008D4D RID: 36173
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

		// Token: 0x17002ED5 RID: 11989
		// (get) Token: 0x06008D50 RID: 36176
		// (set) Token: 0x06008D4F RID: 36175
		[DispId(-2147418036)]
		bool disabled
		{
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002ED6 RID: 11990
		// (get) Token: 0x06008D51 RID: 36177
		[DispId(-2147416108)]
		IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002ED7 RID: 11991
		// (get) Token: 0x06008D53 RID: 36179
		// (set) Token: 0x06008D52 RID: 36178
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

		// Token: 0x17002ED8 RID: 11992
		// (get) Token: 0x06008D55 RID: 36181
		// (set) Token: 0x06008D54 RID: 36180
		[DispId(2002)]
		int size
		{
			[TypeLibFunc(20)]
			[DispId(2002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2002)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002ED9 RID: 11993
		// (get) Token: 0x06008D57 RID: 36183
		// (set) Token: 0x06008D56 RID: 36182
		[DispId(2003)]
		int maxLength
		{
			[TypeLibFunc(20)]
			[DispId(2003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2003)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06008D58 RID: 36184
		[DispId(2004)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void select();

		// Token: 0x17002EDA RID: 11994
		// (get) Token: 0x06008D5A RID: 36186
		// (set) Token: 0x06008D59 RID: 36185
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

		// Token: 0x17002EDB RID: 11995
		// (get) Token: 0x06008D5C RID: 36188
		// (set) Token: 0x06008D5B RID: 36187
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

		// Token: 0x17002EDC RID: 11996
		// (get) Token: 0x06008D5E RID: 36190
		// (set) Token: 0x06008D5D RID: 36189
		[DispId(2005)]
		bool readOnly
		{
			[DispId(2005)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06008D5F RID: 36191
		[DispId(2006)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLTxtRange createTextRange();
	}
}
