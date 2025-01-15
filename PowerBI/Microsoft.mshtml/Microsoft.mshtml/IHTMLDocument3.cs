using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007E2 RID: 2018
	[Guid("3050F485-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLDocument3
	{
		// Token: 0x0600DA66 RID: 55910
		[DispId(1072)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void releaseCapture();

		// Token: 0x0600DA67 RID: 55911
		[DispId(1073)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void recalc([In] bool fForce = false);

		// Token: 0x0600DA68 RID: 55912
		[DispId(1074)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode createTextNode([MarshalAs(19)] [In] string text);

		// Token: 0x170048AD RID: 18605
		// (get) Token: 0x0600DA69 RID: 55913
		[DispId(1075)]
		IHTMLElement documentElement
		{
			[DispId(1075)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048AE RID: 18606
		// (get) Token: 0x0600DA6A RID: 55914
		[DispId(1077)]
		string uniqueID
		{
			[DispId(1077)]
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600DA6B RID: 55915
		[DispId(-2147417605)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600DA6C RID: 55916
		[DispId(-2147417604)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170048AF RID: 18607
		// (get) Token: 0x0600DA6E RID: 55918
		// (set) Token: 0x0600DA6D RID: 55917
		[DispId(-2147412050)]
		object onrowsdelete
		{
			[DispId(-2147412050)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412050)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048B0 RID: 18608
		// (get) Token: 0x0600DA70 RID: 55920
		// (set) Token: 0x0600DA6F RID: 55919
		[DispId(-2147412049)]
		object onrowsinserted
		{
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412049)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048B1 RID: 18609
		// (get) Token: 0x0600DA72 RID: 55922
		// (set) Token: 0x0600DA71 RID: 55921
		[DispId(-2147412048)]
		object oncellchange
		{
			[DispId(-2147412048)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412048)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048B2 RID: 18610
		// (get) Token: 0x0600DA74 RID: 55924
		// (set) Token: 0x0600DA73 RID: 55923
		[DispId(-2147412072)]
		object ondatasetchanged
		{
			[DispId(-2147412072)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412072)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048B3 RID: 18611
		// (get) Token: 0x0600DA76 RID: 55926
		// (set) Token: 0x0600DA75 RID: 55925
		[DispId(-2147412071)]
		object ondataavailable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412071)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412071)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048B4 RID: 18612
		// (get) Token: 0x0600DA78 RID: 55928
		// (set) Token: 0x0600DA77 RID: 55927
		[DispId(-2147412070)]
		object ondatasetcomplete
		{
			[DispId(-2147412070)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048B5 RID: 18613
		// (get) Token: 0x0600DA7A RID: 55930
		// (set) Token: 0x0600DA79 RID: 55929
		[DispId(-2147412065)]
		object onpropertychange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048B6 RID: 18614
		// (get) Token: 0x0600DA7C RID: 55932
		// (set) Token: 0x0600DA7B RID: 55931
		[DispId(-2147412995)]
		string dir
		{
			[DispId(-2147412995)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412995)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170048B7 RID: 18615
		// (get) Token: 0x0600DA7E RID: 55934
		// (set) Token: 0x0600DA7D RID: 55933
		[DispId(-2147412047)]
		object oncontextmenu
		{
			[TypeLibFunc(20)]
			[DispId(-2147412047)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412047)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048B8 RID: 18616
		// (get) Token: 0x0600DA80 RID: 55936
		// (set) Token: 0x0600DA7F RID: 55935
		[DispId(-2147412044)]
		object onstop
		{
			[TypeLibFunc(20)]
			[DispId(-2147412044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412044)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600DA81 RID: 55937
		[DispId(1076)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDocument2 createDocumentFragment();

		// Token: 0x170048B9 RID: 18617
		// (get) Token: 0x0600DA82 RID: 55938
		[DispId(1078)]
		IHTMLDocument2 parentDocument
		{
			[TypeLibFunc(65)]
			[DispId(1078)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048BA RID: 18618
		// (get) Token: 0x0600DA84 RID: 55940
		// (set) Token: 0x0600DA83 RID: 55939
		[DispId(1079)]
		bool enableDownload
		{
			[TypeLibFunc(65)]
			[DispId(1079)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1079)]
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170048BB RID: 18619
		// (get) Token: 0x0600DA86 RID: 55942
		// (set) Token: 0x0600DA85 RID: 55941
		[DispId(1080)]
		string baseUrl
		{
			[DispId(1080)]
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1080)]
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170048BC RID: 18620
		// (get) Token: 0x0600DA87 RID: 55943
		[DispId(-2147417063)]
		object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170048BD RID: 18621
		// (get) Token: 0x0600DA89 RID: 55945
		// (set) Token: 0x0600DA88 RID: 55944
		[DispId(1082)]
		bool inheritStyleSheets
		{
			[DispId(1082)]
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1082)]
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170048BE RID: 18622
		// (get) Token: 0x0600DA8B RID: 55947
		// (set) Token: 0x0600DA8A RID: 55946
		[DispId(-2147412043)]
		object onbeforeeditfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412043)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412043)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600DA8C RID: 55948
		[DispId(1086)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElementCollection getElementsByName([MarshalAs(19)] [In] string v);

		// Token: 0x0600DA8D RID: 55949
		[DispId(1088)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement getElementById([MarshalAs(19)] [In] string v);

		// Token: 0x0600DA8E RID: 55950
		[DispId(1087)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);
	}
}
