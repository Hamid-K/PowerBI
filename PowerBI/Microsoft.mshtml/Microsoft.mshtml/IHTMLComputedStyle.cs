using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CB6 RID: 3254
	[InterfaceType(1)]
	[Guid("3050F6C3-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLComputedStyle
	{
		// Token: 0x1700759F RID: 30111
		// (get) Token: 0x06016296 RID: 90774
		[DispId(1001)]
		bool bold
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075A0 RID: 30112
		// (get) Token: 0x06016297 RID: 90775
		[DispId(1002)]
		bool italic
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075A1 RID: 30113
		// (get) Token: 0x06016298 RID: 90776
		[DispId(1003)]
		bool underline
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075A2 RID: 30114
		// (get) Token: 0x06016299 RID: 90777
		[DispId(1004)]
		bool overline
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075A3 RID: 30115
		// (get) Token: 0x0601629A RID: 90778
		[DispId(1005)]
		bool strikeOut
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075A4 RID: 30116
		// (get) Token: 0x0601629B RID: 90779
		[DispId(1006)]
		bool subScript
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075A5 RID: 30117
		// (get) Token: 0x0601629C RID: 90780
		[DispId(1007)]
		bool superScript
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075A6 RID: 30118
		// (get) Token: 0x0601629D RID: 90781
		[DispId(1008)]
		bool explicitFace
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075A7 RID: 30119
		// (get) Token: 0x0601629E RID: 90782
		[DispId(1009)]
		int fontWeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075A8 RID: 30120
		// (get) Token: 0x0601629F RID: 90783
		[DispId(1010)]
		int fontSize
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075A9 RID: 30121
		// (get) Token: 0x060162A0 RID: 90784
		[DispId(1011)]
		ushort fontName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075AA RID: 30122
		// (get) Token: 0x060162A1 RID: 90785
		[DispId(1012)]
		bool hasBgColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075AB RID: 30123
		// (get) Token: 0x060162A2 RID: 90786
		[DispId(1013)]
		uint textColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075AC RID: 30124
		// (get) Token: 0x060162A3 RID: 90787
		[DispId(1014)]
		uint backgroundColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075AD RID: 30125
		// (get) Token: 0x060162A4 RID: 90788
		[DispId(1015)]
		bool preFormatted
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075AE RID: 30126
		// (get) Token: 0x060162A5 RID: 90789
		[DispId(1016)]
		bool direction
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075AF RID: 30127
		// (get) Token: 0x060162A6 RID: 90790
		[DispId(1017)]
		bool blockDirection
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170075B0 RID: 30128
		// (get) Token: 0x060162A7 RID: 90791
		[DispId(1018)]
		bool OL
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060162A8 RID: 90792
		[MethodImpl(4096, MethodCodeType = 3)]
		void isEqual([MarshalAs(28)] [In] IHTMLComputedStyle pComputedStyle, out bool pfEqual);
	}
}
