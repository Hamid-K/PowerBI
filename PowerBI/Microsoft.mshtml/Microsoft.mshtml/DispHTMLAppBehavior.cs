using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D07 RID: 3335
	[InterfaceType(2)]
	[Guid("3050F57C-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLAppBehavior
	{
		// Token: 0x170075FB RID: 30203
		// (get) Token: 0x060163D1 RID: 91089
		// (set) Token: 0x060163D0 RID: 91088
		[DispId(5000)]
		string applicationName
		{
			[DispId(5000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170075FC RID: 30204
		// (get) Token: 0x060163D3 RID: 91091
		// (set) Token: 0x060163D2 RID: 91090
		[DispId(5001)]
		string version
		{
			[DispId(5001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170075FD RID: 30205
		// (get) Token: 0x060163D5 RID: 91093
		// (set) Token: 0x060163D4 RID: 91092
		[DispId(5002)]
		string icon
		{
			[DispId(5002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170075FE RID: 30206
		// (get) Token: 0x060163D7 RID: 91095
		// (set) Token: 0x060163D6 RID: 91094
		[DispId(5003)]
		string singleInstance
		{
			[DispId(5003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170075FF RID: 30207
		// (get) Token: 0x060163D9 RID: 91097
		// (set) Token: 0x060163D8 RID: 91096
		[DispId(5005)]
		string minimizeButton
		{
			[DispId(5005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007600 RID: 30208
		// (get) Token: 0x060163DB RID: 91099
		// (set) Token: 0x060163DA RID: 91098
		[DispId(5006)]
		string maximizeButton
		{
			[DispId(5006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007601 RID: 30209
		// (get) Token: 0x060163DD RID: 91101
		// (set) Token: 0x060163DC RID: 91100
		[DispId(5007)]
		string border
		{
			[DispId(5007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007602 RID: 30210
		// (get) Token: 0x060163DF RID: 91103
		// (set) Token: 0x060163DE RID: 91102
		[DispId(5008)]
		string borderStyle
		{
			[DispId(5008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007603 RID: 30211
		// (get) Token: 0x060163E1 RID: 91105
		// (set) Token: 0x060163E0 RID: 91104
		[DispId(5009)]
		string sysMenu
		{
			[DispId(5009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007604 RID: 30212
		// (get) Token: 0x060163E3 RID: 91107
		// (set) Token: 0x060163E2 RID: 91106
		[DispId(5010)]
		string caption
		{
			[DispId(5010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007605 RID: 30213
		// (get) Token: 0x060163E5 RID: 91109
		// (set) Token: 0x060163E4 RID: 91108
		[DispId(5011)]
		string windowState
		{
			[DispId(5011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007606 RID: 30214
		// (get) Token: 0x060163E7 RID: 91111
		// (set) Token: 0x060163E6 RID: 91110
		[DispId(5012)]
		string showInTaskBar
		{
			[DispId(5012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007607 RID: 30215
		// (get) Token: 0x060163E8 RID: 91112
		[DispId(5013)]
		string commandLine
		{
			[DispId(5013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007608 RID: 30216
		// (get) Token: 0x060163EA RID: 91114
		// (set) Token: 0x060163E9 RID: 91113
		[DispId(5014)]
		string contextMenu
		{
			[DispId(5014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007609 RID: 30217
		// (get) Token: 0x060163EC RID: 91116
		// (set) Token: 0x060163EB RID: 91115
		[DispId(5015)]
		string innerBorder
		{
			[DispId(5015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700760A RID: 30218
		// (get) Token: 0x060163EE RID: 91118
		// (set) Token: 0x060163ED RID: 91117
		[DispId(5016)]
		string scroll
		{
			[DispId(5016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700760B RID: 30219
		// (get) Token: 0x060163F0 RID: 91120
		// (set) Token: 0x060163EF RID: 91119
		[DispId(5017)]
		string scrollFlat
		{
			[DispId(5017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700760C RID: 30220
		// (get) Token: 0x060163F2 RID: 91122
		// (set) Token: 0x060163F1 RID: 91121
		[DispId(5018)]
		string selection
		{
			[DispId(5018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}
	}
}
