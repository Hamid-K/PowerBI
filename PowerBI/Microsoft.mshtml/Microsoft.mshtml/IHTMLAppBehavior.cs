using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D04 RID: 3332
	[TypeLibType(4160)]
	[Guid("3050F5CA-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLAppBehavior
	{
		// Token: 0x170075E8 RID: 30184
		// (get) Token: 0x060163AC RID: 91052
		// (set) Token: 0x060163AB RID: 91051
		[DispId(5000)]
		string applicationName
		{
			[DispId(5000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075E9 RID: 30185
		// (get) Token: 0x060163AE RID: 91054
		// (set) Token: 0x060163AD RID: 91053
		[DispId(5001)]
		string version
		{
			[DispId(5001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075EA RID: 30186
		// (get) Token: 0x060163B0 RID: 91056
		// (set) Token: 0x060163AF RID: 91055
		[DispId(5002)]
		string icon
		{
			[DispId(5002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075EB RID: 30187
		// (get) Token: 0x060163B2 RID: 91058
		// (set) Token: 0x060163B1 RID: 91057
		[DispId(5003)]
		string singleInstance
		{
			[DispId(5003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075EC RID: 30188
		// (get) Token: 0x060163B4 RID: 91060
		// (set) Token: 0x060163B3 RID: 91059
		[DispId(5005)]
		string minimizeButton
		{
			[DispId(5005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075ED RID: 30189
		// (get) Token: 0x060163B6 RID: 91062
		// (set) Token: 0x060163B5 RID: 91061
		[DispId(5006)]
		string maximizeButton
		{
			[DispId(5006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075EE RID: 30190
		// (get) Token: 0x060163B8 RID: 91064
		// (set) Token: 0x060163B7 RID: 91063
		[DispId(5007)]
		string border
		{
			[DispId(5007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075EF RID: 30191
		// (get) Token: 0x060163BA RID: 91066
		// (set) Token: 0x060163B9 RID: 91065
		[DispId(5008)]
		string borderStyle
		{
			[DispId(5008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075F0 RID: 30192
		// (get) Token: 0x060163BC RID: 91068
		// (set) Token: 0x060163BB RID: 91067
		[DispId(5009)]
		string sysMenu
		{
			[DispId(5009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075F1 RID: 30193
		// (get) Token: 0x060163BE RID: 91070
		// (set) Token: 0x060163BD RID: 91069
		[DispId(5010)]
		string caption
		{
			[DispId(5010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075F2 RID: 30194
		// (get) Token: 0x060163C0 RID: 91072
		// (set) Token: 0x060163BF RID: 91071
		[DispId(5011)]
		string windowState
		{
			[DispId(5011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075F3 RID: 30195
		// (get) Token: 0x060163C2 RID: 91074
		// (set) Token: 0x060163C1 RID: 91073
		[DispId(5012)]
		string showInTaskBar
		{
			[DispId(5012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075F4 RID: 30196
		// (get) Token: 0x060163C3 RID: 91075
		[DispId(5013)]
		string commandLine
		{
			[DispId(5013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
