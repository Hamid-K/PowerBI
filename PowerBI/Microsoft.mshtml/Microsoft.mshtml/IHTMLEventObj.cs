using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000002 RID: 2
	[Guid("3050F32D-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLEventObj
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		[DispId(1001)]
		IHTMLElement srcElement
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		[DispId(1002)]
		bool altKey
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		[DispId(1003)]
		bool ctrlKey
		{
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4
		[DispId(1004)]
		bool shiftKey
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6
		// (set) Token: 0x06000005 RID: 5
		[DispId(1007)]
		object returnValue
		{
			[DispId(1007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8
		// (set) Token: 0x06000007 RID: 7
		[DispId(1008)]
		bool cancelBubble
		{
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9
		[DispId(1009)]
		IHTMLElement fromElement
		{
			[DispId(1009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10
		[DispId(1010)]
		IHTMLElement toElement
		{
			[DispId(1010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12
		// (set) Token: 0x0600000B RID: 11
		[DispId(1011)]
		int keyCode
		{
			[DispId(1011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000D RID: 13
		[DispId(1012)]
		int button
		{
			[DispId(1012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000E RID: 14
		[DispId(1013)]
		string type
		{
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000F RID: 15
		[DispId(1014)]
		string qualifier
		{
			[DispId(1014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000010 RID: 16
		[DispId(1015)]
		int reason
		{
			[DispId(1015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000011 RID: 17
		[DispId(1005)]
		int x
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000012 RID: 18
		[DispId(1006)]
		int y
		{
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000013 RID: 19
		[DispId(1020)]
		int clientX
		{
			[DispId(1020)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000014 RID: 20
		[DispId(1021)]
		int clientY
		{
			[DispId(1021)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000015 RID: 21
		[DispId(1022)]
		int offsetX
		{
			[DispId(1022)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000016 RID: 22
		[DispId(1023)]
		int offsetY
		{
			[DispId(1023)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000017 RID: 23
		[DispId(1024)]
		int screenX
		{
			[DispId(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000018 RID: 24
		[DispId(1025)]
		int screenY
		{
			[DispId(1025)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000019 RID: 25
		[DispId(1026)]
		object srcFilter
		{
			[DispId(1026)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}
	}
}
