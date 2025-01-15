using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F5B RID: 8027
	internal class DBInitializeTypeInfo : InterfaceTypeInfo<IDBInitialize>
	{
		// Token: 0x0600C453 RID: 50259 RVA: 0x00273DC0 File Offset: 0x00271FC0
		private static int Initialize(IntPtr objHandle)
		{
			return InterfaceTypeInfo<IDBInitialize>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<IDBInitialize>.FromIntPtr(objHandle).Initialize();
			}, objHandle);
		}

		// Token: 0x0600C454 RID: 50260 RVA: 0x00273DF4 File Offset: 0x00271FF4
		private static int Uninitialize(IntPtr objHandle)
		{
			return InterfaceTypeInfo<IDBInitialize>.InvokeAndReturnHResult(delegate
			{
				InterfaceTypeInfo<IDBInitialize>.FromIntPtr(objHandle).Uninitialize();
			}, objHandle);
		}

		// Token: 0x0600C455 RID: 50261 RVA: 0x00273E25 File Offset: 0x00272025
		protected override Delegate[] CreateDelegates()
		{
			return new Delegate[]
			{
				new DBInitializeTypeInfo.InitializeCallback(DBInitializeTypeInfo.Initialize),
				new DBInitializeTypeInfo.UninitializeCallback(DBInitializeTypeInfo.Uninitialize)
			};
		}

		// Token: 0x02001F5C RID: 8028
		// (Invoke) Token: 0x0600C458 RID: 50264
		private delegate int InitializeCallback(IntPtr objHandle);

		// Token: 0x02001F5D RID: 8029
		// (Invoke) Token: 0x0600C45C RID: 50268
		private delegate int UninitializeCallback(IntPtr objHandle);
	}
}
