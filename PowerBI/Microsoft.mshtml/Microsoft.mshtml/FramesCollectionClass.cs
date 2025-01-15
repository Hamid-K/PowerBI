using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007BC RID: 1980
	[DefaultMember("item")]
	[Guid("3050F7F6-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[ComImport]
	public class FramesCollectionClass : IHTMLFramesCollection2, FramesCollection
	{
		// Token: 0x0600D744 RID: 55108
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern FramesCollectionClass();

		// Token: 0x0600D745 RID: 55109
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object item([MarshalAs(27)] [In] ref object pvarIndex);

		// Token: 0x170047A4 RID: 18340
		// (get) Token: 0x0600D746 RID: 55110
		[DispId(1001)]
		public virtual extern int length
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
