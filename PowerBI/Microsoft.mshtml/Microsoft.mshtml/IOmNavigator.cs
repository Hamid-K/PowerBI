using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000799 RID: 1945
	[Guid("FECEAAA5-8405-11CF-8BA1-00AA00476DA6")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IOmNavigator
	{
		// Token: 0x17004634 RID: 17972
		// (get) Token: 0x0600D490 RID: 54416
		[DispId(1)]
		string appCodeName
		{
			[DispId(1)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004635 RID: 17973
		// (get) Token: 0x0600D491 RID: 54417
		[DispId(2)]
		string appName
		{
			[DispId(2)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004636 RID: 17974
		// (get) Token: 0x0600D492 RID: 54418
		[DispId(3)]
		string appVersion
		{
			[DispId(3)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004637 RID: 17975
		// (get) Token: 0x0600D493 RID: 54419
		[DispId(4)]
		string userAgent
		{
			[DispId(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600D494 RID: 54420
		[DispId(5)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool javaEnabled();

		// Token: 0x0600D495 RID: 54421
		[DispId(6)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool taintEnabled();

		// Token: 0x17004638 RID: 17976
		// (get) Token: 0x0600D496 RID: 54422
		[DispId(7)]
		CMimeTypes mimeTypes
		{
			[DispId(7)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004639 RID: 17977
		// (get) Token: 0x0600D497 RID: 54423
		[DispId(8)]
		CPlugins plugins
		{
			[DispId(8)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700463A RID: 17978
		// (get) Token: 0x0600D498 RID: 54424
		[DispId(9)]
		bool cookieEnabled
		{
			[DispId(9)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700463B RID: 17979
		// (get) Token: 0x0600D499 RID: 54425
		[DispId(10)]
		COpsProfile opsProfile
		{
			[DispId(10)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D49A RID: 54426
		[DispId(11)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x1700463C RID: 17980
		// (get) Token: 0x0600D49B RID: 54427
		[DispId(12)]
		string cpuClass
		{
			[DispId(12)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700463D RID: 17981
		// (get) Token: 0x0600D49C RID: 54428
		[DispId(13)]
		string systemLanguage
		{
			[DispId(13)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700463E RID: 17982
		// (get) Token: 0x0600D49D RID: 54429
		[DispId(14)]
		string browserLanguage
		{
			[DispId(14)]
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700463F RID: 17983
		// (get) Token: 0x0600D49E RID: 54430
		[DispId(15)]
		string userLanguage
		{
			[DispId(15)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004640 RID: 17984
		// (get) Token: 0x0600D49F RID: 54431
		[DispId(16)]
		string platform
		{
			[DispId(16)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004641 RID: 17985
		// (get) Token: 0x0600D4A0 RID: 54432
		[DispId(17)]
		string appMinorVersion
		{
			[DispId(17)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004642 RID: 17986
		// (get) Token: 0x0600D4A1 RID: 54433
		[DispId(18)]
		int connectionSpeed
		{
			[DispId(18)]
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004643 RID: 17987
		// (get) Token: 0x0600D4A2 RID: 54434
		[DispId(19)]
		bool onLine
		{
			[DispId(19)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004644 RID: 17988
		// (get) Token: 0x0600D4A3 RID: 54435
		[DispId(20)]
		COpsProfile userProfile
		{
			[DispId(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}
	}
}
