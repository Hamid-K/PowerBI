using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200079B RID: 1947
	[ClassInterface(0)]
	[TypeLibType(2)]
	[Guid("FECEAAA3-8405-11CF-8BA1-00AA00476DA6")]
	[ComImport]
	public class HTMLHistoryClass : IOmHistory, HTMLHistory
	{
		// Token: 0x0600D4B8 RID: 54456
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLHistoryClass();

		// Token: 0x1700464D RID: 17997
		// (get) Token: 0x0600D4B9 RID: 54457
		[DispId(1)]
		public virtual extern short length
		{
			[DispId(1)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D4BA RID: 54458
		[DispId(2)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void back([MarshalAs(27)] [In] [Optional] ref object pvargdistance);

		// Token: 0x0600D4BB RID: 54459
		[DispId(3)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void forward([MarshalAs(27)] [In] [Optional] ref object pvargdistance);

		// Token: 0x0600D4BC RID: 54460
		[DispId(4)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void go([MarshalAs(27)] [In] [Optional] ref object pvargdistance);
	}
}
