using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000309 RID: 777
	[TypeLibType(4160)]
	[Guid("3050F1D8-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLBodyElement
	{
		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06002F34 RID: 12084
		// (set) Token: 0x06002F33 RID: 12083
		[DispId(-2147413111)]
		string background
		{
			[DispId(-2147413111)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413111)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06002F36 RID: 12086
		// (set) Token: 0x06002F35 RID: 12085
		[DispId(-2147413067)]
		string bgProperties
		{
			[TypeLibFunc(20)]
			[DispId(-2147413067)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413067)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06002F38 RID: 12088
		// (set) Token: 0x06002F37 RID: 12087
		[DispId(-2147413072)]
		object leftMargin
		{
			[DispId(-2147413072)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413072)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06002F3A RID: 12090
		// (set) Token: 0x06002F39 RID: 12089
		[DispId(-2147413075)]
		object topMargin
		{
			[TypeLibFunc(20)]
			[DispId(-2147413075)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413075)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06002F3C RID: 12092
		// (set) Token: 0x06002F3B RID: 12091
		[DispId(-2147413074)]
		object rightMargin
		{
			[DispId(-2147413074)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413074)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06002F3E RID: 12094
		// (set) Token: 0x06002F3D RID: 12093
		[DispId(-2147413073)]
		object bottomMargin
		{
			[TypeLibFunc(20)]
			[DispId(-2147413073)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413073)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06002F40 RID: 12096
		// (set) Token: 0x06002F3F RID: 12095
		[DispId(-2147413107)]
		bool noWrap
		{
			[TypeLibFunc(20)]
			[DispId(-2147413107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06002F42 RID: 12098
		// (set) Token: 0x06002F41 RID: 12097
		[DispId(-501)]
		object bgColor
		{
			[DispId(-501)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06002F44 RID: 12100
		// (set) Token: 0x06002F43 RID: 12099
		[DispId(-2147413110)]
		object text
		{
			[DispId(-2147413110)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06002F46 RID: 12102
		// (set) Token: 0x06002F45 RID: 12101
		[DispId(2010)]
		object link
		{
			[DispId(2010)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2010)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06002F48 RID: 12104
		// (set) Token: 0x06002F47 RID: 12103
		[DispId(2012)]
		object vLink
		{
			[TypeLibFunc(20)]
			[DispId(2012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2012)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06002F4A RID: 12106
		// (set) Token: 0x06002F49 RID: 12105
		[DispId(2011)]
		object aLink
		{
			[DispId(2011)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2011)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06002F4C RID: 12108
		// (set) Token: 0x06002F4B RID: 12107
		[DispId(-2147412080)]
		object onload
		{
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06002F4E RID: 12110
		// (set) Token: 0x06002F4D RID: 12109
		[DispId(-2147412079)]
		object onunload
		{
			[DispId(-2147412079)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412079)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06002F50 RID: 12112
		// (set) Token: 0x06002F4F RID: 12111
		[DispId(-2147413033)]
		string scroll
		{
			[DispId(-2147413033)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413033)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06002F52 RID: 12114
		// (set) Token: 0x06002F51 RID: 12113
		[DispId(-2147412102)]
		object onselect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412102)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412102)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06002F54 RID: 12116
		// (set) Token: 0x06002F53 RID: 12115
		[DispId(-2147412073)]
		object onbeforeunload
		{
			[TypeLibFunc(20)]
			[DispId(-2147412073)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412073)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06002F55 RID: 12117
		[DispId(2013)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLTxtRange createTextRange();
	}
}
