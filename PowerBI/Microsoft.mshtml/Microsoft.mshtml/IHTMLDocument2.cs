using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007B4 RID: 1972
	[TypeLibType(4160)]
	[Guid("332C4425-26CB-11D0-B483-00C04FD90119")]
	[ComImport]
	public interface IHTMLDocument2 : IHTMLDocument
	{
		// Token: 0x1700472C RID: 18220
		// (get) Token: 0x0600D660 RID: 54880
		[DispId(1001)]
		object Script
		{
			[TypeLibFunc(1088)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700472D RID: 18221
		// (get) Token: 0x0600D661 RID: 54881
		[DispId(1003)]
		IHTMLElementCollection all
		{
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700472E RID: 18222
		// (get) Token: 0x0600D662 RID: 54882
		[DispId(1004)]
		IHTMLElement body
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700472F RID: 18223
		// (get) Token: 0x0600D663 RID: 54883
		[DispId(1005)]
		IHTMLElement activeElement
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004730 RID: 18224
		// (get) Token: 0x0600D664 RID: 54884
		[DispId(1011)]
		IHTMLElementCollection images
		{
			[DispId(1011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004731 RID: 18225
		// (get) Token: 0x0600D665 RID: 54885
		[DispId(1008)]
		IHTMLElementCollection applets
		{
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004732 RID: 18226
		// (get) Token: 0x0600D666 RID: 54886
		[DispId(1009)]
		IHTMLElementCollection links
		{
			[DispId(1009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004733 RID: 18227
		// (get) Token: 0x0600D667 RID: 54887
		[DispId(1010)]
		IHTMLElementCollection forms
		{
			[DispId(1010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004734 RID: 18228
		// (get) Token: 0x0600D668 RID: 54888
		[DispId(1007)]
		IHTMLElementCollection anchors
		{
			[DispId(1007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004735 RID: 18229
		// (get) Token: 0x0600D66A RID: 54890
		// (set) Token: 0x0600D669 RID: 54889
		[DispId(1012)]
		string title
		{
			[DispId(1012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004736 RID: 18230
		// (get) Token: 0x0600D66B RID: 54891
		[DispId(1013)]
		IHTMLElementCollection scripts
		{
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004737 RID: 18231
		// (get) Token: 0x0600D66D RID: 54893
		// (set) Token: 0x0600D66C RID: 54892
		[DispId(1014)]
		string designMode
		{
			[DispId(1014)]
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1014)]
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004738 RID: 18232
		// (get) Token: 0x0600D66E RID: 54894
		[DispId(1017)]
		IHTMLSelectionObject selection
		{
			[DispId(1017)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004739 RID: 18233
		// (get) Token: 0x0600D66F RID: 54895
		[DispId(1018)]
		string readyState
		{
			[TypeLibFunc(4)]
			[DispId(1018)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700473A RID: 18234
		// (get) Token: 0x0600D670 RID: 54896
		[DispId(1019)]
		FramesCollection frames
		{
			[DispId(1019)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700473B RID: 18235
		// (get) Token: 0x0600D671 RID: 54897
		[DispId(1015)]
		IHTMLElementCollection embeds
		{
			[DispId(1015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700473C RID: 18236
		// (get) Token: 0x0600D672 RID: 54898
		[DispId(1021)]
		IHTMLElementCollection plugins
		{
			[DispId(1021)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700473D RID: 18237
		// (get) Token: 0x0600D674 RID: 54900
		// (set) Token: 0x0600D673 RID: 54899
		[DispId(1022)]
		object alinkColor
		{
			[DispId(1022)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1022)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700473E RID: 18238
		// (get) Token: 0x0600D676 RID: 54902
		// (set) Token: 0x0600D675 RID: 54901
		[DispId(-501)]
		object bgColor
		{
			[DispId(-501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700473F RID: 18239
		// (get) Token: 0x0600D678 RID: 54904
		// (set) Token: 0x0600D677 RID: 54903
		[DispId(-2147413110)]
		object fgColor
		{
			[DispId(-2147413110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004740 RID: 18240
		// (get) Token: 0x0600D67A RID: 54906
		// (set) Token: 0x0600D679 RID: 54905
		[DispId(1024)]
		object linkColor
		{
			[DispId(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004741 RID: 18241
		// (get) Token: 0x0600D67C RID: 54908
		// (set) Token: 0x0600D67B RID: 54907
		[DispId(1023)]
		object vlinkColor
		{
			[DispId(1023)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1023)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004742 RID: 18242
		// (get) Token: 0x0600D67D RID: 54909
		[DispId(1027)]
		string referrer
		{
			[DispId(1027)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004743 RID: 18243
		// (get) Token: 0x0600D67E RID: 54910
		[DispId(1026)]
		HTMLLocation location
		{
			[DispId(1026)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004744 RID: 18244
		// (get) Token: 0x0600D67F RID: 54911
		[DispId(1028)]
		string lastModified
		{
			[DispId(1028)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004745 RID: 18245
		// (get) Token: 0x0600D681 RID: 54913
		// (set) Token: 0x0600D680 RID: 54912
		[DispId(1025)]
		string url
		{
			[DispId(1025)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1025)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004746 RID: 18246
		// (get) Token: 0x0600D683 RID: 54915
		// (set) Token: 0x0600D682 RID: 54914
		[DispId(1029)]
		string domain
		{
			[DispId(1029)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1029)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004747 RID: 18247
		// (get) Token: 0x0600D685 RID: 54917
		// (set) Token: 0x0600D684 RID: 54916
		[DispId(1030)]
		string cookie
		{
			[DispId(1030)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1030)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004748 RID: 18248
		// (get) Token: 0x0600D687 RID: 54919
		// (set) Token: 0x0600D686 RID: 54918
		[DispId(1031)]
		bool expando
		{
			[TypeLibFunc(68)]
			[DispId(1031)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(68)]
			[DispId(1031)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004749 RID: 18249
		// (get) Token: 0x0600D689 RID: 54921
		// (set) Token: 0x0600D688 RID: 54920
		[DispId(1032)]
		string charset
		{
			[TypeLibFunc(64)]
			[DispId(1032)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(64)]
			[DispId(1032)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700474A RID: 18250
		// (get) Token: 0x0600D68B RID: 54923
		// (set) Token: 0x0600D68A RID: 54922
		[DispId(1033)]
		string defaultCharset
		{
			[DispId(1033)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1033)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700474B RID: 18251
		// (get) Token: 0x0600D68C RID: 54924
		[DispId(1041)]
		string mimeType
		{
			[DispId(1041)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700474C RID: 18252
		// (get) Token: 0x0600D68D RID: 54925
		[DispId(1042)]
		string fileSize
		{
			[DispId(1042)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700474D RID: 18253
		// (get) Token: 0x0600D68E RID: 54926
		[DispId(1043)]
		string fileCreatedDate
		{
			[DispId(1043)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700474E RID: 18254
		// (get) Token: 0x0600D68F RID: 54927
		[DispId(1044)]
		string fileModifiedDate
		{
			[DispId(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700474F RID: 18255
		// (get) Token: 0x0600D690 RID: 54928
		[DispId(1045)]
		string fileUpdatedDate
		{
			[DispId(1045)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004750 RID: 18256
		// (get) Token: 0x0600D691 RID: 54929
		[DispId(1046)]
		string security
		{
			[DispId(1046)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004751 RID: 18257
		// (get) Token: 0x0600D692 RID: 54930
		[DispId(1047)]
		string protocol
		{
			[DispId(1047)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004752 RID: 18258
		// (get) Token: 0x0600D693 RID: 54931
		[DispId(1048)]
		string nameProp
		{
			[DispId(1048)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600D694 RID: 54932
		[DispId(1054)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void write([MarshalAs(29, SafeArraySubType = 12)] [In] params object[] psarray);

		// Token: 0x0600D695 RID: 54933
		[DispId(1055)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void writeln([MarshalAs(29, SafeArraySubType = 12)] [In] params object[] psarray);

		// Token: 0x0600D696 RID: 54934
		[DispId(1056)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object open([MarshalAs(19)] [In] string url = "text/html", [MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object features, [MarshalAs(27)] [In] [Optional] object replace);

		// Token: 0x0600D697 RID: 54935
		[DispId(1057)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void close();

		// Token: 0x0600D698 RID: 54936
		[DispId(1058)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void clear();

		// Token: 0x0600D699 RID: 54937
		[DispId(1059)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool queryCommandSupported([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600D69A RID: 54938
		[DispId(1060)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool queryCommandEnabled([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600D69B RID: 54939
		[DispId(1061)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool queryCommandState([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600D69C RID: 54940
		[DispId(1062)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool queryCommandIndeterm([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600D69D RID: 54941
		[DispId(1063)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string queryCommandText([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600D69E RID: 54942
		[DispId(1064)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object queryCommandValue([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600D69F RID: 54943
		[DispId(1065)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool execCommand([MarshalAs(19)] [In] string cmdID, [In] bool showUI = false, [MarshalAs(27)] [In] [Optional] object value);

		// Token: 0x0600D6A0 RID: 54944
		[DispId(1066)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool execCommandShowHelp([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600D6A1 RID: 54945
		[DispId(1067)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement createElement([MarshalAs(19)] [In] string eTag);

		// Token: 0x17004753 RID: 18259
		// (get) Token: 0x0600D6A3 RID: 54947
		// (set) Token: 0x0600D6A2 RID: 54946
		[DispId(-2147412099)]
		object onhelp
		{
			[DispId(-2147412099)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004754 RID: 18260
		// (get) Token: 0x0600D6A5 RID: 54949
		// (set) Token: 0x0600D6A4 RID: 54948
		[DispId(-2147412104)]
		object onclick
		{
			[TypeLibFunc(20)]
			[DispId(-2147412104)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004755 RID: 18261
		// (get) Token: 0x0600D6A7 RID: 54951
		// (set) Token: 0x0600D6A6 RID: 54950
		[DispId(-2147412103)]
		object ondblclick
		{
			[DispId(-2147412103)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412103)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004756 RID: 18262
		// (get) Token: 0x0600D6A9 RID: 54953
		// (set) Token: 0x0600D6A8 RID: 54952
		[DispId(-2147412106)]
		object onkeyup
		{
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004757 RID: 18263
		// (get) Token: 0x0600D6AB RID: 54955
		// (set) Token: 0x0600D6AA RID: 54954
		[DispId(-2147412107)]
		object onkeydown
		{
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004758 RID: 18264
		// (get) Token: 0x0600D6AD RID: 54957
		// (set) Token: 0x0600D6AC RID: 54956
		[DispId(-2147412105)]
		object onkeypress
		{
			[TypeLibFunc(20)]
			[DispId(-2147412105)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412105)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004759 RID: 18265
		// (get) Token: 0x0600D6AF RID: 54959
		// (set) Token: 0x0600D6AE RID: 54958
		[DispId(-2147412109)]
		object onmouseup
		{
			[TypeLibFunc(20)]
			[DispId(-2147412109)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412109)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700475A RID: 18266
		// (get) Token: 0x0600D6B1 RID: 54961
		// (set) Token: 0x0600D6B0 RID: 54960
		[DispId(-2147412110)]
		object onmousedown
		{
			[DispId(-2147412110)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700475B RID: 18267
		// (get) Token: 0x0600D6B3 RID: 54963
		// (set) Token: 0x0600D6B2 RID: 54962
		[DispId(-2147412108)]
		object onmousemove
		{
			[TypeLibFunc(20)]
			[DispId(-2147412108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700475C RID: 18268
		// (get) Token: 0x0600D6B5 RID: 54965
		// (set) Token: 0x0600D6B4 RID: 54964
		[DispId(-2147412111)]
		object onmouseout
		{
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700475D RID: 18269
		// (get) Token: 0x0600D6B7 RID: 54967
		// (set) Token: 0x0600D6B6 RID: 54966
		[DispId(-2147412112)]
		object onmouseover
		{
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700475E RID: 18270
		// (get) Token: 0x0600D6B9 RID: 54969
		// (set) Token: 0x0600D6B8 RID: 54968
		[DispId(-2147412087)]
		object onreadystatechange
		{
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700475F RID: 18271
		// (get) Token: 0x0600D6BB RID: 54971
		// (set) Token: 0x0600D6BA RID: 54970
		[DispId(-2147412090)]
		object onafterupdate
		{
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412090)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004760 RID: 18272
		// (get) Token: 0x0600D6BD RID: 54973
		// (set) Token: 0x0600D6BC RID: 54972
		[DispId(-2147412094)]
		object onrowexit
		{
			[TypeLibFunc(20)]
			[DispId(-2147412094)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412094)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004761 RID: 18273
		// (get) Token: 0x0600D6BF RID: 54975
		// (set) Token: 0x0600D6BE RID: 54974
		[DispId(-2147412093)]
		object onrowenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412093)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004762 RID: 18274
		// (get) Token: 0x0600D6C1 RID: 54977
		// (set) Token: 0x0600D6C0 RID: 54976
		[DispId(-2147412077)]
		object ondragstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004763 RID: 18275
		// (get) Token: 0x0600D6C3 RID: 54979
		// (set) Token: 0x0600D6C2 RID: 54978
		[DispId(-2147412075)]
		object onselectstart
		{
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412075)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600D6C4 RID: 54980
		[DispId(1068)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement elementFromPoint([In] int x, [In] int y);

		// Token: 0x17004764 RID: 18276
		// (get) Token: 0x0600D6C5 RID: 54981
		[DispId(1034)]
		IHTMLWindow2 parentWindow
		{
			[DispId(1034)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004765 RID: 18277
		// (get) Token: 0x0600D6C6 RID: 54982
		[DispId(1069)]
		HTMLStyleSheetsCollection styleSheets
		{
			[DispId(1069)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004766 RID: 18278
		// (get) Token: 0x0600D6C8 RID: 54984
		// (set) Token: 0x0600D6C7 RID: 54983
		[DispId(-2147412091)]
		object onbeforeupdate
		{
			[DispId(-2147412091)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412091)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004767 RID: 18279
		// (get) Token: 0x0600D6CA RID: 54986
		// (set) Token: 0x0600D6C9 RID: 54985
		[DispId(-2147412074)]
		object onerrorupdate
		{
			[DispId(-2147412074)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412074)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600D6CB RID: 54987
		[DispId(1070)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x0600D6CC RID: 54988
		[DispId(1071)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLStyleSheet createStyleSheet([MarshalAs(19)] [In] string bstrHref = "", [In] int lIndex = -1);
	}
}
