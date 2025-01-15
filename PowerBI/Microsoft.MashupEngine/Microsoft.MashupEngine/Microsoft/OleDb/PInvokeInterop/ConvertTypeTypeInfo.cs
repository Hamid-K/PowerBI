using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F51 RID: 8017
	internal class ConvertTypeTypeInfo : InterfaceTypeInfo<IConvertType>
	{
		// Token: 0x0600C430 RID: 50224 RVA: 0x00273B80 File Offset: 0x00271D80
		private static int CanConvert(IntPtr objHandle, DBTYPE wFromType, DBTYPE wToType, DBCONVERTFLAGS dwConvertFlags)
		{
			return InterfaceTypeInfo<IConvertType>.InvokeAndReturnHResult(() => InterfaceTypeInfo<IConvertType>.FromIntPtr(objHandle).CanConvert(wFromType, wToType, dwConvertFlags), objHandle);
		}

		// Token: 0x0600C431 RID: 50225 RVA: 0x00273BC6 File Offset: 0x00271DC6
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new ConvertTypeTypeInfo.CanConvertCallback(ConvertTypeTypeInfo.CanConvert)
			};
		}

		// Token: 0x02001F52 RID: 8018
		// (Invoke) Token: 0x0600C434 RID: 50228
		private delegate int CanConvertCallback(IntPtr objHandle, DBTYPE wFromType, DBTYPE wToType, DBCONVERTFLAGS dwConvertFlags);
	}
}
