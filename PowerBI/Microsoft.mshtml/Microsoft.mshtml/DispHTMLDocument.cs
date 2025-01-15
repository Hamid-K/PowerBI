using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007E5 RID: 2021
	[InterfaceType(2)]
	[Guid("3050F55F-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLDocument
	{
		// Token: 0x170048CE RID: 18638
		// (get) Token: 0x0600DAB0 RID: 55984
		[DispId(1001)]
		object Script
		{
			[TypeLibFunc(1088)]
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170048CF RID: 18639
		// (get) Token: 0x0600DAB1 RID: 55985
		[DispId(1003)]
		IHTMLElementCollection all
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048D0 RID: 18640
		// (get) Token: 0x0600DAB2 RID: 55986
		[DispId(1004)]
		IHTMLElement body
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048D1 RID: 18641
		// (get) Token: 0x0600DAB3 RID: 55987
		[DispId(1005)]
		IHTMLElement activeElement
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048D2 RID: 18642
		// (get) Token: 0x0600DAB4 RID: 55988
		[DispId(1011)]
		IHTMLElementCollection images
		{
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048D3 RID: 18643
		// (get) Token: 0x0600DAB5 RID: 55989
		[DispId(1008)]
		IHTMLElementCollection applets
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048D4 RID: 18644
		// (get) Token: 0x0600DAB6 RID: 55990
		[DispId(1009)]
		IHTMLElementCollection links
		{
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048D5 RID: 18645
		// (get) Token: 0x0600DAB7 RID: 55991
		[DispId(1010)]
		IHTMLElementCollection forms
		{
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048D6 RID: 18646
		// (get) Token: 0x0600DAB8 RID: 55992
		[DispId(1007)]
		IHTMLElementCollection anchors
		{
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048D7 RID: 18647
		// (get) Token: 0x0600DABA RID: 55994
		// (set) Token: 0x0600DAB9 RID: 55993
		[DispId(1012)]
		string title
		{
			[DispId(1012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170048D8 RID: 18648
		// (get) Token: 0x0600DABB RID: 55995
		[DispId(1013)]
		IHTMLElementCollection scripts
		{
			[DispId(1013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048D9 RID: 18649
		// (get) Token: 0x0600DABD RID: 55997
		// (set) Token: 0x0600DABC RID: 55996
		[DispId(1014)]
		string designMode
		{
			[TypeLibFunc(64)]
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(64)]
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170048DA RID: 18650
		// (get) Token: 0x0600DABE RID: 55998
		[DispId(1017)]
		IHTMLSelectionObject selection
		{
			[DispId(1017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048DB RID: 18651
		// (get) Token: 0x0600DABF RID: 55999
		[DispId(1018)]
		string readyState
		{
			[DispId(1018)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170048DC RID: 18652
		// (get) Token: 0x0600DAC0 RID: 56000
		[DispId(1019)]
		FramesCollection frames
		{
			[DispId(1019)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048DD RID: 18653
		// (get) Token: 0x0600DAC1 RID: 56001
		[DispId(1015)]
		IHTMLElementCollection embeds
		{
			[DispId(1015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048DE RID: 18654
		// (get) Token: 0x0600DAC2 RID: 56002
		[DispId(1021)]
		IHTMLElementCollection plugins
		{
			[DispId(1021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048DF RID: 18655
		// (get) Token: 0x0600DAC4 RID: 56004
		// (set) Token: 0x0600DAC3 RID: 56003
		[DispId(1022)]
		object alinkColor
		{
			[DispId(1022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048E0 RID: 18656
		// (get) Token: 0x0600DAC6 RID: 56006
		// (set) Token: 0x0600DAC5 RID: 56005
		[DispId(-501)]
		object bgColor
		{
			[DispId(-501)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-501)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048E1 RID: 18657
		// (get) Token: 0x0600DAC8 RID: 56008
		// (set) Token: 0x0600DAC7 RID: 56007
		[DispId(-2147413110)]
		object fgColor
		{
			[DispId(-2147413110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048E2 RID: 18658
		// (get) Token: 0x0600DACA RID: 56010
		// (set) Token: 0x0600DAC9 RID: 56009
		[DispId(1024)]
		object linkColor
		{
			[DispId(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048E3 RID: 18659
		// (get) Token: 0x0600DACC RID: 56012
		// (set) Token: 0x0600DACB RID: 56011
		[DispId(1023)]
		object vlinkColor
		{
			[DispId(1023)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1023)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048E4 RID: 18660
		// (get) Token: 0x0600DACD RID: 56013
		[DispId(1027)]
		string referrer
		{
			[DispId(1027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170048E5 RID: 18661
		// (get) Token: 0x0600DACE RID: 56014
		[DispId(1026)]
		HTMLLocation location
		{
			[DispId(1026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048E6 RID: 18662
		// (get) Token: 0x0600DACF RID: 56015
		[DispId(1028)]
		string lastModified
		{
			[DispId(1028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170048E7 RID: 18663
		// (get) Token: 0x0600DAD1 RID: 56017
		// (set) Token: 0x0600DAD0 RID: 56016
		[DispId(1025)]
		string url
		{
			[DispId(1025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170048E8 RID: 18664
		// (get) Token: 0x0600DAD3 RID: 56019
		// (set) Token: 0x0600DAD2 RID: 56018
		[DispId(1029)]
		string domain
		{
			[DispId(1029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170048E9 RID: 18665
		// (get) Token: 0x0600DAD5 RID: 56021
		// (set) Token: 0x0600DAD4 RID: 56020
		[DispId(1030)]
		string cookie
		{
			[DispId(1030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170048EA RID: 18666
		// (get) Token: 0x0600DAD7 RID: 56023
		// (set) Token: 0x0600DAD6 RID: 56022
		[DispId(1031)]
		bool expando
		{
			[TypeLibFunc(68)]
			[DispId(1031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(68)]
			[DispId(1031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170048EB RID: 18667
		// (get) Token: 0x0600DAD9 RID: 56025
		// (set) Token: 0x0600DAD8 RID: 56024
		[DispId(1032)]
		string charset
		{
			[DispId(1032)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1032)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170048EC RID: 18668
		// (get) Token: 0x0600DADB RID: 56027
		// (set) Token: 0x0600DADA RID: 56026
		[DispId(1033)]
		string defaultCharset
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

		// Token: 0x170048ED RID: 18669
		// (get) Token: 0x0600DADC RID: 56028
		[DispId(1041)]
		string mimeType
		{
			[DispId(1041)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170048EE RID: 18670
		// (get) Token: 0x0600DADD RID: 56029
		[DispId(1042)]
		string fileSize
		{
			[DispId(1042)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170048EF RID: 18671
		// (get) Token: 0x0600DADE RID: 56030
		[DispId(1043)]
		string fileCreatedDate
		{
			[DispId(1043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170048F0 RID: 18672
		// (get) Token: 0x0600DADF RID: 56031
		[DispId(1044)]
		string fileModifiedDate
		{
			[DispId(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170048F1 RID: 18673
		// (get) Token: 0x0600DAE0 RID: 56032
		[DispId(1045)]
		string fileUpdatedDate
		{
			[DispId(1045)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170048F2 RID: 18674
		// (get) Token: 0x0600DAE1 RID: 56033
		[DispId(1046)]
		string security
		{
			[DispId(1046)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170048F3 RID: 18675
		// (get) Token: 0x0600DAE2 RID: 56034
		[DispId(1047)]
		string protocol
		{
			[DispId(1047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170048F4 RID: 18676
		// (get) Token: 0x0600DAE3 RID: 56035
		[DispId(1048)]
		string nameProp
		{
			[DispId(1048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600DAE4 RID: 56036
		[DispId(1054)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void write([MarshalAs(29, SafeArraySubType = 12)] [In] params object[] psarray);

		// Token: 0x0600DAE5 RID: 56037
		[DispId(1055)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void writeln([MarshalAs(29, SafeArraySubType = 12)] [In] params object[] psarray);

		// Token: 0x0600DAE6 RID: 56038
		[DispId(1056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object open([MarshalAs(19)] [In] string url = "text/html", [MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object features, [MarshalAs(27)] [In] [Optional] object replace);

		// Token: 0x0600DAE7 RID: 56039
		[DispId(1057)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void close();

		// Token: 0x0600DAE8 RID: 56040
		[DispId(1058)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void clear();

		// Token: 0x0600DAE9 RID: 56041
		[DispId(1059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool queryCommandSupported([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DAEA RID: 56042
		[DispId(1060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool queryCommandEnabled([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DAEB RID: 56043
		[DispId(1061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool queryCommandState([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DAEC RID: 56044
		[DispId(1062)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool queryCommandIndeterm([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DAED RID: 56045
		[DispId(1063)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string queryCommandText([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DAEE RID: 56046
		[DispId(1064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object queryCommandValue([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DAEF RID: 56047
		[DispId(1065)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool execCommand([MarshalAs(19)] [In] string cmdID, [In] bool showUI = false, [MarshalAs(27)] [In] [Optional] object value);

		// Token: 0x0600DAF0 RID: 56048
		[DispId(1066)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool execCommandShowHelp([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DAF1 RID: 56049
		[DispId(1067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement createElement([MarshalAs(19)] [In] string eTag);

		// Token: 0x170048F5 RID: 18677
		// (get) Token: 0x0600DAF3 RID: 56051
		// (set) Token: 0x0600DAF2 RID: 56050
		[DispId(-2147412099)]
		object onhelp
		{
			[DispId(-2147412099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048F6 RID: 18678
		// (get) Token: 0x0600DAF5 RID: 56053
		// (set) Token: 0x0600DAF4 RID: 56052
		[DispId(-2147412104)]
		object onclick
		{
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048F7 RID: 18679
		// (get) Token: 0x0600DAF7 RID: 56055
		// (set) Token: 0x0600DAF6 RID: 56054
		[DispId(-2147412103)]
		object ondblclick
		{
			[TypeLibFunc(20)]
			[DispId(-2147412103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412103)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048F8 RID: 18680
		// (get) Token: 0x0600DAF9 RID: 56057
		// (set) Token: 0x0600DAF8 RID: 56056
		[DispId(-2147412106)]
		object onkeyup
		{
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048F9 RID: 18681
		// (get) Token: 0x0600DAFB RID: 56059
		// (set) Token: 0x0600DAFA RID: 56058
		[DispId(-2147412107)]
		object onkeydown
		{
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048FA RID: 18682
		// (get) Token: 0x0600DAFD RID: 56061
		// (set) Token: 0x0600DAFC RID: 56060
		[DispId(-2147412105)]
		object onkeypress
		{
			[TypeLibFunc(20)]
			[DispId(-2147412105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048FB RID: 18683
		// (get) Token: 0x0600DAFF RID: 56063
		// (set) Token: 0x0600DAFE RID: 56062
		[DispId(-2147412109)]
		object onmouseup
		{
			[DispId(-2147412109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048FC RID: 18684
		// (get) Token: 0x0600DB01 RID: 56065
		// (set) Token: 0x0600DB00 RID: 56064
		[DispId(-2147412110)]
		object onmousedown
		{
			[TypeLibFunc(20)]
			[DispId(-2147412110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048FD RID: 18685
		// (get) Token: 0x0600DB03 RID: 56067
		// (set) Token: 0x0600DB02 RID: 56066
		[DispId(-2147412108)]
		object onmousemove
		{
			[DispId(-2147412108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048FE RID: 18686
		// (get) Token: 0x0600DB05 RID: 56069
		// (set) Token: 0x0600DB04 RID: 56068
		[DispId(-2147412111)]
		object onmouseout
		{
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170048FF RID: 18687
		// (get) Token: 0x0600DB07 RID: 56071
		// (set) Token: 0x0600DB06 RID: 56070
		[DispId(-2147412112)]
		object onmouseover
		{
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004900 RID: 18688
		// (get) Token: 0x0600DB09 RID: 56073
		// (set) Token: 0x0600DB08 RID: 56072
		[DispId(-2147412087)]
		object onreadystatechange
		{
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004901 RID: 18689
		// (get) Token: 0x0600DB0B RID: 56075
		// (set) Token: 0x0600DB0A RID: 56074
		[DispId(-2147412090)]
		object onafterupdate
		{
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004902 RID: 18690
		// (get) Token: 0x0600DB0D RID: 56077
		// (set) Token: 0x0600DB0C RID: 56076
		[DispId(-2147412094)]
		object onrowexit
		{
			[DispId(-2147412094)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004903 RID: 18691
		// (get) Token: 0x0600DB0F RID: 56079
		// (set) Token: 0x0600DB0E RID: 56078
		[DispId(-2147412093)]
		object onrowenter
		{
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004904 RID: 18692
		// (get) Token: 0x0600DB11 RID: 56081
		// (set) Token: 0x0600DB10 RID: 56080
		[DispId(-2147412077)]
		object ondragstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004905 RID: 18693
		// (get) Token: 0x0600DB13 RID: 56083
		// (set) Token: 0x0600DB12 RID: 56082
		[DispId(-2147412075)]
		object onselectstart
		{
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600DB14 RID: 56084
		[DispId(1068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement elementFromPoint([In] int x, [In] int y);

		// Token: 0x17004906 RID: 18694
		// (get) Token: 0x0600DB15 RID: 56085
		[DispId(1034)]
		IHTMLWindow2 parentWindow
		{
			[DispId(1034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004907 RID: 18695
		// (get) Token: 0x0600DB16 RID: 56086
		[DispId(1069)]
		HTMLStyleSheetsCollection styleSheets
		{
			[DispId(1069)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004908 RID: 18696
		// (get) Token: 0x0600DB18 RID: 56088
		// (set) Token: 0x0600DB17 RID: 56087
		[DispId(-2147412091)]
		object onbeforeupdate
		{
			[DispId(-2147412091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004909 RID: 18697
		// (get) Token: 0x0600DB1A RID: 56090
		// (set) Token: 0x0600DB19 RID: 56089
		[DispId(-2147412074)]
		object onerrorupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600DB1B RID: 56091
		[DispId(1070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x0600DB1C RID: 56092
		[DispId(1071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLStyleSheet createStyleSheet([MarshalAs(19)] [In] string bstrHref = "", [In] int lIndex = -1);

		// Token: 0x0600DB1D RID: 56093
		[DispId(1072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void releaseCapture();

		// Token: 0x0600DB1E RID: 56094
		[DispId(1073)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void recalc([In] bool fForce = false);

		// Token: 0x0600DB1F RID: 56095
		[DispId(1074)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode createTextNode([MarshalAs(19)] [In] string text);

		// Token: 0x1700490A RID: 18698
		// (get) Token: 0x0600DB20 RID: 56096
		[DispId(1075)]
		IHTMLElement documentElement
		{
			[DispId(1075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700490B RID: 18699
		// (get) Token: 0x0600DB21 RID: 56097
		[DispId(1077)]
		string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(1077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600DB22 RID: 56098
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600DB23 RID: 56099
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x1700490C RID: 18700
		// (get) Token: 0x0600DB25 RID: 56101
		// (set) Token: 0x0600DB24 RID: 56100
		[DispId(-2147412050)]
		object onrowsdelete
		{
			[DispId(-2147412050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700490D RID: 18701
		// (get) Token: 0x0600DB27 RID: 56103
		// (set) Token: 0x0600DB26 RID: 56102
		[DispId(-2147412049)]
		object onrowsinserted
		{
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700490E RID: 18702
		// (get) Token: 0x0600DB29 RID: 56105
		// (set) Token: 0x0600DB28 RID: 56104
		[DispId(-2147412048)]
		object oncellchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412048)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700490F RID: 18703
		// (get) Token: 0x0600DB2B RID: 56107
		// (set) Token: 0x0600DB2A RID: 56106
		[DispId(-2147412072)]
		object ondatasetchanged
		{
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004910 RID: 18704
		// (get) Token: 0x0600DB2D RID: 56109
		// (set) Token: 0x0600DB2C RID: 56108
		[DispId(-2147412071)]
		object ondataavailable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412071)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004911 RID: 18705
		// (get) Token: 0x0600DB2F RID: 56111
		// (set) Token: 0x0600DB2E RID: 56110
		[DispId(-2147412070)]
		object ondatasetcomplete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004912 RID: 18706
		// (get) Token: 0x0600DB31 RID: 56113
		// (set) Token: 0x0600DB30 RID: 56112
		[DispId(-2147412065)]
		object onpropertychange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004913 RID: 18707
		// (get) Token: 0x0600DB33 RID: 56115
		// (set) Token: 0x0600DB32 RID: 56114
		[DispId(-2147412995)]
		string dir
		{
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004914 RID: 18708
		// (get) Token: 0x0600DB35 RID: 56117
		// (set) Token: 0x0600DB34 RID: 56116
		[DispId(-2147412047)]
		object oncontextmenu
		{
			[DispId(-2147412047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004915 RID: 18709
		// (get) Token: 0x0600DB37 RID: 56119
		// (set) Token: 0x0600DB36 RID: 56118
		[DispId(-2147412044)]
		object onstop
		{
			[TypeLibFunc(20)]
			[DispId(-2147412044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412044)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600DB38 RID: 56120
		[DispId(1076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDocument2 createDocumentFragment();

		// Token: 0x17004916 RID: 18710
		// (get) Token: 0x0600DB39 RID: 56121
		[DispId(1078)]
		IHTMLDocument2 parentDocument
		{
			[TypeLibFunc(65)]
			[DispId(1078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004917 RID: 18711
		// (get) Token: 0x0600DB3B RID: 56123
		// (set) Token: 0x0600DB3A RID: 56122
		[DispId(1079)]
		bool enableDownload
		{
			[DispId(1079)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1079)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17004918 RID: 18712
		// (get) Token: 0x0600DB3D RID: 56125
		// (set) Token: 0x0600DB3C RID: 56124
		[DispId(1080)]
		string baseUrl
		{
			[TypeLibFunc(65)]
			[DispId(1080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(65)]
			[DispId(1080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004919 RID: 18713
		// (get) Token: 0x0600DB3F RID: 56127
		// (set) Token: 0x0600DB3E RID: 56126
		[DispId(1082)]
		bool inheritStyleSheets
		{
			[DispId(1082)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(65)]
			[DispId(1082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700491A RID: 18714
		// (get) Token: 0x0600DB41 RID: 56129
		// (set) Token: 0x0600DB40 RID: 56128
		[DispId(-2147412043)]
		object onbeforeeditfocus
		{
			[DispId(-2147412043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600DB42 RID: 56130
		[DispId(1086)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElementCollection getElementsByName([MarshalAs(19)] [In] string v);

		// Token: 0x0600DB43 RID: 56131
		[DispId(1088)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement getElementById([MarshalAs(19)] [In] string v);

		// Token: 0x0600DB44 RID: 56132
		[DispId(1087)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600DB45 RID: 56133
		[DispId(1089)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void focus();

		// Token: 0x0600DB46 RID: 56134
		[DispId(1090)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool hasFocus();

		// Token: 0x1700491B RID: 18715
		// (get) Token: 0x0600DB48 RID: 56136
		// (set) Token: 0x0600DB47 RID: 56135
		[DispId(-2147412032)]
		object onselectionchange
		{
			[DispId(-2147412032)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412032)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700491C RID: 18716
		// (get) Token: 0x0600DB49 RID: 56137
		[DispId(1091)]
		object namespaces
		{
			[DispId(1091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600DB4A RID: 56138
		[DispId(1092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDocument2 createDocumentFromUrl([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(19)] [In] string bstrOptions);

		// Token: 0x1700491D RID: 18717
		// (get) Token: 0x0600DB4C RID: 56140
		// (set) Token: 0x0600DB4B RID: 56139
		[DispId(1093)]
		string media
		{
			[DispId(1093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600DB4D RID: 56141
		[DispId(1094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLEventObj CreateEventObject([MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x0600DB4E RID: 56142
		[DispId(1095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x0600DB4F RID: 56143
		[DispId(1096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLRenderStyle createRenderStyle([MarshalAs(19)] [In] string v);

		// Token: 0x1700491E RID: 18718
		// (get) Token: 0x0600DB51 RID: 56145
		// (set) Token: 0x0600DB50 RID: 56144
		[DispId(-2147412033)]
		object oncontrolselect
		{
			[DispId(-2147412033)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412033)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700491F RID: 18719
		// (get) Token: 0x0600DB52 RID: 56146
		[DispId(1097)]
		string URLUnencoded
		{
			[DispId(1097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004920 RID: 18720
		// (get) Token: 0x0600DB54 RID: 56148
		// (set) Token: 0x0600DB53 RID: 56147
		[DispId(-2147412036)]
		object onmousewheel
		{
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004921 RID: 18721
		// (get) Token: 0x0600DB55 RID: 56149
		[DispId(1098)]
		IHTMLDOMNode doctype
		{
			[DispId(1098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004922 RID: 18722
		// (get) Token: 0x0600DB56 RID: 56150
		[DispId(1099)]
		IHTMLDOMImplementation implementation
		{
			[DispId(1099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600DB57 RID: 56151
		[DispId(1100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute createAttribute([MarshalAs(19)] [In] string bstrattrName);

		// Token: 0x0600DB58 RID: 56152
		[DispId(1101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode createComment([MarshalAs(19)] [In] string bstrdata);

		// Token: 0x17004923 RID: 18723
		// (get) Token: 0x0600DB5A RID: 56154
		// (set) Token: 0x0600DB59 RID: 56153
		[DispId(-2147412021)]
		object onfocusin
		{
			[TypeLibFunc(20)]
			[DispId(-2147412021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004924 RID: 18724
		// (get) Token: 0x0600DB5C RID: 56156
		// (set) Token: 0x0600DB5B RID: 56155
		[DispId(-2147412020)]
		object onfocusout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004925 RID: 18725
		// (get) Token: 0x0600DB5E RID: 56158
		// (set) Token: 0x0600DB5D RID: 56157
		[DispId(-2147412025)]
		object onactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004926 RID: 18726
		// (get) Token: 0x0600DB60 RID: 56160
		// (set) Token: 0x0600DB5F RID: 56159
		[DispId(-2147412024)]
		object ondeactivate
		{
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004927 RID: 18727
		// (get) Token: 0x0600DB62 RID: 56162
		// (set) Token: 0x0600DB61 RID: 56161
		[DispId(-2147412022)]
		object onbeforeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004928 RID: 18728
		// (get) Token: 0x0600DB64 RID: 56164
		// (set) Token: 0x0600DB63 RID: 56163
		[DispId(-2147412035)]
		object onbeforedeactivate
		{
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004929 RID: 18729
		// (get) Token: 0x0600DB65 RID: 56165
		[DispId(1102)]
		string compatMode
		{
			[DispId(1102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700492A RID: 18730
		// (get) Token: 0x0600DB66 RID: 56166
		[DispId(-2147417066)]
		int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700492B RID: 18731
		// (get) Token: 0x0600DB67 RID: 56167
		[DispId(-2147417065)]
		IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600DB68 RID: 56168
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool hasChildNodes();

		// Token: 0x1700492C RID: 18732
		// (get) Token: 0x0600DB69 RID: 56169
		[DispId(-2147417063)]
		object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700492D RID: 18733
		// (get) Token: 0x0600DB6A RID: 56170
		[DispId(-2147417062)]
		object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600DB6B RID: 56171
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600DB6C RID: 56172
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600DB6D RID: 56173
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600DB6E RID: 56174
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600DB6F RID: 56175
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600DB70 RID: 56176
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600DB71 RID: 56177
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600DB72 RID: 56178
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x1700492E RID: 18734
		// (get) Token: 0x0600DB73 RID: 56179
		[DispId(-2147417038)]
		string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700492F RID: 18735
		// (get) Token: 0x0600DB75 RID: 56181
		// (set) Token: 0x0600DB74 RID: 56180
		[DispId(-2147417037)]
		object nodeValue
		{
			[DispId(-2147417037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147417037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004930 RID: 18736
		// (get) Token: 0x0600DB76 RID: 56182
		[DispId(-2147417036)]
		IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004931 RID: 18737
		// (get) Token: 0x0600DB77 RID: 56183
		[DispId(-2147417035)]
		IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004932 RID: 18738
		// (get) Token: 0x0600DB78 RID: 56184
		[DispId(-2147417034)]
		IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004933 RID: 18739
		// (get) Token: 0x0600DB79 RID: 56185
		[DispId(-2147417033)]
		IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004934 RID: 18740
		// (get) Token: 0x0600DB7A RID: 56186
		[DispId(-2147416999)]
		object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}
	}
}
