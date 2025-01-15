using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CD7 RID: 3287
	[ComSourceInterfaces("mshtml.HTMLNamespaceEvents\0\0")]
	[ClassInterface(0)]
	[Guid("3050F6BC-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLNamespaceClass : IHTMLNamespace, HTMLNamespace, HTMLNamespaceEvents_Event
	{
		// Token: 0x06016341 RID: 90945
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLNamespaceClass();

		// Token: 0x170075BF RID: 30143
		// (get) Token: 0x06016342 RID: 90946
		[DispId(1000)]
		public virtual extern string name
		{
			[DispId(1000)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170075C0 RID: 30144
		// (get) Token: 0x06016343 RID: 90947
		[DispId(1001)]
		public virtual extern string urn
		{
			[DispId(1001)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170075C1 RID: 30145
		// (get) Token: 0x06016344 RID: 90948
		[DispId(1002)]
		public virtual extern object tagNames
		{
			[TypeLibFunc(4)]
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170075C2 RID: 30146
		// (get) Token: 0x06016345 RID: 90949
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[TypeLibFunc(4)]
			[DispId(-2147412996)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170075C3 RID: 30147
		// (get) Token: 0x06016347 RID: 90951
		// (set) Token: 0x06016346 RID: 90950
		[DispId(-2147412087)]
		public virtual extern object onreadystatechange
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

		// Token: 0x06016348 RID: 90952
		[DispId(1003)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void doImport([MarshalAs(19)] [In] string bstrImplementationUrl);

		// Token: 0x06016349 RID: 90953
		[DispId(-2147417605)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0601634A RID: 90954
		[DispId(-2147417604)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x14002B06 RID: 11014
		// (add) Token: 0x0601634B RID: 90955
		// (remove) Token: 0x0601634C RID: 90956
		public virtual extern event HTMLNamespaceEvents_onreadystatechangeEventHandler HTMLNamespaceEvents_Event_onreadystatechange;
	}
}
