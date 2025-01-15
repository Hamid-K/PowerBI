using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000004 RID: 4
	[Guid("3050F25E-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLStyle
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000072 RID: 114
		// (set) Token: 0x06000071 RID: 113
		[DispId(-2147413094)]
		string fontFamily
		{
			[TypeLibFunc(20)]
			[DispId(-2147413094)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413094)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000074 RID: 116
		// (set) Token: 0x06000073 RID: 115
		[DispId(-2147413088)]
		string fontStyle
		{
			[DispId(-2147413088)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413088)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000076 RID: 118
		// (set) Token: 0x06000075 RID: 117
		[DispId(-2147413087)]
		string fontVariant
		{
			[TypeLibFunc(20)]
			[DispId(-2147413087)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413087)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000078 RID: 120
		// (set) Token: 0x06000077 RID: 119
		[DispId(-2147413085)]
		string fontWeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413085)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413085)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600007A RID: 122
		// (set) Token: 0x06000079 RID: 121
		[DispId(-2147413093)]
		object fontSize
		{
			[DispId(-2147413093)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413093)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600007C RID: 124
		// (set) Token: 0x0600007B RID: 123
		[DispId(-2147413071)]
		string font
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413071)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413071)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600007E RID: 126
		// (set) Token: 0x0600007D RID: 125
		[DispId(-2147413110)]
		object color
		{
			[DispId(-2147413110)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413110)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000080 RID: 128
		// (set) Token: 0x0600007F RID: 127
		[DispId(-2147413080)]
		string background
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413080)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413080)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000082 RID: 130
		// (set) Token: 0x06000081 RID: 129
		[DispId(-501)]
		object backgroundColor
		{
			[TypeLibFunc(20)]
			[DispId(-501)]
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000084 RID: 132
		// (set) Token: 0x06000083 RID: 131
		[DispId(-2147413111)]
		string backgroundImage
		{
			[DispId(-2147413111)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000086 RID: 134
		// (set) Token: 0x06000085 RID: 133
		[DispId(-2147413068)]
		string backgroundRepeat
		{
			[TypeLibFunc(20)]
			[DispId(-2147413068)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413068)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000088 RID: 136
		// (set) Token: 0x06000087 RID: 135
		[DispId(-2147413067)]
		string backgroundAttachment
		{
			[DispId(-2147413067)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413067)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600008A RID: 138
		// (set) Token: 0x06000089 RID: 137
		[DispId(-2147413066)]
		string backgroundPosition
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413066)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413066)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600008C RID: 140
		// (set) Token: 0x0600008B RID: 139
		[DispId(-2147413079)]
		object backgroundPositionX
		{
			[DispId(-2147413079)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413079)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600008E RID: 142
		// (set) Token: 0x0600008D RID: 141
		[DispId(-2147413078)]
		object backgroundPositionY
		{
			[DispId(-2147413078)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413078)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000090 RID: 144
		// (set) Token: 0x0600008F RID: 143
		[DispId(-2147413065)]
		object wordSpacing
		{
			[TypeLibFunc(20)]
			[DispId(-2147413065)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413065)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000092 RID: 146
		// (set) Token: 0x06000091 RID: 145
		[DispId(-2147413104)]
		object letterSpacing
		{
			[DispId(-2147413104)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413104)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000094 RID: 148
		// (set) Token: 0x06000093 RID: 147
		[DispId(-2147413077)]
		string textDecoration
		{
			[TypeLibFunc(20)]
			[DispId(-2147413077)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413077)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000096 RID: 150
		// (set) Token: 0x06000095 RID: 149
		[DispId(-2147413089)]
		bool textDecorationNone
		{
			[TypeLibFunc(20)]
			[DispId(-2147413089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000098 RID: 152
		// (set) Token: 0x06000097 RID: 151
		[DispId(-2147413091)]
		bool textDecorationUnderline
		{
			[DispId(-2147413091)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147413091)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600009A RID: 154
		// (set) Token: 0x06000099 RID: 153
		[DispId(-2147413043)]
		bool textDecorationOverline
		{
			[TypeLibFunc(20)]
			[DispId(-2147413043)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413043)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600009C RID: 156
		// (set) Token: 0x0600009B RID: 155
		[DispId(-2147413092)]
		bool textDecorationLineThrough
		{
			[TypeLibFunc(20)]
			[DispId(-2147413092)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413092)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600009E RID: 158
		// (set) Token: 0x0600009D RID: 157
		[DispId(-2147413090)]
		bool textDecorationBlink
		{
			[TypeLibFunc(20)]
			[DispId(-2147413090)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413090)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000A0 RID: 160
		// (set) Token: 0x0600009F RID: 159
		[DispId(-2147413064)]
		object verticalAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147413064)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413064)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000A2 RID: 162
		// (set) Token: 0x060000A1 RID: 161
		[DispId(-2147413108)]
		string textTransform
		{
			[DispId(-2147413108)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000A4 RID: 164
		// (set) Token: 0x060000A3 RID: 163
		[DispId(-2147418040)]
		string textAlign
		{
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000A6 RID: 166
		// (set) Token: 0x060000A5 RID: 165
		[DispId(-2147413105)]
		object textIndent
		{
			[TypeLibFunc(20)]
			[DispId(-2147413105)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413105)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000A8 RID: 168
		// (set) Token: 0x060000A7 RID: 167
		[DispId(-2147413106)]
		object lineHeight
		{
			[DispId(-2147413106)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000AA RID: 170
		// (set) Token: 0x060000A9 RID: 169
		[DispId(-2147413075)]
		object marginTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413075)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413075)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000AC RID: 172
		// (set) Token: 0x060000AB RID: 171
		[DispId(-2147413074)]
		object marginRight
		{
			[DispId(-2147413074)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413074)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000AE RID: 174
		// (set) Token: 0x060000AD RID: 173
		[DispId(-2147413073)]
		object marginBottom
		{
			[DispId(-2147413073)]
			[TypeLibFunc(20)]
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000B0 RID: 176
		// (set) Token: 0x060000AF RID: 175
		[DispId(-2147413072)]
		object marginLeft
		{
			[DispId(-2147413072)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413072)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000B2 RID: 178
		// (set) Token: 0x060000B1 RID: 177
		[DispId(-2147413076)]
		string margin
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413076)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413076)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000B4 RID: 180
		// (set) Token: 0x060000B3 RID: 179
		[DispId(-2147413100)]
		object paddingTop
		{
			[DispId(-2147413100)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413100)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000B6 RID: 182
		// (set) Token: 0x060000B5 RID: 181
		[DispId(-2147413099)]
		object paddingRight
		{
			[DispId(-2147413099)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413099)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000B8 RID: 184
		// (set) Token: 0x060000B7 RID: 183
		[DispId(-2147413098)]
		object paddingBottom
		{
			[DispId(-2147413098)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413098)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000BA RID: 186
		// (set) Token: 0x060000B9 RID: 185
		[DispId(-2147413097)]
		object paddingLeft
		{
			[DispId(-2147413097)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413097)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000BC RID: 188
		// (set) Token: 0x060000BB RID: 187
		[DispId(-2147413101)]
		string padding
		{
			[DispId(-2147413101)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413101)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000BE RID: 190
		// (set) Token: 0x060000BD RID: 189
		[DispId(-2147413063)]
		string border
		{
			[DispId(-2147413063)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413063)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000C0 RID: 192
		// (set) Token: 0x060000BF RID: 191
		[DispId(-2147413062)]
		string borderTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413062)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413062)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000C2 RID: 194
		// (set) Token: 0x060000C1 RID: 193
		[DispId(-2147413061)]
		string borderRight
		{
			[DispId(-2147413061)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413061)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000C4 RID: 196
		// (set) Token: 0x060000C3 RID: 195
		[DispId(-2147413060)]
		string borderBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413060)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413060)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000C6 RID: 198
		// (set) Token: 0x060000C5 RID: 197
		[DispId(-2147413059)]
		string borderLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413059)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413059)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000C8 RID: 200
		// (set) Token: 0x060000C7 RID: 199
		[DispId(-2147413058)]
		string borderColor
		{
			[DispId(-2147413058)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413058)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000CA RID: 202
		// (set) Token: 0x060000C9 RID: 201
		[DispId(-2147413057)]
		object borderTopColor
		{
			[DispId(-2147413057)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413057)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000CC RID: 204
		// (set) Token: 0x060000CB RID: 203
		[DispId(-2147413056)]
		object borderRightColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413056)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413056)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000CE RID: 206
		// (set) Token: 0x060000CD RID: 205
		[DispId(-2147413055)]
		object borderBottomColor
		{
			[DispId(-2147413055)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413055)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000D0 RID: 208
		// (set) Token: 0x060000CF RID: 207
		[DispId(-2147413054)]
		object borderLeftColor
		{
			[DispId(-2147413054)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413054)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000D2 RID: 210
		// (set) Token: 0x060000D1 RID: 209
		[DispId(-2147413053)]
		string borderWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413053)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413053)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000D4 RID: 212
		// (set) Token: 0x060000D3 RID: 211
		[DispId(-2147413052)]
		object borderTopWidth
		{
			[DispId(-2147413052)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413052)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000D6 RID: 214
		// (set) Token: 0x060000D5 RID: 213
		[DispId(-2147413051)]
		object borderRightWidth
		{
			[DispId(-2147413051)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413051)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000D8 RID: 216
		// (set) Token: 0x060000D7 RID: 215
		[DispId(-2147413050)]
		object borderBottomWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413050)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413050)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000DA RID: 218
		// (set) Token: 0x060000D9 RID: 217
		[DispId(-2147413049)]
		object borderLeftWidth
		{
			[DispId(-2147413049)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413049)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000DC RID: 220
		// (set) Token: 0x060000DB RID: 219
		[DispId(-2147413048)]
		string borderStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413048)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413048)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000DE RID: 222
		// (set) Token: 0x060000DD RID: 221
		[DispId(-2147413047)]
		string borderTopStyle
		{
			[DispId(-2147413047)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413047)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000E0 RID: 224
		// (set) Token: 0x060000DF RID: 223
		[DispId(-2147413046)]
		string borderRightStyle
		{
			[DispId(-2147413046)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413046)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000E2 RID: 226
		// (set) Token: 0x060000E1 RID: 225
		[DispId(-2147413045)]
		string borderBottomStyle
		{
			[DispId(-2147413045)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413045)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000E4 RID: 228
		// (set) Token: 0x060000E3 RID: 227
		[DispId(-2147413044)]
		string borderLeftStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413044)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000E6 RID: 230
		// (set) Token: 0x060000E5 RID: 229
		[DispId(-2147418107)]
		object width
		{
			[DispId(-2147418107)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000E8 RID: 232
		// (set) Token: 0x060000E7 RID: 231
		[DispId(-2147418106)]
		object height
		{
			[TypeLibFunc(20)]
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000EA RID: 234
		// (set) Token: 0x060000E9 RID: 233
		[DispId(-2147413042)]
		string styleFloat
		{
			[TypeLibFunc(20)]
			[DispId(-2147413042)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413042)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000EC RID: 236
		// (set) Token: 0x060000EB RID: 235
		[DispId(-2147413096)]
		string clear
		{
			[DispId(-2147413096)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413096)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000EE RID: 238
		// (set) Token: 0x060000ED RID: 237
		[DispId(-2147413041)]
		string display
		{
			[TypeLibFunc(20)]
			[DispId(-2147413041)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413041)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060000F0 RID: 240
		// (set) Token: 0x060000EF RID: 239
		[DispId(-2147413032)]
		string visibility
		{
			[DispId(-2147413032)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413032)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060000F2 RID: 242
		// (set) Token: 0x060000F1 RID: 241
		[DispId(-2147413040)]
		string listStyleType
		{
			[DispId(-2147413040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060000F4 RID: 244
		// (set) Token: 0x060000F3 RID: 243
		[DispId(-2147413039)]
		string listStylePosition
		{
			[DispId(-2147413039)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413039)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060000F6 RID: 246
		// (set) Token: 0x060000F5 RID: 245
		[DispId(-2147413038)]
		string listStyleImage
		{
			[DispId(-2147413038)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413038)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060000F8 RID: 248
		// (set) Token: 0x060000F7 RID: 247
		[DispId(-2147413037)]
		string listStyle
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413037)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413037)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060000FA RID: 250
		// (set) Token: 0x060000F9 RID: 249
		[DispId(-2147413036)]
		string whiteSpace
		{
			[DispId(-2147413036)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413036)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060000FC RID: 252
		// (set) Token: 0x060000FB RID: 251
		[DispId(-2147418108)]
		object top
		{
			[DispId(-2147418108)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060000FE RID: 254
		// (set) Token: 0x060000FD RID: 253
		[DispId(-2147418109)]
		object left
		{
			[TypeLibFunc(20)]
			[DispId(-2147418109)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418109)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060000FF RID: 255
		[DispId(-2147413022)]
		string position
		{
			[TypeLibFunc(20)]
			[DispId(-2147413022)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000101 RID: 257
		// (set) Token: 0x06000100 RID: 256
		[DispId(-2147413021)]
		object zIndex
		{
			[TypeLibFunc(20)]
			[DispId(-2147413021)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413021)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000103 RID: 259
		// (set) Token: 0x06000102 RID: 258
		[DispId(-2147413102)]
		string overflow
		{
			[DispId(-2147413102)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413102)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000105 RID: 261
		// (set) Token: 0x06000104 RID: 260
		[DispId(-2147413035)]
		string pageBreakBefore
		{
			[DispId(-2147413035)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413035)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000107 RID: 263
		// (set) Token: 0x06000106 RID: 262
		[DispId(-2147413034)]
		string pageBreakAfter
		{
			[TypeLibFunc(20)]
			[DispId(-2147413034)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413034)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000109 RID: 265
		// (set) Token: 0x06000108 RID: 264
		[DispId(-2147413013)]
		string cssText
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600010B RID: 267
		// (set) Token: 0x0600010A RID: 266
		[DispId(-2147414112)]
		int pixelTop
		{
			[DispId(-2147414112)]
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600010D RID: 269
		// (set) Token: 0x0600010C RID: 268
		[DispId(-2147414111)]
		int pixelLeft
		{
			[DispId(-2147414111)]
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600010F RID: 271
		// (set) Token: 0x0600010E RID: 270
		[DispId(-2147414110)]
		int pixelWidth
		{
			[TypeLibFunc(84)]
			[DispId(-2147414110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147414110)]
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000111 RID: 273
		// (set) Token: 0x06000110 RID: 272
		[DispId(-2147414109)]
		int pixelHeight
		{
			[DispId(-2147414109)]
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147414109)]
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000113 RID: 275
		// (set) Token: 0x06000112 RID: 274
		[DispId(-2147414108)]
		float posTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147414108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147414108)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000115 RID: 277
		// (set) Token: 0x06000114 RID: 276
		[DispId(-2147414107)]
		float posLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147414107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147414107)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000117 RID: 279
		// (set) Token: 0x06000116 RID: 278
		[DispId(-2147414106)]
		float posWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147414106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147414106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000119 RID: 281
		// (set) Token: 0x06000118 RID: 280
		[DispId(-2147414105)]
		float posHeight
		{
			[DispId(-2147414105)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147414105)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600011B RID: 283
		// (set) Token: 0x0600011A RID: 282
		[DispId(-2147413010)]
		string cursor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600011D RID: 285
		// (set) Token: 0x0600011C RID: 284
		[DispId(-2147413020)]
		string clip
		{
			[TypeLibFunc(20)]
			[DispId(-2147413020)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413020)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600011F RID: 287
		// (set) Token: 0x0600011E RID: 286
		[DispId(-2147413030)]
		string filter
		{
			[DispId(-2147413030)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413030)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x06000120 RID: 288
		[DispId(-2147417611)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06000121 RID: 289
		[DispId(-2147417610)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06000122 RID: 290
		[DispId(-2147417609)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x06000123 RID: 291
		[DispId(-2147414104)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();
	}
}
