using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F67 RID: 8039
	internal class SupportErrorInfoTypeInfo : InterfaceTypeInfo<ISupportErrorInfo>
	{
		// Token: 0x0600C47D RID: 50301 RVA: 0x0027402C File Offset: 0x0027222C
		private static int InterfaceSupportsErrorInfo(IntPtr objHandle, ref Guid iid)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<ISupportErrorInfo>.ValidateHResult(InterfaceTypeInfo<ISupportErrorInfo>.FromIntPtr(objHandle).InterfaceSupportsErrorInfo(ref iid), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				num = InterfaceTypeInfo<ISupportErrorInfo>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C47E RID: 50302 RVA: 0x00274074 File Offset: 0x00272274
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new SupportErrorInfoTypeInfo.InterfaceSupportsErrorInfoCallback(SupportErrorInfoTypeInfo.InterfaceSupportsErrorInfo)
			};
		}

		// Token: 0x02001F68 RID: 8040
		// (Invoke) Token: 0x0600C481 RID: 50305
		private delegate int InterfaceSupportsErrorInfoCallback(IntPtr objHandle, ref Guid iid);
	}
}
