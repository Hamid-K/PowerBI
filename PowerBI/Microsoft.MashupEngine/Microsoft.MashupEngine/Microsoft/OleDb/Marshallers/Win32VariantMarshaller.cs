using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.Marshallers
{
	// Token: 0x02001FD6 RID: 8150
	public class Win32VariantMarshaller : IVariantMarshaller, IMarshaller<object>, IMarshaller
	{
		// Token: 0x1700303C RID: 12348
		// (get) Token: 0x0600C716 RID: 50966 RVA: 0x0027A144 File Offset: 0x00278344
		public unsafe int NativeSizeInBytes
		{
			get
			{
				return sizeof(VARIANT);
			}
		}

		// Token: 0x1700303D RID: 12349
		// (get) Token: 0x0600C717 RID: 50967 RVA: 0x001422C0 File Offset: 0x001404C0
		public VARTYPE Type
		{
			get
			{
				return VARTYPE.VARIANT;
			}
		}

		// Token: 0x0600C718 RID: 50968 RVA: 0x0027A6D0 File Offset: 0x002788D0
		public object GetManaged(IntPtr variant)
		{
			return Marshal.GetObjectForNativeVariant(variant);
		}

		// Token: 0x0600C719 RID: 50969 RVA: 0x0027A6D8 File Offset: 0x002788D8
		public void GetNative(object obj, IntPtr variant)
		{
			Marshal.GetNativeVariantForObject(obj, variant);
		}

		// Token: 0x0600C71A RID: 50970 RVA: 0x00002139 File Offset: 0x00000339
		public bool CanHandle(VARTYPE variantType)
		{
			return true;
		}

		// Token: 0x0600C71B RID: 50971 RVA: 0x0027A3F4 File Offset: 0x002785F4
		public unsafe void Cleanup(IntPtr variant)
		{
			Variant.FreeAllocatedMemory((VARIANT*)(void*)variant);
		}
	}
}
