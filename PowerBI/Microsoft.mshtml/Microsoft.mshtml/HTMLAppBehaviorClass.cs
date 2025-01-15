using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D08 RID: 3336
	[ClassInterface(0)]
	[TypeLibType(2)]
	[Guid("3050F5CB-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLAppBehaviorClass : DispHTMLAppBehavior, HTMLAppBehavior, IHTMLAppBehavior, IHTMLAppBehavior2
	{
		// Token: 0x060163F3 RID: 91123
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLAppBehaviorClass();

		// Token: 0x1700760D RID: 30221
		// (get) Token: 0x060163F5 RID: 91125
		// (set) Token: 0x060163F4 RID: 91124
		[DispId(5000)]
		public virtual extern string applicationName
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

		// Token: 0x1700760E RID: 30222
		// (get) Token: 0x060163F7 RID: 91127
		// (set) Token: 0x060163F6 RID: 91126
		[DispId(5001)]
		public virtual extern string version
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

		// Token: 0x1700760F RID: 30223
		// (get) Token: 0x060163F9 RID: 91129
		// (set) Token: 0x060163F8 RID: 91128
		[DispId(5002)]
		public virtual extern string icon
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

		// Token: 0x17007610 RID: 30224
		// (get) Token: 0x060163FB RID: 91131
		// (set) Token: 0x060163FA RID: 91130
		[DispId(5003)]
		public virtual extern string singleInstance
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

		// Token: 0x17007611 RID: 30225
		// (get) Token: 0x060163FD RID: 91133
		// (set) Token: 0x060163FC RID: 91132
		[DispId(5005)]
		public virtual extern string minimizeButton
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

		// Token: 0x17007612 RID: 30226
		// (get) Token: 0x060163FF RID: 91135
		// (set) Token: 0x060163FE RID: 91134
		[DispId(5006)]
		public virtual extern string maximizeButton
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

		// Token: 0x17007613 RID: 30227
		// (get) Token: 0x06016401 RID: 91137
		// (set) Token: 0x06016400 RID: 91136
		[DispId(5007)]
		public virtual extern string border
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

		// Token: 0x17007614 RID: 30228
		// (get) Token: 0x06016403 RID: 91139
		// (set) Token: 0x06016402 RID: 91138
		[DispId(5008)]
		public virtual extern string borderStyle
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

		// Token: 0x17007615 RID: 30229
		// (get) Token: 0x06016405 RID: 91141
		// (set) Token: 0x06016404 RID: 91140
		[DispId(5009)]
		public virtual extern string sysMenu
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

		// Token: 0x17007616 RID: 30230
		// (get) Token: 0x06016407 RID: 91143
		// (set) Token: 0x06016406 RID: 91142
		[DispId(5010)]
		public virtual extern string caption
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

		// Token: 0x17007617 RID: 30231
		// (get) Token: 0x06016409 RID: 91145
		// (set) Token: 0x06016408 RID: 91144
		[DispId(5011)]
		public virtual extern string windowState
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

		// Token: 0x17007618 RID: 30232
		// (get) Token: 0x0601640B RID: 91147
		// (set) Token: 0x0601640A RID: 91146
		[DispId(5012)]
		public virtual extern string showInTaskBar
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

		// Token: 0x17007619 RID: 30233
		// (get) Token: 0x0601640C RID: 91148
		[DispId(5013)]
		public virtual extern string commandLine
		{
			[DispId(5013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700761A RID: 30234
		// (get) Token: 0x0601640E RID: 91150
		// (set) Token: 0x0601640D RID: 91149
		[DispId(5014)]
		public virtual extern string contextMenu
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

		// Token: 0x1700761B RID: 30235
		// (get) Token: 0x06016410 RID: 91152
		// (set) Token: 0x0601640F RID: 91151
		[DispId(5015)]
		public virtual extern string innerBorder
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

		// Token: 0x1700761C RID: 30236
		// (get) Token: 0x06016412 RID: 91154
		// (set) Token: 0x06016411 RID: 91153
		[DispId(5016)]
		public virtual extern string scroll
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

		// Token: 0x1700761D RID: 30237
		// (get) Token: 0x06016414 RID: 91156
		// (set) Token: 0x06016413 RID: 91155
		[DispId(5017)]
		public virtual extern string scrollFlat
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

		// Token: 0x1700761E RID: 30238
		// (get) Token: 0x06016416 RID: 91158
		// (set) Token: 0x06016415 RID: 91157
		[DispId(5018)]
		public virtual extern string selection
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

		// Token: 0x1700761F RID: 30239
		// (get) Token: 0x06016418 RID: 91160
		// (set) Token: 0x06016417 RID: 91159
		public virtual extern string IHTMLAppBehavior_applicationName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007620 RID: 30240
		// (get) Token: 0x0601641A RID: 91162
		// (set) Token: 0x06016419 RID: 91161
		public virtual extern string IHTMLAppBehavior_version
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007621 RID: 30241
		// (get) Token: 0x0601641C RID: 91164
		// (set) Token: 0x0601641B RID: 91163
		public virtual extern string IHTMLAppBehavior_icon
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007622 RID: 30242
		// (get) Token: 0x0601641E RID: 91166
		// (set) Token: 0x0601641D RID: 91165
		public virtual extern string IHTMLAppBehavior_singleInstance
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007623 RID: 30243
		// (get) Token: 0x06016420 RID: 91168
		// (set) Token: 0x0601641F RID: 91167
		public virtual extern string IHTMLAppBehavior_minimizeButton
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007624 RID: 30244
		// (get) Token: 0x06016422 RID: 91170
		// (set) Token: 0x06016421 RID: 91169
		public virtual extern string IHTMLAppBehavior_maximizeButton
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007625 RID: 30245
		// (get) Token: 0x06016424 RID: 91172
		// (set) Token: 0x06016423 RID: 91171
		public virtual extern string IHTMLAppBehavior_border
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007626 RID: 30246
		// (get) Token: 0x06016426 RID: 91174
		// (set) Token: 0x06016425 RID: 91173
		public virtual extern string IHTMLAppBehavior_borderStyle
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007627 RID: 30247
		// (get) Token: 0x06016428 RID: 91176
		// (set) Token: 0x06016427 RID: 91175
		public virtual extern string IHTMLAppBehavior_sysMenu
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007628 RID: 30248
		// (get) Token: 0x0601642A RID: 91178
		// (set) Token: 0x06016429 RID: 91177
		public virtual extern string IHTMLAppBehavior_caption
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007629 RID: 30249
		// (get) Token: 0x0601642C RID: 91180
		// (set) Token: 0x0601642B RID: 91179
		public virtual extern string IHTMLAppBehavior_windowState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700762A RID: 30250
		// (get) Token: 0x0601642E RID: 91182
		// (set) Token: 0x0601642D RID: 91181
		public virtual extern string IHTMLAppBehavior_showInTaskBar
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700762B RID: 30251
		// (get) Token: 0x0601642F RID: 91183
		public virtual extern string IHTMLAppBehavior_commandLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700762C RID: 30252
		// (get) Token: 0x06016431 RID: 91185
		// (set) Token: 0x06016430 RID: 91184
		public virtual extern string IHTMLAppBehavior2_contextMenu
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700762D RID: 30253
		// (get) Token: 0x06016433 RID: 91187
		// (set) Token: 0x06016432 RID: 91186
		public virtual extern string IHTMLAppBehavior2_innerBorder
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700762E RID: 30254
		// (get) Token: 0x06016435 RID: 91189
		// (set) Token: 0x06016434 RID: 91188
		public virtual extern string IHTMLAppBehavior2_scroll
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700762F RID: 30255
		// (get) Token: 0x06016437 RID: 91191
		// (set) Token: 0x06016436 RID: 91190
		public virtual extern string IHTMLAppBehavior2_scrollFlat
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007630 RID: 30256
		// (get) Token: 0x06016439 RID: 91193
		// (set) Token: 0x06016438 RID: 91192
		public virtual extern string IHTMLAppBehavior2_selection
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
