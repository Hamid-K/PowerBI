using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ATL
{
	// Token: 0x02000025 RID: 37
	[NativeCppClass]
	[StructLayout(LayoutKind.Sequential, Size = 8)]
	internal struct CComBSTR
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000240C File Offset: 0x0000180C
		public unsafe static void <MarshalCopy>(CComBSTR* A_0, CComBSTR* A_1)
		{
			if (A_0 != null)
			{
				*(long*)A_0 = *(long*)A_1;
				*(long*)A_1 = 0L;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000242C File Offset: 0x0000182C
		public unsafe static void <MarshalDestroy>(CComBSTR* A_0)
		{
			<Module>.SysFreeString(*(long*)A_0);
		}

		// Token: 0x04000128 RID: 296
		private long <alignment\u0020member>;
	}
}
