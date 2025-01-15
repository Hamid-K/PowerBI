using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F4C RID: 8012
	internal class CommandTypeInfo : InterfaceTypeInfo<ICommand>
	{
		// Token: 0x0600C41D RID: 50205 RVA: 0x00273A5C File Offset: 0x00271C5C
		private static int Cancel(IntPtr objHandle)
		{
			return InterfaceTypeInfo<ICommand>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<ICommand>.FromIntPtr(objHandle).Cancel();
			}, objHandle);
		}

		// Token: 0x0600C41E RID: 50206 RVA: 0x00273A90 File Offset: 0x00271C90
		private unsafe static int Execute(IntPtr objHandle, IntPtr pUnkOuter, ref Guid iid, DBPARAMS* pParams, DBROWCOUNT* cRowsAffected, out IntPtr ppv)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<ICommand>.ValidateHResult(InterfaceTypeInfo<ICommand>.FromIntPtr(objHandle).Execute(pUnkOuter, ref iid, pParams, cRowsAffected, out ppv), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				ppv = IntPtr.Zero;
				num = InterfaceTypeInfo<ICommand>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C41F RID: 50207 RVA: 0x00273AE4 File Offset: 0x00271CE4
		private static int GetDBSession(IntPtr objHandle, ref Guid iid, out IntPtr session)
		{
			try
			{
				InterfaceTypeInfo<ICommand>.FromIntPtr(objHandle).GetDBSession(ref iid, out session);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				session = IntPtr.Zero;
				return InterfaceTypeInfo<ICommand>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C420 RID: 50208 RVA: 0x00273B30 File Offset: 0x00271D30
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new CommandTypeInfo.CancelCallback(CommandTypeInfo.Cancel),
				new CommandTypeInfo.ExecuteCallback(CommandTypeInfo.Execute),
				new CommandTypeInfo.GetDBSessionCallback(CommandTypeInfo.GetDBSession)
			};
		}

		// Token: 0x02001F4D RID: 8013
		// (Invoke) Token: 0x0600C423 RID: 50211
		private delegate int CancelCallback(IntPtr objHandle);

		// Token: 0x02001F4E RID: 8014
		// (Invoke) Token: 0x0600C427 RID: 50215
		private unsafe delegate int ExecuteCallback(IntPtr objHandle, IntPtr pUnkOuter, ref Guid iid, DBPARAMS* pParams, DBROWCOUNT* cRowsAffected, out IntPtr ppv);

		// Token: 0x02001F4F RID: 8015
		// (Invoke) Token: 0x0600C42B RID: 50219
		private delegate int GetDBSessionCallback(IntPtr objHandle, ref Guid iid, out IntPtr session);
	}
}
