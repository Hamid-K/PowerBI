using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020006E2 RID: 1762
	[TypeLibType(4160)]
	[Guid("3050F2B5-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLMarqueeElement
	{
		// Token: 0x1700367C RID: 13948
		// (get) Token: 0x0600A894 RID: 43156
		// (set) Token: 0x0600A893 RID: 43155
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

		// Token: 0x1700367D RID: 13949
		// (get) Token: 0x0600A896 RID: 43158
		// (set) Token: 0x0600A895 RID: 43157
		[DispId(6000)]
		int scrollDelay
		{
			[DispId(6000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(6000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700367E RID: 13950
		// (get) Token: 0x0600A898 RID: 43160
		// (set) Token: 0x0600A897 RID: 43159
		[DispId(6001)]
		string direction
		{
			[DispId(6001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(6001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700367F RID: 13951
		// (get) Token: 0x0600A89A RID: 43162
		// (set) Token: 0x0600A899 RID: 43161
		[DispId(6002)]
		string behavior
		{
			[DispId(6002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(6002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003680 RID: 13952
		// (get) Token: 0x0600A89C RID: 43164
		// (set) Token: 0x0600A89B RID: 43163
		[DispId(6003)]
		int scrollAmount
		{
			[DispId(6003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(6003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003681 RID: 13953
		// (get) Token: 0x0600A89E RID: 43166
		// (set) Token: 0x0600A89D RID: 43165
		[DispId(6004)]
		int loop
		{
			[DispId(6004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(6004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003682 RID: 13954
		// (get) Token: 0x0600A8A0 RID: 43168
		// (set) Token: 0x0600A89F RID: 43167
		[DispId(6005)]
		int vspace
		{
			[DispId(6005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(6005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003683 RID: 13955
		// (get) Token: 0x0600A8A2 RID: 43170
		// (set) Token: 0x0600A8A1 RID: 43169
		[DispId(6006)]
		int hspace
		{
			[DispId(6006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(6006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003684 RID: 13956
		// (get) Token: 0x0600A8A4 RID: 43172
		// (set) Token: 0x0600A8A3 RID: 43171
		[DispId(-2147412086)]
		object onfinish
		{
			[TypeLibFunc(20)]
			[DispId(-2147412086)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412086)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003685 RID: 13957
		// (get) Token: 0x0600A8A6 RID: 43174
		// (set) Token: 0x0600A8A5 RID: 43173
		[DispId(-2147412085)]
		object onstart
		{
			[DispId(-2147412085)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412085)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003686 RID: 13958
		// (get) Token: 0x0600A8A8 RID: 43176
		// (set) Token: 0x0600A8A7 RID: 43175
		[DispId(-2147412092)]
		object onbounce
		{
			[DispId(-2147412092)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412092)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003687 RID: 13959
		// (get) Token: 0x0600A8AA RID: 43178
		// (set) Token: 0x0600A8A9 RID: 43177
		[DispId(-2147418107)]
		object width
		{
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003688 RID: 13960
		// (get) Token: 0x0600A8AC RID: 43180
		// (set) Token: 0x0600A8AB RID: 43179
		[DispId(-2147418106)]
		object height
		{
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003689 RID: 13961
		// (get) Token: 0x0600A8AE RID: 43182
		// (set) Token: 0x0600A8AD RID: 43181
		[DispId(6007)]
		bool trueSpeed
		{
			[DispId(6007)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(6007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x0600A8AF RID: 43183
		[DispId(6010)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void Start();

		// Token: 0x0600A8B0 RID: 43184
		[DispId(6011)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void stop();
	}
}
