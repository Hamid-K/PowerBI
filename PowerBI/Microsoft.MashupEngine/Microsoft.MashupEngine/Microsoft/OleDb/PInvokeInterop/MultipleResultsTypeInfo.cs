using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F54 RID: 8020
	internal class MultipleResultsTypeInfo : InterfaceTypeInfo<IMultipleResults>
	{
		// Token: 0x0600C439 RID: 50233 RVA: 0x00273C0C File Offset: 0x00271E0C
		private unsafe static int GetResult(IntPtr objHandle, [In] IntPtr pUnkOuter, [In] IntPtr lResultFlag, [In] ref Guid riid, [Out] DBROWCOUNT* cRowsAffected, out IntPtr ppRowset)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<IMultipleResults>.ValidateHResult(InterfaceTypeInfo<IMultipleResults>.FromIntPtr(objHandle).GetResult(pUnkOuter, lResultFlag, ref riid, cRowsAffected, out ppRowset), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				ppRowset = IntPtr.Zero;
				num = InterfaceTypeInfo<IMultipleResults>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C43A RID: 50234 RVA: 0x00273C60 File Offset: 0x00271E60
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new MultipleResultsTypeInfo.GetResultCallback(MultipleResultsTypeInfo.GetResult)
			};
		}

		// Token: 0x02001F55 RID: 8021
		// (Invoke) Token: 0x0600C43D RID: 50237
		private unsafe delegate int GetResultCallback(IntPtr objHandle, [In] IntPtr pUnkOuter, [In] IntPtr lResultFlag, [In] ref Guid riid, [Out] DBROWCOUNT* cRowsAffected, out IntPtr ppRowset);
	}
}
