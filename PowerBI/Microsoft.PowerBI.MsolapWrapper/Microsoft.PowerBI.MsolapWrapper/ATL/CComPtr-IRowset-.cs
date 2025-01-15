using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ATL
{
	// Token: 0x02000061 RID: 97
	[NativeCppClass]
	[StructLayout(LayoutKind.Sequential, Size = 8)]
	internal struct CComPtr<IRowset>
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00006AE4 File Offset: 0x00005EE4
		public unsafe static void <MarshalCopy>(CComPtr<IRowset>* A_0, CComPtr<IRowset>* A_1)
		{
			if (A_0 != null)
			{
				*(long*)A_0 = 0L;
				try
				{
					IRowset* ptr = *(long*)A_1;
					*(long*)A_1 = *(long*)A_0;
					*(long*)A_0 = ptr;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtrBase<IRowset>.{dtor}), (void*)A_0);
					throw;
				}
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00002758 File Offset: 0x00001B58
		public unsafe static void <MarshalDestroy>(CComPtr<IRowset>* A_0)
		{
			ulong num = (ulong)(*(long*)A_0);
			if (num != 0UL)
			{
				ulong num2 = num;
				uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
			}
		}

		// Token: 0x0400017C RID: 380
		private long <alignment\u0020member>;
	}
}
