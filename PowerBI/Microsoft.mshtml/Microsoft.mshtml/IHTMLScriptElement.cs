using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A92 RID: 2706
	[TypeLibType(4160)]
	[Guid("3050F28B-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLScriptElement
	{
		// Token: 0x17005E17 RID: 24087
		// (get) Token: 0x06011CBE RID: 72894
		// (set) Token: 0x06011CBD RID: 72893
		[DispId(1001)]
		string src
		{
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005E18 RID: 24088
		// (get) Token: 0x06011CC0 RID: 72896
		// (set) Token: 0x06011CBF RID: 72895
		[DispId(1004)]
		string htmlFor
		{
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005E19 RID: 24089
		// (get) Token: 0x06011CC2 RID: 72898
		// (set) Token: 0x06011CC1 RID: 72897
		[DispId(1005)]
		string @event
		{
			[TypeLibFunc(20)]
			[DispId(1005)]
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

		// Token: 0x17005E1A RID: 24090
		// (get) Token: 0x06011CC4 RID: 72900
		// (set) Token: 0x06011CC3 RID: 72899
		[DispId(1006)]
		string text
		{
			[TypeLibFunc(20)]
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1006)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005E1B RID: 24091
		// (get) Token: 0x06011CC6 RID: 72902
		// (set) Token: 0x06011CC5 RID: 72901
		[DispId(1007)]
		bool defer
		{
			[TypeLibFunc(20)]
			[DispId(1007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1007)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17005E1C RID: 24092
		// (get) Token: 0x06011CC7 RID: 72903
		[DispId(-2147412996)]
		string readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005E1D RID: 24093
		// (get) Token: 0x06011CC9 RID: 72905
		// (set) Token: 0x06011CC8 RID: 72904
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

		// Token: 0x17005E1E RID: 24094
		// (get) Token: 0x06011CCB RID: 72907
		// (set) Token: 0x06011CCA RID: 72906
		[DispId(1009)]
		string type
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
	}
}
