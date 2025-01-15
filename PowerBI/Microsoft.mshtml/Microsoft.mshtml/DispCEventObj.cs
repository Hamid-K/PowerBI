using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007AF RID: 1967
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F558-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface DispCEventObj
	{
		// Token: 0x17004695 RID: 18069
		// (get) Token: 0x0600D54A RID: 54602
		// (set) Token: 0x0600D549 RID: 54601
		[DispId(1007)]
		object returnValue
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

		// Token: 0x17004696 RID: 18070
		// (get) Token: 0x0600D54C RID: 54604
		// (set) Token: 0x0600D54B RID: 54603
		[DispId(1008)]
		bool cancelBubble
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17004697 RID: 18071
		// (get) Token: 0x0600D54E RID: 54606
		// (set) Token: 0x0600D54D RID: 54605
		[DispId(1011)]
		int keyCode
		{
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600D54F RID: 54607
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600D550 RID: 54608
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600D551 RID: 54609
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17004698 RID: 18072
		// (get) Token: 0x0600D553 RID: 54611
		// (set) Token: 0x0600D552 RID: 54610
		[DispId(1027)]
		string propertyName
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

		// Token: 0x17004699 RID: 18073
		// (get) Token: 0x0600D555 RID: 54613
		// (set) Token: 0x0600D554 RID: 54612
		[DispId(1031)]
		IHTMLBookmarkCollection bookmarks
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

		// Token: 0x1700469A RID: 18074
		// (get) Token: 0x0600D557 RID: 54615
		// (set) Token: 0x0600D556 RID: 54614
		[DispId(1032)]
		object recordset
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

		// Token: 0x1700469B RID: 18075
		// (get) Token: 0x0600D559 RID: 54617
		// (set) Token: 0x0600D558 RID: 54616
		[DispId(1033)]
		string dataFld
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

		// Token: 0x1700469C RID: 18076
		// (get) Token: 0x0600D55B RID: 54619
		// (set) Token: 0x0600D55A RID: 54618
		[DispId(1034)]
		IHTMLElementCollection boundElements
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

		// Token: 0x1700469D RID: 18077
		// (get) Token: 0x0600D55D RID: 54621
		// (set) Token: 0x0600D55C RID: 54620
		[DispId(1035)]
		bool repeat
		{
			[DispId(1035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700469E RID: 18078
		// (get) Token: 0x0600D55F RID: 54623
		// (set) Token: 0x0600D55E RID: 54622
		[DispId(1036)]
		string srcUrn
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

		// Token: 0x1700469F RID: 18079
		// (get) Token: 0x0600D561 RID: 54625
		// (set) Token: 0x0600D560 RID: 54624
		[DispId(1001)]
		IHTMLElement srcElement
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

		// Token: 0x170046A0 RID: 18080
		// (get) Token: 0x0600D563 RID: 54627
		// (set) Token: 0x0600D562 RID: 54626
		[DispId(1002)]
		bool altKey
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046A1 RID: 18081
		// (get) Token: 0x0600D565 RID: 54629
		// (set) Token: 0x0600D564 RID: 54628
		[DispId(1003)]
		bool ctrlKey
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046A2 RID: 18082
		// (get) Token: 0x0600D567 RID: 54631
		// (set) Token: 0x0600D566 RID: 54630
		[DispId(1004)]
		bool shiftKey
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046A3 RID: 18083
		// (get) Token: 0x0600D569 RID: 54633
		// (set) Token: 0x0600D568 RID: 54632
		[DispId(1009)]
		IHTMLElement fromElement
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

		// Token: 0x170046A4 RID: 18084
		// (get) Token: 0x0600D56B RID: 54635
		// (set) Token: 0x0600D56A RID: 54634
		[DispId(1010)]
		IHTMLElement toElement
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

		// Token: 0x170046A5 RID: 18085
		// (get) Token: 0x0600D56D RID: 54637
		// (set) Token: 0x0600D56C RID: 54636
		[DispId(1012)]
		int button
		{
			[DispId(1012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046A6 RID: 18086
		// (get) Token: 0x0600D56F RID: 54639
		// (set) Token: 0x0600D56E RID: 54638
		[DispId(1013)]
		string type
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

		// Token: 0x170046A7 RID: 18087
		// (get) Token: 0x0600D571 RID: 54641
		// (set) Token: 0x0600D570 RID: 54640
		[DispId(1014)]
		string qualifier
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

		// Token: 0x170046A8 RID: 18088
		// (get) Token: 0x0600D573 RID: 54643
		// (set) Token: 0x0600D572 RID: 54642
		[DispId(1015)]
		int reason
		{
			[DispId(1015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046A9 RID: 18089
		// (get) Token: 0x0600D575 RID: 54645
		// (set) Token: 0x0600D574 RID: 54644
		[DispId(1005)]
		int x
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046AA RID: 18090
		// (get) Token: 0x0600D577 RID: 54647
		// (set) Token: 0x0600D576 RID: 54646
		[DispId(1006)]
		int y
		{
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046AB RID: 18091
		// (get) Token: 0x0600D579 RID: 54649
		// (set) Token: 0x0600D578 RID: 54648
		[DispId(1020)]
		int clientX
		{
			[DispId(1020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046AC RID: 18092
		// (get) Token: 0x0600D57B RID: 54651
		// (set) Token: 0x0600D57A RID: 54650
		[DispId(1021)]
		int clientY
		{
			[DispId(1021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046AD RID: 18093
		// (get) Token: 0x0600D57D RID: 54653
		// (set) Token: 0x0600D57C RID: 54652
		[DispId(1022)]
		int offsetX
		{
			[DispId(1022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046AE RID: 18094
		// (get) Token: 0x0600D57F RID: 54655
		// (set) Token: 0x0600D57E RID: 54654
		[DispId(1023)]
		int offsetY
		{
			[DispId(1023)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1023)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046AF RID: 18095
		// (get) Token: 0x0600D581 RID: 54657
		// (set) Token: 0x0600D580 RID: 54656
		[DispId(1024)]
		int screenX
		{
			[DispId(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046B0 RID: 18096
		// (get) Token: 0x0600D583 RID: 54659
		// (set) Token: 0x0600D582 RID: 54658
		[DispId(1025)]
		int screenY
		{
			[DispId(1025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046B1 RID: 18097
		// (get) Token: 0x0600D585 RID: 54661
		// (set) Token: 0x0600D584 RID: 54660
		[DispId(1026)]
		object srcFilter
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

		// Token: 0x170046B2 RID: 18098
		// (get) Token: 0x0600D586 RID: 54662
		[DispId(1037)]
		IHTMLDataTransfer dataTransfer
		{
			[DispId(1037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170046B3 RID: 18099
		// (get) Token: 0x0600D587 RID: 54663
		[DispId(1038)]
		bool contentOverflow
		{
			[DispId(1038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046B4 RID: 18100
		// (get) Token: 0x0600D589 RID: 54665
		// (set) Token: 0x0600D588 RID: 54664
		[DispId(1039)]
		bool shiftLeft
		{
			[DispId(1039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046B5 RID: 18101
		// (get) Token: 0x0600D58B RID: 54667
		// (set) Token: 0x0600D58A RID: 54666
		[DispId(1040)]
		bool altLeft
		{
			[DispId(1040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046B6 RID: 18102
		// (get) Token: 0x0600D58D RID: 54669
		// (set) Token: 0x0600D58C RID: 54668
		[DispId(1041)]
		bool ctrlLeft
		{
			[DispId(1041)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1041)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170046B7 RID: 18103
		// (get) Token: 0x0600D58E RID: 54670
		[ComAliasName("mshtml.LONG_PTR")]
		[DispId(1042)]
		int imeCompositionChange
		{
			[TypeLibFunc(1089)]
			[DispId(1042)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x170046B8 RID: 18104
		// (get) Token: 0x0600D58F RID: 54671
		[ComAliasName("mshtml.LONG_PTR")]
		[DispId(1043)]
		int imeNotifyCommand
		{
			[DispId(1043)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x170046B9 RID: 18105
		// (get) Token: 0x0600D590 RID: 54672
		[ComAliasName("mshtml.LONG_PTR")]
		[DispId(1044)]
		int imeNotifyData
		{
			[TypeLibFunc(1089)]
			[DispId(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x170046BA RID: 18106
		// (get) Token: 0x0600D591 RID: 54673
		[ComAliasName("mshtml.LONG_PTR")]
		[DispId(1046)]
		int imeRequest
		{
			[DispId(1046)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x170046BB RID: 18107
		// (get) Token: 0x0600D592 RID: 54674
		[DispId(1047)]
		[ComAliasName("mshtml.LONG_PTR")]
		int imeRequestData
		{
			[DispId(1047)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x170046BC RID: 18108
		// (get) Token: 0x0600D593 RID: 54675
		[DispId(1045)]
		[ComAliasName("mshtml.LONG_PTR")]
		int keyboardLayout
		{
			[TypeLibFunc(1089)]
			[DispId(1045)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x170046BD RID: 18109
		// (get) Token: 0x0600D594 RID: 54676
		[DispId(1048)]
		int behaviorCookie
		{
			[DispId(1048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046BE RID: 18110
		// (get) Token: 0x0600D595 RID: 54677
		[DispId(1049)]
		int behaviorPart
		{
			[DispId(1049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170046BF RID: 18111
		// (get) Token: 0x0600D596 RID: 54678
		[DispId(1050)]
		string nextPage
		{
			[DispId(1050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170046C0 RID: 18112
		// (get) Token: 0x0600D597 RID: 54679
		[DispId(1051)]
		int wheelDelta
		{
			[DispId(1051)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}
	}
}
