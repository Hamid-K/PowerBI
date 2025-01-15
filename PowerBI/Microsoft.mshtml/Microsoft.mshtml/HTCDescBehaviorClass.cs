using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000B2 RID: 178
	[ClassInterface(0)]
	[TypeLibType(2)]
	[Guid("3050F5DD-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTCDescBehaviorClass : DispHTCDescBehavior, HTCDescBehavior, IHTCDescBehavior
	{
		// Token: 0x06000DA1 RID: 3489
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTCDescBehaviorClass();

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06000DA2 RID: 3490
		[DispId(-2147417612)]
		public virtual extern string urn
		{
			[DispId(-2147417612)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06000DA3 RID: 3491
		[DispId(-2147417611)]
		public virtual extern string name
		{
			[TypeLibFunc(4)]
			[DispId(-2147417611)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06000DA4 RID: 3492
		public virtual extern string IHTCDescBehavior_urn
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06000DA5 RID: 3493
		public virtual extern string IHTCDescBehavior_name
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
