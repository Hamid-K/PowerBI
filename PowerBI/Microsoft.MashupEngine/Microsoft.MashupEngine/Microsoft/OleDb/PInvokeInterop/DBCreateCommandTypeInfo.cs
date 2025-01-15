using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F95 RID: 8085
	internal class DBCreateCommandTypeInfo : InterfaceTypeInfo<IDBCreateCommand>
	{
		// Token: 0x0600C530 RID: 50480 RVA: 0x00274E9C File Offset: 0x0027309C
		private static int CreateCommand(IntPtr objHandle, IntPtr punkOuter, ref Guid iid, out IntPtr command)
		{
			int num;
			try
			{
				num = InterfaceTypeInfo<IDBCreateCommand>.ValidateHResult(InterfaceTypeInfo<IDBCreateCommand>.FromIntPtr(objHandle).CreateCommand(punkOuter, ref iid, out command), objHandle);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				command = IntPtr.Zero;
				num = InterfaceTypeInfo<IDBCreateCommand>.ValidateException(ex, objHandle);
			}
			return num;
		}

		// Token: 0x0600C531 RID: 50481 RVA: 0x00274EEC File Offset: 0x002730EC
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new DBCreateCommandTypeInfo.CreateCommandCallback(DBCreateCommandTypeInfo.CreateCommand)
			};
		}

		// Token: 0x02001F96 RID: 8086
		// (Invoke) Token: 0x0600C534 RID: 50484
		private delegate int CreateCommandCallback(IntPtr objHandle, IntPtr punkOuter, ref Guid iid, out IntPtr command);
	}
}
