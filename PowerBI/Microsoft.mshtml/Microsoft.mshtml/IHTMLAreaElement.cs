using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000957 RID: 2391
	[DefaultMember("href")]
	[Guid("3050F265-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLAreaElement
	{
		// Token: 0x17004D4F RID: 19791
		// (get) Token: 0x0600ECBA RID: 60602
		// (set) Token: 0x0600ECB9 RID: 60601
		[DispId(1001)]
		string shape
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004D50 RID: 19792
		// (get) Token: 0x0600ECBC RID: 60604
		// (set) Token: 0x0600ECBB RID: 60603
		[DispId(1002)]
		string coords
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004D51 RID: 19793
		// (get) Token: 0x0600ECBE RID: 60606
		// (set) Token: 0x0600ECBD RID: 60605
		[DispId(0)]
		[IndexerName("href")]
		string href
		{
			[DispId(0)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(0)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004D52 RID: 19794
		// (get) Token: 0x0600ECC0 RID: 60608
		// (set) Token: 0x0600ECBF RID: 60607
		[DispId(1004)]
		string target
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004D53 RID: 19795
		// (get) Token: 0x0600ECC2 RID: 60610
		// (set) Token: 0x0600ECC1 RID: 60609
		[DispId(1005)]
		string alt
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004D54 RID: 19796
		// (get) Token: 0x0600ECC4 RID: 60612
		// (set) Token: 0x0600ECC3 RID: 60611
		[DispId(1006)]
		bool noHref
		{
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004D55 RID: 19797
		// (get) Token: 0x0600ECC6 RID: 60614
		// (set) Token: 0x0600ECC5 RID: 60613
		[DispId(1007)]
		string host
		{
			[DispId(1007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004D56 RID: 19798
		// (get) Token: 0x0600ECC8 RID: 60616
		// (set) Token: 0x0600ECC7 RID: 60615
		[DispId(1008)]
		string hostname
		{
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004D57 RID: 19799
		// (get) Token: 0x0600ECCA RID: 60618
		// (set) Token: 0x0600ECC9 RID: 60617
		[DispId(1009)]
		string pathname
		{
			[DispId(1009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004D58 RID: 19800
		// (get) Token: 0x0600ECCC RID: 60620
		// (set) Token: 0x0600ECCB RID: 60619
		[DispId(1010)]
		string port
		{
			[DispId(1010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004D59 RID: 19801
		// (get) Token: 0x0600ECCE RID: 60622
		// (set) Token: 0x0600ECCD RID: 60621
		[DispId(1011)]
		string protocol
		{
			[DispId(1011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004D5A RID: 19802
		// (get) Token: 0x0600ECD0 RID: 60624
		// (set) Token: 0x0600ECCF RID: 60623
		[DispId(1012)]
		string search
		{
			[DispId(1012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004D5B RID: 19803
		// (get) Token: 0x0600ECD2 RID: 60626
		// (set) Token: 0x0600ECD1 RID: 60625
		[DispId(1013)]
		string hash
		{
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004D5C RID: 19804
		// (get) Token: 0x0600ECD4 RID: 60628
		// (set) Token: 0x0600ECD3 RID: 60627
		[DispId(-2147412097)]
		object onblur
		{
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004D5D RID: 19805
		// (get) Token: 0x0600ECD6 RID: 60630
		// (set) Token: 0x0600ECD5 RID: 60629
		[DispId(-2147412098)]
		object onfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004D5E RID: 19806
		// (get) Token: 0x0600ECD8 RID: 60632
		// (set) Token: 0x0600ECD7 RID: 60631
		[DispId(-2147418097)]
		short tabIndex
		{
			[DispId(-2147418097)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147418097)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x0600ECD9 RID: 60633
		[DispId(-2147416112)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void focus();

		// Token: 0x0600ECDA RID: 60634
		[DispId(-2147416110)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void blur();
	}
}
