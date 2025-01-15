using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F45 RID: 8005
	internal class CommandTextTypeInfo : InterfaceTypeInfo<ICommandText>
	{
		// Token: 0x0600C400 RID: 50176 RVA: 0x0027387C File Offset: 0x00271A7C
		private static int Cancel(IntPtr objHandle)
		{
			return InterfaceTypeInfo<ICommandText>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<ICommandText>.FromIntPtr(objHandle).Cancel();
			}, objHandle);
		}

		// Token: 0x0600C401 RID: 50177 RVA: 0x002738B0 File Offset: 0x00271AB0
		private unsafe static int Execute(IntPtr objHandle, IntPtr pUnkOuter, ref Guid iid, DBPARAMS* pParams, DBROWCOUNT* cRowsAffected, out IntPtr ppv)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<ICommandText>.ValidateHResult(InterfaceTypeInfo<ICommandText>.FromIntPtr(objHandle).Execute(pUnkOuter, ref iid, pParams, cRowsAffected, out ppv), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				ppv = IntPtr.Zero;
				num = InterfaceTypeInfo<ICommandText>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C402 RID: 50178 RVA: 0x00273904 File Offset: 0x00271B04
		private static int GetDBSession(IntPtr objHandle, ref Guid iid, out IntPtr session)
		{
			try
			{
				InterfaceTypeInfo<ICommandText>.FromIntPtr(objHandle).GetDBSession(ref iid, out session);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				session = IntPtr.Zero;
				return InterfaceTypeInfo<ICommandText>.ValidateException(ex, objHandle);
			}
			return 0;
		}

		// Token: 0x0600C403 RID: 50179 RVA: 0x00273950 File Offset: 0x00271B50
		private unsafe static int GetCommandText(IntPtr objHandle, Guid* pguidDialect, out char* command)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<ICommandText>.ValidateHResult(InterfaceTypeInfo<ICommandText>.FromIntPtr(objHandle).GetCommandText(pguidDialect, out command), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				command = (IntPtr)((UIntPtr)0);
				num = InterfaceTypeInfo<ICommandText>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C404 RID: 50180 RVA: 0x0027399C File Offset: 0x00271B9C
		private unsafe static int SetCommandText(IntPtr objHandle, ref Guid guidDialect, char* command)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<ICommandText>.ValidateHResult(InterfaceTypeInfo<ICommandText>.FromIntPtr(objHandle).SetCommandText(ref guidDialect, command), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				num = InterfaceTypeInfo<ICommandText>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C405 RID: 50181 RVA: 0x002739E4 File Offset: 0x00271BE4
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new CommandTextTypeInfo.CancelCallback(CommandTextTypeInfo.Cancel),
				new CommandTextTypeInfo.ExecuteCallback(CommandTextTypeInfo.Execute),
				new CommandTextTypeInfo.GetDBSessionCallback(CommandTextTypeInfo.GetDBSession),
				new CommandTextTypeInfo.GetCommandTextCallback(CommandTextTypeInfo.GetCommandText),
				new CommandTextTypeInfo.SetCommandTextCallback(CommandTextTypeInfo.SetCommandText)
			};
		}

		// Token: 0x02001F46 RID: 8006
		// (Invoke) Token: 0x0600C408 RID: 50184
		private delegate int CancelCallback(IntPtr objHandle);

		// Token: 0x02001F47 RID: 8007
		// (Invoke) Token: 0x0600C40C RID: 50188
		private unsafe delegate int ExecuteCallback(IntPtr objHandle, IntPtr pUnkOuter, ref Guid iid, DBPARAMS* pParams, DBROWCOUNT* cRowsAffected, out IntPtr ppv);

		// Token: 0x02001F48 RID: 8008
		// (Invoke) Token: 0x0600C410 RID: 50192
		private delegate int GetDBSessionCallback(IntPtr objHandle, ref Guid iid, out IntPtr session);

		// Token: 0x02001F49 RID: 8009
		// (Invoke) Token: 0x0600C414 RID: 50196
		private unsafe delegate int GetCommandTextCallback(IntPtr objHandle, Guid* pguidDialect, out char* command);

		// Token: 0x02001F4A RID: 8010
		// (Invoke) Token: 0x0600C418 RID: 50200
		private unsafe delegate int SetCommandTextCallback(IntPtr objHandle, ref Guid guidDialect, char* command);
	}
}
