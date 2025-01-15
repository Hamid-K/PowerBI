using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007A3 RID: 1955
	[ClassInterface(0)]
	[Guid("FECEAAA6-8405-11CF-8BA1-00AA00476DA6")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLNavigatorClass : IOmNavigator, HTMLNavigator
	{
		// Token: 0x0600D4CC RID: 54476
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLNavigatorClass();

		// Token: 0x17004650 RID: 18000
		// (get) Token: 0x0600D4CD RID: 54477
		[DispId(1)]
		public virtual extern string appCodeName
		{
			[DispId(1)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004651 RID: 18001
		// (get) Token: 0x0600D4CE RID: 54478
		[DispId(2)]
		public virtual extern string appName
		{
			[DispId(2)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004652 RID: 18002
		// (get) Token: 0x0600D4CF RID: 54479
		[DispId(3)]
		public virtual extern string appVersion
		{
			[DispId(3)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004653 RID: 18003
		// (get) Token: 0x0600D4D0 RID: 54480
		[DispId(4)]
		public virtual extern string userAgent
		{
			[DispId(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600D4D1 RID: 54481
		[DispId(5)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool javaEnabled();

		// Token: 0x0600D4D2 RID: 54482
		[DispId(6)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool taintEnabled();

		// Token: 0x17004654 RID: 18004
		// (get) Token: 0x0600D4D3 RID: 54483
		[DispId(7)]
		public virtual extern CMimeTypes mimeTypes
		{
			[DispId(7)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004655 RID: 18005
		// (get) Token: 0x0600D4D4 RID: 54484
		[DispId(8)]
		public virtual extern CPlugins plugins
		{
			[DispId(8)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004656 RID: 18006
		// (get) Token: 0x0600D4D5 RID: 54485
		[DispId(9)]
		public virtual extern bool cookieEnabled
		{
			[DispId(9)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004657 RID: 18007
		// (get) Token: 0x0600D4D6 RID: 54486
		[DispId(10)]
		public virtual extern COpsProfile opsProfile
		{
			[DispId(10)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D4D7 RID: 54487
		[DispId(11)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17004658 RID: 18008
		// (get) Token: 0x0600D4D8 RID: 54488
		[DispId(12)]
		public virtual extern string cpuClass
		{
			[DispId(12)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004659 RID: 18009
		// (get) Token: 0x0600D4D9 RID: 54489
		[DispId(13)]
		public virtual extern string systemLanguage
		{
			[DispId(13)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700465A RID: 18010
		// (get) Token: 0x0600D4DA RID: 54490
		[DispId(14)]
		public virtual extern string browserLanguage
		{
			[DispId(14)]
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700465B RID: 18011
		// (get) Token: 0x0600D4DB RID: 54491
		[DispId(15)]
		public virtual extern string userLanguage
		{
			[DispId(15)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700465C RID: 18012
		// (get) Token: 0x0600D4DC RID: 54492
		[DispId(16)]
		public virtual extern string platform
		{
			[DispId(16)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700465D RID: 18013
		// (get) Token: 0x0600D4DD RID: 54493
		[DispId(17)]
		public virtual extern string appMinorVersion
		{
			[DispId(17)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700465E RID: 18014
		// (get) Token: 0x0600D4DE RID: 54494
		[DispId(18)]
		public virtual extern int connectionSpeed
		{
			[TypeLibFunc(64)]
			[DispId(18)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700465F RID: 18015
		// (get) Token: 0x0600D4DF RID: 54495
		[DispId(19)]
		public virtual extern bool onLine
		{
			[DispId(19)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004660 RID: 18016
		// (get) Token: 0x0600D4E0 RID: 54496
		[DispId(20)]
		public virtual extern COpsProfile userProfile
		{
			[DispId(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}
	}
}
