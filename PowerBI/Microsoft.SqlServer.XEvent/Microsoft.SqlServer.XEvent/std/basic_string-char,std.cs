using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace std
{
	// Token: 0x02000062 RID: 98
	[UnsafeValueType]
	[NativeCppClass]
	[StructLayout(LayoutKind.Sequential, Size = 32)]
	internal struct basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00003900 File Offset: 0x00003900
		public unsafe static void <MarshalCopy>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_1)
		{
			if (A_0 != null)
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_0, A_1);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00003920 File Offset: 0x00003920
		public unsafe static void <MarshalDestroy>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(A_0);
		}

		// Token: 0x04000153 RID: 339
		private long <alignment\u0020member>;
	}
}
