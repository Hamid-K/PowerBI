using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007B0 RID: 1968
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F48A-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class CEventObjClass : DispCEventObj, CEventObj, IHTMLEventObj, IHTMLEventObj2, IHTMLEventObj3, IHTMLEventObj4
	{
		// Token: 0x0600D598 RID: 54680
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern CEventObjClass();

		// Token: 0x170046C1 RID: 18113
		// (get) Token: 0x0600D59A RID: 54682
		// (set) Token: 0x0600D599 RID: 54681
		[DispId(1007)]
		public virtual extern object returnValue
		{
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170046C2 RID: 18114
		// (get) Token: 0x0600D59C RID: 54684
		// (set) Token: 0x0600D59B RID: 54683
		[DispId(1008)]
		public virtual extern bool cancelBubble
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046C3 RID: 18115
		// (get) Token: 0x0600D59E RID: 54686
		// (set) Token: 0x0600D59D RID: 54685
		[DispId(1011)]
		public virtual extern int keyCode
		{
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600D59F RID: 54687
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600D5A0 RID: 54688
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600D5A1 RID: 54689
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170046C4 RID: 18116
		// (get) Token: 0x0600D5A3 RID: 54691
		// (set) Token: 0x0600D5A2 RID: 54690
		[DispId(1027)]
		public virtual extern string propertyName
		{
			[DispId(1027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170046C5 RID: 18117
		// (get) Token: 0x0600D5A5 RID: 54693
		// (set) Token: 0x0600D5A4 RID: 54692
		[DispId(1031)]
		public virtual extern IHTMLBookmarkCollection bookmarks
		{
			[DispId(1031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[DispId(1031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			set;
		}

		// Token: 0x170046C6 RID: 18118
		// (get) Token: 0x0600D5A7 RID: 54695
		// (set) Token: 0x0600D5A6 RID: 54694
		[DispId(1032)]
		public virtual extern object recordset
		{
			[DispId(1032)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
			[DispId(1032)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(26)]
			set;
		}

		// Token: 0x170046C7 RID: 18119
		// (get) Token: 0x0600D5A9 RID: 54697
		// (set) Token: 0x0600D5A8 RID: 54696
		[DispId(1033)]
		public virtual extern string dataFld
		{
			[DispId(1033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170046C8 RID: 18120
		// (get) Token: 0x0600D5AB RID: 54699
		// (set) Token: 0x0600D5AA RID: 54698
		[DispId(1034)]
		public virtual extern IHTMLElementCollection boundElements
		{
			[DispId(1034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[DispId(1034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			set;
		}

		// Token: 0x170046C9 RID: 18121
		// (get) Token: 0x0600D5AD RID: 54701
		// (set) Token: 0x0600D5AC RID: 54700
		[DispId(1035)]
		public virtual extern bool repeat
		{
			[DispId(1035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046CA RID: 18122
		// (get) Token: 0x0600D5AF RID: 54703
		// (set) Token: 0x0600D5AE RID: 54702
		[DispId(1036)]
		public virtual extern string srcUrn
		{
			[DispId(1036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170046CB RID: 18123
		// (get) Token: 0x0600D5B1 RID: 54705
		// (set) Token: 0x0600D5B0 RID: 54704
		[DispId(1001)]
		public virtual extern IHTMLElement srcElement
		{
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			set;
		}

		// Token: 0x170046CC RID: 18124
		// (get) Token: 0x0600D5B3 RID: 54707
		// (set) Token: 0x0600D5B2 RID: 54706
		[DispId(1002)]
		public virtual extern bool altKey
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046CD RID: 18125
		// (get) Token: 0x0600D5B5 RID: 54709
		// (set) Token: 0x0600D5B4 RID: 54708
		[DispId(1003)]
		public virtual extern bool ctrlKey
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046CE RID: 18126
		// (get) Token: 0x0600D5B7 RID: 54711
		// (set) Token: 0x0600D5B6 RID: 54710
		[DispId(1004)]
		public virtual extern bool shiftKey
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046CF RID: 18127
		// (get) Token: 0x0600D5B9 RID: 54713
		// (set) Token: 0x0600D5B8 RID: 54712
		[DispId(1009)]
		public virtual extern IHTMLElement fromElement
		{
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			set;
		}

		// Token: 0x170046D0 RID: 18128
		// (get) Token: 0x0600D5BB RID: 54715
		// (set) Token: 0x0600D5BA RID: 54714
		[DispId(1010)]
		public virtual extern IHTMLElement toElement
		{
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			set;
		}

		// Token: 0x170046D1 RID: 18129
		// (get) Token: 0x0600D5BD RID: 54717
		// (set) Token: 0x0600D5BC RID: 54716
		[DispId(1012)]
		public virtual extern int button
		{
			[DispId(1012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046D2 RID: 18130
		// (get) Token: 0x0600D5BF RID: 54719
		// (set) Token: 0x0600D5BE RID: 54718
		[DispId(1013)]
		public virtual extern string type
		{
			[DispId(1013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170046D3 RID: 18131
		// (get) Token: 0x0600D5C1 RID: 54721
		// (set) Token: 0x0600D5C0 RID: 54720
		[DispId(1014)]
		public virtual extern string qualifier
		{
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170046D4 RID: 18132
		// (get) Token: 0x0600D5C3 RID: 54723
		// (set) Token: 0x0600D5C2 RID: 54722
		[DispId(1015)]
		public virtual extern int reason
		{
			[DispId(1015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046D5 RID: 18133
		// (get) Token: 0x0600D5C5 RID: 54725
		// (set) Token: 0x0600D5C4 RID: 54724
		[DispId(1005)]
		public virtual extern int x
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046D6 RID: 18134
		// (get) Token: 0x0600D5C7 RID: 54727
		// (set) Token: 0x0600D5C6 RID: 54726
		[DispId(1006)]
		public virtual extern int y
		{
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046D7 RID: 18135
		// (get) Token: 0x0600D5C9 RID: 54729
		// (set) Token: 0x0600D5C8 RID: 54728
		[DispId(1020)]
		public virtual extern int clientX
		{
			[DispId(1020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046D8 RID: 18136
		// (get) Token: 0x0600D5CB RID: 54731
		// (set) Token: 0x0600D5CA RID: 54730
		[DispId(1021)]
		public virtual extern int clientY
		{
			[DispId(1021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046D9 RID: 18137
		// (get) Token: 0x0600D5CD RID: 54733
		// (set) Token: 0x0600D5CC RID: 54732
		[DispId(1022)]
		public virtual extern int offsetX
		{
			[DispId(1022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046DA RID: 18138
		// (get) Token: 0x0600D5CF RID: 54735
		// (set) Token: 0x0600D5CE RID: 54734
		[DispId(1023)]
		public virtual extern int offsetY
		{
			[DispId(1023)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1023)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046DB RID: 18139
		// (get) Token: 0x0600D5D1 RID: 54737
		// (set) Token: 0x0600D5D0 RID: 54736
		[DispId(1024)]
		public virtual extern int screenX
		{
			[DispId(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046DC RID: 18140
		// (get) Token: 0x0600D5D3 RID: 54739
		// (set) Token: 0x0600D5D2 RID: 54738
		[DispId(1025)]
		public virtual extern int screenY
		{
			[DispId(1025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046DD RID: 18141
		// (get) Token: 0x0600D5D5 RID: 54741
		// (set) Token: 0x0600D5D4 RID: 54740
		[DispId(1026)]
		public virtual extern object srcFilter
		{
			[DispId(1026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
			[DispId(1026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(26)]
			set;
		}

		// Token: 0x170046DE RID: 18142
		// (get) Token: 0x0600D5D6 RID: 54742
		[DispId(1037)]
		public virtual extern IHTMLDataTransfer dataTransfer
		{
			[DispId(1037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170046DF RID: 18143
		// (get) Token: 0x0600D5D7 RID: 54743
		[DispId(1038)]
		public virtual extern bool contentOverflow
		{
			[DispId(1038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046E0 RID: 18144
		// (get) Token: 0x0600D5D9 RID: 54745
		// (set) Token: 0x0600D5D8 RID: 54744
		[DispId(1039)]
		public virtual extern bool shiftLeft
		{
			[DispId(1039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046E1 RID: 18145
		// (get) Token: 0x0600D5DB RID: 54747
		// (set) Token: 0x0600D5DA RID: 54746
		[DispId(1040)]
		public virtual extern bool altLeft
		{
			[DispId(1040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046E2 RID: 18146
		// (get) Token: 0x0600D5DD RID: 54749
		// (set) Token: 0x0600D5DC RID: 54748
		[DispId(1041)]
		public virtual extern bool ctrlLeft
		{
			[DispId(1041)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1041)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046E3 RID: 18147
		// (get) Token: 0x0600D5DE RID: 54750
		[ComAliasName("mshtml.LONG_PTR")]
		[DispId(1042)]
		public virtual extern int imeCompositionChange
		{
			[DispId(1042)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x170046E4 RID: 18148
		// (get) Token: 0x0600D5DF RID: 54751
		[DispId(1043)]
		[ComAliasName("mshtml.LONG_PTR")]
		public virtual extern int imeNotifyCommand
		{
			[TypeLibFunc(1089)]
			[DispId(1043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x170046E5 RID: 18149
		// (get) Token: 0x0600D5E0 RID: 54752
		[DispId(1044)]
		[ComAliasName("mshtml.LONG_PTR")]
		public virtual extern int imeNotifyData
		{
			[TypeLibFunc(1089)]
			[DispId(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x170046E6 RID: 18150
		// (get) Token: 0x0600D5E1 RID: 54753
		[ComAliasName("mshtml.LONG_PTR")]
		[DispId(1046)]
		public virtual extern int imeRequest
		{
			[TypeLibFunc(1089)]
			[DispId(1046)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x170046E7 RID: 18151
		// (get) Token: 0x0600D5E2 RID: 54754
		[ComAliasName("mshtml.LONG_PTR")]
		[DispId(1047)]
		public virtual extern int imeRequestData
		{
			[DispId(1047)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x170046E8 RID: 18152
		// (get) Token: 0x0600D5E3 RID: 54755
		[ComAliasName("mshtml.LONG_PTR")]
		[DispId(1045)]
		public virtual extern int keyboardLayout
		{
			[DispId(1045)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x170046E9 RID: 18153
		// (get) Token: 0x0600D5E4 RID: 54756
		[DispId(1048)]
		public virtual extern int behaviorCookie
		{
			[DispId(1048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046EA RID: 18154
		// (get) Token: 0x0600D5E5 RID: 54757
		[DispId(1049)]
		public virtual extern int behaviorPart
		{
			[DispId(1049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046EB RID: 18155
		// (get) Token: 0x0600D5E6 RID: 54758
		[DispId(1050)]
		public virtual extern string nextPage
		{
			[DispId(1050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170046EC RID: 18156
		// (get) Token: 0x0600D5E7 RID: 54759
		[DispId(1051)]
		public virtual extern int wheelDelta
		{
			[DispId(1051)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046ED RID: 18157
		// (get) Token: 0x0600D5E8 RID: 54760
		public virtual extern IHTMLElement IHTMLEventObj_srcElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170046EE RID: 18158
		// (get) Token: 0x0600D5E9 RID: 54761
		public virtual extern bool IHTMLEventObj_altKey
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046EF RID: 18159
		// (get) Token: 0x0600D5EA RID: 54762
		public virtual extern bool IHTMLEventObj_ctrlKey
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046F0 RID: 18160
		// (get) Token: 0x0600D5EB RID: 54763
		public virtual extern bool IHTMLEventObj_shiftKey
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046F1 RID: 18161
		// (get) Token: 0x0600D5ED RID: 54765
		// (set) Token: 0x0600D5EC RID: 54764
		public virtual extern object IHTMLEventObj_returnValue
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170046F2 RID: 18162
		// (get) Token: 0x0600D5EF RID: 54767
		// (set) Token: 0x0600D5EE RID: 54766
		public virtual extern bool IHTMLEventObj_cancelBubble
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170046F3 RID: 18163
		// (get) Token: 0x0600D5F0 RID: 54768
		public virtual extern IHTMLElement IHTMLEventObj_fromElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170046F4 RID: 18164
		// (get) Token: 0x0600D5F1 RID: 54769
		public virtual extern IHTMLElement IHTMLEventObj_toElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170046F5 RID: 18165
		// (get) Token: 0x0600D5F3 RID: 54771
		// (set) Token: 0x0600D5F2 RID: 54770
		public virtual extern int IHTMLEventObj_keyCode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170046F6 RID: 18166
		// (get) Token: 0x0600D5F4 RID: 54772
		public virtual extern int IHTMLEventObj_button
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046F7 RID: 18167
		// (get) Token: 0x0600D5F5 RID: 54773
		public virtual extern string IHTMLEventObj_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170046F8 RID: 18168
		// (get) Token: 0x0600D5F6 RID: 54774
		public virtual extern string IHTMLEventObj_qualifier
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170046F9 RID: 18169
		// (get) Token: 0x0600D5F7 RID: 54775
		public virtual extern int IHTMLEventObj_reason
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046FA RID: 18170
		// (get) Token: 0x0600D5F8 RID: 54776
		public virtual extern int IHTMLEventObj_x
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046FB RID: 18171
		// (get) Token: 0x0600D5F9 RID: 54777
		public virtual extern int IHTMLEventObj_y
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046FC RID: 18172
		// (get) Token: 0x0600D5FA RID: 54778
		public virtual extern int IHTMLEventObj_clientX
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046FD RID: 18173
		// (get) Token: 0x0600D5FB RID: 54779
		public virtual extern int IHTMLEventObj_clientY
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046FE RID: 18174
		// (get) Token: 0x0600D5FC RID: 54780
		public virtual extern int IHTMLEventObj_offsetX
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046FF RID: 18175
		// (get) Token: 0x0600D5FD RID: 54781
		public virtual extern int IHTMLEventObj_offsetY
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004700 RID: 18176
		// (get) Token: 0x0600D5FE RID: 54782
		public virtual extern int IHTMLEventObj_screenX
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004701 RID: 18177
		// (get) Token: 0x0600D5FF RID: 54783
		public virtual extern int IHTMLEventObj_screenY
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004702 RID: 18178
		// (get) Token: 0x0600D600 RID: 54784
		public virtual extern object IHTMLEventObj_srcFilter
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600D601 RID: 54785
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLEventObj2_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600D602 RID: 54786
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLEventObj2_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600D603 RID: 54787
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLEventObj2_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17004703 RID: 18179
		// (get) Token: 0x0600D605 RID: 54789
		// (set) Token: 0x0600D604 RID: 54788
		public virtual extern string IHTMLEventObj2_propertyName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004704 RID: 18180
		// (get) Token: 0x0600D607 RID: 54791
		// (set) Token: 0x0600D606 RID: 54790
		public virtual extern IHTMLBookmarkCollection IHTMLEventObj2_bookmarks
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			[param: In]
			set;
		}

		// Token: 0x17004705 RID: 18181
		// (get) Token: 0x0600D609 RID: 54793
		// (set) Token: 0x0600D608 RID: 54792
		public virtual extern object IHTMLEventObj2_recordset
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(26)]
			[param: In]
			set;
		}

		// Token: 0x17004706 RID: 18182
		// (get) Token: 0x0600D60B RID: 54795
		// (set) Token: 0x0600D60A RID: 54794
		public virtual extern string IHTMLEventObj2_dataFld
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004707 RID: 18183
		// (get) Token: 0x0600D60D RID: 54797
		// (set) Token: 0x0600D60C RID: 54796
		public virtual extern IHTMLElementCollection IHTMLEventObj2_boundElements
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			[param: In]
			set;
		}

		// Token: 0x17004708 RID: 18184
		// (get) Token: 0x0600D60F RID: 54799
		// (set) Token: 0x0600D60E RID: 54798
		public virtual extern bool IHTMLEventObj2_repeat
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004709 RID: 18185
		// (get) Token: 0x0600D611 RID: 54801
		// (set) Token: 0x0600D610 RID: 54800
		public virtual extern string IHTMLEventObj2_srcUrn
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700470A RID: 18186
		// (get) Token: 0x0600D613 RID: 54803
		// (set) Token: 0x0600D612 RID: 54802
		public virtual extern IHTMLElement IHTMLEventObj2_srcElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			[param: In]
			set;
		}

		// Token: 0x1700470B RID: 18187
		// (get) Token: 0x0600D615 RID: 54805
		// (set) Token: 0x0600D614 RID: 54804
		public virtual extern bool IHTMLEventObj2_altKey
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700470C RID: 18188
		// (get) Token: 0x0600D617 RID: 54807
		// (set) Token: 0x0600D616 RID: 54806
		public virtual extern bool IHTMLEventObj2_ctrlKey
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700470D RID: 18189
		// (get) Token: 0x0600D619 RID: 54809
		// (set) Token: 0x0600D618 RID: 54808
		public virtual extern bool IHTMLEventObj2_shiftKey
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700470E RID: 18190
		// (get) Token: 0x0600D61B RID: 54811
		// (set) Token: 0x0600D61A RID: 54810
		public virtual extern IHTMLElement IHTMLEventObj2_fromElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			[param: In]
			set;
		}

		// Token: 0x1700470F RID: 18191
		// (get) Token: 0x0600D61D RID: 54813
		// (set) Token: 0x0600D61C RID: 54812
		public virtual extern IHTMLElement IHTMLEventObj2_toElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			[param: In]
			set;
		}

		// Token: 0x17004710 RID: 18192
		// (get) Token: 0x0600D61F RID: 54815
		// (set) Token: 0x0600D61E RID: 54814
		public virtual extern int IHTMLEventObj2_button
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004711 RID: 18193
		// (get) Token: 0x0600D621 RID: 54817
		// (set) Token: 0x0600D620 RID: 54816
		public virtual extern string IHTMLEventObj2_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004712 RID: 18194
		// (get) Token: 0x0600D623 RID: 54819
		// (set) Token: 0x0600D622 RID: 54818
		public virtual extern string IHTMLEventObj2_qualifier
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004713 RID: 18195
		// (get) Token: 0x0600D625 RID: 54821
		// (set) Token: 0x0600D624 RID: 54820
		public virtual extern int IHTMLEventObj2_reason
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004714 RID: 18196
		// (get) Token: 0x0600D627 RID: 54823
		// (set) Token: 0x0600D626 RID: 54822
		public virtual extern int IHTMLEventObj2_x
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004715 RID: 18197
		// (get) Token: 0x0600D629 RID: 54825
		// (set) Token: 0x0600D628 RID: 54824
		public virtual extern int IHTMLEventObj2_y
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004716 RID: 18198
		// (get) Token: 0x0600D62B RID: 54827
		// (set) Token: 0x0600D62A RID: 54826
		public virtual extern int IHTMLEventObj2_clientX
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004717 RID: 18199
		// (get) Token: 0x0600D62D RID: 54829
		// (set) Token: 0x0600D62C RID: 54828
		public virtual extern int IHTMLEventObj2_clientY
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004718 RID: 18200
		// (get) Token: 0x0600D62F RID: 54831
		// (set) Token: 0x0600D62E RID: 54830
		public virtual extern int IHTMLEventObj2_offsetX
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004719 RID: 18201
		// (get) Token: 0x0600D631 RID: 54833
		// (set) Token: 0x0600D630 RID: 54832
		public virtual extern int IHTMLEventObj2_offsetY
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700471A RID: 18202
		// (get) Token: 0x0600D633 RID: 54835
		// (set) Token: 0x0600D632 RID: 54834
		public virtual extern int IHTMLEventObj2_screenX
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700471B RID: 18203
		// (get) Token: 0x0600D635 RID: 54837
		// (set) Token: 0x0600D634 RID: 54836
		public virtual extern int IHTMLEventObj2_screenY
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700471C RID: 18204
		// (get) Token: 0x0600D637 RID: 54839
		// (set) Token: 0x0600D636 RID: 54838
		public virtual extern object IHTMLEventObj2_srcFilter
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(26)]
			[param: In]
			set;
		}

		// Token: 0x1700471D RID: 18205
		// (get) Token: 0x0600D638 RID: 54840
		public virtual extern IHTMLDataTransfer IHTMLEventObj2_dataTransfer
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700471E RID: 18206
		// (get) Token: 0x0600D639 RID: 54841
		public virtual extern bool IHTMLEventObj3_contentOverflow
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700471F RID: 18207
		// (get) Token: 0x0600D63B RID: 54843
		// (set) Token: 0x0600D63A RID: 54842
		public virtual extern bool IHTMLEventObj3_shiftLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004720 RID: 18208
		// (get) Token: 0x0600D63D RID: 54845
		// (set) Token: 0x0600D63C RID: 54844
		public virtual extern bool IHTMLEventObj3_altLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004721 RID: 18209
		// (get) Token: 0x0600D63F RID: 54847
		// (set) Token: 0x0600D63E RID: 54846
		public virtual extern bool IHTMLEventObj3_ctrlLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004722 RID: 18210
		// (get) Token: 0x0600D640 RID: 54848
		[ComAliasName("mshtml.LONG_PTR")]
		public virtual extern int IHTMLEventObj3_imeCompositionChange
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x17004723 RID: 18211
		// (get) Token: 0x0600D641 RID: 54849
		[ComAliasName("mshtml.LONG_PTR")]
		public virtual extern int IHTMLEventObj3_imeNotifyCommand
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x17004724 RID: 18212
		// (get) Token: 0x0600D642 RID: 54850
		[ComAliasName("mshtml.LONG_PTR")]
		public virtual extern int IHTMLEventObj3_imeNotifyData
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x17004725 RID: 18213
		// (get) Token: 0x0600D643 RID: 54851
		[ComAliasName("mshtml.LONG_PTR")]
		public virtual extern int IHTMLEventObj3_imeRequest
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x17004726 RID: 18214
		// (get) Token: 0x0600D644 RID: 54852
		[ComAliasName("mshtml.LONG_PTR")]
		public virtual extern int IHTMLEventObj3_imeRequestData
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x17004727 RID: 18215
		// (get) Token: 0x0600D645 RID: 54853
		[ComAliasName("mshtml.LONG_PTR")]
		public virtual extern int IHTMLEventObj3_keyboardLayout
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x17004728 RID: 18216
		// (get) Token: 0x0600D646 RID: 54854
		public virtual extern int IHTMLEventObj3_behaviorCookie
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004729 RID: 18217
		// (get) Token: 0x0600D647 RID: 54855
		public virtual extern int IHTMLEventObj3_behaviorPart
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700472A RID: 18218
		// (get) Token: 0x0600D648 RID: 54856
		public virtual extern string IHTMLEventObj3_nextPage
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700472B RID: 18219
		// (get) Token: 0x0600D649 RID: 54857
		public virtual extern int IHTMLEventObj4_wheelDelta
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
