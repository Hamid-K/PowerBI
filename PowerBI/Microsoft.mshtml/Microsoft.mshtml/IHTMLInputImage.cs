using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000582 RID: 1410
	[TypeLibType(4160)]
	[Guid("3050F2C2-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLInputImage
	{
		// Token: 0x17002EF1 RID: 12017
		// (get) Token: 0x06008D85 RID: 36229
		[DispId(2000)]
		string type
		{
			[DispId(2000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002EF2 RID: 12018
		// (get) Token: 0x06008D87 RID: 36231
		// (set) Token: 0x06008D86 RID: 36230
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

		// Token: 0x17002EF3 RID: 12019
		// (get) Token: 0x06008D89 RID: 36233
		// (set) Token: 0x06008D88 RID: 36232
		[DispId(2012)]
		object border
		{
			[TypeLibFunc(20)]
			[DispId(2012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2012)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EF4 RID: 12020
		// (get) Token: 0x06008D8B RID: 36235
		// (set) Token: 0x06008D8A RID: 36234
		[DispId(2013)]
		int vspace
		{
			[DispId(2013)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EF5 RID: 12021
		// (get) Token: 0x06008D8D RID: 36237
		// (set) Token: 0x06008D8C RID: 36236
		[DispId(2014)]
		int hspace
		{
			[DispId(2014)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2014)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EF6 RID: 12022
		// (get) Token: 0x06008D8F RID: 36239
		// (set) Token: 0x06008D8E RID: 36238
		[DispId(2010)]
		string alt
		{
			[TypeLibFunc(20)]
			[DispId(2010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EF7 RID: 12023
		// (get) Token: 0x06008D91 RID: 36241
		// (set) Token: 0x06008D90 RID: 36240
		[DispId(2011)]
		string src
		{
			[TypeLibFunc(20)]
			[DispId(2011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2011)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EF8 RID: 12024
		// (get) Token: 0x06008D93 RID: 36243
		// (set) Token: 0x06008D92 RID: 36242
		[DispId(2015)]
		string lowsrc
		{
			[TypeLibFunc(20)]
			[DispId(2015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2015)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EF9 RID: 12025
		// (get) Token: 0x06008D95 RID: 36245
		// (set) Token: 0x06008D94 RID: 36244
		[DispId(2016)]
		string vrml
		{
			[DispId(2016)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2016)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EFA RID: 12026
		// (get) Token: 0x06008D97 RID: 36247
		// (set) Token: 0x06008D96 RID: 36246
		[DispId(2017)]
		string dynsrc
		{
			[TypeLibFunc(20)]
			[DispId(2017)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2017)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EFB RID: 12027
		// (get) Token: 0x06008D98 RID: 36248
		[DispId(-2147412996)]
		string readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002EFC RID: 12028
		// (get) Token: 0x06008D99 RID: 36249
		[DispId(2018)]
		bool complete
		{
			[DispId(2018)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002EFD RID: 12029
		// (get) Token: 0x06008D9B RID: 36251
		// (set) Token: 0x06008D9A RID: 36250
		[DispId(2019)]
		object loop
		{
			[TypeLibFunc(20)]
			[DispId(2019)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2019)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EFE RID: 12030
		// (get) Token: 0x06008D9D RID: 36253
		// (set) Token: 0x06008D9C RID: 36252
		[DispId(-2147418039)]
		string align
		{
			[DispId(-2147418039)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418039)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EFF RID: 12031
		// (get) Token: 0x06008D9F RID: 36255
		// (set) Token: 0x06008D9E RID: 36254
		[DispId(-2147412080)]
		object onload
		{
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002F00 RID: 12032
		// (get) Token: 0x06008DA1 RID: 36257
		// (set) Token: 0x06008DA0 RID: 36256
		[DispId(-2147412083)]
		object onerror
		{
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002F01 RID: 12033
		// (get) Token: 0x06008DA3 RID: 36259
		// (set) Token: 0x06008DA2 RID: 36258
		[DispId(-2147412084)]
		object onabort
		{
			[TypeLibFunc(20)]
			[DispId(-2147412084)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412084)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002F02 RID: 12034
		// (get) Token: 0x06008DA5 RID: 36261
		// (set) Token: 0x06008DA4 RID: 36260
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

		// Token: 0x17002F03 RID: 12035
		// (get) Token: 0x06008DA7 RID: 36263
		// (set) Token: 0x06008DA6 RID: 36262
		[DispId(-2147418107)]
		int width
		{
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002F04 RID: 12036
		// (get) Token: 0x06008DA9 RID: 36265
		// (set) Token: 0x06008DA8 RID: 36264
		[DispId(-2147418106)]
		int height
		{
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002F05 RID: 12037
		// (get) Token: 0x06008DAB RID: 36267
		// (set) Token: 0x06008DAA RID: 36266
		[DispId(2020)]
		string Start
		{
			[TypeLibFunc(20)]
			[DispId(2020)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2020)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
