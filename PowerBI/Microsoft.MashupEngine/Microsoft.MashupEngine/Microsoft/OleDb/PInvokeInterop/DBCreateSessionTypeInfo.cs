using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F56 RID: 8022
	internal class DBCreateSessionTypeInfo : InterfaceTypeInfo<IDBCreateSession>
	{
		// Token: 0x0600C440 RID: 50240 RVA: 0x00273C80 File Offset: 0x00271E80
		private static int CreateSession(IntPtr objHandle, IntPtr punkOuter, ref Guid iid, out IntPtr session)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<IDBCreateSession>.ValidateHResult(InterfaceTypeInfo<IDBCreateSession>.FromIntPtr(objHandle).CreateSession(punkOuter, ref iid, out session), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				session = IntPtr.Zero;
				num = InterfaceTypeInfo<IDBCreateSession>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C441 RID: 50241 RVA: 0x00273CD0 File Offset: 0x00271ED0
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new DBCreateSessionTypeInfo.CreateSessionCallback(DBCreateSessionTypeInfo.CreateSession)
			};
		}

		// Token: 0x02001F57 RID: 8023
		// (Invoke) Token: 0x0600C444 RID: 50244
		private delegate int CreateSessionCallback(IntPtr objHandle, IntPtr punkOuter, ref Guid iid, out IntPtr session);
	}
}
