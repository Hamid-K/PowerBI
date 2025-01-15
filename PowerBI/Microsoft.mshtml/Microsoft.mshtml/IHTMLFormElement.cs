using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020001E2 RID: 482
	[TypeLibType(4160)]
	[Guid("3050F1F7-98B5-11CF-BB82-00AA00BDCE0B")]
	[DefaultMember("item")]
	[ComImport]
	public interface IHTMLFormElement : IEnumerable
	{
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06001BD5 RID: 7125
		// (set) Token: 0x06001BD4 RID: 7124
		[DispId(1001)]
		string action
		{
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06001BD7 RID: 7127
		// (set) Token: 0x06001BD6 RID: 7126
		[DispId(-2147412995)]
		string dir
		{
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06001BD9 RID: 7129
		// (set) Token: 0x06001BD8 RID: 7128
		[DispId(1003)]
		string encoding
		{
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06001BDB RID: 7131
		// (set) Token: 0x06001BDA RID: 7130
		[DispId(1004)]
		string method
		{
			[DispId(1004)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06001BDC RID: 7132
		[DispId(1005)]
		object elements
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06001BDE RID: 7134
		// (set) Token: 0x06001BDD RID: 7133
		[DispId(1006)]
		string target
		{
			[TypeLibFunc(20)]
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1006)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06001BE0 RID: 7136
		// (set) Token: 0x06001BDF RID: 7135
		[DispId(-2147418112)]
		string name
		{
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06001BE2 RID: 7138
		// (set) Token: 0x06001BE1 RID: 7137
		[DispId(-2147412101)]
		object onsubmit
		{
			[DispId(-2147412101)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412101)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06001BE4 RID: 7140
		// (set) Token: 0x06001BE3 RID: 7139
		[DispId(-2147412100)]
		object onreset
		{
			[TypeLibFunc(20)]
			[DispId(-2147412100)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412100)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06001BE5 RID: 7141
		[DispId(1009)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void submit();

		// Token: 0x06001BE6 RID: 7142
		[DispId(1010)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void reset();

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06001BE8 RID: 7144
		// (set) Token: 0x06001BE7 RID: 7143
		[DispId(1500)]
		int length
		{
			[DispId(1500)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1500)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06001BE9 RID: 7145
		[DispId(-4)]
		[TypeLibFunc(65)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x06001BEA RID: 7146
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x06001BEB RID: 7147
		[DispId(1502)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object tags([MarshalAs(27)] [In] object tagName);
	}
}
