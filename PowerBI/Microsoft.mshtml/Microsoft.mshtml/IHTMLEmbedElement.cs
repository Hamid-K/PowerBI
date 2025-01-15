using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000847 RID: 2119
	[TypeLibType(4160)]
	[Guid("3050F25F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLEmbedElement
	{
		// Token: 0x17004A14 RID: 18964
		// (get) Token: 0x0600DF41 RID: 57153
		// (set) Token: 0x0600DF40 RID: 57152
		[DispId(-2147415102)]
		string hidden
		{
			[DispId(-2147415102)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415102)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004A15 RID: 18965
		// (get) Token: 0x0600DF42 RID: 57154
		[DispId(-2147415108)]
		string palette
		{
			[DispId(-2147415108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004A16 RID: 18966
		// (get) Token: 0x0600DF43 RID: 57155
		[DispId(-2147415107)]
		string pluginspage
		{
			[DispId(-2147415107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004A17 RID: 18967
		// (get) Token: 0x0600DF45 RID: 57157
		// (set) Token: 0x0600DF44 RID: 57156
		[DispId(-2147415106)]
		string src
		{
			[DispId(-2147415106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004A18 RID: 18968
		// (get) Token: 0x0600DF47 RID: 57159
		// (set) Token: 0x0600DF46 RID: 57158
		[DispId(-2147415104)]
		string units
		{
			[DispId(-2147415104)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415104)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004A19 RID: 18969
		// (get) Token: 0x0600DF49 RID: 57161
		// (set) Token: 0x0600DF48 RID: 57160
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

		// Token: 0x17004A1A RID: 18970
		// (get) Token: 0x0600DF4B RID: 57163
		// (set) Token: 0x0600DF4A RID: 57162
		[DispId(-2147418107)]
		object width
		{
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004A1B RID: 18971
		// (get) Token: 0x0600DF4D RID: 57165
		// (set) Token: 0x0600DF4C RID: 57164
		[DispId(-2147418106)]
		object height
		{
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}
	}
}
