using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007DD RID: 2013
	[ComSourceInterfaces("mshtml.HTMLWindowEvents\0mshtml.HTMLWindowEvents2\0\0")]
	[TypeLibType(2)]
	[DefaultMember("item")]
	[Guid("3050F391-98B5-11CF-BB82-00AA00BDCE0B")]
	[ClassInterface(0)]
	[ComImport]
	public class HTMLWindowProxyClass : DispHTMLWindowProxy, HTMLWindowProxy, HTMLWindowEvents_Event, IHTMLWindow2, IHTMLWindow3, IHTMLWindow4, HTMLWindowEvents2_Event
	{
		// Token: 0x0600D945 RID: 55621
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLWindowProxyClass();

		// Token: 0x0600D946 RID: 55622
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object item([MarshalAs(27)] [In] ref object pvarIndex);

		// Token: 0x17004861 RID: 18529
		// (get) Token: 0x0600D947 RID: 55623
		[DispId(1001)]
		public virtual extern int length
		{
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004862 RID: 18530
		// (get) Token: 0x0600D948 RID: 55624
		[DispId(1100)]
		public virtual extern FramesCollection frames
		{
			[DispId(1100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004863 RID: 18531
		// (get) Token: 0x0600D94A RID: 55626
		// (set) Token: 0x0600D949 RID: 55625
		[DispId(1101)]
		public virtual extern string defaultStatus
		{
			[DispId(1101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004864 RID: 18532
		// (get) Token: 0x0600D94C RID: 55628
		// (set) Token: 0x0600D94B RID: 55627
		[DispId(1102)]
		public virtual extern string status
		{
			[DispId(1102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600D94D RID: 55629
		[DispId(1104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearTimeout([In] int timerID);

		// Token: 0x0600D94E RID: 55630
		[DispId(1105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void alert([MarshalAs(19)] [In] string message = "");

		// Token: 0x0600D94F RID: 55631
		[DispId(1110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool confirm([MarshalAs(19)] [In] string message = "");

		// Token: 0x0600D950 RID: 55632
		[DispId(1111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object prompt([MarshalAs(19)] [In] string message = "", [MarshalAs(19)] [In] string defstr = "undefined");

		// Token: 0x17004865 RID: 18533
		// (get) Token: 0x0600D951 RID: 55633
		[DispId(1125)]
		public virtual extern HTMLImageElementFactory Image
		{
			[DispId(1125)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004866 RID: 18534
		// (get) Token: 0x0600D952 RID: 55634
		[DispId(14)]
		public virtual extern HTMLLocation location
		{
			[DispId(14)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004867 RID: 18535
		// (get) Token: 0x0600D953 RID: 55635
		[DispId(2)]
		public virtual extern HTMLHistory history
		{
			[DispId(2)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D954 RID: 55636
		[DispId(3)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void close();

		// Token: 0x17004868 RID: 18536
		// (get) Token: 0x0600D956 RID: 55638
		// (set) Token: 0x0600D955 RID: 55637
		[DispId(4)]
		public virtual extern object opener
		{
			[DispId(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004869 RID: 18537
		// (get) Token: 0x0600D957 RID: 55639
		[DispId(5)]
		public virtual extern HTMLNavigator navigator
		{
			[DispId(5)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700486A RID: 18538
		// (get) Token: 0x0600D959 RID: 55641
		// (set) Token: 0x0600D958 RID: 55640
		[DispId(11)]
		public virtual extern string name
		{
			[DispId(11)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(11)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700486B RID: 18539
		// (get) Token: 0x0600D95A RID: 55642
		[DispId(12)]
		public virtual extern IHTMLWindow2 parent
		{
			[DispId(12)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D95B RID: 55643
		[DispId(13)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLWindow2 open([MarshalAs(19)] [In] string url = "", [MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string features = "", [In] bool replace = false);

		// Token: 0x1700486C RID: 18540
		// (get) Token: 0x0600D95C RID: 55644
		[DispId(20)]
		public virtual extern IHTMLWindow2 self
		{
			[DispId(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700486D RID: 18541
		// (get) Token: 0x0600D95D RID: 55645
		[DispId(21)]
		public virtual extern IHTMLWindow2 top
		{
			[DispId(21)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700486E RID: 18542
		// (get) Token: 0x0600D95E RID: 55646
		[DispId(22)]
		public virtual extern IHTMLWindow2 window
		{
			[DispId(22)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D95F RID: 55647
		[DispId(25)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void navigate([MarshalAs(19)] [In] string url);

		// Token: 0x1700486F RID: 18543
		// (get) Token: 0x0600D961 RID: 55649
		// (set) Token: 0x0600D960 RID: 55648
		[DispId(-2147412098)]
		public virtual extern object onfocus
		{
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004870 RID: 18544
		// (get) Token: 0x0600D963 RID: 55651
		// (set) Token: 0x0600D962 RID: 55650
		[DispId(-2147412097)]
		public virtual extern object onblur
		{
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004871 RID: 18545
		// (get) Token: 0x0600D965 RID: 55653
		// (set) Token: 0x0600D964 RID: 55652
		[DispId(-2147412080)]
		public virtual extern object onload
		{
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004872 RID: 18546
		// (get) Token: 0x0600D967 RID: 55655
		// (set) Token: 0x0600D966 RID: 55654
		[DispId(-2147412073)]
		public virtual extern object onbeforeunload
		{
			[TypeLibFunc(20)]
			[DispId(-2147412073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004873 RID: 18547
		// (get) Token: 0x0600D969 RID: 55657
		// (set) Token: 0x0600D968 RID: 55656
		[DispId(-2147412079)]
		public virtual extern object onunload
		{
			[DispId(-2147412079)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412079)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004874 RID: 18548
		// (get) Token: 0x0600D96B RID: 55659
		// (set) Token: 0x0600D96A RID: 55658
		[DispId(-2147412099)]
		public virtual extern object onhelp
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

		// Token: 0x17004875 RID: 18549
		// (get) Token: 0x0600D96D RID: 55661
		// (set) Token: 0x0600D96C RID: 55660
		[DispId(-2147412083)]
		public virtual extern object onerror
		{
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004876 RID: 18550
		// (get) Token: 0x0600D96F RID: 55663
		// (set) Token: 0x0600D96E RID: 55662
		[DispId(-2147412076)]
		public virtual extern object onresize
		{
			[DispId(-2147412076)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412076)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004877 RID: 18551
		// (get) Token: 0x0600D971 RID: 55665
		// (set) Token: 0x0600D970 RID: 55664
		[DispId(-2147412081)]
		public virtual extern object onscroll
		{
			[TypeLibFunc(20)]
			[DispId(-2147412081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004878 RID: 18552
		// (get) Token: 0x0600D972 RID: 55666
		[DispId(1151)]
		public virtual extern IHTMLDocument2 document
		{
			[DispId(1151)]
			[TypeLibFunc(2)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004879 RID: 18553
		// (get) Token: 0x0600D973 RID: 55667
		[DispId(1152)]
		public virtual extern IHTMLEventObj @event
		{
			[DispId(1152)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700487A RID: 18554
		// (get) Token: 0x0600D974 RID: 55668
		[DispId(1153)]
		public virtual extern object _newEnum
		{
			[DispId(1153)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(25)]
			get;
		}

		// Token: 0x0600D975 RID: 55669
		[DispId(1154)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object showModalDialog([MarshalAs(19)] [In] string dialog, [MarshalAs(27)] [In] [Optional] ref object varArgIn, [MarshalAs(27)] [In] [Optional] ref object varOptions);

		// Token: 0x0600D976 RID: 55670
		[DispId(1155)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void showHelp([MarshalAs(19)] [In] string helpURL, [MarshalAs(27)] [In] [Optional] object helpArg, [MarshalAs(19)] [In] string features = "");

		// Token: 0x1700487B RID: 18555
		// (get) Token: 0x0600D977 RID: 55671
		[DispId(1156)]
		public virtual extern IHTMLScreen screen
		{
			[DispId(1156)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700487C RID: 18556
		// (get) Token: 0x0600D978 RID: 55672
		[DispId(1157)]
		public virtual extern HTMLOptionElementFactory Option
		{
			[DispId(1157)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D979 RID: 55673
		[DispId(1158)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x1700487D RID: 18557
		// (get) Token: 0x0600D97A RID: 55674
		[DispId(23)]
		public virtual extern bool closed
		{
			[DispId(23)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D97B RID: 55675
		[DispId(1159)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600D97C RID: 55676
		[DispId(1160)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scroll([In] int x, [In] int y);

		// Token: 0x1700487E RID: 18558
		// (get) Token: 0x0600D97D RID: 55677
		[DispId(1161)]
		public virtual extern HTMLNavigator clientInformation
		{
			[DispId(1161)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D97E RID: 55678
		[DispId(1163)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearInterval([In] int timerID);

		// Token: 0x1700487F RID: 18559
		// (get) Token: 0x0600D980 RID: 55680
		// (set) Token: 0x0600D97F RID: 55679
		[DispId(1164)]
		public virtual extern object offscreenBuffering
		{
			[DispId(1164)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1164)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600D981 RID: 55681
		[DispId(1165)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object execScript([MarshalAs(19)] [In] string code, [MarshalAs(19)] [In] string language = "JScript");

		// Token: 0x0600D982 RID: 55682
		[DispId(1166)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x0600D983 RID: 55683
		[DispId(1167)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollBy([In] int x, [In] int y);

		// Token: 0x0600D984 RID: 55684
		[DispId(1168)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollTo([In] int x, [In] int y);

		// Token: 0x0600D985 RID: 55685
		[DispId(6)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void moveTo([In] int x, [In] int y);

		// Token: 0x0600D986 RID: 55686
		[DispId(7)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void moveBy([In] int x, [In] int y);

		// Token: 0x0600D987 RID: 55687
		[DispId(9)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void resizeTo([In] int x, [In] int y);

		// Token: 0x0600D988 RID: 55688
		[DispId(8)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void resizeBy([In] int x, [In] int y);

		// Token: 0x17004880 RID: 18560
		// (get) Token: 0x0600D989 RID: 55689
		[DispId(1169)]
		public virtual extern object external
		{
			[DispId(1169)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004881 RID: 18561
		// (get) Token: 0x0600D98A RID: 55690
		[DispId(1170)]
		public virtual extern int screenLeft
		{
			[DispId(1170)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004882 RID: 18562
		// (get) Token: 0x0600D98B RID: 55691
		[DispId(1171)]
		public virtual extern int screenTop
		{
			[DispId(1171)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D98C RID: 55692
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600D98D RID: 55693
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600D98E RID: 55694
		[DispId(1103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int setTimeout([MarshalAs(27)] [In] ref object expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D98F RID: 55695
		[DispId(1162)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int setInterval([MarshalAs(27)] [In] ref object expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D990 RID: 55696
		[DispId(1174)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void print();

		// Token: 0x17004883 RID: 18563
		// (get) Token: 0x0600D992 RID: 55698
		// (set) Token: 0x0600D991 RID: 55697
		[DispId(-2147412046)]
		public virtual extern object onbeforeprint
		{
			[DispId(-2147412046)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412046)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004884 RID: 18564
		// (get) Token: 0x0600D994 RID: 55700
		// (set) Token: 0x0600D993 RID: 55699
		[DispId(-2147412045)]
		public virtual extern object onafterprint
		{
			[DispId(-2147412045)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412045)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004885 RID: 18565
		// (get) Token: 0x0600D995 RID: 55701
		[DispId(1175)]
		public virtual extern IHTMLDataTransfer clipboardData
		{
			[DispId(1175)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D996 RID: 55702
		[DispId(1176)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLWindow2 showModelessDialog([MarshalAs(19)] [In] string url = "", [MarshalAs(27)] [In] [Optional] ref object varArgIn, [MarshalAs(27)] [In] [Optional] ref object options);

		// Token: 0x0600D997 RID: 55703
		[DispId(1180)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createPopup([MarshalAs(27)] [In] [Optional] ref object varArgIn);

		// Token: 0x17004886 RID: 18566
		// (get) Token: 0x0600D998 RID: 55704
		[DispId(1181)]
		public virtual extern IHTMLFrameBase frameElement
		{
			[DispId(1181)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D999 RID: 55705
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLWindow2_item([MarshalAs(27)] [In] ref object pvarIndex);

		// Token: 0x17004887 RID: 18567
		// (get) Token: 0x0600D99A RID: 55706
		public virtual extern int IHTMLWindow2_length
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004888 RID: 18568
		// (get) Token: 0x0600D99B RID: 55707
		public virtual extern FramesCollection IHTMLWindow2_frames
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004889 RID: 18569
		// (get) Token: 0x0600D99D RID: 55709
		// (set) Token: 0x0600D99C RID: 55708
		public virtual extern string IHTMLWindow2_defaultStatus
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700488A RID: 18570
		// (get) Token: 0x0600D99F RID: 55711
		// (set) Token: 0x0600D99E RID: 55710
		public virtual extern string IHTMLWindow2_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x0600D9A0 RID: 55712
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int setTimeout([MarshalAs(19)] [In] string expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D9A1 RID: 55713
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_clearTimeout([In] int timerID);

		// Token: 0x0600D9A2 RID: 55714
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_alert([MarshalAs(19)] [In] string message = "");

		// Token: 0x0600D9A3 RID: 55715
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLWindow2_confirm([MarshalAs(19)] [In] string message = "");

		// Token: 0x0600D9A4 RID: 55716
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLWindow2_prompt([MarshalAs(19)] [In] string message = "", [MarshalAs(19)] [In] string defstr = "undefined");

		// Token: 0x1700488B RID: 18571
		// (get) Token: 0x0600D9A5 RID: 55717
		public virtual extern HTMLImageElementFactory IHTMLWindow2_Image
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700488C RID: 18572
		// (get) Token: 0x0600D9A6 RID: 55718
		public virtual extern HTMLLocation IHTMLWindow2_location
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700488D RID: 18573
		// (get) Token: 0x0600D9A7 RID: 55719
		public virtual extern HTMLHistory IHTMLWindow2_history
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D9A8 RID: 55720
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_close();

		// Token: 0x1700488E RID: 18574
		// (get) Token: 0x0600D9AA RID: 55722
		// (set) Token: 0x0600D9A9 RID: 55721
		public virtual extern object IHTMLWindow2_opener
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700488F RID: 18575
		// (get) Token: 0x0600D9AB RID: 55723
		public virtual extern HTMLNavigator IHTMLWindow2_navigator
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004890 RID: 18576
		// (get) Token: 0x0600D9AD RID: 55725
		// (set) Token: 0x0600D9AC RID: 55724
		public virtual extern string IHTMLWindow2_name
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004891 RID: 18577
		// (get) Token: 0x0600D9AE RID: 55726
		public virtual extern IHTMLWindow2 IHTMLWindow2_parent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D9AF RID: 55727
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLWindow2 IHTMLWindow2_open([MarshalAs(19)] [In] string url = "", [MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string features = "", [In] bool replace = false);

		// Token: 0x17004892 RID: 18578
		// (get) Token: 0x0600D9B0 RID: 55728
		public virtual extern IHTMLWindow2 IHTMLWindow2_self
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004893 RID: 18579
		// (get) Token: 0x0600D9B1 RID: 55729
		public virtual extern IHTMLWindow2 IHTMLWindow2_top
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004894 RID: 18580
		// (get) Token: 0x0600D9B2 RID: 55730
		public virtual extern IHTMLWindow2 IHTMLWindow2_window
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D9B3 RID: 55731
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_navigate([MarshalAs(19)] [In] string url);

		// Token: 0x17004895 RID: 18581
		// (get) Token: 0x0600D9B5 RID: 55733
		// (set) Token: 0x0600D9B4 RID: 55732
		public virtual extern object IHTMLWindow2_onfocus
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004896 RID: 18582
		// (get) Token: 0x0600D9B7 RID: 55735
		// (set) Token: 0x0600D9B6 RID: 55734
		public virtual extern object IHTMLWindow2_onblur
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004897 RID: 18583
		// (get) Token: 0x0600D9B9 RID: 55737
		// (set) Token: 0x0600D9B8 RID: 55736
		public virtual extern object IHTMLWindow2_onload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004898 RID: 18584
		// (get) Token: 0x0600D9BB RID: 55739
		// (set) Token: 0x0600D9BA RID: 55738
		public virtual extern object IHTMLWindow2_onbeforeunload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004899 RID: 18585
		// (get) Token: 0x0600D9BD RID: 55741
		// (set) Token: 0x0600D9BC RID: 55740
		public virtual extern object IHTMLWindow2_onunload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700489A RID: 18586
		// (get) Token: 0x0600D9BF RID: 55743
		// (set) Token: 0x0600D9BE RID: 55742
		public virtual extern object IHTMLWindow2_onhelp
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700489B RID: 18587
		// (get) Token: 0x0600D9C1 RID: 55745
		// (set) Token: 0x0600D9C0 RID: 55744
		public virtual extern object IHTMLWindow2_onerror
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700489C RID: 18588
		// (get) Token: 0x0600D9C3 RID: 55747
		// (set) Token: 0x0600D9C2 RID: 55746
		public virtual extern object IHTMLWindow2_onresize
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700489D RID: 18589
		// (get) Token: 0x0600D9C5 RID: 55749
		// (set) Token: 0x0600D9C4 RID: 55748
		public virtual extern object IHTMLWindow2_onscroll
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700489E RID: 18590
		// (get) Token: 0x0600D9C6 RID: 55750
		public virtual extern IHTMLDocument2 IHTMLWindow2_document
		{
			[TypeLibFunc(2)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700489F RID: 18591
		// (get) Token: 0x0600D9C7 RID: 55751
		public virtual extern IHTMLEventObj IHTMLWindow2_event
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048A0 RID: 18592
		// (get) Token: 0x0600D9C8 RID: 55752
		public virtual extern object IHTMLWindow2__newEnum
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(25)]
			get;
		}

		// Token: 0x0600D9C9 RID: 55753
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLWindow2_showModalDialog([MarshalAs(19)] [In] string dialog, [MarshalAs(27)] [In] [Optional] ref object varArgIn, [MarshalAs(27)] [In] [Optional] ref object varOptions);

		// Token: 0x0600D9CA RID: 55754
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_showHelp([MarshalAs(19)] [In] string helpURL, [MarshalAs(27)] [In] [Optional] object helpArg, [MarshalAs(19)] [In] string features = "");

		// Token: 0x170048A1 RID: 18593
		// (get) Token: 0x0600D9CB RID: 55755
		public virtual extern IHTMLScreen IHTMLWindow2_screen
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048A2 RID: 18594
		// (get) Token: 0x0600D9CC RID: 55756
		public virtual extern HTMLOptionElementFactory IHTMLWindow2_Option
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D9CD RID: 55757
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_focus();

		// Token: 0x170048A3 RID: 18595
		// (get) Token: 0x0600D9CE RID: 55758
		public virtual extern bool IHTMLWindow2_closed
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D9CF RID: 55759
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_blur();

		// Token: 0x0600D9D0 RID: 55760
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_scroll([In] int x, [In] int y);

		// Token: 0x170048A4 RID: 18596
		// (get) Token: 0x0600D9D1 RID: 55761
		public virtual extern HTMLNavigator IHTMLWindow2_clientInformation
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D9D2 RID: 55762
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int setInterval([MarshalAs(19)] [In] string expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D9D3 RID: 55763
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_clearInterval([In] int timerID);

		// Token: 0x170048A5 RID: 18597
		// (get) Token: 0x0600D9D5 RID: 55765
		// (set) Token: 0x0600D9D4 RID: 55764
		public virtual extern object IHTMLWindow2_offscreenBuffering
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600D9D6 RID: 55766
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLWindow2_execScript([MarshalAs(19)] [In] string code, [MarshalAs(19)] [In] string language = "JScript");

		// Token: 0x0600D9D7 RID: 55767
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLWindow2_toString();

		// Token: 0x0600D9D8 RID: 55768
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_scrollBy([In] int x, [In] int y);

		// Token: 0x0600D9D9 RID: 55769
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_scrollTo([In] int x, [In] int y);

		// Token: 0x0600D9DA RID: 55770
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_moveTo([In] int x, [In] int y);

		// Token: 0x0600D9DB RID: 55771
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_moveBy([In] int x, [In] int y);

		// Token: 0x0600D9DC RID: 55772
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_resizeTo([In] int x, [In] int y);

		// Token: 0x0600D9DD RID: 55773
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_resizeBy([In] int x, [In] int y);

		// Token: 0x170048A6 RID: 18598
		// (get) Token: 0x0600D9DE RID: 55774
		public virtual extern object IHTMLWindow2_external
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170048A7 RID: 18599
		// (get) Token: 0x0600D9DF RID: 55775
		public virtual extern int IHTMLWindow3_screenLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170048A8 RID: 18600
		// (get) Token: 0x0600D9E0 RID: 55776
		public virtual extern int IHTMLWindow3_screenTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D9E1 RID: 55777
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLWindow3_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600D9E2 RID: 55778
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow3_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600D9E3 RID: 55779
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int setTimeout([MarshalAs(27)] [In] ref object expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D9E4 RID: 55780
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int setInterval([MarshalAs(27)] [In] ref object expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D9E5 RID: 55781
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow3_print();

		// Token: 0x170048A9 RID: 18601
		// (get) Token: 0x0600D9E7 RID: 55783
		// (set) Token: 0x0600D9E6 RID: 55782
		public virtual extern object IHTMLWindow3_onbeforeprint
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048AA RID: 18602
		// (get) Token: 0x0600D9E9 RID: 55785
		// (set) Token: 0x0600D9E8 RID: 55784
		public virtual extern object IHTMLWindow3_onafterprint
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048AB RID: 18603
		// (get) Token: 0x0600D9EA RID: 55786
		public virtual extern IHTMLDataTransfer IHTMLWindow3_clipboardData
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D9EB RID: 55787
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLWindow2 IHTMLWindow3_showModelessDialog([MarshalAs(19)] [In] string url = "", [MarshalAs(27)] [In] [Optional] ref object varArgIn, [MarshalAs(27)] [In] [Optional] ref object options);

		// Token: 0x0600D9EC RID: 55788
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLWindow4_createPopup([MarshalAs(27)] [In] [Optional] ref object varArgIn);

		// Token: 0x170048AC RID: 18604
		// (get) Token: 0x0600D9ED RID: 55789
		public virtual extern IHTMLFrameBase IHTMLWindow4_frameElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x14001A47 RID: 6727
		// (add) Token: 0x0600D9EE RID: 55790
		// (remove) Token: 0x0600D9EF RID: 55791
		public virtual extern event HTMLWindowEvents_onloadEventHandler HTMLWindowEvents_Event_onload;

		// Token: 0x14001A48 RID: 6728
		// (add) Token: 0x0600D9F0 RID: 55792
		// (remove) Token: 0x0600D9F1 RID: 55793
		public virtual extern event HTMLWindowEvents_onunloadEventHandler HTMLWindowEvents_Event_onunload;

		// Token: 0x14001A49 RID: 6729
		// (add) Token: 0x0600D9F2 RID: 55794
		// (remove) Token: 0x0600D9F3 RID: 55795
		public virtual extern event HTMLWindowEvents_onhelpEventHandler HTMLWindowEvents_Event_onhelp;

		// Token: 0x14001A4A RID: 6730
		// (add) Token: 0x0600D9F4 RID: 55796
		// (remove) Token: 0x0600D9F5 RID: 55797
		public virtual extern event HTMLWindowEvents_onfocusEventHandler HTMLWindowEvents_Event_onfocus;

		// Token: 0x14001A4B RID: 6731
		// (add) Token: 0x0600D9F6 RID: 55798
		// (remove) Token: 0x0600D9F7 RID: 55799
		public virtual extern event HTMLWindowEvents_onblurEventHandler HTMLWindowEvents_Event_onblur;

		// Token: 0x14001A4C RID: 6732
		// (add) Token: 0x0600D9F8 RID: 55800
		// (remove) Token: 0x0600D9F9 RID: 55801
		public virtual extern event HTMLWindowEvents_onerrorEventHandler HTMLWindowEvents_Event_onerror;

		// Token: 0x14001A4D RID: 6733
		// (add) Token: 0x0600D9FA RID: 55802
		// (remove) Token: 0x0600D9FB RID: 55803
		public virtual extern event HTMLWindowEvents_onresizeEventHandler HTMLWindowEvents_Event_onresize;

		// Token: 0x14001A4E RID: 6734
		// (add) Token: 0x0600D9FC RID: 55804
		// (remove) Token: 0x0600D9FD RID: 55805
		public virtual extern event HTMLWindowEvents_onscrollEventHandler HTMLWindowEvents_Event_onscroll;

		// Token: 0x14001A4F RID: 6735
		// (add) Token: 0x0600D9FE RID: 55806
		// (remove) Token: 0x0600D9FF RID: 55807
		public virtual extern event HTMLWindowEvents_onbeforeunloadEventHandler HTMLWindowEvents_Event_onbeforeunload;

		// Token: 0x14001A50 RID: 6736
		// (add) Token: 0x0600DA00 RID: 55808
		// (remove) Token: 0x0600DA01 RID: 55809
		public virtual extern event HTMLWindowEvents_onbeforeprintEventHandler HTMLWindowEvents_Event_onbeforeprint;

		// Token: 0x14001A51 RID: 6737
		// (add) Token: 0x0600DA02 RID: 55810
		// (remove) Token: 0x0600DA03 RID: 55811
		public virtual extern event HTMLWindowEvents_onafterprintEventHandler HTMLWindowEvents_Event_onafterprint;

		// Token: 0x14001A52 RID: 6738
		// (add) Token: 0x0600DA04 RID: 55812
		// (remove) Token: 0x0600DA05 RID: 55813
		public virtual extern event HTMLWindowEvents2_onloadEventHandler HTMLWindowEvents2_Event_onload;

		// Token: 0x14001A53 RID: 6739
		// (add) Token: 0x0600DA06 RID: 55814
		// (remove) Token: 0x0600DA07 RID: 55815
		public virtual extern event HTMLWindowEvents2_onunloadEventHandler HTMLWindowEvents2_Event_onunload;

		// Token: 0x14001A54 RID: 6740
		// (add) Token: 0x0600DA08 RID: 55816
		// (remove) Token: 0x0600DA09 RID: 55817
		public virtual extern event HTMLWindowEvents2_onhelpEventHandler HTMLWindowEvents2_Event_onhelp;

		// Token: 0x14001A55 RID: 6741
		// (add) Token: 0x0600DA0A RID: 55818
		// (remove) Token: 0x0600DA0B RID: 55819
		public virtual extern event HTMLWindowEvents2_onfocusEventHandler HTMLWindowEvents2_Event_onfocus;

		// Token: 0x14001A56 RID: 6742
		// (add) Token: 0x0600DA0C RID: 55820
		// (remove) Token: 0x0600DA0D RID: 55821
		public virtual extern event HTMLWindowEvents2_onblurEventHandler HTMLWindowEvents2_Event_onblur;

		// Token: 0x14001A57 RID: 6743
		// (add) Token: 0x0600DA0E RID: 55822
		// (remove) Token: 0x0600DA0F RID: 55823
		public virtual extern event HTMLWindowEvents2_onerrorEventHandler HTMLWindowEvents2_Event_onerror;

		// Token: 0x14001A58 RID: 6744
		// (add) Token: 0x0600DA10 RID: 55824
		// (remove) Token: 0x0600DA11 RID: 55825
		public virtual extern event HTMLWindowEvents2_onresizeEventHandler HTMLWindowEvents2_Event_onresize;

		// Token: 0x14001A59 RID: 6745
		// (add) Token: 0x0600DA12 RID: 55826
		// (remove) Token: 0x0600DA13 RID: 55827
		public virtual extern event HTMLWindowEvents2_onscrollEventHandler HTMLWindowEvents2_Event_onscroll;

		// Token: 0x14001A5A RID: 6746
		// (add) Token: 0x0600DA14 RID: 55828
		// (remove) Token: 0x0600DA15 RID: 55829
		public virtual extern event HTMLWindowEvents2_onbeforeunloadEventHandler HTMLWindowEvents2_Event_onbeforeunload;

		// Token: 0x14001A5B RID: 6747
		// (add) Token: 0x0600DA16 RID: 55830
		// (remove) Token: 0x0600DA17 RID: 55831
		public virtual extern event HTMLWindowEvents2_onbeforeprintEventHandler HTMLWindowEvents2_Event_onbeforeprint;

		// Token: 0x14001A5C RID: 6748
		// (add) Token: 0x0600DA18 RID: 55832
		// (remove) Token: 0x0600DA19 RID: 55833
		public virtual extern event HTMLWindowEvents2_onafterprintEventHandler HTMLWindowEvents2_Event_onafterprint;
	}
}
