using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007A5 RID: 1957
	[DefaultMember("href")]
	[Guid("163BB1E1-6E00-11CF-837A-48DC04C10000")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[ComImport]
	public class HTMLLocationClass : IHTMLLocation, HTMLLocation
	{
		// Token: 0x0600D4E1 RID: 54497
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLLocationClass();

		// Token: 0x17004661 RID: 18017
		// (get) Token: 0x0600D4E3 RID: 54499
		// (set) Token: 0x0600D4E2 RID: 54498
		[DispId(0)]
		[IndexerName("href")]
		public virtual extern string href
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

		// Token: 0x17004662 RID: 18018
		// (get) Token: 0x0600D4E5 RID: 54501
		// (set) Token: 0x0600D4E4 RID: 54500
		[DispId(1)]
		public virtual extern string protocol
		{
			[DispId(1)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004663 RID: 18019
		// (get) Token: 0x0600D4E7 RID: 54503
		// (set) Token: 0x0600D4E6 RID: 54502
		[DispId(2)]
		public virtual extern string host
		{
			[DispId(2)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004664 RID: 18020
		// (get) Token: 0x0600D4E9 RID: 54505
		// (set) Token: 0x0600D4E8 RID: 54504
		[DispId(3)]
		public virtual extern string hostname
		{
			[DispId(3)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(3)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004665 RID: 18021
		// (get) Token: 0x0600D4EB RID: 54507
		// (set) Token: 0x0600D4EA RID: 54506
		[DispId(4)]
		public virtual extern string port
		{
			[DispId(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004666 RID: 18022
		// (get) Token: 0x0600D4ED RID: 54509
		// (set) Token: 0x0600D4EC RID: 54508
		[DispId(5)]
		public virtual extern string pathname
		{
			[DispId(5)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004667 RID: 18023
		// (get) Token: 0x0600D4EF RID: 54511
		// (set) Token: 0x0600D4EE RID: 54510
		[DispId(6)]
		public virtual extern string search
		{
			[DispId(6)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(6)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004668 RID: 18024
		// (get) Token: 0x0600D4F1 RID: 54513
		// (set) Token: 0x0600D4F0 RID: 54512
		[DispId(7)]
		public virtual extern string hash
		{
			[DispId(7)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(7)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x0600D4F2 RID: 54514
		[DispId(8)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void reload([In] bool flag = false);

		// Token: 0x0600D4F3 RID: 54515
		[DispId(9)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void replace([MarshalAs(19)] [In] string bstr);

		// Token: 0x0600D4F4 RID: 54516
		[DispId(10)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void assign([MarshalAs(19)] [In] string bstr);

		// Token: 0x0600D4F5 RID: 54517
		[DispId(11)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();
	}
}
