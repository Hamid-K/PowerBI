using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200027B RID: 635
	[Guid("3050F240-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLImgElement
	{
		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06002821 RID: 10273
		// (set) Token: 0x06002820 RID: 10272
		[DispId(2002)]
		bool isMap
		{
			[TypeLibFunc(20)]
			[DispId(2002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06002823 RID: 10275
		// (set) Token: 0x06002822 RID: 10274
		[DispId(2008)]
		string useMap
		{
			[DispId(2008)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2008)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06002824 RID: 10276
		[DispId(2010)]
		string mimeType
		{
			[DispId(2010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06002825 RID: 10277
		[DispId(2011)]
		string fileSize
		{
			[DispId(2011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06002826 RID: 10278
		[DispId(2012)]
		string fileCreatedDate
		{
			[DispId(2012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06002827 RID: 10279
		[DispId(2013)]
		string fileModifiedDate
		{
			[DispId(2013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06002828 RID: 10280
		[DispId(2014)]
		string fileUpdatedDate
		{
			[DispId(2014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06002829 RID: 10281
		[DispId(2015)]
		string protocol
		{
			[DispId(2015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x0600282A RID: 10282
		[DispId(2016)]
		string href
		{
			[DispId(2016)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x0600282B RID: 10283
		[DispId(2017)]
		string nameProp
		{
			[DispId(2017)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x0600282D RID: 10285
		// (set) Token: 0x0600282C RID: 10284
		[DispId(1004)]
		object border
		{
			[DispId(1004)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1004)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x0600282F RID: 10287
		// (set) Token: 0x0600282E RID: 10286
		[DispId(1005)]
		int vspace
		{
			[TypeLibFunc(20)]
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06002831 RID: 10289
		// (set) Token: 0x06002830 RID: 10288
		[DispId(1006)]
		int hspace
		{
			[TypeLibFunc(20)]
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1006)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06002833 RID: 10291
		// (set) Token: 0x06002832 RID: 10290
		[DispId(1002)]
		string alt
		{
			[TypeLibFunc(20)]
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06002835 RID: 10293
		// (set) Token: 0x06002834 RID: 10292
		[DispId(1003)]
		string src
		{
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06002837 RID: 10295
		// (set) Token: 0x06002836 RID: 10294
		[DispId(1007)]
		string lowsrc
		{
			[DispId(1007)]
			[TypeLibFunc(20)]
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

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06002839 RID: 10297
		// (set) Token: 0x06002838 RID: 10296
		[DispId(1008)]
		string vrml
		{
			[DispId(1008)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1008)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x0600283B RID: 10299
		// (set) Token: 0x0600283A RID: 10298
		[DispId(1009)]
		string dynsrc
		{
			[DispId(1009)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x0600283C RID: 10300
		[DispId(-2147412996)]
		string readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x0600283D RID: 10301
		[DispId(1010)]
		bool complete
		{
			[DispId(1010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x0600283F RID: 10303
		// (set) Token: 0x0600283E RID: 10302
		[DispId(1011)]
		object loop
		{
			[TypeLibFunc(20)]
			[DispId(1011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(1011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06002841 RID: 10305
		// (set) Token: 0x06002840 RID: 10304
		[DispId(-2147418039)]
		string align
		{
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06002843 RID: 10307
		// (set) Token: 0x06002842 RID: 10306
		[DispId(-2147412080)]
		object onload
		{
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06002845 RID: 10309
		// (set) Token: 0x06002844 RID: 10308
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

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06002847 RID: 10311
		// (set) Token: 0x06002846 RID: 10310
		[DispId(-2147412084)]
		object onabort
		{
			[DispId(-2147412084)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412084)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06002849 RID: 10313
		// (set) Token: 0x06002848 RID: 10312
		[DispId(-2147418112)]
		string name
		{
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
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

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x0600284B RID: 10315
		// (set) Token: 0x0600284A RID: 10314
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

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x0600284D RID: 10317
		// (set) Token: 0x0600284C RID: 10316
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

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x0600284F RID: 10319
		// (set) Token: 0x0600284E RID: 10318
		[DispId(1013)]
		string Start
		{
			[TypeLibFunc(20)]
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
