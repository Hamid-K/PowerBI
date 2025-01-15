using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000AC RID: 172
	[ClassInterface(0)]
	[Guid("3050F4FE-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTCEventBehaviorClass : DispHTCEventBehavior, HTCEventBehavior, IHTCEventBehavior
	{
		// Token: 0x06000D94 RID: 3476
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTCEventBehaviorClass();

		// Token: 0x06000D95 RID: 3477
		[DispId(-2147417612)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void fire([MarshalAs(28)] [In] IHTMLEventObj pVar);

		// Token: 0x06000D96 RID: 3478
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTCEventBehavior_fire([MarshalAs(28)] [In] IHTMLEventObj pVar);
	}
}
