using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000839 RID: 2105
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.DWebBridgeEvents\0\0")]
	[Guid("AE24FDAE-03C6-11D1-8B76-0080C744F389")]
	[TypeLibType(34)]
	[ComImport]
	public class ScriptletClass : IWebBridge, Scriptlet, DWebBridgeEvents_Event
	{
		// Token: 0x0600DEFA RID: 57082
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern ScriptletClass();

		// Token: 0x17004A0F RID: 18959
		// (get) Token: 0x0600DEFC RID: 57084
		// (set) Token: 0x0600DEFB RID: 57083
		[DispId(1)]
		public virtual extern string url
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

		// Token: 0x17004A10 RID: 18960
		// (get) Token: 0x0600DEFE RID: 57086
		// (set) Token: 0x0600DEFD RID: 57085
		[DispId(2)]
		public virtual extern bool Scrollbar
		{
			[DispId(2)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004A11 RID: 18961
		// (get) Token: 0x0600DF00 RID: 57088
		// (set) Token: 0x0600DEFF RID: 57087
		[DispId(3)]
		public virtual extern bool embed
		{
			[DispId(3)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(3)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004A12 RID: 18962
		// (get) Token: 0x0600DF01 RID: 57089
		[DispId(1152)]
		public virtual extern object @event
		{
			[DispId(1152)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004A13 RID: 18963
		// (get) Token: 0x0600DF02 RID: 57090
		[DispId(-525)]
		public virtual extern int readyState
		{
			[DispId(-525)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600DF03 RID: 57091
		[DispId(-552)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void AboutBox();

		// Token: 0x14001AF5 RID: 6901
		// (add) Token: 0x0600DF04 RID: 57092
		// (remove) Token: 0x0600DF05 RID: 57093
		public virtual extern event DWebBridgeEvents_onscriptleteventEventHandler onscriptletevent;

		// Token: 0x14001AF6 RID: 6902
		// (add) Token: 0x0600DF06 RID: 57094
		// (remove) Token: 0x0600DF07 RID: 57095
		public virtual extern event DWebBridgeEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14001AF7 RID: 6903
		// (add) Token: 0x0600DF08 RID: 57096
		// (remove) Token: 0x0600DF09 RID: 57097
		public virtual extern event DWebBridgeEvents_onclickEventHandler onclick;

		// Token: 0x14001AF8 RID: 6904
		// (add) Token: 0x0600DF0A RID: 57098
		// (remove) Token: 0x0600DF0B RID: 57099
		public virtual extern event DWebBridgeEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14001AF9 RID: 6905
		// (add) Token: 0x0600DF0C RID: 57100
		// (remove) Token: 0x0600DF0D RID: 57101
		public virtual extern event DWebBridgeEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14001AFA RID: 6906
		// (add) Token: 0x0600DF0E RID: 57102
		// (remove) Token: 0x0600DF0F RID: 57103
		public virtual extern event DWebBridgeEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14001AFB RID: 6907
		// (add) Token: 0x0600DF10 RID: 57104
		// (remove) Token: 0x0600DF11 RID: 57105
		public virtual extern event DWebBridgeEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14001AFC RID: 6908
		// (add) Token: 0x0600DF12 RID: 57106
		// (remove) Token: 0x0600DF13 RID: 57107
		public virtual extern event DWebBridgeEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14001AFD RID: 6909
		// (add) Token: 0x0600DF14 RID: 57108
		// (remove) Token: 0x0600DF15 RID: 57109
		public virtual extern event DWebBridgeEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14001AFE RID: 6910
		// (add) Token: 0x0600DF16 RID: 57110
		// (remove) Token: 0x0600DF17 RID: 57111
		public virtual extern event DWebBridgeEvents_onmouseupEventHandler onmouseup;
	}
}
