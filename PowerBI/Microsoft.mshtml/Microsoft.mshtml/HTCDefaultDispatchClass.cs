using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000A3 RID: 163
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F4FC-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTCDefaultDispatchClass : DispHTCDefaultDispatch, HTCDefaultDispatch, IHTCDefaultDispatch
	{
		// Token: 0x06000D7F RID: 3455
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTCDefaultDispatchClass();

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06000D80 RID: 3456
		[DispId(-2147412969)]
		public virtual extern IHTMLElement element
		{
			[DispId(-2147412969)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06000D81 RID: 3457
		[DispId(-2147412968)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLEventObj CreateEventObject();

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06000D82 RID: 3458
		[DispId(-2147412947)]
		public virtual extern object defaults
		{
			[DispId(-2147412947)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06000D83 RID: 3459
		[DispId(-2147412970)]
		public virtual extern object document
		{
			[DispId(-2147412970)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06000D84 RID: 3460
		public virtual extern IHTMLElement IHTCDefaultDispatch_element
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06000D85 RID: 3461
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLEventObj IHTCDefaultDispatch_CreateEventObject();

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06000D86 RID: 3462
		public virtual extern object IHTCDefaultDispatch_defaults
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06000D87 RID: 3463
		public virtual extern object IHTCDefaultDispatch_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}
	}
}
