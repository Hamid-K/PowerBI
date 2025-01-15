using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000150 RID: 336
	[TypeLibType(4160)]
	[Guid("3050F205-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLLinkElement
	{
		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060014B3 RID: 5299
		// (set) Token: 0x060014B2 RID: 5298
		[DispId(1005)]
		string href
		{
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060014B5 RID: 5301
		// (set) Token: 0x060014B4 RID: 5300
		[DispId(1006)]
		string rel
		{
			[DispId(1006)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060014B7 RID: 5303
		// (set) Token: 0x060014B6 RID: 5302
		[DispId(1007)]
		string rev
		{
			[TypeLibFunc(20)]
			[DispId(1007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1007)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060014B9 RID: 5305
		// (set) Token: 0x060014B8 RID: 5304
		[DispId(1008)]
		string type
		{
			[DispId(1008)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060014BA RID: 5306
		[DispId(-2147412996)]
		string readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060014BC RID: 5308
		// (set) Token: 0x060014BB RID: 5307
		[DispId(-2147412087)]
		object onreadystatechange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060014BE RID: 5310
		// (set) Token: 0x060014BD RID: 5309
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

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060014C0 RID: 5312
		// (set) Token: 0x060014BF RID: 5311
		[DispId(-2147412083)]
		object onerror
		{
			[DispId(-2147412083)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412083)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060014C1 RID: 5313
		[DispId(1014)]
		IHTMLStyleSheet styleSheet
		{
			[DispId(1014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060014C3 RID: 5315
		// (set) Token: 0x060014C2 RID: 5314
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

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060014C5 RID: 5317
		// (set) Token: 0x060014C4 RID: 5316
		[DispId(1016)]
		string media
		{
			[DispId(1016)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1016)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
