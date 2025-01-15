using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200079A RID: 1946
	[TypeLibType(4160)]
	[DefaultMember("href")]
	[Guid("163BB1E0-6E00-11CF-837A-48DC04C10000")]
	[ComImport]
	public interface IHTMLLocation
	{
		// Token: 0x17004645 RID: 17989
		// (get) Token: 0x0600D4A5 RID: 54437
		// (set) Token: 0x0600D4A4 RID: 54436
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

		// Token: 0x17004646 RID: 17990
		// (get) Token: 0x0600D4A7 RID: 54439
		// (set) Token: 0x0600D4A6 RID: 54438
		[DispId(1)]
		string protocol
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

		// Token: 0x17004647 RID: 17991
		// (get) Token: 0x0600D4A9 RID: 54441
		// (set) Token: 0x0600D4A8 RID: 54440
		[DispId(2)]
		string host
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

		// Token: 0x17004648 RID: 17992
		// (get) Token: 0x0600D4AB RID: 54443
		// (set) Token: 0x0600D4AA RID: 54442
		[DispId(3)]
		string hostname
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

		// Token: 0x17004649 RID: 17993
		// (get) Token: 0x0600D4AD RID: 54445
		// (set) Token: 0x0600D4AC RID: 54444
		[DispId(4)]
		string port
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

		// Token: 0x1700464A RID: 17994
		// (get) Token: 0x0600D4AF RID: 54447
		// (set) Token: 0x0600D4AE RID: 54446
		[DispId(5)]
		string pathname
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

		// Token: 0x1700464B RID: 17995
		// (get) Token: 0x0600D4B1 RID: 54449
		// (set) Token: 0x0600D4B0 RID: 54448
		[DispId(6)]
		string search
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

		// Token: 0x1700464C RID: 17996
		// (get) Token: 0x0600D4B3 RID: 54451
		// (set) Token: 0x0600D4B2 RID: 54450
		[DispId(7)]
		string hash
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

		// Token: 0x0600D4B4 RID: 54452
		[DispId(8)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void reload([In] bool flag = false);

		// Token: 0x0600D4B5 RID: 54453
		[DispId(9)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void replace([MarshalAs(19)] [In] string bstr);

		// Token: 0x0600D4B6 RID: 54454
		[DispId(10)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void assign([MarshalAs(19)] [In] string bstr);

		// Token: 0x0600D4B7 RID: 54455
		[DispId(11)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();
	}
}
