using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000A6 RID: 166
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F5DE-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTCPropertyBehaviorClass : DispHTCPropertyBehavior, HTCPropertyBehavior, IHTCPropertyBehavior
	{
		// Token: 0x06000D8B RID: 3467
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTCPropertyBehaviorClass();

		// Token: 0x06000D8C RID: 3468
		[DispId(-2147417612)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void fireChange();

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06000D8E RID: 3470
		// (set) Token: 0x06000D8D RID: 3469
		[DispId(-2147412971)]
		public virtual extern object value
		{
			[DispId(-2147412971)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412971)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06000D8F RID: 3471
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTCPropertyBehavior_fireChange();

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06000D91 RID: 3473
		// (set) Token: 0x06000D90 RID: 3472
		public virtual extern object IHTCPropertyBehavior_value
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}
	}
}
