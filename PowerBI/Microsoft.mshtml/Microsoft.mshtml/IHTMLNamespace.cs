using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CD5 RID: 3285
	[TypeLibType(4160)]
	[Guid("3050F6BB-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLNamespace
	{
		// Token: 0x170075B9 RID: 30137
		// (get) Token: 0x06016335 RID: 90933
		[DispId(1000)]
		string name
		{
			[DispId(1000)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170075BA RID: 30138
		// (get) Token: 0x06016336 RID: 90934
		[DispId(1001)]
		string urn
		{
			[TypeLibFunc(4)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170075BB RID: 30139
		// (get) Token: 0x06016337 RID: 90935
		[DispId(1002)]
		object tagNames
		{
			[DispId(1002)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170075BC RID: 30140
		// (get) Token: 0x06016338 RID: 90936
		[DispId(-2147412996)]
		object readyState
		{
			[DispId(-2147412996)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170075BD RID: 30141
		// (get) Token: 0x0601633A RID: 90938
		// (set) Token: 0x06016339 RID: 90937
		[DispId(-2147412087)]
		object onreadystatechange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0601633B RID: 90939
		[DispId(1003)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void doImport([MarshalAs(19)] [In] string bstrImplementationUrl);

		// Token: 0x0601633C RID: 90940
		[DispId(-2147417605)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0601633D RID: 90941
		[DispId(-2147417604)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);
	}
}
