using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007B8 RID: 1976
	[Guid("3050F4AE-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLWindow3
	{
		// Token: 0x17004791 RID: 18321
		// (get) Token: 0x0600D71F RID: 55071
		[DispId(1170)]
		int screenLeft
		{
			[DispId(1170)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004792 RID: 18322
		// (get) Token: 0x0600D720 RID: 55072
		[DispId(1171)]
		int screenTop
		{
			[DispId(1171)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D721 RID: 55073
		[DispId(-2147417605)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600D722 RID: 55074
		[DispId(-2147417604)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600D723 RID: 55075
		[DispId(1103)]
		[MethodImpl(4096, MethodCodeType = 3)]
		int setTimeout([MarshalAs(27)] [In] ref object expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D724 RID: 55076
		[DispId(1162)]
		[MethodImpl(4096, MethodCodeType = 3)]
		int setInterval([MarshalAs(27)] [In] ref object expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D725 RID: 55077
		[DispId(1174)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void print();

		// Token: 0x17004793 RID: 18323
		// (get) Token: 0x0600D727 RID: 55079
		// (set) Token: 0x0600D726 RID: 55078
		[DispId(-2147412046)]
		object onbeforeprint
		{
			[DispId(-2147412046)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412046)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004794 RID: 18324
		// (get) Token: 0x0600D729 RID: 55081
		// (set) Token: 0x0600D728 RID: 55080
		[DispId(-2147412045)]
		object onafterprint
		{
			[TypeLibFunc(20)]
			[DispId(-2147412045)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412045)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004795 RID: 18325
		// (get) Token: 0x0600D72A RID: 55082
		[DispId(1175)]
		IHTMLDataTransfer clipboardData
		{
			[DispId(1175)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D72B RID: 55083
		[DispId(1176)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLWindow2 showModelessDialog([MarshalAs(19)] [In] string url = "", [MarshalAs(27)] [In] [Optional] ref object varArgIn, [MarshalAs(27)] [In] [Optional] ref object options);
	}
}
